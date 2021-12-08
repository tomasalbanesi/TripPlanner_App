using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class Viaje
    {
        private Datos.Viaje objDatosViaje = new Datos.Viaje();

        public List<Entidades.Viaje> Listar()
        {
            return objDatosViaje.Listar();  
        }

        public int Registrar(Entidades.Viaje objEntidadesViaje, out string Mensaje)
        {
            Mensaje = string.Empty;

            if(objEntidadesViaje.Titulo == "")
            {
                Mensaje += "Es necesario el titulo del viaje\n";
            }

            if(Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objDatosViaje.Registrar(objEntidadesViaje, out Mensaje);
            }

        }

        public bool Editar(Entidades.Viaje objEntidadesViaje, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (objEntidadesViaje.Titulo == "")
            {
                Mensaje += "Es necesario el titulo del viaje\n";
            }

            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objDatosViaje.Editar(objEntidadesViaje, out Mensaje);
            }

        }

        public bool Eliminar(Entidades.Viaje objEntidadesViaje, out string Mensaje)
        {

            return objDatosViaje.Eliminar(objEntidadesViaje, out Mensaje);

        }

    }
}
