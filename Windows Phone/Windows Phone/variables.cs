using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Windows_Phone
{
    class variables
    {
        static string servidor = "localhost";
        static string db = "restaurant";
        static string usuario = "root";
        static string pass = "";

        public static string Servidor
        {
            get { return variables.servidor; }
            set { variables.servidor = value; }
        }

        public static string Db
        {
            get { return variables.db; }
            set { variables.db = value; }
        }

        public static string Usuario
        {
            get { return variables.usuario; }
            set { variables.usuario = value; }
        }

        public static string Pass
        {
            get { return variables.pass; }
            set { variables.pass = value; }
        }

        static string NumeroOrden;

        public static string numeroorden
        {
            get { return variables.NumeroOrden; }
            set { variables.NumeroOrden = value; }
        }

        static int Idmesero;

        public static int idmesero
        {
            get { return variables.Idmesero; }
            set { variables.Idmesero = value; }
        }

        static string Nombre;

        public static string nombre
        {
            get { return variables.Nombre; }
            set { variables.Nombre = value; }
        }
    }
}
