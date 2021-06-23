using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CodeVault_DotNet
{
    class EncryptingTool
    {

        public void EncryptString()
        {
            string original = "{\"Email\":\"dinith2@yopmail.com\",\"Token\":\"CfDJ8PAM6qS9vfBGlXGWXNNCnwCk66mUs2awACpxNqTNr0TdZ3zlgEfR/DrjU+yPlKqWi647csc4blp+AWYV6I2RlPfu4raMWoW7CuWhNIvc36xkNUfoJhNqm4/79abbjgb4D1WqpFF0nTITMk1omHBBywDtqEaqlnJTHKw2G0ghiJKR1hw5+pBx2UyMxCm3MTElmlTUHpnu2SnDoKYNS3MvPvON9MqCaTYLI01S/supeeZ8\"}";

            // Create a new instance of the Aes
            var keybytes = Encoding.UTF8.GetBytes("kljsdkkdlo4454GG");
                var iv = Encoding.UTF8.GetBytes("kljsdkkdlo4454GG");
                // Encrypt the string to an array of bytes.
                byte[] encrypted = EncryptStringToBytes_Aes(original, keybytes, iv);

            Console.WriteLine(Convert.ToBase64String(encrypted));
                //Display the original data and the decrypted data.
                Console.WriteLine("Original:   {0}", original);
               // Console.WriteLine("Round Trip: {0}", roundtrip);
            
        }

        private byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }

        public void DecryptString()
        {
            var encrypted = Convert.FromBase64String("14YCoEVRMOHEIavPj4FHjLGJ32h3MQXmFUiwpzIaH5NWGCAiigIreaf/4g9uH3zyg5V354vki86wgJqkLSMTCFvtRpF07KSl7k8yNogYskc1MsQF2gcb7Ju4zgNNJKIiPidvz0P0eGiGeYPhnkHhe8B8A7qQtQY/HgsefLUijUgroM7aIUCmVddweAnNF774A1g7XV+n4MEUzS+oBzLJn+a1mzEeuJSIeQ7gMfJ8O47ozQAc6oY/tM1twRrxfG3TT/aoGsguk6fhlmWBMPNSqBE281khSCHqaHen6S469/Ou+ssIsGCL5i7tcY6TjP7tRvxXRxpbr+i4Jv77yYdxBggjSXTpYPFDCUOLbcwg4OeNPhRL66H0ECAjdvsvZxnh");
            var keybytes = Encoding.UTF8.GetBytes("kljsdkkdlo4454GG");
            var iv = Encoding.UTF8.GetBytes("kljsdkkdlo4454GG");

            string decryptedText = DecryptStringFromBytes_Aes(encrypted, keybytes, iv);

            Console.WriteLine(decryptedText);
        }

        private string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }
    }
}

