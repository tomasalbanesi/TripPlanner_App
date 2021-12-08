using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class Detalle_Viaje
    {
        private Datos.Detalle_Viaje objDatosDetalleViaje = new Datos.Detalle_Viaje();

        public List<Entidades.Detalle_Viaje> Listar(int idViaje)
        {
            return objDatosDetalleViaje.Listar(idViaje);
        }

        public int Registrar(Entidades.Detalle_Viaje objEntidadesDetalleViaje, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (objEntidadesDetalleViaje.NroDia <= 0)
            {
                Mensaje += "Es necesario numero de dia del viaje\n";
            }

            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objDatosDetalleViaje.Registrar(objEntidadesDetalleViaje, out Mensaje);
            }

        }

        public bool Editar(Entidades.Detalle_Viaje objEntidadesDetalleViaje, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (objEntidadesDetalleViaje.NroDia <= 0)
            {
                Mensaje += "Es necesario el numero de dia del viaje\n";
            }

            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objDatosDetalleViaje.Editar(objEntidadesDetalleViaje, out Mensaje);
            }

        }

        public bool Eliminar(Entidades.Detalle_Viaje objEntidadesDetalleViaje, out string Mensaje)
        {

            return objDatosDetalleViaje.Eliminar(objEntidadesDetalleViaje, out Mensaje);

        }
    }
}
