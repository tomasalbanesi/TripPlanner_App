using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Detalle_Viaje
    {
        public int IdDetalleViaje { get; set; }
        public int IdViaje { get; set; }
        public int NroDia { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
