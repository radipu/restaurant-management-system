using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace TomaFoodRestaurant.Model
{
    public class Cryptograph
    {
        public Cryptograph()
        {

        }
        public string EncodeString(string originalString)
        {
            //Declarations
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;

            //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)
            md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(originalString);
            encodedBytes = md5.ComputeHash(originalBytes);

            //Convert encoded bytes back to a 'readable' string
            return BitConverter.ToString(encodedBytes);
        }

        public string EncryptString(string Message, string Passphrase)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

            // Step 1. We hash the passphrase using MD5
            // We use the MD5 hash generator as the result is a 128 bit byte array
            // which is a valid length for the TripleDES encoder we use below

            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));

            // Step 2. Create a new TripleDESCryptoServiceProvider object
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

            // Step 3. Setup the encoder
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            // Step 4. Convert the input string to a byte[]
            byte[] DataToEncrypt = UTF8.GetBytes(Message);

            // Step 5. Attempt to encrypt the string
            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }

            // Step 6. Return the encrypted string as a base64 encoded string
            return Convert.ToBase64String(Results);
        }

        public string DecryptString(string Message, string Passphrase)
        {
            //byte[] Results;
            //System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

            //// Step 1. We hash the passphrase using MD5
            //// We use the MD5 hash generator as the result is a 128 bit byte array
            //// which is a valid length for the TripleDES encoder we use below

            //MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            //byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));

            //// Step 2. Create a new TripleDESCryptoServiceProvider object
            //TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

            //// Step 3. Setup the decoder
            //TDESAlgorithm.Key = TDESKey;
            //TDESAlgorithm.Mode = CipherMode.ECB;
            //TDESAlgorithm.Padding = PaddingMode.PKCS7;

            //// Step 4. Convert the input string to a byte[]
            ////byte[] DataToDecrypt=new byte[1000];
            ////try
            ////{
                
            ////}
            ////catch (Exception ex)
            ////{
            ////    MessageBox.Show(ex.ToString());
            ////}
            //byte[] DataToDecrypt = Convert.FromBase64String(Message);
            //// Step 5. Attempt to decrypt the string
            //try
            //{
            //    ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
            //    Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            //}
            //finally
            //{
            //    // Clear the TripleDes and Hashprovider services of any sensitive information
            //    TDESAlgorithm.Clear();
            //    HashProvider.Clear();
            //}

            //// Step 6. Return the decrypted string in UTF8 format
            //return UTF8.GetString(Results);



            byte[] results;
            UTF8Encoding utf8 = new UTF8Encoding();
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] deskey = md5.ComputeHash(utf8.GetBytes(Message));
            TripleDESCryptoServiceProvider desalg = new TripleDESCryptoServiceProvider();
            desalg.Key = deskey;
            desalg.Mode = CipherMode.ECB;
            desalg.Padding = PaddingMode.PKCS7;
            byte[] decrypt_data = Convert.FromBase64String(Passphrase);
            try
            {
                //To transform the utf binary code to md5 decrypt
                ICryptoTransform decryptor = desalg.CreateDecryptor();
                results = decryptor.TransformFinalBlock(decrypt_data, 0, decrypt_data.Length);
            }
            finally
            {
                desalg.Clear();
                md5.Clear();

            }
            //TO convert decrypted binery code to string
            return utf8.GetString(results);


        }




    }
}
