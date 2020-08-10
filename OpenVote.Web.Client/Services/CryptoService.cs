using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Logging;

namespace OpenVote.Web.Client.Services
{
    public class CryptoService
    {
        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
            {
                hex.AppendFormat("{0:x2}", b);
            }
            return hex.ToString();
        }
        
        public RSAParameters? GenerateKey(int dwKeySize)
        {
            RSAParameters? RSAKeyInfo = null;
            try
            {
                Console.WriteLine("Start generating key");
                // Generate a public/private key using RSA
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(dwKeySize);
                // Read public key in a string
                RSAKeyInfo = RSA.ExportParameters(true);
                Console.WriteLine($"Public Key Lenght = {RSAKeyInfo.Value.Exponent.Length}");
                Console.WriteLine($"Public Key = {ByteArrayToString(RSAKeyInfo.Value.Exponent)}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ex is {ex}");
            }
            return RSAKeyInfo;
        }
    }
}