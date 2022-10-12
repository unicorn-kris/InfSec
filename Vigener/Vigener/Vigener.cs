using System.Text;

namespace Vigener
{
    internal class Vigener
    {
        public void Encrypt(string key, string pathInput, string pathOutput, string fileName)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding win1251 = Encoding.GetEncoding("Windows-1251");

            List<string> allfiles = Directory.EnumerateFiles(pathInput, "*", SearchOption.AllDirectories).ToList();

            for (int i = 0; i < allfiles.Count; i++)
            {
                allfiles[i] = allfiles[i].Replace(pathInput, "");
            }

            Dictionary<string, string> map = new Dictionary<string, string>();

            foreach (string file in allfiles)
            {
                string input = "";

                using (StreamReader sr = new StreamReader(pathInput + file))
                {
                    input = sr.ReadToEnd();
                }

                string result = "";

                int keyword_index = 0;

                foreach (char symbol in input)
                {
                    byte[] p = new byte[1] {(byte)((win1251.GetBytes(symbol.ToString())[0] +
                        (win1251.GetBytes(key[keyword_index].ToString())[0])) % 255) };

                    result += win1251.GetChars(p)[0];

                    keyword_index++;

                    if ((keyword_index + 1) == key.Length)
                        keyword_index = 0;
                }
                map.Add(file, result);
            }

            using (FileStream fs = File.Create(pathOutput + $"/{fileName}"))
            { }
            using (StreamWriter sw = new StreamWriter(pathOutput + $"/{fileName}"))
            {
                sw.WriteLine(pathInput.Split('/').Last());

                foreach (var item in map)
                {
                    sw.WriteLine(item.Key);
                    sw.WriteLine(item.Value);

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

        public void Decrypt(string key, string pathInput, string pathOutput)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding win1251 = Encoding.GetEncoding("Windows-1251");

            List<string> allfiles = new List<string>();

            using (StreamReader sr = new StreamReader(pathInput))
            {
                allfiles.AddRange(sr.ReadToEnd().Split('\n'));
            }

            Dictionary<string, string> map = new Dictionary<string, string>();

            for (int i = 1; i < allfiles.Count - 1; i += 2)
            {
                map.Add((string)allfiles[i], (string)allfiles[i + 1]);
            }

            DirectoryInfo di = Directory.CreateDirectory(pathOutput + allfiles[0].Split('\\').Last().Replace('\r', ' ') + "new");

            foreach (var item in map)
            {
                int keyword_index = 0;
                string result = "";
                string input = item.Value.Replace('\r', ' ').Remove(item.Value.Length - 1);


                foreach (char symbol in input)
                {
                    byte[] p = new byte[1] {(byte)((win1251.GetBytes(symbol.ToString())[0] + 255 -
                        (win1251.GetBytes(key[keyword_index].ToString())[0])) % 255) };

                    result += win1251.GetChars(p)[0];

                    keyword_index++;

                    if ((keyword_index + 1) == key.Length)
                        keyword_index = 0;
                }

                List<string> dir = item.Key.Split('\\').ToList();

                if (dir.Count == 1)
                {
                    using (FileStream fs = File.Create(di + "\\" + dir[0]))
                    { }
                    if (!string.IsNullOrEmpty(item.Key.Replace('\r', ' ')))
                    {
                        using (StreamWriter sw = new StreamWriter(di + "\\" + item.Key.Replace('\r', ' ')))
                        {
                            sw.WriteLine(result);
                        }
                    }
                }
                else
                {
                    DirectoryInfo di1 = new DirectoryInfo(di.Name);
                    for (int i = 0; i < dir.Count - 1; i++)
                    {
                        if (!string.IsNullOrEmpty(dir[i].Replace('\r', ' ')))
                        {
                            di1 = Directory.CreateDirectory(di + "\\" + dir[i].Split('\\').Last().Replace('\r', ' '));
                        }
                    }

                    using (FileStream fs = File.Create(di1 + "\\" + dir[dir.Count - 1].Replace('\r', ' ')))
                    { }

                    using (StreamWriter sw = new StreamWriter((di1 + "\\" + dir[dir.Count - 1].Replace('\r', ' '))))
                    {
                        sw.WriteLine(result);
                    }
                }
                //Console.WriteLine(result);
            }
        }
    }
}
