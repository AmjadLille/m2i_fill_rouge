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
        int idCanal;
        //List<Commentaire> commentaires;
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
            this.idCanal = idCana;
            //this.commentaires = new List<Commentaire>();
            this.link = link;
            this.img = img;
            this.isStatut = isStatut;
        }
        #endregion
        #region Getter/setter
        public int Id { get => id; set => id = value; }
        public string Titre { get => titre; set => titre = value; }
        public int IdUser { get => idUser; set => idUser = value; }
        public int IdCana { get => idCanal; set => idCanal = value; }
        public string Link { get => link; set => link = value; }
        public string Img { get => img; set => img = value; }
        public int IsStatut { get => isStatut; set => isStatut = value; }
        //public List<Commentaire> Commentaires { get => commentaires; set => commentaires = value; }
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
                    request += "titre = @t ";
                }
                if (idU != -1)
                {
                    if (i != 0)
                    { request += " and "; }
                    request += "idUser = @idU ";
                }
                if (idCo != -1)
                {
                    if (i != 0)
                    { request += " and "; }
                    request += "idComment = @idCo ";
                }
                if (idCa != -1)
                {
                    if (i != 0)
                    { request += " and "; }
                    request += "idCanal = @idCa ";
                }
                if (link != "")
                {
                    if (i != 0)
                    { request += " and "; }
                    request += "link = @link ";
                }
                if (img != "")
                {
                    if (i != 0)
                    { request += " and "; }
                    request += "img = @img ";
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
            command.Parameters.Add(new SqlParameter("@n", t));
            command.Parameters.Add(new SqlParameter("@idU", idU));
            command.Parameters.Add(new SqlParameter("@idCo", idCo));
            command.Parameters.Add(new SqlParameter("@idCa", idCa));
            command.Parameters.Add(new SqlParameter("@link", link));
            command.Parameters.Add(new SqlParameter("@img", img));
            command.Parameters.Add(new SqlParameter("@isStatut", statut));
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Contenu c = new Contenu(
                    reader.GetInt32(0),//id
                    reader.GetString(1),//titre                   
                    reader.GetInt32(2),//idUser                   
                    //reader.GetInt32(3),//idComment                   
                    reader.GetInt32(4),//idCanal             
                    reader.GetString(5),//link                   
                    reader.GetString(6),//img                   
                    reader.GetInt32(7)//isStatut                   
                    );
                liste.Add(c);
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
            command.Parameters.Add(new SqlParameter("@isAdmin", u.IsAdmin));
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
            command.Parameters.Add(new SqlParameter("@isStatut", u.IsStatut));
            command.Parameters.Add(new SqlParameter("@isAdmin", u.IsAdmin));
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
