using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace Diccionario
{
    [Table("Repuestos")]
    public class Repuesto
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public int Stock {  get; set; }
        public decimal Precio { get; set; }
        public bool Estado { get; set; }
    }
}
