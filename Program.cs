using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace SearchSame
{
    class Program
    {
        static System.Collections.Hashtable ht;

        static Hashtable htTam;



        public static void Main(string[] args)
        {



            bool exhaustivo = false;

            ht = new Hashtable();
            htTam = new Hashtable();



            if (args.Length <= 0)
            {
                
                Console.WriteLine("SearchSame searches for duplicated files in the directories specified");
                Console.WriteLine("Yangosoft 20090628");
                Console.WriteLine("http://usuarios.lycos.es/sisar");
                Console.WriteLine("This program is GPL 2.0\n\n");
                Console.WriteLine("searchsame.exe [-e] directory [directory2 ...]");
                Console.WriteLine("The output format is: OriginalFileName = DuplicateFileName = md5Hash\n");
                Console.WriteLine("-e: Compare files making a md5 hash of the content of each file. (Very SLOW)");
                Console.WriteLine("if the flag -e exists aditionally prints a list of the md5 hashes of each file like:\nmd5Hash FileName");

                return;

            }


            foreach (string path in args)
            {
                if (path == "-e")
                {
                    exhaustivo = true;
                }
            }

            if (exhaustivo == false)
            {

                foreach (string path in args)
                {
                    if (Directory.Exists(path))

                        CompareFilesQuick(path);

                }
            }
            else
            {

                foreach (string path in args)
                {
                    if (path != "-e")
                    {
                        if (Directory.Exists(path))

                            CompareFiles(path);
                    }

                }

                Console.WriteLine("List of Hashes");

                IDictionaryEnumerator ie = ht.GetEnumerator();

                String hash;

                String file;

                while (ie.MoveNext())
                {

                    hash = (String)ie.Key;

                    file = (String)ie.Value;

                    Console.WriteLine(hash + " " + file);



                }
            }





        }
        public static void CompareFiles(string path)
        {
            String md5Hash;
            String original;


            DirectoryInfo di = new DirectoryInfo(path);

            DirectoryInfo[] rgFiles = di.GetDirectories();
            foreach (DirectoryInfo d in rgFiles)
            {
                CompareFiles(d.FullName);
            }
            FileInfo[] fi = di.GetFiles();
            foreach (FileInfo f in fi)
            {
                md5Hash = GetMD5Hash(f.FullName);
                if (ht.ContainsKey(md5Hash))
                {
                    original = (String)ht[md5Hash];
                    Console.WriteLine(f.FullName + " = " + original + " = " + md5Hash);

                }
                else
                {
                    //Console.WriteLine(md5Hash + " " +f.FullName);
                    ht.Add(md5Hash, f.FullName);
                }
            }
        }



        public static void CompareFilesQuick(string path)
        {

            String md5Hash;
            String md5HashOriginal;

            String original;


            DirectoryInfo di = new DirectoryInfo(path);



            DirectoryInfo[] rgFiles = di.GetDirectories();

            foreach (DirectoryInfo d in rgFiles)
            {

                CompareFilesQuick(d.FullName);

            }

            FileInfo[] fi = di.GetFiles();

            foreach (FileInfo f in fi)
            {
                long len = f.Length;
                if (htTam.ContainsKey(len))
                {
                    original = (String)htTam[len];
                    if (SameFileContent(original, f.FullName) == true)
                    {
                        if (ht.ContainsValue(original))
                        {

                        }
                        else
                        {
                            md5HashOriginal = GetMD5Hash(original);
                            ht.Add(md5HashOriginal, original);
                        }


                        md5Hash = GetMD5Hash(f.FullName);

                        if (ht.ContainsKey(md5Hash))
                        {

                            //original=(String)ht[md5Hash];

                            Console.WriteLine(f.FullName + " = " + original + " = " + md5Hash);



                        }
                        else
                        {

                            //Console.WriteLine(md5Hash + " " +f.FullName);

                            ht.Add(md5Hash, f.FullName);

                        }
                    }
                }
                else
                {
                    htTam.Add(len, f.FullName);
                }

            }

        }


        private static bool SameFileContent(string original, string duplicado)
        {
            byte[] bOrg;
            byte[] bDup;
            try
            {
                FileStream f = new FileStream(original, FileMode.Open);

                FileStream fd = new FileStream(duplicado, FileMode.Open);



                if (f.Length < 512)
                {

                    bOrg = new byte[f.Length];
                }
                else
                {
                    bOrg = new byte[512];

                }
                bDup = new byte[bOrg.Length];


                f.Read(bOrg, 0, bOrg.Length);
                f.Close();
                fd.Read(bDup, 0, bDup.Length);
                fd.Close();
                for (int i = 0; i < bOrg.Length; i++)
                {
                    if (bOrg[i] != bDup[i])
                        return false;
                }



            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }







        public static string GetMD5Hash(string path)
        {

            byte[] bs;

            System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();

            //System.IO.StreamReader sr = new StreamReader(path);

            try
            {

                FileStream f = new FileStream(path, FileMode.Open);







                bs = new byte[f.Length];

                f.Read(bs, 0, bs.Length);

                f.Close();

            }

            catch (Exception ex)
            {

                return "";

            }

            bs = x.ComputeHash(bs);

            System.Text.StringBuilder s = new System.Text.StringBuilder();

            foreach (byte b in bs)
            {

                s.Append(b.ToString("x2").ToLower());

            }

            string password = s.ToString();

            return password;

        }
    }
}
