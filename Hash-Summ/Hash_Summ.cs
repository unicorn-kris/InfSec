using System.Collections;
using System.Text.Json;

namespace Hash_Summ
{
    public class Hash_Summ
    {
        IList<Hash> filesHash = new List<Hash>();

        public FilesHash GetFilesHash()
        {
            FilesHash result = new FilesHash();
            string path = string.Empty;
            using (StreamReader sr = new StreamReader("C:/Users/krisF/Documents/GitHub/InfSec/Hash-Summ/path.txt"))
            {
                path = sr.ReadToEnd();
            }
            List<string> allfiles = Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories).ToList();

            foreach (string file in allfiles)
            {
                using (FileStream fs = new FileStream(file, FileMode.Open))
                {
                    BitArray hashsum = null;
                    byte[] data;

                    data = new byte[fs.Length];
                    fs.Read(data, 0, data.Length);

                    var bitArray = new BitArray(data);

                    for (int i = 0; i < bitArray.Length / 16; i++)
                    {
                        BitArray bits = new BitArray(16);
                        for (int j = i * 16; j < (i + 1) * 16; ++j)
                        {
                            bits[j - (i * 16)] = bitArray[j];
                        }
                        if (i != 0)
                        {
                            hashsum.Xor(bits);
                        }
                        else
                        {
                            hashsum = bits;
                        }
                    }
                    //add 0 values if less then 16 bits and if empty file
                    if (bitArray.Length % 16 != 0 || bitArray.Length == 0)
                    {
                        BitArray bits = new BitArray(16);
                        int i = 0;
                        for (int j = bitArray.Length / 16 * 16; j < bitArray.Length; ++j)
                        {
                            bits[i] = bitArray[j];
                            ++i;
                        }
                        for (int k = bits.Length - 1; k < 16; ++k)
                        {
                            bits[k] = false;
                        }

                        if (hashsum != null && hashsum.Count != 0)
                        {
                            hashsum.Xor(bits);
                        }
                        else
                        {
                            hashsum = bits;
                        }
                    }
                    bool[] array = new bool[0];
                    if (!(hashsum == null))
                    {
                        array = new bool[hashsum.Count];

                        hashsum.CopyTo(array, 0);
                    }
                    filesHash.Add(new Hash() { file = file, hash = array.ToList() });
                }



            }
            result = new FilesHash() { FileHash = filesHash.ToList(), Path = path };
            string jsonString = JsonSerializer.Serialize(result);
            FilesHash files = null;

            using (StreamReader str = new StreamReader("C:/Users/krisF/Documents/GitHub/InfSec/Hash-Summ/files_hash.json"))
            {
                if (!String.IsNullOrEmpty(str.ReadToEnd()))
                {
                    return result;
                }
            }
            using (StreamWriter sr = new StreamWriter("C:/Users/krisF/Documents/GitHub/InfSec/Hash-Summ/files_hash.json"))
            {
                sr.WriteLine(jsonString);
            }
            return result;

        }

        public bool CheckFilesHash(FilesHash fileHash)
        {
            var str = new StreamReader("C:/Users/krisF/Documents/GitHub/InfSec/Hash-Summ/files_hash.json").ReadToEnd();
            FilesHash files = JsonSerializer.Deserialize<FilesHash>(str);

            var errors = 0;

            if (files.Path != null)
            {
                if (fileHash.Path.Equals(files.Path))
                {
                    foreach (var file in fileHash.FileHash)
                    {
                        if (!files.FileHash.Select(fh => fh.file).Contains(file.file))
                        {
                            Console.WriteLine($"add new file {file.file}");
                            errors++;
                        }
                        else
                        {
                            int count = 0;
                            var hash = files.FileHash.Where(f => f.file.Equals(file.file)).First().hash;
                            for (int i = 0; i < hash.Count; i++)
                            {
                                if (file.hash[i] != hash[i])
                                    count++;
                            }
                            if (count > 0)
                            {
                                Console.WriteLine($"change the file {file.file} in path {files.Path}");
                                errors++;
                            }
                        }
                    }
                    foreach (var file in files.FileHash)
                    {
                        if (!fileHash.FileHash.Select(fh => fh.file).Contains(file.file))
                        {
                            Console.WriteLine($"delete the file {file.file} in path {files.Path}");
                            errors++;
                        }
                    }
                }
            }

            if (errors > 0)
            {
                return false;
            }
            return true;
        }
    }
}

