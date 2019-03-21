using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Rijndael256;

namespace CourseManagement.Utilities
{
    public static class Encrypter
    {

        public static string Encrypt(string plainText, string passPhrase)
        {
            string encrypted = RijndaelEtM.Encrypt(plainText, passPhrase, KeySize.Aes256);
            return encrypted;
           
        }

        public static string Decrypt(string cipherText, string passPhrase)
        {
            string decrypted = RijndaelEtM.Decrypt(cipherText, passPhrase, KeySize.Aes256);
            return decrypted;
           
        }
    }
}