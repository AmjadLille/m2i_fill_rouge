using ProjetFilRouge.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProjetFilRouge
{
    /// <summary>
    /// Logique d'interaction pour Connexion.xaml
    /// </summary>
    public partial class Connexion : Window
    {
        public Connexion()
        {
            InitializeComponent();
        }

        public void Login_Click(object sender, RoutedEventArgs e)
        {
            string mdp = MdpUtilisateur.Text;
            string pseudo = Pseudo.Text;

            List<User> us = User.UserRecherche(-1,"","",pseudo,"");

            if (us.Count == 0)
            {
                MessageBox.Show("Aucun compte avec ce pseudo", "Compte introuvable", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                //Expression lambda recherche un user dans la liste us dont le pseudo est notre pseudo
                User admin = us.Find(user => user.Pseudo == pseudo);

                if (admin.IsAdmin)
                {
                    Acceuil g = new Acceuil(admin.Id);
                    g.Show();
                    Close();
                }
                else
                { MessageBox.Show("Vous n'êtes pas administrateur", "Compte admin requis", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
           
        }
    }
}
