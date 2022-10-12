using System;

namespace Vigener
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var vig = new Vigener();
			vig.Encrypt("key", "C:/Users/User/Documents/GitHub/InfSec/Vigener/Docs", "C:/Users/User/Documents/GitHub/InfSec/Vigener", "docs.txt");
			vig.Decrypt("key", "C:/Users/User/Documents/GitHub/InfSec/Vigener/docs.txt", "C:/Users/User/Documents/");

			
		}
	}
}
