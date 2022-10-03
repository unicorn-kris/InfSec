using System;

namespace Hash_Summ
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var hash = new Hash_Summ();
			var hashs = hash.GetFilesHash();
			var result = hash.CheckFilesHash(hashs);

			if (result)
            {
				Console.WriteLine("Good!");
            }
            else
            {
				Console.WriteLine("Have changes in path!");
            }
		}
	}
}