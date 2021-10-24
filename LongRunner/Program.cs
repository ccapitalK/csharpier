using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace LongRunner
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var streamReader = new StreamReader(
                Console.OpenStandardInput(),
                Console.InputEncoding
            );

            var reader = new StringBuilder();
            string fileName = null;
            string fileContent = null;

            while (true)
            {
                while (true)
                {
                    var character = Convert.ToChar(streamReader.Read());
                    if (character == '\u0004')
                    {
                        break;
                    }

                    reader.Append(character);
                }

                if (fileName == null)
                {
                    fileName = reader.ToString();
                    reader.Clear();
                }
                else
                {
                    fileContent = reader.ToString();
                    Console.WriteLine(fileContent);
                    reader.Clear();
                    fileName = null;
                    fileContent = null;
                }
            }
        }
    }
}
