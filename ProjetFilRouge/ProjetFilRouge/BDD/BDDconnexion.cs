using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ProjetFilRouge.BDD
{
    class BDDconnexion
    {
        static string connectionString = @"Data Source=(LOCALDB)\projetFilRouge;Integrated Security=True";
        public static SqlConnection Connection { get => new SqlConnection(connectionString); }
    }
}
