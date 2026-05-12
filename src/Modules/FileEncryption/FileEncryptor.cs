using System;
using System.IO;
using System.Security.Cryptography;

namespace FileEncryption
{
    /// <summary>
    /// AES-256 file encryptor with PBKDF2 key derivation.
    /// File format: [Magic 8B][Salt 32B][IV 16B][Ciphertext...]
    /// </summary>
    public class FileEncryptor
    {
        private static readonly byte[] MagicBytes = { 0x4D, 0x44, 0x42, 0x45, 0x4E, 0x43, 0x52, 0x59 }; // "MDBENCRY"
        private const int SaltSize = 32;
        private const int IvSize = 16;
        private const int KeySize = 32; // 256 bits
        private const int Iterations = 100_000;

        /// <summary>
        /// Encrypts a file using AES-256 with a password-derived key.
        /// </summary>
        public static void EncryptFile(string inputPath, string outputPath, string password)
        {
            byte[] salt = new byte[SaltSize];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            byte[] key = DeriveKey(password, salt);

            using (var inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
            using (var outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                // Write header: magic + salt
                outputStream.Write(MagicBytes, 0, MagicBytes.Length);
                outputStream.Write(salt, 0, salt.Length);

                using (var aes = new AesCryptoServiceProvider())
                {
                    aes.KeySize = 256;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;
                    aes.Key = key;
                    aes.GenerateIV();

                    // Write IV
                    outputStream.Write(aes.IV, 0, aes.IV.Length);

                    using (var cryptoStream = new CryptoStream(outputStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        byte[] buffer = new byte[81920];
                        int bytesRead;
                        while ((bytesRead = inputStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            cryptoStream.Write(buffer, 0, bytesRead);
                        }
                        cryptoStream.FlushFinalBlock();
                    }
                }
            }
        }

        /// <summary>
        /// Decrypts a file that was encrypted with EncryptFile.
        /// </summary>
        public static void DecryptFile(string inputPath, string outputPath, string password)
        {
            using (var inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
            {
                // Read and verify magic bytes
                byte[] magic = new byte[MagicBytes.Length];
                ReadExactly(inputStream, magic, magic.Length);
                for (int i = 0; i < MagicBytes.Length; i++)
                {
                    if (magic[i] != MagicBytes[i])
                        throw new InvalidDataException("文件不是有效的加密文件（魔术字节不匹配）。");
                }

                // Read salt
                byte[] salt = new byte[SaltSize];
                ReadExactly(inputStream, salt, salt.Length);

                // Read IV
                byte[] iv = new byte[IvSize];
                ReadExactly(inputStream, iv, iv.Length);

                byte[] key = DeriveKey(password, salt);

                using (var aes = new AesCryptoServiceProvider())
                {
                    aes.KeySize = 256;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;
                    aes.Key = key;
                    aes.IV = iv;

                    using (var outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                    using (var cryptoStream = new CryptoStream(inputStream, aes.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        byte[] buffer = new byte[81920];
                        int bytesRead;
                        while ((bytesRead = cryptoStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            outputStream.Write(buffer, 0, bytesRead);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Derives a 256-bit key from a password and salt using PBKDF2 (HMAC-SHA256).
        /// </summary>
        public static byte[] DeriveKey(string password, byte[] salt)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
            {
                return deriveBytes.GetBytes(KeySize);
            }
        }

        /// <summary>
        /// Checks whether a file was encrypted by this tool.
        /// </summary>
        public static bool IsEncryptedFile(string filePath)
        {
            if (!File.Exists(filePath)) return false;
            try
            {
                using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    if (stream.Length < MagicBytes.Length + SaltSize + IvSize) return false;
                    byte[] magic = new byte[MagicBytes.Length];
                    ReadExactly(stream, magic, magic.Length);
                    for (int i = 0; i < MagicBytes.Length; i++)
                    {
                        if (magic[i] != MagicBytes[i]) return false;
                    }
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        private static void ReadExactly(Stream stream, byte[] buffer, int count)
        {
            int offset = 0;
            while (offset < count)
            {
                int read = stream.Read(buffer, offset, count - offset);
                if (read == 0)
                    throw new EndOfStreamException("读取加密文件头时意外到达流末尾。");
                offset += read;
            }
        }
    }
}
