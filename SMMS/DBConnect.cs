using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMMS
{
    internal class DBConnect
    {
        private string con;
        public string myConnection()
        {
            con = @"Data Source=DESKTOP-HU84IA2;Initial Catalog=DBMarket;Integrated Security=True;Encrypt=False";
            return con;
        }
    }
}
