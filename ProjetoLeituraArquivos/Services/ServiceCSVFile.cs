using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using ProjetoLeituraArquivos.Models;
using ProjetoLeituraArquivos.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ProjetoLeituraArquivos.Services
{
    public class ServiceCSVFile
    {
        //Pega todos os arquivos do tipo CSV de um diretório
        public static string[] GetCSVFiles(string path)
        {
            string[] csvFiles = Directory.GetFiles(path, "*.csv", System.IO.SearchOption.AllDirectories);

            return csvFiles;
        }

        //Converter o Arquivo Json para 
        public async static Task<List<EmployeeInformationCSV>> ConvertCSVToJSON(string csvFile)
        {
            var records = new List<dynamic>();

            using (var parser = new TextFieldParser(csvFile, Encoding.GetEncoding("ISO-8859-1")))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(";");

                string[] headersDocumento = parser.ReadFields();
                headersDocumento = Helpers.RemoveAccentsAndSpaces(headersDocumento);

                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    dynamic record = new ExpandoObject();

                    for (int i = 0; i < fields.Length; i++)
                    {
                        string header = headersDocumento[i];
                        string value = fields[i];
                        ((IDictionary<string, object>)record)[header] = value;
                    }

                    records.Add(record);
                }
            }

            string jsonSerialized = JsonConvert.SerializeObject(records);

            List<EmployeeInformationCSV> result = JsonConvert.DeserializeObject<List<EmployeeInformationCSV>>(jsonSerialized);

            return await Task.FromResult(result);
        }

        public async static Task CovertFileJsonTCSV(string destinationPath, string fileName, List<PayrollClosing> listPayrollClosing)
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented,
            };

            string convertListToString = JsonConvert.SerializeObject(listPayrollClosing, settings);

            await File.WriteAllTextAsync(Path.Combine(destinationPath, $"{fileName}.json"), convertListToString);
        }
    }
}
