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
    /// Logique d'interaction pour Acceuil.xaml
    /// </summary>
    public partial class Acceuil : Window
    {

        int id;

        public int Id { get => id; set => id = value; }

        public Acceuil(int id)
        {
            InitializeComponent();
            int Id = id;
        }

        private void deconnexion_Click(object sender, RoutedEventArgs e) 
        {
            Close();
        }

        private void GestionContenu_Click(object sender, RoutedEventArgs e)
        {
            GestionContenu g = new GestionContenu();
            g.Show();
        }

        private void GestionUtilisateur_Click(object sender, RoutedEventArgs e)
        {
            GestionUtilisateur g = new GestionUtilisateur();
            g.Show();
        }

        private void AfficherNomAdmin(int id)
        {
            List<User> us = User.UserRecherche(id,"","","","");  
        }
    }
}
