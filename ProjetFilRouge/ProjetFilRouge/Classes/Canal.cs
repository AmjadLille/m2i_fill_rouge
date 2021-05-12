using ProjetFilRouge.BDD;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ProjetFilRouge.Classes
{
    class Canal
    {
        int id;
        string theme;
        int idUser;
        int idComment;
        int isStatut;

        #region Construteurs
        public Canal()
        { }
        public Canal(int id, string theme, int idUser, int idComment, int isStatut)
        {
            this.id = id;
            this.theme = theme;
            this.idUser = idUser;
            this.idComment = idComment;
            this.isStatut = isStatut;
        }
        #endregion

        #region MyRegion
        public int Id { get => id; set => id = value; }
        public string Theme { get => theme; set => theme = value; }
        public int IdUser { get => idUser; set => idUser = value; }
        public int IdComment { get => idComment; set => idComment = value; }
        public int IsStatut { get => isStatut; set => isStatut = value; }
        #endregion

        public static List<Canal> CanalRecherche(int id, string theme,int idU, int idCo, int statut)
        {
            List<Canal> liste = new List<Canal>();

            SqlConnection connection = BDDconnexion.Connection;
            string request = "Select * from Canal";

            if (id != -1 || theme != "" || idU != -1 || idCo != -1 || statut != 0)
            {
                int i = 0;
                request += " where ";
                if (id != -1)
                {
                    if (i != 0)
                    { request += " and "; }
                    request += "id = @id ";
                }
                if (theme != "")
                {
                    if (i != 0)
                    { request += " and "; }
                    request += "theme = @theme ";
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
            command.Parameters.Add(new SqlParameter("@theme", theme));
            command.Parameters.Add(new SqlParameter("@isStatut", statut));
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Canal c = new Canal(
                    reader.GetInt32(0),//id               
                    reader.GetString(1),//theme                   
                    reader.GetInt32(2),//idUser                   
                    reader.GetInt32(3),//IdComment                              
                    reader.GetInt32(4)//isStatut                   
                    );
                liste.Add(c);
            }
            reader.Close();
            command.Dispose();
            connection.Close();
            return liste;
        }

        public static int AjouterContenu(Canal c)
        {
            SqlConnection connection = BDDconnexion.Connection;
            string request = "INSERT INTO Commentaire " +
                             "values(@theme,@idUser, @idComment,@isStatut)";

            SqlCommand command = new SqlCommand(request, connection);
            command.Parameters.Add(new SqlParameter("@theme", c.Theme));
            command.Parameters.Add(new SqlParameter("@idUser", c.IdUser));
            command.Parameters.Add(new SqlParameter("@idContenu", c.IdComment));
            command.Parameters.Add(new SqlParameter("@isStatut", c.IsStatut));
            connection.Open();

            int idContenu = (int)command.ExecuteScalar();
            command.Dispose();
            connection.Close();

            return idContenu;
        }

        public static bool ModifierCommentaire(Canal c)
        {
            SqlConnection connection = BDDconnexion.Connection;
            string request = "Update Canal set " +
                "idUser = @idUser, " +
                "idComment = @idComment, " +
                "theme = @theme " +
                "isStatut = @isStatut " +
                "where id = @id";

            SqlCommand command = new SqlCommand(request, connection);
            command.Parameters.Add(new SqlParameter("@theme", c.Theme));
            command.Parameters.Add(new SqlParameter("@idUser", c.IdUser));
            command.Parameters.Add(new SqlParameter("@idContenu", c.IdComment));
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
