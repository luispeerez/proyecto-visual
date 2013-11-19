using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba___Proyecto
{
    class variables
    {
        static int cantidadMesas;

        public static int CantidadMesas
        {
            get { return variables.cantidadMesas; }
            set { variables.cantidadMesas = value; }
        }

    }
}
