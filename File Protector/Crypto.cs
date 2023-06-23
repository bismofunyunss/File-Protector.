using System.IO;
using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;

public static class Crypto
{
    private static readonly int ByteSize = 24;
    private const int Iterations = 50;
    private const double MemorySize = 1024 * 1024 * 10; // 10GiB
    public static readonly int SaltSize = 384 / 6; // 64 Bit
    public static string Salt { get; set; } = string.Empty;
    public static string Hash { get; set; } = string.Empty;
    public static async Task<string?> HashAndDeriveAsync(string password, byte[] salt)
    {
        using var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
        {
            Salt = salt,
            DegreeOfParallelism = Environment.ProcessorCount,
            Iterations = Iterations,
            MemorySize = (int)MemorySize
        };
        try
        {
            bool complete = false;
            string result = string.Empty;
            while (!complete)
            {
                GC.Collect(GC.MaxGeneration, GCCollectionMode.Aggressive, true, true);
                result = Convert.ToHexString(await argon2.GetBytesAsync(ByteSize).ConfigureAwait(false));
                if (!string.IsNullOrEmpty(result))
                    break;
            }
            return result;
        }
        catch (CryptographicException ex)
        {
            MessageBox.Show(ex.Message);
            ErrorLogging.ErrorLog(ex);
            return null;
        }
    }
   
    public static async Task<bool?> ComparePassword(string hash)
    {
        if (Hash != null)
        {
            return await Task.Run(() =>
            {
                return CryptographicOperations.FixedTimeEquals(DataConversionHelpers.HexStringToByteArray( hash), DataConversionHelpers.HexStringToByteArray(Hash));
            }).ConfigureAwait(false);
        }

        return null;
       // return Hash != null && CryptographicOperations.FixedTimeEquals(Convert.FromHexString(Hash), Convert.FromHexString(hash));
    }

    private static readonly RandomNumberGenerator rndNum = RandomNumberGenerator.Create();
    public static string? GenerateRndPassword()
    {
        int len = 18;
        StringBuilder stringBuilder = new StringBuilder();
        string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()-_=+[{]};:'\"<\\,.>/?";

        for (int i = 0; i < len; i++)
        {
            int index = BoundedInt(0, chars.Length);
            stringBuilder.Append(chars[index]);
        }
        return stringBuilder.ToString();
    }
    private static int RndInt()
    {
        byte[] buffer = new byte[(sizeof(int))];
        rndNum.GetBytes(buffer);
        int result = BitConverter.ToInt32(buffer, 0);
        return result;
    }
    private static int BoundedInt(int min, int max)
    {
        var value = RndInt();
        int range = max - min;
        int result = min + (Math.Abs(value) % range);

        return result;
    }
    public static byte[] RndByteSized(int size)
    {
        var buffer = new byte[size];
        rndNum.GetBytes(buffer);
        return buffer;
    }
#pragma warning disable
    private const int SaltBitSize = 512;
    private static byte[] SaltBytes = new byte[SaltBitSize / 8];
    private const int BlockBitSize = 128;
    private const int KeyBitSize = 256;
    private const int IVBit = 128;
    public static byte[]? Encrypt(byte[]? PlainText, byte[]? Key)
    {
        try
        {
            byte[] iv = RndByteSized(IVBit / 8);
            SaltBytes = RndByteSized(SaltBitSize / 8);
            byte[] cipherText;

            using (Aes aes = Aes.Create())
            {
                aes.BlockSize = BlockBitSize;
                aes.KeySize = KeyBitSize;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using (var encryptor = aes.CreateEncryptor(Key, iv))
                using (var memStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(PlainText, 0, PlainText?.Length ?? 0);
                        cryptoStream.FlushFinalBlock();
                    }
                    cipherText = memStream.ToArray();
                }
            }
            Key = null;
            byte[] prependItems = new byte[iv.Length + SaltBytes.Length + cipherText.Length];
            Buffer.BlockCopy(iv, 0, prependItems, 0, iv.Length);
            Buffer.BlockCopy(SaltBytes, 0, prependItems, iv.Length, SaltBytes.Length);
            Buffer.BlockCopy(cipherText, 0, prependItems, iv.Length + SaltBytes.Length, cipherText.Length);

            return prependItems;
        }
        catch (Exception ex)
        {
            ErrorLogging.ErrorLog(ex);
            string error = ex.Message + " " + ex.InnerException;
            MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
    }
    public static byte[] Decrypt(byte[]? CipherText, byte[]? Key)
    {
        try
        {
            if (CipherText == null)
            {
                throw new ArgumentException("Value was empty or null.", nameof(CipherText));
            }

            using (var aes = Aes.Create())
            {
                aes.BlockSize = BlockBitSize;
                aes.KeySize = KeyBitSize;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                var IV = new byte[IVBit / 8];
                Buffer.BlockCopy(CipherText, 0, IV, 0, IV.Length);

                var ciphertextOffset = SaltBytes.Length + IV.Length;
                var cipherResult = new byte[CipherText.Length - SaltBytes.Length - IV.Length];
                Buffer.BlockCopy(CipherText, ciphertextOffset, cipherResult, 0, cipherResult.Length);

                using (var decryptor = aes.CreateDecryptor(Key, IV))
                using (var memStrm = new MemoryStream())
                {
                    using (var decryptStream = new CryptoStream(memStrm, decryptor, CryptoStreamMode.Write))
                    using (var plainTextStream = new MemoryStream(cipherResult))
                    {
                        plainTextStream.CopyTo(decryptStream, plainTextStream.Capacity);
                    }
                    Key = null;
                    return memStrm.ToArray();
                }
            }
        }
        catch (Exception ex)
        {
            Key = null;
            ErrorLogging.ErrorLog(ex);
            string error = ex.Message + " " + ex.InnerException;
            System.Windows.Forms.MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
#pragma warning restore
    }
}
