namespace Vigener
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var vig = new Vigener();
            Console.WriteLine("Enter key");
            string key = Console.ReadLine();

           
            
            string inputPath = null;
            string outputPath = null;
            string file = null;

            using(StreamReader sr = new StreamReader("C:/Users/krisF/Documents/GitHub/InfSec/Vigener/Vigener/path.txt"))
            {
                var a = sr.ReadToEnd();
                inputPath = a.Split("\r\n")[0];
                outputPath = a.Split("\r\n")[1];
                file = a.Split("\r\n")[2];
            }

            Console.WriteLine("enter 1 for encrypt, 2 for decrypt, 3 for finish");

            while (true)
            {
                string choose = Console.ReadLine();
                switch (choose)
                {
                    case "1":
                        {
                            vig.Encrypt(key, inputPath, outputPath, file);
                            Console.WriteLine("enter 1 for encrypt, 2 for decrypt, 3 for finish");

                            break;
                        }
                    case "2":
                        {
                            vig.Decrypt(key, outputPath + '/' + file, outputPath);
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


            //vig.Encrypt(key, "C:/Users/KrisF/Documents/GitHub/InfSec/Vigener/Docs", "C:/Users/KrisF/Documents/GitHub/InfSec/Vigener", "docs.txt");
            //vig.Decrypt(key, "C:/Users/KrisF/Documents/GitHub/InfSec/Vigener/docs.txt", "C:/Users/KrisF/Documents/GitHub/InfSec/");
        }
    }
}
