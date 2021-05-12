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
        int idCommentaires;
        string link;
        string img;
        int isStatut;

        #region Constructeurs
        public Contenu()
        {}
        public Contenu(int id, string titre, int idUser, int idCana, int idCo ,string link, string img, int isStatut)
        {
            this.id = id;
            this.titre = titre;
            this.idUser = idUser;
            this.idCanal = idCana;
            this.idCommentaires = idCo;
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
        public int IdCommentaires { get => idCommentaires; set => idCommentaires = value; }
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
                    reader.GetInt32(3),//idComment                   
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

        public static int AjouterContenu(Contenu c)
        {
            SqlConnection connection = BDDconnexion.Connection;
            string request = "INSERT INTO Contenu " +
                             "values(@titre, @idUser, @idComment, @idCanal, @link, @img, @isStatut)";

            SqlCommand command = new SqlCommand(request, connection);
            command.Parameters.Add(new SqlParameter("@titre", c.Titre));
            command.Parameters.Add(new SqlParameter("@idUser", c.IdUser));
            command.Parameters.Add(new SqlParameter("@idComment", c.IdCommentaires));
            command.Parameters.Add(new SqlParameter("@idCanal", c.IdCana));
            command.Parameters.Add(new SqlParameter("@link", c.Link));
            command.Parameters.Add(new SqlParameter("@img", c.Img));
            command.Parameters.Add(new SqlParameter("@isStatut", c.IsStatut));
            connection.Open();

            int idContenu = (int)command.ExecuteScalar();
            command.Dispose();
            connection.Close();

            return idContenu;
        }

        public static bool ModifierContenu(Contenu c)
        {
            SqlConnection connection = BDDconnexion.Connection;
            string request = "Update Contenu set " +
                "titre = @titre, " +
                "idUser = @idUser, " +
                "idComment = @idComment, " +
                "idCanal = @idCanal," +
                "link = @link" +
                "img = @img " +
                "isStatut = @isStatut " +
                "where id = @id";

            SqlCommand command = new SqlCommand(request, connection);
            command.Parameters.Add(new SqlParameter("@titre", c.Titre));
            command.Parameters.Add(new SqlParameter(" @idUser", c.IdUser));
            command.Parameters.Add(new SqlParameter("@idComment", c.idCommentaires));
            command.Parameters.Add(new SqlParameter("@idCanal", c.IdCana));
            command.Parameters.Add(new SqlParameter("@link", c.Link));
            command.Parameters.Add(new SqlParameter("@img", c.Img));
            command.Parameters.Add(new SqlParameter("@isStatut", c.IsStatut));
            command.Parameters.Add(new SqlParameter("@id", c.Id));
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
