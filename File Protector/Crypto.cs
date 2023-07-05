using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Input;
using Konscious.Security.Cryptography;

public static class Crypto
{
    public static readonly int ByteSize = 24; // 48 bit hex string
    public static readonly int KeySize = 16; // 32 bit hex string
    private const int Iterations = 50;
    private const double MemorySize = 1024 * 1024 * 10; // 10GiB
    public static readonly int SaltSize = 384 / 8; // 64 Bit
    public static string Salt { get; set; } = string.Empty;
    public static string Hash { get; set; } = string.Empty;
    public static string? checkSum { get; set; } = string.Empty;
    public static async Task<string?> HashAsync(string password, byte[] salt)
    {
        using var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
        {
            Salt = salt, // 64 bit
            DegreeOfParallelism = Environment.ProcessorCount, //Maximum cores
            Iterations = Iterations, // 50 Iterations
            MemorySize = (int)MemorySize //explicitly cast MemorySize from double to int                                 
        };
        try
        {
            string result = string.Empty;
            result = Convert.ToHexString(await argon2.GetBytesAsync(ByteSize).ConfigureAwait(false));
            return result;
        }
        catch (CryptographicException ex)
        {
            MessageBox.Show(ex.Message);
            ErrorLogging.ErrorLog(ex);
            return null;
        }
    }
    public static async Task<string?> DeriveAsync(string password, byte[] salt)
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
            string result = string.Empty;

            result = Convert.ToHexString(await argon2.GetBytesAsync(KeySize).ConfigureAwait(false));
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
        return await Task.FromResult(Hash != null && CryptographicOperations.FixedTimeEquals(Convert.FromHexString(Hash), Convert.FromHexString(hash))).ConfigureAwait(false);
    }
    public static string ComputeChecksum(string input)
    {
        byte[] hashValue = SHA512.HashData(Encoding.UTF8.GetBytes(input));
        string checksum = DataConversionHelpers.ByteArrayToHexString(hashValue) ?? string.Empty;
        return checksum;
    }

    private static readonly RandomNumberGenerator rndNum = RandomNumberGenerator.Create();

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
    private const int BlockBitSize = 128;
    private const int KeyBitSize = 256;
    private const int IVBit = 128;
    public static byte[]? Encrypt(byte[]? PlainText, byte[]? Key)
    {
        try
        {
            byte[] iv = RndByteSized(IVBit / 8);
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
            byte[] prependItems = new byte[iv.Length + cipherText.Length];
            Buffer.BlockCopy(iv, 0, prependItems, 0, iv.Length);
            Buffer.BlockCopy(cipherText, 0, prependItems, iv.Length, cipherText.Length);
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

                var ciphertextOffset = IV.Length;
                var cipherResult = new byte[CipherText.Length - IV.Length];
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
            return null;
        }
#pragma warning restore
    }
}
