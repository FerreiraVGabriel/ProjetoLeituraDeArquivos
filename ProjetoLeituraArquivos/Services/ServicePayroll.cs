using ProjetoLeituraArquivos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoLeituraArquivos.Services
{
    public class ServicePayroll
    {
        public static PayrollClosing EmployeePayroll(List<EmployeeInformationCSV> listEmployeeInformationCSV, string csvFile)
        {
            PayrollClosing payrollClosing = new PayrollClosing();

            string[] departmentInformations = DepartmentInformations(csvFile);
            payrollClosing.Departamento = departmentInformations[0];
            payrollClosing.MesVigencia = departmentInformations[1];
            payrollClosing.AnoVigencia = departmentInformations[2];

            payrollClosing.Funcionarios = ServiceEmployee.GetEmployeesInformation(listEmployeeInformationCSV, departmentInformations[1], departmentInformations[2]);

            payrollClosing = PayrollClosingInformations(payrollClosing);

            return payrollClosing;
        }

        //Retorna as informaçoes do departamento
        //1 Posição - Nome
        //2 Posição - Mês
        //3 Posição - Ano
        private static string[] DepartmentInformations(string csvFile)
        {
            string fileName = Path.GetFileName(csvFile);
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            return fileNameWithoutExtension.Split('-');
        }

        //Retorna as informações dos departamentos do mes, com base nos funcionarios
        private static PayrollClosing PayrollClosingInformations(PayrollClosing payrollClosing)
        {
            payrollClosing.TotalPagar = TotalPayable(payrollClosing.Funcionarios);
            payrollClosing.TotalDescontos = TotalDebitHours(payrollClosing.Funcionarios);
            payrollClosing.TotalExtras = TotalExtraHours(payrollClosing.Funcionarios);

            return payrollClosing;
        }

        //Calcula a soma do valor que sera pago a todos os funcionarios
        private static decimal TotalPayable (List<EmployeeInformation> listEmployeeInformation)
        {
            return listEmployeeInformation.Select(x=>x.TotalReceber).Sum();
        }

        //Calcula a soma do valor de horas extras de todos os funcionarios
        private static decimal TotalExtraHours(List<EmployeeInformation> listEmployeeInformation)
        {
            return listEmployeeInformation.Select(x => x.ValorHorasExtras).Sum();
        }

        //Calcula a soma do valor de débito de todos os funcionarios
        private static decimal TotalDebitHours(List<EmployeeInformation> listEmployeeInformation)
        {
            return listEmployeeInformation.Select(x => x.ValorHorasDebito).Sum();
        }
    }
}
