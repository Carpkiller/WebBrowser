using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace WebBrowser.SpravaHesiel
{
    public class SpravaHesiel
    {
        public static UserData LoadHesla()
        {
            if (File.Exists("UserData.dat"))
            {
                try
                {
                    var res = File.ReadAllBytes("UserData.dat");

                    var meno = res.Take(Config.DlzkaHesla).ToArray();
                    var heslo = res.Skip(Config.DlzkaHesla).Take(Config.DlzkaHesla).ToArray();

                    byte[] meno1 = ProtectedData.Unprotect(meno, null, DataProtectionScope.LocalMachine);
                    byte[] heslo1 = ProtectedData.Unprotect(heslo, null, DataProtectionScope.LocalMachine);

                    string men = Encoding.UTF8.GetString(meno1);
                    string hes = Encoding.UTF8.GetString(heslo1);

                    return new UserData(men, hes);
                }
                catch (Exception)
                {
                    Console.WriteLine(@"Chyba pri nacitavani konfiguracneho suboru");
                    return null;
                }
            }
            return null;
        }

        public static void UlozHeslo(string meno, string heslo)
        {
            try
            {
                byte[] plaintextMeno = Encoding.UTF8.GetBytes(meno);
                byte[] plaintextHeslo = Encoding.UTF8.GetBytes(heslo);

                byte[] entropy = new byte[20];
                using (var rng = new RNGCryptoServiceProvider())
                {
                    rng.GetBytes(entropy);
                }

                byte[] ciphertextMeno = ProtectedData.Protect(plaintextMeno, null,
                    DataProtectionScope.LocalMachine);
                byte[] ciphertextHeslo = ProtectedData.Protect(plaintextHeslo, null,
                    DataProtectionScope.LocalMachine);

                var fileStream = new FileStream("UserData.dat", FileMode.Create, FileAccess.Write);

                fileStream.Write(ciphertextMeno, 0, ciphertextMeno.Length);
                fileStream.Write(ciphertextHeslo, 0, ciphertextMeno.Length);

                fileStream.Close();

                new Jadro().AktualizujConfig(ciphertextHeslo.Length, 4);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Chyba pri zapise konfiguracneho suboru", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }

        }
    }
}
