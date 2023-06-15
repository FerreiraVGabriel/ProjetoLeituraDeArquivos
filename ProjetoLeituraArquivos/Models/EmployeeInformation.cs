using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoLeituraArquivos.Models
{
    public class EmployeeInformation
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public decimal TotalReceber { get; set; }
        public TimeSpan HorasExtras { get; set; }
        public TimeSpan HorasDebito { get; set; }
        public int DiasFalta { get; set; }
        public int DiasExtras { get; set; }
        public int DiasTrabalhados { get; set; }
        public decimal ValorHorasDebito { get; set; }
        public decimal ValorHorasExtras { get; set; }
    }
}
