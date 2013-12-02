using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba___Proyecto
{
    class PasoVariables
    {
        static int verifica;

        public static int Verifica
        {
            get { return PasoVariables.verifica; }
            set { PasoVariables.verifica = value; }
        }
    }
}
