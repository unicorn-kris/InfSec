using System.Text;


namespace Steganographics
{
    internal class Steganographic
    {
        //а е о р с у х А B Е К О Р С Т Х
        public Dictionary<char, char> dict = new Dictionary<char, char>()
        {
            {'а', 'a'},
            {'е', 'e'},
            {'о', 'o'},
            {'р', 'p'},
            {'с', 'c'},
            {'у', 'y'},
            {'х', 'x'},
            {'А', 'A'},
            {'В', 'B'},
            {'Е', 'E'},
            {'К', 'K'},
            {'О', 'O'},
            {'Р', 'P'},
            {'С', 'C'},
            {'Т', 'T'},
            {'Х', 'X'}
        };

        public void Encrypt(string key, string pathInput, string pathOutput, string fileName)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding win1251 = Encoding.GetEncoding("Windows-1251");

            //bytes of encrypt word
            byte[] p = win1251.GetBytes(key.ToString());

            //text
            string input = "";

            using (StreamReader sr = new StreamReader(pathInput))
            {
                input = sr.ReadToEnd();
            }

            //encrypt text
            string result = "";

            //encrypt container
            List<byte> bytes = new List<byte>();

            int p_index = 0;

            //encrypt text and fill container
            foreach (char symbol in input)
            {
                if (dict.ContainsKey(symbol))
                {
                    if (p_index < p.Length)
                    {
                        if (p[p_index] == 1)
                        {
                            result += dict[symbol];
                        }
                        else
                        {
                            result += symbol;
                        }
                        p_index++;
                        bytes.Add(p[p_index]);
                    }
                    else
                    {
                        result += symbol;
                        bytes.Add(0);
                    }
                }
                else
                {
                    result += symbol;
                }
            }

            using (StreamWriter sw = new StreamWriter(pathOutput))
            {
                sw.WriteLine(result);
                Console.WriteLine(result);
            }

            using (StreamWriter sw = new StreamWriter(fileName))
            {
                foreach(var b in bytes)
                {
                    sw.Write(b);
                    Console.WriteLine(b);
                }
            }

            //Directory.Delete(pathInput, true);

            //foreach (var item in map)
            //{
            //    Console.WriteLine(item.Key);
            //    foreach(var k in item.Value)
            //        Console.Write(k + " ");
            //    Console.WriteLine();
            //}
        }

        public void Decrypt(string pathInput, string fileName)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding win1251 = Encoding.GetEncoding("Windows-1251");

            ////bytes of encrypt word
            //byte[] p = win1251.GetBytes(key.ToString());

            ////text
            //string input = "";

            //using (StreamReader sr = new StreamReader(pathInput))
            //{
            //    input = sr.ReadToEnd();
            //}

            ////encrypt text
            //string result = "";

            ////encrypt container
            //List<byte> bytes = new List<byte>();

            //int p_index = 0;

            ////encrypt text and fill container
            //foreach (char symbol in input)
            //{
            //    if (dict.ContainsKey(symbol))
            //    {
            //        if (p_index < p.Length)
            //        {
            //            if (p[p_index] == 1)
            //            {
            //                result += dict[symbol];
            //            }
            //            else
            //            {
            //                result += symbol;
            //            }
            //            p_index++;
            //            bytes.Add(p[p_index]);
            //        }
            //        else
            //        {
            //            result += symbol;
            //            bytes.Add(0);
            //        }
            //    }
            //    else
            //    {
            //        result += symbol;
            //    }
            //}

            //using (StreamWriter sw = new StreamWriter(pathOutput))
            //{
            //    sw.WriteLine(result);
            //    Console.WriteLine(result);
            //}

            //using (StreamWriter sw = new StreamWriter(fileName))
            //{
            //    foreach (var b in bytes)
            //    {
            //        sw.Write(b);
            //        Console.WriteLine(b);
            //    }
            //}

            ////Directory.Delete(pathInput, true);

            ////foreach (var item in map)
            ////{
            ////    Console.WriteLine(item.Key);
            ////    foreach(var k in item.Value)
            ////        Console.Write(k + " ");
            ////    Console.WriteLine();
            ////}
        }
    }
}
