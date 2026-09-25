using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diccionario
{
    public class ResumenInventario
    {
        public int CantidadRepuestos { get; set; }
        public int stockTotal { get; set; }
        public double PromedioStockGeneral { get; set; }
        public Repuesto? StockMayor { get; set; }
        public Repuesto? StockMenor { get; set; }
    }
}
