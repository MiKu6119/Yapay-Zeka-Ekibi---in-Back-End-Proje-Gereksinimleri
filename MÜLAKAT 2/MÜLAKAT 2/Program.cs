using System.Drawing;
using System.Threading.Channels;

namespace MÜLAKAT_2
{
    internal class Program
    {
        #region giriş
        static void GİRİS()
        {
            Console.Clear();
            string path = "C:\\MÜLAKAT";
            if (!Directory.Exists(path))
            {
               Directory.CreateDirectory(path);
            }
            Console.WriteLine("--------------------------------");
            Console.WriteLine("     1- KULLANICI GİRİŞİ");
            Console.WriteLine("     2- ÜYE OL");
            Console.WriteLine("     3- YÖNETİCİ GİRİŞİ");
            Console.WriteLine("--------------------------------");
            Console.WriteLine();
            Console.Write("SEÇİM :");
            Dictionary<string,string> eslesme = new Dictionary<string,string>();
            Dictionary<string,string> guvenliksorusu = new Dictionary<string,string>();
            Dictionary<string, string> guvenliksorusayi = new Dictionary<string, string>();
            string[] dosyalar =Directory.GetFiles(path);
            if (dosyalar.Length > 0)
            {
                for (int i = 0; i < dosyalar.Length; i++)
                {

                    string[] satirlar = File.ReadAllLines(dosyalar[i]);
                    eslesme.Add(satirlar[0], satirlar[1]);
                    guvenliksorusu.Add(satirlar[0],satirlar[3]);
                    guvenliksorusayi.Add(satirlar[0], satirlar[2]);
                }
            }
            byte islem =Convert.ToByte(Console.ReadLine());
            #region kullanıcıgiriş
            if (islem == 1)
            {
                byte sayac = 0;
                DÖNÜS:
                Console.Clear();
                Console.Write("Kullanıcı adınızı giriniz..:");
                string kullanicigiris=Console.ReadLine();
                if (!eslesme.ContainsKey(kullanicigiris))
                {
                    Console.WriteLine("Kullanıcı adı bulunamadı lütfen tekrar giriniz..:");
                    Thread.Sleep(3000);
                    goto DÖNÜS;
                }
                Console.Write("şifrenizi giriniz..:");
                string sifregiris=Console.ReadLine();
                Console.Clear();
                if (eslesme[kullanicigiris] != sifregiris)
                {
                    if (sayac < 3)
                    {
                        Console.WriteLine("ŞİFRE HATALI TEKRAR DENE !!!");
                        Thread.Sleep(3000);
                        sayac++;
                        goto DÖNÜS;
                    }
                    else if (sayac >= 3 && sayac <= 5)
                    {
                        Console.WriteLine("ŞİFRENİZİ 3 KEZ YANLIŞ DENEDİNİZ ŞİFRENİZİ DEĞİŞTİRMEK İSTER MİSİNİZ !? (evet , hayır)");
                        string karar = Console.ReadLine();
                        if (karar.ToLower() == "evet")
                        {
                            Console.Clear();
                            if (guvenliksorusayi[kullanicigiris] == "1") 
                            {
                                Console.WriteLine("1- İLK EVCİL HAYVANINIZIN İSMİ  :");
                            }
                            else if (guvenliksorusayi[kullanicigiris] == "2") 
                            {
                                Console.WriteLine("2- İLKOKUL ÖĞRETMENİNİZİN İSMİ  :");
                            }
                            else if (guvenliksorusayi[kullanicigiris] == "3") 
                            {
                                Console.WriteLine("3- EN SEVDİĞİNİZ RENK            :");
                            }
                            else if (guvenliksorusayi[kullanicigiris] == "4") 
                            {
                                Console.WriteLine("4- EN SEVDİĞİNİZ HAYVAN          :");
                            }
                        GEL:
                            byte ksayac = 0;
                            string cevap = Console.ReadLine();
                            if (guvenliksorusu[kullanicigiris] == cevap)
                            {
                                Console.WriteLine("Yeni şifrenizi giriniz..:");
                                string yenisifre = Console.ReadLine();
                                string[] temp = File.ReadAllLines("C:\\MÜLAKAT\\" + kullanicigiris + ".txt");
                                temp[1] = yenisifre;
                                File.WriteAllText("C:\\MÜLAKAT\\" + kullanicigiris + ".txt", temp[0]+"\n");
                                for (int i = 1; i < temp.Length; i++)
                                {
                                    File.AppendAllText("C:\\MÜLAKAT\\" + kullanicigiris + ".txt", temp[i]+"\n");
                                }
                                Console.WriteLine("Şifreniz başarıyla değiştirildi giriş ekranına yönlendiriliyorsunuz...:");
                                Thread.Sleep(3000);
                                GİRİS();
                            }
                            else
                            {
                                if (ksayac == 0)
                                {
                                    Console.WriteLine("SON DENEME HAKKINIZDIR TEKRAR YANLIŞ CEVAP GİRDİĞİNİZ HALDE HESABINIZ SİLİNECEKTİR !!!!");
                                    Thread.Sleep(3000);
                                    ksayac++;
                                    goto GEL;
                                }
                                else
                                {

                                }

                            }
                        }
                        else if (karar.ToLower() == "hayır")
                        {
                            Console.WriteLine("GİRİŞ EKRANINA TEKRAR YÖNLENDİRİLİYORSUNUZ...");
                            Thread.Sleep(3000);
                            goto DÖNÜS;
                        }
                    }
                    else
                    {
                        Console.WriteLine("ÇOK KEZ YANLIŞ DENEMEDEN ÖTÜRÜ EKRAN 1 DAKİKA BLOKE OLMUŞTUR !!!");
                        Thread.Sleep(3600000);
                        sayac = 0;
                        goto DÖNÜS;
                    }
                }
                else
                {                                       
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("                             GİRİŞ BAŞARILI MENÜYE YÖNLENDİRİLİYORSUNUZ...");
                    Console.ResetColor();
                    Thread.Sleep(3000);
                    Console.Clear();
                    KullanıcıEkran();
                }
                
            }
            #endregion
            #region üyeol

            else if (islem == 2)
            {

                DON:
                Console.Clear();
                Console.Write("Kullanıcı adı belirleyiniz..:");
                string kullaniciadi=Console.ReadLine();
                Console.Write("Şifre belirleyiniz..:");
                string sifre=Console.ReadLine();
                
                if (File.Exists(path + "\\" + kullaniciadi + ".txt"))
                {
                    Console.WriteLine("Kullanıcı adı zaten alınmış");
                    Console.WriteLine("GİRİŞ EKRANINA YÖNLENDİRİLİYORSUNUZ....");
                    Thread.Sleep(3000);
                    goto DON;

                }
                Console.Clear();
                Console.WriteLine("         ----------------------------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("             LÜTFEN KENDİNİZE BİR GÜVENLİK SORUSU SEÇİNİZ VE CEVAPLAYINIZ..:");
                Console.ResetColor();
                Console.WriteLine("         ----------------------------------------------------------------------------");
                Console.WriteLine();
                Console.WriteLine("1- İLK EVCİL HAYVANINIZIN İSMİ  :");
                Console.WriteLine("2- İLKOKUL ÖĞRETMENİNİZİN İSMİ  :");
                Console.WriteLine("3- EN SEVDİĞİNİZ RENK            :");
                Console.WriteLine("4- EN SEVDİĞİNİZ HAYVAN          :");
                Console.WriteLine();
                Console.Write("SORU NO SEÇ..:   ");
                byte no = Convert.ToByte(Console.ReadLine());

                if (no == 1 || no == 2 || no == 3 || no == 4)
                {
                    Console.Write("Seçtiğiniz sorunun cevabını giriniz..:   ");
                    string cevap = Console.ReadLine();
                    FileStream fs = File.Create(path + "\\" + kullaniciadi + ".txt");
                    fs.Close();
                    File.AppendAllText(path + "\\" + kullaniciadi + ".txt", kullaniciadi + "\n");
                    File.AppendAllText(path + "\\" + kullaniciadi + ".txt", sifre+"\n");
                    File.AppendAllText(path + "\\" + kullaniciadi + ".txt", no+ "\n");
                    File.AppendAllText(path + "\\" + kullaniciadi + ".txt", cevap + "\n");

                }
                else
                {
                    Console.WriteLine("LÜTFEN SADECE EKRANDAKİ İŞLEMLERDEN BİRİNİ TUŞLAYINIZ..:");
                    Thread.Sleep(3000);
                    goto DON;
                }
                Console.Clear();
                Console.WriteLine("Kullanıcı profili başarıyla oluşturuldu.");
                Console.WriteLine("Giriş ekranına dönmek için herhangi bir tuşa basınız..:");
                Console.ReadKey();
                Thread.Sleep(500);
                Console.Clear();
                GİRİS();
                   

                
             
            }
            #endregion
            #region admingiriş

            else if (islem == 3) 
            {
                Console.Clear();
                if (!Directory.Exists("C:\\ADMİN_KLASÖR"))
                {
                    Directory.CreateDirectory("C:\\ADMİN_KLASÖR");
                }
                string adminPath ="C:\\ADMİN_KLASÖR\\ADMİN.txt";
                if (!File.Exists(adminPath))
                {
                   File.Create(adminPath).Close();
        
                }
                //ADMİN id miku şifre 1
                //sadece adminler başka bir admin ekleyebilir veya silebilir
                DÖN:
                Console.Write("YÖNETİCİ İD: ");
                string id = Console.ReadLine();
                Console.Write("YÖNETİCİ ŞİFRE: ");
                string sifre= Console.ReadLine();
                string[] satirlar = File.ReadAllLines(adminPath);
                byte adminsayac = 0;
             for (int i = 0; i < satirlar.Length; i++)
                {
                    if (satirlar[i] ==id && satirlar[i+1] ==sifre)
                    {
                        adminsayac++;
                        break;
                    }
                }

                if (adminsayac == 0)
                {
                    Console.WriteLine("KULLANICI ADI VEYA ŞİFRE YANLIŞ");
                    Console.WriteLine("ADMİNLER İÇİN ŞİFRE YENİLEME SEÇENEĞİ YOKTUR BAŞKA BİR ADMİN HESABINIZI SİLEBİLİR VE YENİDEN OLUŞTURABİLİR.");
                    Console.WriteLine("GİRİŞ YAPMAYI TEKRAR DENEMEK İSTER MİSİNİZ (E , H)");
                    string sec= Console.ReadLine();
                    if (sec.ToLower() == "e")
                    {
                        goto DÖN;
                    }
                    else if (sec.ToLower() == "h")
                    {
                        Console.WriteLine("GİRİŞ EKRANINA YÖNLENDİRİLİYORSUNUZ..");
                        Thread.Sleep(3000);
                        GİRİS();
                    }
                    else
                    {
                        Console.WriteLine("LÜTFEN YALNIZCA E VEYA H OLARAK CEVAPLAYIN !!!");
                        Thread.Sleep(4000);
                        goto DÖN;
                    }
                    

                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("                 HOŞGELDİNİZ SEVGİLİ ADMİN HEMEN ADMİN MENÜSÜNE YÖNLENDİRİLİYORSUNUZ....");
                    Console.ResetColor();
                    Thread.Sleep(5000);
                    ADMİN();
                }
            }
            #endregion
            else
            {
                Console.WriteLine("LÜTFEN SADECE EKRANDAKİ İŞLEMLERDEN BİRİNİ TUŞLAYINIZ !!!");
                Thread.Sleep(3000);
                GİRİS();
            }
        }
        #endregion
        #region ADMİNPANEL
        static void ADMİN()
        {
            Console.Clear();
            string adminPath = "C:\\ADMİN_KLASÖR\\ADMİN.txt";
            if (!Directory.Exists("C:\\ADMİN_KLASÖR"))
            {
                Directory.CreateDirectory("C:\\ADMİN_KLASÖR");
            }
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("                                  ============================================");
            Console.WriteLine("                                                YÖNETİCİ PANELİ     "        );
            Console.WriteLine("                                  ============================================");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("-------------------------------");
            Console.WriteLine("1-).........ADMİN EKLE");
            Console.WriteLine("2-).........ADMİN ÇIKAR");
            Console.WriteLine("3-).........ÜRÜN EKLE");
            Console.WriteLine("-------------------------------");
            Console.WriteLine();
            Console.Write("SEÇİM:");
            string no = Console.ReadLine();
            #region adminekleçıkar
            if (no == "1")
            {
            DÖN:
                Console.Clear();
                Console.Write("EKLENECEK ADMİN İD :");
                string id = Console.ReadLine();
                Console.Write("EKLENECEK ADMİN ŞİFRE :");
                string sifre = Console.ReadLine();
                if (! File.Exists(adminPath))
                {
                    File.Create(adminPath).Close();
                }
                string[] satirlar = File.ReadAllLines(adminPath);
                for (int i = 0; i < satirlar.Length; i += 2)
                {
                    if (satirlar[i] == id)
                    {
                        Console.WriteLine("BAŞKA BİR İD SEÇİNİZ..:");
                        Thread.Sleep(3000);
                        goto DÖN;
                    }
                }
                File.AppendAllText(adminPath, id + "\n");
                File.AppendAllText(adminPath, sifre + "\n");
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("                                 İŞLEM BAŞARIYLA GERÇEKLEŞTİRİLDİ...");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("BAŞKA BİR İŞLEM İÇİN PANELE YÖNLENDİRİLİYORSUNUZ..");
                Thread.Sleep(4000);
                ADMİN();
            }
            if (no == "2")
            {
                Console.Clear();
                if (!File.Exists(adminPath))
                {
                    Console.WriteLine("ŞUAN SİSTEME KAYITLI ADMİN OLMADIĞINDAN ADMİN SİLME İŞLEMİ YAPAMAZSINIZ..");
                    Console.WriteLine("ÖNCELİKLE SİSTEME ADMİN EKLEMEK İÇİN ADMİN EKLEME EKRANINA YÖNLENDİRİLİYORSUNUZ....");
                    Thread.Sleep(5000);
                    ADMİN();
                }
                string[] satirlar = File.ReadAllLines(adminPath);
                Dictionary<string,string> adminKey = new Dictionary<string,string>();
                for (int i = 0; i < satirlar.Length; i += 2)
                {
                    adminKey.Add(satirlar[i],satirlar[i + 1]);

                }
                Console.WriteLine("SİLMEK İSTEDİĞİNİZ ADMİN İD :");
                string sil = Console.ReadLine();
                Console.Clear();
                if (adminKey.ContainsKey(sil))
                {
                    Console.WriteLine(sil + " ADMİNİNİN KAYDI SİLİNİYOR EMİN MİSİNİZ ? (E , H)");
                    string emin = Console.ReadLine();
                    Console.Clear();
                    if (emin.ToLower() == "e")
                    {
                    
                        adminKey.Remove(sil);
                        File.WriteAllText(adminPath,"");
                        foreach (string key in adminKey.Keys)
                        {
                            File.AppendAllText(adminPath, key + "\n");
                            File.AppendAllText(adminPath, adminKey[key] + "\n");
                        }
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("                                 İŞLEM BAŞARIYLA GERÇEKLEŞTİRİLDİ...");
                        Console.ResetColor();
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("BAŞKA BİR İŞLEM İÇİN PANELE YÖNLENDİRİLİYORSUNUZ..");
                        Thread.Sleep(4000);
                        ADMİN();
                    }
                    else if (emin.ToLower() == "h")
                    {
                        Console.WriteLine("BAŞKA BİR İŞLEM İÇİN PANELE YÖNLENDİRİLİYORSUNUZ..");
                        Thread.Sleep(4000);
                        ADMİN();
                    }
                }
                else
                {
                    Console.WriteLine("GİRDİĞİNİZ İD BİR ADMİNE AİT DEĞİLDİR");
                    Console.WriteLine("BAŞKA BİR İŞLEM İÇİN PANELE YÖNLENDİRİLİYORSUNUZ..");
                    Thread.Sleep(4000);
                    ADMİN();
                }
            }
            #endregion
            if (no == "3")
            {
                Console.Clear();
                string PATH = "C:\\ÜRÜN_KLASÖR\\";
                if (!Directory.Exists(PATH))
                {
                    Directory.CreateDirectory(PATH);
                }
                Console.Write("EKLEMEK İSTEDİĞİNİZ ÜRÜN BAŞLIĞI :");
                string ürün=Console.ReadLine();
                if (!Directory.Exists(PATH+"\\"+ürün))
                {
                    Directory.CreateDirectory(PATH+"\\"+ürün);
                }
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine("                                                         "+ürün.ToUpper()  );
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine();
                TEKRAR:
                Console.Write("ÜRÜN MARKASI : ");
                string marka = Console.ReadLine();
                Console.Write("ÜRÜN FİYATI  : ");
                string fiyat = Console.ReadLine();
                Console.Write("ÜRÜN ADETİ   : ");
                string adet = Console.ReadLine();
                Console.Write("ÜRÜN RENGİ   : ");
                string renk = Console.ReadLine();
                Console.Clear();
                Console.WriteLine("MARKA --> "+marka);
                Console.WriteLine("FİYAT --> "+fiyat);
                Console.WriteLine("ADET ---> "+adet);
                Console.WriteLine("RENK ---> "+renk);
                Console.WriteLine();
                cevap:
                Console.WriteLine("GİRDİĞİNİZ BİLGİLER DOĞRU MU ? (E, H)");
                string kontrol=Console.ReadLine();
                if (kontrol.ToLower() =="e")
                {
                    string EklePath = PATH + "\\" + ürün + "\\" + marka + ".txt";
                    //marka
                    //renk
                    //fiyat
                    //adet olarak dosya da sıralanır

                    if (!File.Exists(EklePath))
                    {
                        File.Create(EklePath).Close();
                    }
                    File.AppendAllText(PATH + "\\" + ürün + "\\" + marka + ".txt", marka + "\n");
                    File.AppendAllText(PATH + "\\" + ürün + "\\" + marka + ".txt",renk+"\n");
                    File.AppendAllText(PATH + "\\" + ürün + "\\" + marka + ".txt", fiyat + "\n");
                    File.AppendAllText(PATH + "\\" + ürün + "\\" + marka + ".txt", adet + "\n");
                    Console.Clear() ;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("                                 İŞLEM BAŞARIYLA GERÇEKLEŞTİRİLDİ...");
                    Console.ResetColor();
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("BAŞKA BİR İŞLEM İÇİN PANELE YÖNLENDİRİLİYORSUNUZ..");
                    Thread.Sleep(4000);
                    ADMİN();
                }
                else if  (kontrol.ToLower()=="h")
                {
                    Console.Clear();
                    Console.WriteLine("LÜTFEN BİLGİLERİNİZİ GÜNCELLEYİNİZ !");
                    Console.WriteLine();
                    goto TEKRAR;
                }
                else
                {
                    Console.WriteLine("yalnızca e veya h olarak cevaplayınız !!!");
                    goto cevap;
                }










            }

        }
        #endregion
        static void KullanıcıEkran()
        {
            MENÜ:
            Console.ForegroundColor = ConsoleColor.Blue;

            Console.WriteLine("                                  ============================================");
            Console.WriteLine("                                                SİTEMİZE HOŞGELDİNİZ  ");
            Console.WriteLine("                                  ============================================");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("-------------------------------");
            Console.WriteLine("1-).........ALIŞVERİŞ YAP ");
            Console.WriteLine("2-).........PROFİL GÖRÜNTÜLE ");
            Console.WriteLine("3-).........ÇIKIŞ YAP");
            Console.WriteLine("-------------------------------");
            Console.WriteLine();
            Console.Write("SEÇİM:");
            string no = Console.ReadLine();
            if (no =="1")
            {
                Console.Clear();
                Console.WriteLine("**************************");
                Console.WriteLine("1-)....ARAMA MOTORU");
                Console.WriteLine("2-).....SİTE'DE GEZ");
                Console.WriteLine("**************************");
                Console.WriteLine();
                Console.WriteLine();
                Console.Write("SEÇİM : ");
                string sec= Console.ReadLine();
                if (sec == "1")
                {
                    Console.Clear();
                    Console.WriteLine("LÜTFEN ARADIĞINIZ ÜRÜNÜZ GİRİN..");
                    string ürün = "";
                    while (true)
                    {
                        ürün = Console.ReadLine();
                        Console.Clear();
                        string PATH = "C:\\ÜRÜN_KLASÖR\\" + ürün;
                        Console.Clear();
                        if (ürün == "0")
                        {
                            goto MENÜ;
                        }
                        if (!Directory.Exists(PATH))
                        {
                            Console.WriteLine("MAALESEF ŞUANDA İSTEDİĞİNİZ ÜRÜN ELİMİZDE YOKTUR.");
                            Console.WriteLine("DİLERSENİZ BAŞKA BİR ÜRÜN ARATIN VEYA 0'ı TUŞLAYARAK MENÜYE DÖNÜN");
                        }
                        else
                        {
                            break;
                        }
                    }
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("                                  ============================================");
                    Console.WriteLine("                                      " + ürün.ToUpper() + "  MARKA SEÇENEKLERİ");
                    Console.WriteLine("                                  ============================================");
                    Console.ResetColor();
                    string PATh = "C:\\ÜRÜN_KLASÖR\\" + ürün;

                    string[] marka = (Directory.GetFiles(PATh));
                    string[] ayir = new string[marka.Length];


                    int seciliIndex = 0;

                    ConsoleKeyInfo tus;

                    do
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("                                      ============================================");
                        Console.WriteLine("                                           " + ürün.ToUpper() + "  MARKA SEÇENEKLERİ");
                        Console.WriteLine("                                      ============================================");
                        Console.ResetColor();
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine();

                        for (int i = 0; i < marka.Length; i++)
                        {
                            if (i == seciliIndex)
                            {
                                
                                Console.BackgroundColor = ConsoleColor.DarkCyan;
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            else
                            {
                                Console.ResetColor();
                            }
                            ayir = File.ReadAllLines(marka[i]);
                            Console.WriteLine(ayir[0].ToUpper());


                        }



                        Console.ResetColor();
                        tus = Console.ReadKey(true); 

                        if (tus.Key == ConsoleKey.UpArrow)
                        {
                            seciliIndex = (seciliIndex == 0) ? ayir.Length - 1 : seciliIndex - 1;
                        }
                        else if (tus.Key == ConsoleKey.DownArrow)
                        {
                            seciliIndex = (seciliIndex + 1) % ayir.Length;
                        }

                    } while (tus.Key != ConsoleKey.Enter);

                    Console.Clear();
                    ayir = File.ReadAllLines(marka[seciliIndex]);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("                               ====================================================              ");
                    Console.ResetColor();
                    Console.Write("                                                    Seçilen:  ");
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine(ayir[0].ToUpper());
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("                               ====================================================              ");
                    Console.ResetColor();
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("-------------------------------");
                    Console.WriteLine("         ÖZELLİKLER");
                    Console.WriteLine("-------------------------------");
                    Console.ResetColor();
                    Console.WriteLine();
                    byte secim = 0;
                    for (int i = 0; i < ayir.Length - 3; i += 4)
                    {
                        Console.Write("* MARKA *    ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(ayir[i]);
                        Console.ResetColor();
                        Console.WriteLine();
                        Console.Write("*  RENK *    ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(ayir[i + 1]);
                        Console.ResetColor();
                        Console.WriteLine();
                        Console.Write("* FİYAT *    ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(ayir[i + 2]);
                        Console.ResetColor();
                        Console.WriteLine();
                        Console.Write("*  STOK *    ");

                        if (ayir[i + 3] == "1" || ayir[i + 3] == "0")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(ayir[i + 3]);
                            Console.ResetColor();
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(ayir[i + 3]);
                            Console.ResetColor();
                            Console.WriteLine();
                        }
                        secim++;
                        Console.WriteLine("SEÇİM : " + secim);
                        Console.WriteLine("==============================================");
                    }
                }
                else if (sec == "2")
                {
                    string klasorYolu = @"C:\ÜRÜN_KLASÖR\"; 
                    string[] klasorler = Directory.GetDirectories(klasorYolu);

                    int seciliIndex = 0;
                    ConsoleKeyInfo tus;

                    do
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Blue; 

                        Console.WriteLine("                             ==================================");
                        Console.WriteLine("                                          ÜRÜNLER");
                        Console.WriteLine("                             ==================================");
                        Console.ResetColor();
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine();

                        for (int i = 0; i < klasorler.Length; i++)
                        {
                            if (i == seciliIndex)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine(">> " + Path.GetFileName(klasorler[i]).ToUpper());
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.WriteLine("   " + Path.GetFileName(klasorler[i]).ToUpper());
                            }
                        }

                        tus = Console.ReadKey(true);

                        if (tus.Key == ConsoleKey.UpArrow && seciliIndex > 0)
                            seciliIndex--;
                        else if (tus.Key == ConsoleKey.DownArrow && seciliIndex < klasorler.Length - 1)
                            seciliIndex++;

                    } while (tus.Key != ConsoleKey.Enter);

                    Console.Clear();
                    if (Directory.Exists(klasorler[seciliIndex]))
                    {
                        string[] dosyalar = Directory.GetFiles(klasorler[seciliIndex]);
                        int dosyaSeciliIndex = 0;

                        if (dosyalar.Length == 0)
                        {
                            Console.Clear();
                            Console.WriteLine("Bu klasörde hiç dosya yok.");
                        }
                        else
                        {
                            do
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.DarkBlue;
                                Console.WriteLine("                            ==============================================" );
                                Console.WriteLine( "                                           MARKA SEÇİMİ"           );
                                Console.WriteLine("                            ==============================================");
                                Console.ResetColor ();
                                Console.WriteLine();
                                Console.WriteLine();
                                Console.WriteLine();
                                for (int i = 0; i < dosyalar.Length; i++)
                                {
                                    if (i == dosyaSeciliIndex)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                        Console.WriteLine(">> " + Path.GetFileNameWithoutExtension(dosyalar[i]).ToUpper()); 
                                        Console.ResetColor();
                                    }
                                    else
                                    {
                                        Console.WriteLine("   " + Path.GetFileNameWithoutExtension(dosyalar[i]).ToUpper());
                                    }
                                }

                                tus = Console.ReadKey(true);

                                if (tus.Key == ConsoleKey.UpArrow && dosyaSeciliIndex > 0)
                                    dosyaSeciliIndex--;
                                else if (tus.Key == ConsoleKey.DownArrow && dosyaSeciliIndex < dosyalar.Length - 1)
                                    dosyaSeciliIndex++;

                            } while (tus.Key != ConsoleKey.Enter);

                            Console.Clear();
                            string[] ayir = new string[dosyalar.Length];

                            Console.Clear();
                            ayir = File.ReadAllLines(dosyalar[dosyaSeciliIndex]); 
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("                               ====================================================              ");
                            Console.ResetColor();
                            Console.Write("                                                    Seçilen:  ");
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.WriteLine(ayir[0].ToUpper());
                            Console.ResetColor();
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("                               ====================================================              ");
                            Console.ResetColor();
                            Console.WriteLine();
                            Console.WriteLine();
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("-------------------------------");
                            Console.WriteLine("         ÖZELLİKLER");
                            Console.WriteLine("-------------------------------");
                            Console.ResetColor();
                            Console.WriteLine();
                            byte secim = 0;
                            for (int i = 0; i < ayir.Length - 3; i += 4)
                            {
                                Console.Write("* MARKA *    ");
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine(ayir[i]);
                                Console.ResetColor();
                                Console.WriteLine();
                                Console.Write("*  RENK *    ");
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine(ayir[i + 1]);
                                Console.ResetColor();
                                Console.WriteLine();
                                Console.Write("* FİYAT *    ");
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine(ayir[i + 2]);
                                Console.ResetColor();
                                Console.WriteLine();
                                Console.Write("*  STOK *    ");

                                if (ayir[i + 3] == "1" || ayir[i + 3] == "0")
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine(ayir[i + 3]);
                                    Console.ResetColor();
                                    Console.WriteLine();
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine(ayir[i + 3]);
                                    Console.ResetColor();
                                    Console.WriteLine();
                                }
                                secim++;
                                Console.WriteLine("SEÇİM : " + secim);
                                Console.WriteLine("==============================================");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Klasör bulunamadı!");
                    }

                    Console.WriteLine("\nÇıkmak için bir tuşa bas...");
                    Console.ReadKey();
                }
                


            }
            else if (no == "3")
            {
                Console.Clear();
                GİRİS();
            }
            else
            {
                Console.WriteLine("LÜTFEN YALNIZCA EKRANDAKİ İŞLEMLERDEN BİRİNİ SEÇİN !!!");
                Thread.Sleep(3000);
                goto MENÜ;
            }
        }
        static void Main(string[] args)
        {
           GİRİS();
           // ADMİN();
          //KullanıcıEkran();
        }
    }
}
