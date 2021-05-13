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
    /// Logique d'interaction pour GestionContenu.xaml
    /// </summary>
    public partial class GestionContenu : Window
    {
        public GestionContenu()
        {
            InitializeComponent();
            AfficherListeContenu();
        }

        private void ParcourirContenu_Click(object sender, RoutedEventArgs e)
        {
            
            
        }
        private void AfficherListeContenu()
        {
           List<Contenu> liste = Contenu.ContenuRecherche(-1, "", -1, -1, -1, "", "", 0);
        }
    }
}
