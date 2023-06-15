using ProjetoLeituraArquivos.Models;
using ProjetoLeituraArquivos.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoLeituraArquivos.Services
{
    public class ServiceEmployee
    {
        //Horas Trabalhada no dia
        static readonly TimeSpan WorkedHoursInDay = new TimeSpan(8, 0, 0);
        public static List<EmployeeInformation> GetEmployeesInformation(List<EmployeeInformationCSV> listEmployeeInformationCSV, string month, string year)
        {
            List<EmployeeInformation> returnEmployeeInformation = new List<EmployeeInformation>();

            int[] EmployeesKey = listEmployeeInformationCSV.Select(x=>x.Codigo).Distinct().ToArray();

            foreach (var employeeKey in EmployeesKey)
            {
                List<EmployeeInformationCSV> listEmployeeInformation = listEmployeeInformationCSV.Where(x=> x.Codigo == employeeKey).ToList();
                returnEmployeeInformation.Add(GetEmployeeInformation(listEmployeeInformation, month, year));

            }
            return returnEmployeeInformation;
        }

        //Pega as informações dos funcionários
        public static EmployeeInformation GetEmployeeInformation(List<EmployeeInformationCSV> listEmployeeInformation, string month, string year)
        {
            EmployeeInformation returnEmployeeInformation = new EmployeeInformation();
            returnEmployeeInformation.Nome = listEmployeeInformation.First().Nome;
            returnEmployeeInformation.Codigo = listEmployeeInformation.First().Codigo;

            int monthNumber = DateTime.ParseExact(month, "MMMM", CultureInfo.CurrentCulture).Month;

            DateTime firstDay = new DateTime(Convert.ToInt16(year), monthNumber, 1);
            DateTime lastDay = new DateTime(Convert.ToInt16(year), monthNumber,
                    DateTime.DaysInMonth(Convert.ToInt16(year), monthNumber));

            DateTime calendarDay = firstDay;

            for (int i = 1; i<= lastDay.Day; i++)
            {
                EmployeeInformationCSV employeeInformation = listEmployeeInformation.Where(x=>Convert.ToDateTime(x.Data).Day == i).FirstOrDefault();


                if (employeeInformation == null)
                {
                    if (!IsWeekend(calendarDay))
                    {
                        returnEmployeeInformation.HorasDebito += WorkedHoursInDay;
                        returnEmployeeInformation.DiasFalta += 1;
                        returnEmployeeInformation.ValorHorasDebito += CalculateDayValue(Helpers.ConvertMoneyTypeStringToDecimal(listEmployeeInformation.FirstOrDefault().ValorHora), new TimeSpan(8, 0, 0));
                    }
                }
                else
                {
                    returnEmployeeInformation.DiasTrabalhados += 1;
                    returnEmployeeInformation.TotalReceber += Helpers.ConvertMoneyTypeStringToDecimal(listEmployeeInformation.FirstOrDefault().ValorHora);

                    TimeSpan lunchTime = new TimeSpan();
                    TimeSpan workerHours = new TimeSpan();

                    lunchTime = CalculateLunchTime(employeeInformation.Almoco);
                    workerHours = CalculateWorkerHours(Convert.ToDateTime(employeeInformation.Entrada), Convert.ToDateTime(employeeInformation.Saida), lunchTime);

                    if (IsWeekend(calendarDay))
                    {
                        returnEmployeeInformation.HorasExtras += workerHours;
                    }
                    else
                    {
                        if (workerHours > WorkedHoursInDay)
                        {
                            returnEmployeeInformation.HorasExtras += (workerHours - WorkedHoursInDay);
                            returnEmployeeInformation.ValorHorasExtras += CalculateDayValue(Helpers.ConvertMoneyTypeStringToDecimal(listEmployeeInformation.FirstOrDefault().ValorHora), (workerHours - WorkedHoursInDay));
                        }

                        if (workerHours < WorkedHoursInDay)
                        {
                            returnEmployeeInformation.HorasDebito -= workerHours - WorkedHoursInDay;
                            returnEmployeeInformation.ValorHorasDebito += CalculateDayValue(Helpers.ConvertMoneyTypeStringToDecimal(listEmployeeInformation.FirstOrDefault().ValorHora), (workerHours - WorkedHoursInDay));
                        }
                    }

                    calendarDay = calendarDay.AddDays(1);
                }
            }

            return returnEmployeeInformation;
        }

        //Verifica se o dia é fim de semana
        private static bool IsWeekend(DateTime date)
        {
            if (date.DayOfWeek == DayOfWeek.Saturday)
                return true;

            else if (date.DayOfWeek == DayOfWeek.Sunday)
                return true;
            else 
                return false;

        }

        //Calcula hora de almoço, tendo base que o minimo é 1 hora
        private static TimeSpan CalculateLunchTime(string lunchTime)
        {
            string[] times = lunchTime.Split('-');
            string startLunchTimeString = times[0].Trim();
            string endLunchTimeString = times[1].Trim();

            TimeSpan startLunchTime = TimeSpan.Parse(startLunchTimeString);
            TimeSpan endLunchTime = TimeSpan.Parse(endLunchTimeString);

            return (endLunchTime - startLunchTime);

        }

        //Calcula as horas trabalhadas no dia
        private static TimeSpan CalculateWorkerHours(DateTime startWork, DateTime endWork, TimeSpan lunchTime)
        {
            TimeSpan workedHours = endWork - startWork;
            return workedHours - lunchTime;
        }

        //Calcula valor recebido por horas trabalhadas
        private static decimal CalculateDayValue(decimal hourValue, TimeSpan workerHours)
        {
            return hourValue * Convert.ToDecimal(workerHours.TotalHours);
        }
    }
}
