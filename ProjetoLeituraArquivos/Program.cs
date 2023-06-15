using ProjetoLeituraArquivos;
using ProjetoLeituraArquivos.Models;
using ProjetoLeituraArquivos.Services;
using ProjetoLeituraArquivos.Utils;
using System;
using System.Collections.Generic;
using System.IO;

class App
{
    //Usado o * na consulta quando quer buscar por TODOS
    public const string GetAll = "*";
    public static void Main()
    {
        string path = ServiceDirectoryPath.DirectoryPath();
        string destinationPath = ServiceDirectoryPath.DestinationPathDirectoryPath();
        string fileName = ServiceDirectoryPath.DestinationPathName();
        string[] csvFiles = ServiceCSVFile.GetCSVFiles(path);

        List<PayrollClosing> listPayrollClosing = new List<PayrollClosing>();

        foreach (string csvFile in csvFiles)
        {
            var jsonFile = ServiceCSVFile.ConvertCSVToJSON(csvFile);
            List<EmployeeInformationCSV> listEmployeeInformationCSV = jsonFile.Result;

            listPayrollClosing.Add(ServicePayroll.EmployeePayroll(listEmployeeInformationCSV, csvFile));
        }

        ServiceCSVFile.CovertFileJsonTCSV(destinationPath, fileName, listPayrollClosing);
    }
} 
