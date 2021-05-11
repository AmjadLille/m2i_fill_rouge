using ProjetFilRouge.BDD;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ProjetFilRouge.Classes
{
    class Contenu
    {
        int id;
        string titre;
        int idUser;
        int idCana;
        string link;
        string img;
        int isStatut;

        #region Constructeurs
        public Contenu()
        {}
        public Contenu(int id, string titre, int idUser, int idCana, string link, string img, int isStatut)
        {
            this.id = id;
            this.titre = titre;
            this.idUser = idUser;
            this.idCana = idCana;
            this.link = link;
            this.img = img;
            this.isStatut = isStatut;
        }
        #endregion
        #region Getter/setter
        public int Id { get => id; set => id = value; }
        public string Titre { get => titre; set => titre = value; }
        public int IdUser { get => idUser; set => idUser = value; }
        public int IdCana { get => idCana; set => idCana = value; }
        public string Link { get => link; set => link = value; }
        public string Img { get => img; set => img = value; }
        public int IsStatut { get => isStatut; set => isStatut = value; }
        #endregion

        public static List<Contenu> ContenuRecherche(int id, string t, int idU, int idCo, int idCa, string link, string img, int statut)
        {
            List<Contenu> liste = new List<Contenu>();

            SqlConnection connection = BDDconnexion.Connection;
            string request = "Select * from contenu";

            if (id != -1 || t != "" || idU != -1 || idCo != -1 || idCa != -1 || link != "" || img != "" || statut != 0)
            {
                int i = 0;
                request += " where ";
                if (id != -1)
                {
                    if (i != 0)
                    { request += " and "; }
                    request += "id = @id ";
                }
                if (t != "")
                {
                    if (i != 0)
                    { request += " and "; }
                    request += "nom = @n ";
                }
                if (idU != -1)
                {
                    if (i != 0)
                    { request += " and "; }
                    request += "prenom = @p ";
                }
                if (idCo != -1)
                {
                    if (i != 0)
                    { request += " and "; }
                    request += "pseudo = @ps ";
                }
                if (idCa != -1)
                {
                    if (i != 0)
                    { request += " and "; }
                    request += "email = @email ";
                }
                if (link != "")
                {
                    if (i != 0)
                    { request += " and "; }
                    request += "email = @email ";
                }
                if (img != "")
                {
                    if (i != 0)
                    { request += " and "; }
                    request += "email = @email ";
                }
                if (statut != 0)
                {
                    if (i != 0)
                    { request += " and "; }
                    request += "isStatut = @isS ";
                }
            }

            SqlCommand command = new SqlCommand(request, connection);
            command.Parameters.Add(new SqlParameter("@id", id));
            command.Parameters.Add(new SqlParameter("@n", n));
            command.Parameters.Add(new SqlParameter("@p", p));
            command.Parameters.Add(new SqlParameter("@ps", ps));
            command.Parameters.Add(new SqlParameter("@email", email));
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
                    reader.GetInt32(7)//isAdmin                   
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
                "isStatut = @isStatut" +
                "isAdmin = @isAdmin " +
                "where id = @id";

            SqlCommand command = new SqlCommand(request, connection);
            command.Parameters.Add(new SqlParameter("@nom", u.Nom));
            command.Parameters.Add(new SqlParameter("@pseudo", u.Pseudo));
            command.Parameters.Add(new SqlParameter("@mdp", u.Mdp));
            command.Parameters.Add(new SqlParameter("@email", u.Email));
            command.Parameters.Add(new SqlParameter("@isStatut", u.isStatut));
            command.Parameters.Add(new SqlParameter("@isAdmin", u.isAdmin));
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
