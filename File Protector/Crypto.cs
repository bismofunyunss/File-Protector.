﻿using System.IO;
using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;
public static class Crypto
{
    private const int _keySize = 256 / 8 / 2;
    private const int _iterations = 50;
    private const double _memorySize = 1024 * 1024 * 8; //8MB
    private static readonly HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA512;

    private static string _Salt = string.Empty;
    private static string _Hash = string.Empty;

    public static string Hash
    {
        get { return _Hash; }
        set { _Hash = value; }
    }

    public static string Salt
    {
        get { return _Salt; }
        set { _Salt = value; }
    }

    public static string HashPassword(string password, byte[] salt)
    {
        /// <summary>
        /// This method is obsolete. Moving over to Argon2id
        /// </summary>
        var hash = Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(password), salt, _iterations, _hashAlgorithm, _keySize);
        return Convert.ToHexString(hash);
    }

    public static string HashPasswordV2(string password, byte[] salt)
    {
        using var _argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
        {
            Salt = salt,
            DegreeOfParallelism = Environment.ProcessorCount,
            Iterations = _iterations,
            MemorySize = (int)_memorySize
        };
        return Convert.ToHexString(_argon2.GetBytes(_keySize));
    }
    public static string? deriveKey(string password, byte[] salt, int size)
    {
        try
        {
            using var _argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,
                DegreeOfParallelism = Environment.ProcessorCount,
                Iterations = _iterations,
                MemorySize = (int)_memorySize
            };
            return Convert.ToHexString(_argon2.GetBytes(size));
        }
        catch(Exception ex)
        {
            MessageBox.Show(ex.Message);
            ErrorLogging.ErrorLog(ex);
            return null;
        }
    }
    public static bool ComparePassword(string hash)
    {
        string _CompareHash = Hash;

        if (_CompareHash != null)
            return CryptographicOperations.FixedTimeEquals(Convert.FromHexString(_CompareHash), Convert.FromHexString(hash));

        return false;
    }
    /// <summary>
    /// Create cryptography classes
    /// </summary>
    private static readonly RandomNumberGenerator rndNum = RandomNumberGenerator.Create();
    private static readonly Aes aes = Aes.Create();
    /// <summary>
    /// Salt size.
    /// </summary>
    private const int SaltBitSize = 512;
    private static byte[] SaltBytes = new byte[SaltBitSize / 8];
    /// <summary>
    /// AES values.
    /// </summary>
    private const int BlockBitSize = 128;
    private const int KeyBitSize = 256;
    private const int IVBit = 128;
    /// <summary>
    /// Generates a random alphabetical numerical string that includes special characters. This is
    /// used to generate a random key value.
    /// </summary>
    /// <param name="size">How many characters the string should be.</param>
    /// <returns>A random string based on the parameter "Size".</returns>
    public static string GenerateRndString(int size)
    {
        char[] alphaNumeric = @"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()-_=+[{]};:'|""\<,.>/?".ToCharArray();
        StringBuilder result = new StringBuilder(size);
        for (int i = 0; i < size; i++)
        {
            int index = BoundedInt(0, alphaNumeric.Length);
            result.Append(alphaNumeric[index]);
        }
        return result.ToString();
    }
    /// <summary>
    /// Generates a random integer using the random number generator class.
    /// </summary>
    /// <returns>A random integer without bounds.</returns>
    private static int RndInt()
    {
        byte[] buffer = new byte[(sizeof(int))];
        rndNum.GetBytes(buffer);
        int result = BitConverter.ToInt32(buffer, 0);
        return result;
    }
    /// <summary>
    /// Generates a random integer that is bounded with a minimum and maximum value. Seed value
    /// is generated by calling generate random integer. Based on the random number generator class.
    /// </summary>
    /// <param name="min">The minimum value to be generated.</param>
    /// <param name="max">The maximum value to be generated.</param>
    /// <returns></returns>
    private static int BoundedInt(int min, int max)
    {
        var seed = RndInt();
#pragma warning disable
        return new Random().Next(min, max);
#pragma warning restore
    }
    /// <summary>
    /// Generates a random byte array that's determined by the parameter "Size".
    /// </summary>
    /// <param name="size">The size of the byte array to create.</param>
    /// <returns>A random byte array that is determined by the parameter "Size".</returns>
    public static byte[] RndByteSized(int size)
    {
        var buffer = new byte[size];
        rndNum.GetBytes(buffer);
        return buffer;
    }

    /// <summary>
    /// Encrypts a string.
    /// </summary>
    /// <param name="PlainText">The plain text to encrypt.</param>
    /// <param name="Key">The key used for encryption.</param>
    /// <returns>An encrypted string in the format of Base64.</returns>
#pragma warning disable
    public static byte[]? Encrypt(byte[]? PlainText, byte[]? Key)
    {
        ///<remarks>Set up variables.
        ///<remarks>Set up variables.
        var IV = RndByteSized(IVBit / 8);
        SaltBytes = RndByteSized(SaltBitSize / 8);
        byte[] CipherText = Array.Empty<byte>();
        ///</remarks>
        ///<remarks>Check parameters.
        ///
        try
        {
            /// </remarks>
            ///<remarks>Set up parameters for AES.
            using (aes)
            {
                aes.BlockSize = BlockBitSize;
                aes.KeySize = KeyBitSize;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
            };
            ///	</remarks>	
            ///	
            ///<remarks>Begin encryption.
#pragma warning disable CA5401
            using (var Encryptor = aes.CreateEncryptor(Key, IV))
#pragma warning restore CA5401
            using (var memStream = new MemoryStream())
            {
                using (var CryptoStream = new CryptoStream(memStream, Encryptor, CryptoStreamMode.Write))
                using (var encryptStream = new MemoryStream(PlainText))
                {
                    encryptStream.CopyTo(CryptoStream, encryptStream.Capacity);
                    CryptoStream.FlushFinalBlock();
                    encryptStream.Flush();
                    CryptoStream.Clear();
                }
                CipherText = memStream.ToArray();
            }
        }

        ///<summary>Catch blocks.</summary>
        ///

        catch (OutOfMemoryException ex)
        {
            ErrorLogging.ErrorLog(ex);
            string error = ex.Message + " " + ex.InnerException;
            System.Windows.Forms.MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
        catch (ArgumentException ex)
        {
            ErrorLogging.ErrorLog(ex);
            string error = ex.Message + " " + ex.InnerException;
            System.Windows.Forms.MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
        catch (CryptographicException ex)
        {
            ErrorLogging.ErrorLog(ex);
            string error = ex.Message + " " + ex.InnerException;
            System.Windows.Forms.MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
        catch (IOException ex)
        {
            ErrorLogging.ErrorLog(ex);
            string error = ex.Message + " " + ex.InnerException;
            System.Windows.Forms.MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }

        try
        {
            byte[] prependItems = new byte[IV.Length + SaltBytes.Length + CipherText.Length];

            Buffer.BlockCopy(IV, 0, prependItems, 0, IV.Length);
            Buffer.BlockCopy(SaltBytes, 0, prependItems, IV.Length, SaltBytes.Length);
            Buffer.BlockCopy(CipherText, 0, prependItems, IV.Length + SaltBytes.Length, CipherText.Length);

            Key = null;
            return prependItems;
        }
        catch (System.Exception ex)
        {
            ErrorLogging.ErrorLog(ex);
            string error = ex.Message + " " + ex.InnerException;
            System.Windows.Forms.MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null; ;
        }
    }
    /// </summary>
    /// <summary>
    /// Decrypts a string.
    /// </summary>
    /// <param name="CipherText">Text to decrypt.</param>
    /// <param name="Key">Key used for encryption and decryption.</param>
    /// <returns>Decrypted string and hash value. </returns>
    public static byte[] Decrypt(byte[]? CipherText, byte[]? Key)
    {
        try
        {

            if (CipherText == null)
            {
                throw new ArgumentException("Value was empty or null.", nameof(CipherText));
            }

            ///<remarks>Set up parameters for AES.
            ///</remarks>
            using (aes)
            {
                aes.BlockSize = BlockBitSize;
                aes.KeySize = KeyBitSize;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
            };

            var IV = new byte[IVBit / 8];

            Buffer.BlockCopy(CipherText, 0, IV, 0, IV.Length);

            var ciphertextOffset = SaltBytes.Length + IV.Length;
            var cipherResult = new byte[CipherText.Length - SaltBytes.Length - IV.Length];

            Buffer.BlockCopy(CipherText, ciphertextOffset, cipherResult, 0, cipherResult.Length);
            ///<remarks>Begin decryption.</remarks>
            ///
            using (var decryptor = aes.CreateDecryptor(Key, IV))
            using (var memStrm = new MemoryStream())
            {
                using (var decryptStream = new CryptoStream(memStrm, decryptor, CryptoStreamMode.Write))
                using (var plainTextStream = new MemoryStream(cipherResult))
                {
                    plainTextStream.CopyTo(decryptStream, plainTextStream.Capacity);
                }
                return (memStrm.ToArray());
            }
        }
        catch (System.Exception ex)
        {
            ErrorLogging.ErrorLog(ex);
            string error = ex.Message + " " + ex.InnerException;
            System.Windows.Forms.MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return (null);
        }
    }
#pragma warning restore
}
