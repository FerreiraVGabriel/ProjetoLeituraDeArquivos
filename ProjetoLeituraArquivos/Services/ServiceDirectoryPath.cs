using ProjetoLeituraArquivos.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoLeituraArquivos.Services
{
    public class ServiceDirectoryPath
    {
        //Pega o caminho do diretorio de arquivos
        public static string DirectoryPath()
        {
            Console.WriteLine(Resources.ReadPathFile);
            Console.WriteLine(Resources.EmptyMessage);
            Console.WriteLine(Resources.TypeFileCSV);

            string directoryPath = Console.ReadLine();

            return Helpers.ValidDirectory(directoryPath);
        }

        //Pega o caminho do diretorio de arquivos que sera salvo o json
        public static string DestinationPathDirectoryPath()
        {
            Console.WriteLine(Resources.DownloadPathFile);

            string directoryPath = Console.ReadLine();

            return Helpers.ValidDirectory(directoryPath);
        }

        //Pega o nome do arquivo que sera salvo o json
        public static string DestinationPathName()
        {
            Console.WriteLine(Resources.DestinationPathName);

            string directoryName = Console.ReadLine();

            return Helpers.ValidDirectoryName(directoryName);

        }
    }
}
