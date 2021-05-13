using ProjetFilRouge.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Logique d'interaction pour GestionContenu.xaml
    /// </summary>
    public partial class GestionContenu : Window
    {
        private static List<Contenu> contenus;
        public GestionContenu()
        {
            InitializeComponent();
            AfficherListeContenu();
            //AddToList();
        }

        private void ParcourirContenu_Click(object sender, RoutedEventArgs e)
        {
            int s = 0;
            if (StatutActif.IsChecked == true)
            { s = 2; }
            if (StatutInactif.IsChecked == true)
            { s = 3; }

            List<Contenu> liste = Contenu.ContenuRecherche(-1,TitreContenu.Text, -1, -1, -1, "", "", s);
            contenus = liste;
            ListeViewContact.ItemsSource = contenus;
        }

        private void AfficherListeContenu()
        {
            List<Contenu> liste = Contenu.ContenuRecherche(-1,"",-1,-1,-1,"","",0);
            contenus = liste;
            ListeViewContact.ItemsSource = contenus;
        }
    }
}
