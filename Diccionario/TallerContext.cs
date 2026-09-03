using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diccionario
{
    public class TallerContext: DbContext
    {
        public DbSet<Repuesto> Repuestos { get; set; }
    }
}
