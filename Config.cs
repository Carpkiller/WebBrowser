﻿using System;
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
    }
}
