using System.Collections;

namespace Hash_Summ
{
    public class FilesHash
    {
        public string Path { get; set; }
        public IList<Hash> FileHash { get; set; }
    }

    public class Hash
    {
        public string file { get; set; }
        public List<bool> hash { get; set; }
    }
}
