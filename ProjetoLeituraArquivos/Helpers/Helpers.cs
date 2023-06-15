using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjetoLeituraArquivos.Utils
{
    public class Helpers
    {
        //Valida o caminho do diretório digitado pelo usuario
        public static string ValidDirectory(string directoryPath)
        {
            directoryPath = IsDirectoryNullOrWhiteSpace(directoryPath);
            directoryPath = ExistDirectory(directoryPath);
            return directoryPath;
        }    
        
        //Valida o nome do caminho do diretório digitado pelo usuario
        public static string ValidDirectoryName(string directoryName)
        {
            char[] invalidFileChars = Path.GetInvalidFileNameChars();

            while (directoryName.Any(x => invalidFileChars.Contains(x)))
            {
                Console.WriteLine(Resources.InvalidName);
                Console.WriteLine(Resources.EmptyMessage);
                Console.WriteLine(Resources.ValidPathToDirectory);
                directoryName = Console.ReadLine();
            }
            return directoryName;
        }

        //Verifica se o caminho do diretório é diferente de vazio ou Null
        private static string IsDirectoryNullOrWhiteSpace(string directoryPath)
        {
            while (string.IsNullOrWhiteSpace(directoryPath))
            {
                Console.WriteLine(Resources.InvalidDirectory);
                Console.WriteLine(Resources.EmptyMessage);
                Console.WriteLine(Resources.ValidPathToDirectory);
                directoryPath = Console.ReadLine();
            }
            return directoryPath;
        }

        //Verifica se o caminho do diretório existe
        private static string ExistDirectory(string directoryPath)
        {
            while (!Directory.Exists(directoryPath))
            {
                Console.WriteLine(Resources.DirectoryNotExist);
                Console.WriteLine(Resources.EmptyMessage);
                Console.WriteLine(Resources.ValidPathToDirectory);
                directoryPath = Console.ReadLine();
            }
            return directoryPath;
        }

        //Remove acentos e espaços em branco
        public static string[]? RemoveAccentsAndSpaces(string[]? headersDocumento)
        {
            List<string> returnArray = new List<string>();
            foreach (string header in headersDocumento)
            {

                string normalize = header.Normalize(NormalizationForm.FormD);

                string pattern = @"\p{M}";
                string textWithouAccentsAndSpaces = Regex.Replace(normalize, pattern, "");
                textWithouAccentsAndSpaces = textWithouAccentsAndSpaces.Replace(" ", "");
                returnArray.Add(textWithouAccentsAndSpaces);

            }

            return returnArray.ToArray();
        }

        //Converter um valor (dinheiro) do tipo string para o tipo decimal
        public static decimal ConvertMoneyTypeStringToDecimal(string money)
        {
            money = money.Replace("R$", "").Replace(" ", "").Trim();
            return decimal.Parse(money);
        }
    }
}
