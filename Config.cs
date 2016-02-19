using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WebBrowser
{
    public static class Config
    {
        public static DateTime ZaciatokVeku
        {
            get
            {
                try
                {
                    if (File.Exists("UserConfig.dat"))
                    {
                        var fileStream = new StreamReader("UserConfig.dat");
                        var date = DateTime.Parse(fileStream.ReadLine());
                        fileStream.Close();
                        return date;
                    }
                    return DateTime.Now;
                }
                catch
                {
                    return DateTime.Now;
                }
            }
        }

        public static int CenaNN
        {
            get
            {
                try
                {
                    if (File.Exists("UserConfig.dat"))
                    {
                        IEnumerable<string> lines = File.ReadLines("UserConfig.dat");
                        var cena = int.Parse(lines.Skip(1).First());

                        return cena;
                    }
                    return 0;
                }
                catch
                {
                    return 0;
                }
            }
        }

        public static DateTime? PoslednyUpload
        {
            get
            {
                try
                {
                    if (File.Exists("UserConfig.dat"))
                    {
                        IEnumerable<string> lines = File.ReadLines("UserConfig.dat");
                        var date = DateTime.Parse(lines.Skip(2).First());

                        return date;
                    }
                    return null;
                }
                catch
                {
                    return null;
                }
            }
        }

        public static DateTime? PoslednyDownload
        {
            get
            {
                try
                {
                    if (File.Exists("UserConfig.dat"))
                    {
                        IEnumerable<string> lines = File.ReadLines("UserConfig.dat");
                        var date = DateTime.Parse(lines.Skip(3).First());

                        return date;
                    }
                    return null;
                }
                catch
                {
                    return null;
                }
            }
        }

        public static int DlzkaHesla
        {
            get
            {
                try
                {
                    if (File.Exists("UserConfig.dat"))
                    {
                        IEnumerable<string> lines = File.ReadLines("UserConfig.dat");
                        var dlzka = int.Parse(lines.Skip(4).First());

                        return dlzka;
                    }
                    return 146;
                }
                catch
                {
                    return 146;
                }
            }
        }

        public static string RelativnaSuradnicaX
        {
            get
            {
                try
                {
                    if (File.Exists("UserConfig.dat"))
                    {
                        IEnumerable<string> lines = File.ReadLines("UserConfig.dat");
                        var suradnica = lines.Skip(5).First();

                        return suradnica;
                    }
                    return "0";
                }
                catch
                {
                    return "0";
                }
            }
        }

        public static string RelativnaSuradnicaY
        {
            get
            {
                try
                {
                    if (File.Exists("UserConfig.dat"))
                    {
                        IEnumerable<string> lines = File.ReadLines("UserConfig.dat");
                        var suradnica = lines.Skip(6).First();

                        return suradnica;
                    }
                    return "0";
                }
                catch
                {
                    return "0";
                }
            }
        }

        public static string Vhodnost0
        {
            get
            {
                try
                {
                    if (File.Exists("UserConfig.dat"))
                    {
                        IEnumerable<string> lines = File.ReadLines("UserConfig.dat");
                        var vhodnost = lines.Skip(7).First();

                        return vhodnost;
                    }
                    return "100";
                }
                catch
                {
                    return "100";
                }
            }
        }

        public static string Vhodnost5
        {
            get
            {
                try
                {
                    if (File.Exists("UserConfig.dat"))
                    {
                        IEnumerable<string> lines = File.ReadLines("UserConfig.dat");
                        var vhodnost = lines.Skip(8).First();

                        return vhodnost;
                    }
                    return "95";
                }
                catch
                {
                    return "95";
                }
            }
        }

        public static string Vhodnost10
        {
            get
            {
                try
                {
                    if (File.Exists("UserConfig.dat"))
                    {
                        IEnumerable<string> lines = File.ReadLines("UserConfig.dat");
                        var vhodnost = lines.Skip(9).First();

                        return vhodnost;
                    }
                    return "85";
                }
                catch
                {
                    return "85";
                }
            }
        }

        public static string Vhodnost25
        {
            get
            {
                try
                {
                    if (File.Exists("UserConfig.dat"))
                    {
                        IEnumerable<string> lines = File.ReadLines("UserConfig.dat");
                        var vhodnost = lines.Skip(10).First();

                        return vhodnost;
                    }
                    return "105";
                }
                catch
                {
                    return "105";
                }
            }
        }

        public static string Vhodnost50
        {
            get
            {
                try
                {
                    if (File.Exists("UserConfig.dat"))
                    {
                        IEnumerable<string> lines = File.ReadLines("UserConfig.dat");
                        var vhodnost = lines.Skip(11).First();

                        return vhodnost;
                    }
                    return "150";
                }
                catch
                {
                    return "150";
                }
            }
        }

        public static string VhodnostDefault
        {
            get
            {
                try
                {
                    if (File.Exists("UserConfig.dat"))
                    {
                        IEnumerable<string> lines = File.ReadLines("UserConfig.dat");
                        var vhodnost = lines.Skip(12).First();

                        return vhodnost;
                    }
                    return "300";
                }
                catch
                {
                    return "300";
                }
            }
        }

        public static string PocetMiest130
        {
            get
            {
                try
                {
                    if (File.Exists("UserConfig.dat"))
                    {
                        IEnumerable<string> lines = File.ReadLines("UserConfig.dat");
                        var pocetMiestPriorita = lines.Skip(13).First();

                        return pocetMiestPriorita;
                    }
                    return "30";
                }
                catch
                {
                    return "30";
                }
            }
        }

        public static string PocetMiest110
        {
            get
            {
                try
                {
                    if (File.Exists("UserConfig.dat"))
                    {
                        IEnumerable<string> lines = File.ReadLines("UserConfig.dat");
                        var pocetMiestPriorita = lines.Skip(14).First();

                        return pocetMiestPriorita;
                    }
                    return "75";
                }
                catch
                {
                    return "75";
                }
            }
        }

        public static string PocetMiest100
        {
            get
            {
                try
                {
                    if (File.Exists("UserConfig.dat"))
                    {
                        IEnumerable<string> lines = File.ReadLines("UserConfig.dat");
                        var pocetMiestPriorita = lines.Skip(15).First();

                        return pocetMiestPriorita;
                    }
                    return "100";
                }
                catch
                {
                    return "100";
                }
            }
        }

        public static string PocetMiest90
        {
            get
            {
                try
                {
                    if (File.Exists("UserConfig.dat"))
                    {
                        IEnumerable<string> lines = File.ReadLines("UserConfig.dat");
                        var pocetMiestPriorita = lines.Skip(16).First();

                        return pocetMiestPriorita;
                    }
                    return "120";
                }
                catch
                {
                    return "120";
                }
            }
        }

        public static string PocetMiestDefault
        {
            get
            {
                try
                {
                    if (File.Exists("UserConfig.dat"))
                    {
                        IEnumerable<string> lines = File.ReadLines("UserConfig.dat");
                        var pocetMiestPriorita = lines.Skip(17).First();

                        return pocetMiestPriorita;
                    }
                    return "150";
                }
                catch
                {
                    return "150";
                }
            }
        }

        public static string TextNp
        {
            get
            {
                try
                {
                    if (File.Exists("UserConfig.dat"))
                    {
                        IEnumerable<string> lines = File.ReadLines("UserConfig.dat");
                        var text = lines.Skip(18).First();

                        return text;
                    }
                    return "NN {0}-{1}";
                }
                catch
                {
                    return "NN {0}-{1}";
                }
            }
        }
        public static string NNMinMinut
        {
            get
            {
                try
                {
                    if (File.Exists("UserConfig.dat"))
                    {
                        IEnumerable<string> lines = File.ReadLines("UserConfig.dat");
                        var cena = lines.Skip(19).First();

                        return cena;
                    }
                    return "60";
                }
                catch
                {
                    return "60";
                }
            }
        }
        public static string NNMaxMinut
        {
            get
            {
                try
                {
                    if (File.Exists("UserConfig.dat"))
                    {
                        IEnumerable<string> lines = File.ReadLines("UserConfig.dat");
                        var cena = lines.Skip(20).First();

                        return cena;
                    }
                    return "90";
                }
                catch
                {
                    return "90";
                }
            }
        }
    }
}
