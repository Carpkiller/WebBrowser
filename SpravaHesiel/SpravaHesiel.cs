﻿using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace WebBrowser.SpravaHesiel
{
    public class SpravaHesiel
    {
        public static UserData LoadHesla()
        {
            if (File.Exists("UserData.dat"))
            {
                var res = File.ReadAllBytes("UserData.dat");

                var meno = res.Take(146).ToArray();
                var heslo = res.Skip(146).Take(146).ToArray();

                byte[] meno1 = ProtectedData.Unprotect(meno, null, DataProtectionScope.CurrentUser);
                byte[] heslo1 = ProtectedData.Unprotect(heslo, null, DataProtectionScope.CurrentUser);

                string men = Encoding.UTF8.GetString(meno1);
                string hes = Encoding.UTF8.GetString(heslo1);

                return new UserData(men, hes);
            }
            return null;
        }

        public static void UlozHeslo(string meno, string heslo)
        {
            byte[] plaintextMeno = Encoding.UTF8.GetBytes(meno);
            byte[] plaintextHeslo = Encoding.UTF8.GetBytes(heslo);

            byte[] entropy = new byte[20];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(entropy);
            }

            byte[] ciphertextMeno = ProtectedData.Protect(plaintextMeno, null,
                DataProtectionScope.CurrentUser);
            byte[] ciphertextHeslo = ProtectedData.Protect(plaintextHeslo, null,
                DataProtectionScope.CurrentUser);

            var fileStream = new FileStream("UserData.dat", FileMode.Create,FileAccess.Write);

            fileStream.Write(ciphertextMeno, 0, ciphertextMeno.Length);
            fileStream.Write(ciphertextHeslo, 0, ciphertextMeno.Length);

            fileStream.Close();
        }
    }
}
