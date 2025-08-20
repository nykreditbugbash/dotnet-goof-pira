
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace NETStandaloneBlot.Cryptography
{
    class InsecureECB1
    {
        public void Run()
        {
            // Use CBC mode and set a random IV for secure encryption
            using (AesCryptoServiceProvider aesCryptoServiceProvider = new AesCryptoServiceProvider())
            {
                aesCryptoServiceProvider.Mode = CipherMode.CBC;
                aesCryptoServiceProvider.GenerateIV();
                // Use aesCryptoServiceProvider here
            }

            using (AesCryptoServiceProvider aesCryptoServiceProvider2 = new AesCryptoServiceProvider())
            {
                aesCryptoServiceProvider2.Mode = CipherMode.CBC;
                aesCryptoServiceProvider2.GenerateIV();
                // Use aesCryptoServiceProvider2 here
            }

            using (AesManaged aesManaged = new AesManaged())
            {
                aesManaged.Mode = CipherMode.CBC;
                aesManaged.GenerateIV();
                // Use aesManaged here
            }

            using (AesManaged aesManaged2 = new AesManaged())
            {
                aesManaged2.Mode = CipherMode.CBC;
                aesManaged2.GenerateIV();
                // Use aesManaged2 here
            }

            using (AesManaged rm = new AesManaged())
            {
                rm.Mode = CipherMode.CBC;
                rm.GenerateIV();
                // Use rm here
            }

            using (AesManaged rm2 = new AesManaged())
            {
                rm2.Mode = CipherMode.CBC;
                rm2.GenerateIV();
                // Use rm2 here
            }

            using (TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider())
            {
                tdes.Mode = CipherMode.CBC;
                tdes.GenerateIV();
                // Use tdes here
            }

            using (TripleDESCryptoServiceProvider tdes2 = new TripleDESCryptoServiceProvider())
            {
                tdes2.Mode = CipherMode.CBC;
                tdes2.GenerateIV();
                // Use tdes2 here
            }


        }
    }
}
