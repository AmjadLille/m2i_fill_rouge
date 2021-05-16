using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using ProjetFilRouge.BDD;

namespace ProjetFilRouge.Classes
{
    class User
    {
        int id;
        string nom;
        string prenom;
        string pseudo;
        string mdp;
        string email;
        int isStatut;
        bool isAdmin;

        #region Constructeurs
        public User()
        {}
        public User(int id, string nom, string prenom, string pseudo, string mdp, string email, int isStatut, bool isAdmin)
        {
            this.id = id;
            this.nom = nom;
            this.prenom = prenom;
            this.pseudo = pseudo;
            this.mdp = mdp;
            this.email = email;
            this.isStatut = isStatut;
            this.isAdmin = isAdmin;
        }
        #endregion

        #region Getter/Setter
        public int Id { get => id; set => id = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public string Pseudo { get => pseudo; set => pseudo = value; }
        public string Mdp { get => mdp; set => mdp = value; }
        public string Email { get => email; set => email = value; }
        public int IsStatut { get => isStatut; set => isStatut = value; }
        public bool IsAdmin { get => isAdmin; set => isAdmin = value; }

        #endregion

        public static List<User> UserRecherche(int id, string n, string p, string ps, string email, int admin, int statut)
        {
            List<User> liste = new List<User>();

            SqlConnection connection = BDDconnexion.Connection;
            string request = "Select * from users";

            if (id != -1 || n != "" || p != "" || ps != "" || email != "" || admin != -1 || statut != 0)
            {
                int i = 0;
                request += " where ";
                if (id != -1)    
                {
                    if (i != 0)
                    { request += " and "; }
                    request += "id = @id "; 
                }
                if (n != "")     
                {
                    if (i != 0)
                    { request += " and "; }
                    request += "nom = @n "; 
                }
                if (p != "")     
                {
                    if (i != 0)
                    { request += " and "; }
                    request += "prenom = @p "; 
                }
                if (ps != "")    
                {
                    if (i != 0)
                    { request += " and "; }
                    request += "pseudo = @ps "; 
                }
                if (email != "") 
                {
                    if (i != 0)
                    { request += " and "; }
                    request += "email = @email "; 
                }
                if (admin != -1)
                {
                    if (i != 0)
                    { request += " and "; }
                    request += "isAdmin = @isAdmin ";
                }
                if (statut != 0)
                {
                    if (i != 0)
                    { request += " and "; }
                    request += "isStatut = @isStatut ";
                }
            }

            SqlCommand command = new SqlCommand(request,connection);
            command.Parameters.Add(new SqlParameter("@id", id));
            command.Parameters.Add(new SqlParameter("@n", n));
            command.Parameters.Add(new SqlParameter("@p", p));
            command.Parameters.Add(new SqlParameter("@ps", ps));
            command.Parameters.Add(new SqlParameter("@email", email));
            command.Parameters.Add(new SqlParameter("@isStatut", statut));
            if (admin == 1)
            { command.Parameters.Add(new SqlParameter("@isAdmin", true)); }
            else if (admin == 0)
            { command.Parameters.Add(new SqlParameter("@isAdmin", false)); }

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                User u = new User(
                    reader.GetInt32(0),//id
                    reader.GetString(1),//nom                   
                    reader.GetString(2),//prenom                   
                    reader.GetString(3),//pseudo                   
                    reader.GetString(4),//mdp                   
                    reader.GetString(5),//email                   
                    reader.GetInt32(6),//isStatut                   
                    reader.GetBoolean(7)//isAdmin                   
                    );
                liste.Add(u);
            }
            reader.Close();
            command.Dispose();
            connection.Close();
            return liste;
        }

        public static int AjouterUser(User u)
        {
            SqlConnection connection = BDDconnexion.Connection;
            string request = "Insert into Users output inserted.id " +
                             "values(@nom, @prenom, @pseudo, @mdp, @email, 1, @isAdmin)";

            SqlCommand command = new SqlCommand(request, connection);
            command.Parameters.Add(new SqlParameter("@nom", u.Nom));
            command.Parameters.Add(new SqlParameter("@prenom", u.Prenom));
            command.Parameters.Add(new SqlParameter("@pseudo", u.Pseudo));
            command.Parameters.Add(new SqlParameter("@mdp", u.Mdp));
            command.Parameters.Add(new SqlParameter("@email", u.Email));
            command.Parameters.Add(new SqlParameter("@isAdmin", u.isAdmin));
            connection.Open();

            int idUser = (int)command.ExecuteScalar();
            command.Dispose();
            connection.Close();

            return idUser;
        }

        public static bool ModifierUser(User u)
        {
            SqlConnection connection = BDDconnexion.Connection;
            string request = "Update Users set " +
                "nom = @nom, " +
                "pseudo = @pseudo, " +
                "mdp = @mdp, " +
                "email = @email," +
                "isStatut = @isStatut," +
                "isAdmin = @isAdmin " +
                "where id = @id";

            SqlCommand command = new SqlCommand(request, connection);
            command.Parameters.Add(new SqlParameter("@nom", u.Nom));
            command.Parameters.Add(new SqlParameter("@pseudo", u.Pseudo));
            command.Parameters.Add(new SqlParameter("@mdp", u.Mdp));
            command.Parameters.Add(new SqlParameter("@email", u.Email));
            command.Parameters.Add(new SqlParameter("@isStatut", u.isStatut));
            command.Parameters.Add(new SqlParameter("@isAdmin", u.isAdmin));
            command.Parameters.Add(new SqlParameter("@id", u.Id));
            connection.Open();

            int valid = command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();

            if (valid > 0)
            { return true; }
            else
            { return false; }
        }
    }
}
