using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace CRUDCadastroClientes
{
    class Conexao
    {
        public static string Local()
        {
            return ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
            // String do local aonde está seu database.
        }
    }
}
