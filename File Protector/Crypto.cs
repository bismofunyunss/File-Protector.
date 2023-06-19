using System.IO;
using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;

public static class Crypto
{
    private const int _keyBits = 128;
    private const int _iterations = 50;
    private const double _memorySize = 1024 * 1024 * 8; //8MB
    public static readonly int saltSize = 32; // 64 Bit
    public static string Salt { get; set; } = string.Empty;
    public static string Hash { get; set; } = string.Empty;

    public static string HashPasswordV2(string password, byte[] salt)
    {
        using var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
        {
            Salt = salt,
            DegreeOfParallelism = Environment.ProcessorCount,
            Iterations = _iterations,
            MemorySize = (int)_memorySize
        };
        return Convert.ToHexString(argon2.GetBytes(_keyBits / 8));
    }

    public static string? DeriveKey(string password, byte[] salt)
    {
        try
        {
            using var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,
                DegreeOfParallelism = Environment.ProcessorCount,
                Iterations = _iterations,
                MemorySize = (int)_memorySize
            };
            return Convert.ToHexString(argon2.GetBytes(_keyBits / 8));
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            ErrorLogging.ErrorLog(ex);
            return null;
        }
    }
    public static bool ComparePassword(string hash)
    {
        return Hash != null && CryptographicOperations.FixedTimeEquals(Convert.FromHexString(Hash), Convert.FromHexString(hash));
    }

    private static readonly RandomNumberGenerator rndNum = RandomNumberGenerator.Create();
    private const int SaltBitSize = 512;
    private static byte[] SaltBytes = new byte[SaltBitSize / 8];
    private const int BlockBitSize = 128;
    private const int KeyBitSize = 256;
    private const int IVBit = 128;
    public static string? GenerateRndKey()
    {
       byte[]? Key = RndByteSized(128 * 16);
        return Key != null ? DataConversionHelpers.ByteArrayToHexString(Key) : null;
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
        var seed = RndInt();
        return new Random(seed).Next(min, max);
    }
    public static byte[] RndByteSized(int size)
    {
        var buffer = new byte[size];
        rndNum.GetBytes(buffer);
        return buffer;
    }
#pragma warning disable
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
                    Key = null;
                    cipherText = memStream.ToArray();
                }
            }

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
                    return memStrm.ToArray();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogging.ErrorLog(ex);
            string error = ex.Message + " " + ex.InnerException;
            System.Windows.Forms.MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
#pragma warning restore
    }
}
