using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLib
{
    class Baza
    {
        public static string ConnectionString()
        {
            return string.Format(
                "Data Source={0}; Initial Catalog={1}; Integrated Security=True;",
                host,
                database
            );
        }

        private const string host = @"DESKTOP-TL2MLQ4\LOCALSERVER";

        private const string database = "csprojekt";
    }
}
