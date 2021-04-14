using System;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;

namespace ZipBreaker
{
    class Program
    {
        static void Main(string[] args)
        {
            string wList = "./wordlist.txt";
            string zFile = "./test.zip";
            try
            {
                WordList wordList = new WordList(wList);
                string zipFile = zFile;

                foreach (string line in wordList.NextWord())
                {
                    Console.WriteLine("Testing {0}", line);
                    try
                    {
                        ZipTester.ExtractZipContent(zipFile, line, "./tmp");
                        Console.WriteLine("Password found: {0}", line);
                        break;
                    }
                    catch (ZipException e)
                    {
                        if (e.Message.Equals("Invalid password"))
                        {
                            continue;
                        }

                        Console.WriteLine(e.StackTrace);
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
