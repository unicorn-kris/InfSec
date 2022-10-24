using System;

namespace Steganographics
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var steg = new Steganographic();

            Console.WriteLine("Enter encrypting word");
            string key = Console.ReadLine();

            string inputPath = "C:/Users/krisF/Documents/GitHub/InfSec/Steganographics/Steganographics/input.txt";
            string outputPath = "C:/Users/krisF/Documents/GitHub/InfSec/Steganographics/Steganographics/output.txt";
            string file = "C:/Users/krisF/Documents/GitHub/InfSec/Steganographics/Steganographics/file.txt";
            string result = "C:/Users/krisF/Documents/GitHub/InfSec/Steganographics/Steganographics/result.txt";

            Console.WriteLine("enter 1 for encrypt, 2 for decrypt, 3 for finish");

            while (true)
            {
                string choose = Console.ReadLine();
                switch (choose)
                {
                    case "1":
                        {
                            steg.Encrypt(key, inputPath, outputPath, file);
                            Console.WriteLine("enter 1 for encrypt, 2 for decrypt, 3 for finish");

                            break;
                        }
                    case "2":
                        {
                            steg.Decrypt(outputPath, result);
                            Console.WriteLine("enter 1 for encrypt, 2 for decrypt, 3 for finish");

                            break;
                        }
                    case "3":
                        {
                            return;
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("enter the right value");
                            choose = Console.ReadLine();
                            break;
                        }
                }
            }
        }
    }
}
