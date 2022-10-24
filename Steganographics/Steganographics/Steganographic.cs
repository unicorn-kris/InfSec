using System.Collections;
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

            var str = win1251.GetBytes(key.ToString());
            //bytes of encrypt word
            BitArray p = new BitArray(str);

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
                        if (p[p_index])
                        {
                            result += dict[symbol];
                            bytes.Add(1);
                        }
                        else
                        {
                            result += symbol;
                            bytes.Add(0);
                        }
                        p_index++;
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
                //Console.WriteLine(result);
            }

            using (StreamWriter sw = new StreamWriter(fileName))
            {
                foreach (var b in bytes)
                {
                    sw.Write(b);
                    //Console.WriteLine(b);
                }
            }
        }

        public void Decrypt(string pathInput, string resultfile)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding win1251 = Encoding.GetEncoding("Windows-1251");

            //text
            string input = "";

            using (StreamReader sr = new StreamReader(pathInput))
            {
                input = sr.ReadToEnd();
            }

            //decrypt result text
            string result = "";

            //decrypt container
            List<byte> bytes = new List<byte>();

            //decrypt text and fill bytes array
            foreach (char symbol in input)
            {
                foreach (var d in dict)
                {
                    if (symbol == d.Value)
                    {
                        bytes.Add(1);
                    }
                    else if (symbol == d.Key)
                    {
                        bytes.Add(0);
                    }
                }
            }

            //list of list[8] array for decrypt word

            List<List<byte>> list = new List<List<byte>>();

            int byte_index = 0;

            List<byte> list_in = new List<byte>();

            foreach (byte b in bytes)
            {
                list_in.Add(b);

                if (byte_index % 8 == 7)
                {
                    list.Add(list_in);
                    list_in = new List<byte>();
                }
                byte_index++;
            }

            foreach (var l in list)
            {
                int stepof2 = 1;
                int summ = 0;
                if (l.Contains(1))
                {
                    //перевод из 8-битного в байты
                    for (int i = 0; i <= 7; i++)
                    {
                        summ += l[i] * stepof2;
                        stepof2 *= 2;
                    }
                    byte[] b = new byte[1];
                    b[0] = (byte)summ;
                    result += win1251.GetChars(b)[0];
                    stepof2 = 1;
                    summ = 0;
                }
                
            }

            using (StreamWriter sw = new StreamWriter(resultfile))
            {
                Console.WriteLine(result);
                sw.WriteLine(result);
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
    }
}
