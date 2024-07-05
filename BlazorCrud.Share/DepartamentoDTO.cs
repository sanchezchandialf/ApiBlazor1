using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace BlazorCrud.Shared
{
    public class DepartamentoDTO
    {
        public int IdDepartamento { get; set; }
        public string Nombre { get; set; } = null!;
    }
}
