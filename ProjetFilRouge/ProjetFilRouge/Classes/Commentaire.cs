using ProjetFilRouge.BDD;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ProjetFilRouge.Classes
{
    class Commentaire
    {
        int id;
        int idUser;
        int idContenu;
        string msg;
        int isStatut;

        #region Constructeurs
        public Commentaire()
        { }
        public Commentaire(int id,int idUser, int idContenu, string msg, int isStatut)
        {
            this.id = id;
            this.idUser = idUser;
            this.idContenu = idContenu;
            this.msg = msg;
            this.isStatut = isStatut;
        }
        #endregion

        #region Getter/Setter
        public int IdUser { get => idUser; set => idUser = value; }
        public int IdContenu { get => idContenu; set => idContenu = value; }
        public string Msg { get => msg; set => msg = value; }
        public int IsStatut { get => isStatut; set => isStatut = value; }
        public int Id { get => id; set => id = value; }

        #endregion

        public static List<Commentaire> CommentaireRecherche(int id, int idU, int idCo, string msg, int statut)
        {
            List<Commentaire> liste = new List<Commentaire>();

            SqlConnection connection = BDDconnexion.Connection;
            string request = "Select * from Commentaire";

            if (id != -1 || idU != -1 || idCo != -1 || statut != 0)
            {
                int i = 0;
                request += " where ";
                if (id != -1)
                {
                    if (i != 0)
                    { request += " and "; }
                    request += "id = @id ";
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
                if (msg != "")
                {
                    if (i != 0)
                    { request += " and "; }
                    request += "msg = @msg ";
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
            command.Parameters.Add(new SqlParameter("@idU", idU));
            command.Parameters.Add(new SqlParameter("@idCo", idCo));
            command.Parameters.Add(new SqlParameter("@msg", msg));
            command.Parameters.Add(new SqlParameter("@isStatut", statut));
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Commentaire c = new Commentaire(
                    reader.GetInt32(0),//id               
                    reader.GetInt32(1),//idUser                   
                    reader.GetInt32(2),//idComment                   
                    reader.GetString(3),//msg                              
                    reader.GetInt32(4)//isStatut                   
                    );
                liste.Add(c);
            }
            reader.Close();
            command.Dispose();
            connection.Close();
            return liste;
        }

        public static int AjouterContenu(Commentaire c)
        {
            SqlConnection connection = BDDconnexion.Connection;
            string request = "INSERT INTO Commentaire " +
                             "values(@idUser, @idContenu, @msg, @isStatut)";

            SqlCommand command = new SqlCommand(request, connection);
            command.Parameters.Add(new SqlParameter("@idUser", c.IdUser));
            command.Parameters.Add(new SqlParameter("@idContenu", c.IdContenu));
            command.Parameters.Add(new SqlParameter("@msg", c.Msg));
            command.Parameters.Add(new SqlParameter("@isStatut", c.IsStatut));
            connection.Open();

            int idContenu = (int)command.ExecuteScalar();
            command.Dispose();
            connection.Close();

            return idContenu;
        }

        public static bool ModifierCommentaire(Commentaire c)
        {
            SqlConnection connection = BDDconnexion.Connection;
            string request = "Update Commentaire set " +
                "idUser = @idUser, " +
                "idContenu = @idContenu, " +
                "msg = @msg " +
                "isStatut = @isStatut " +
                "where id = @id";

            SqlCommand command = new SqlCommand(request, connection);
            command.Parameters.Add(new SqlParameter(" @idUser", c.IdUser));
            command.Parameters.Add(new SqlParameter("@idContenu", c.IdContenu));
            command.Parameters.Add(new SqlParameter("@img", c.Msg));
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
