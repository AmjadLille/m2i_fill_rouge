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
    /// Logique d'interaction pour GestionUtilisateur.xaml
    /// </summary>
    public partial class GestionUtilisateur : Window
    {
        public GestionUtilisateur()
        {
            InitializeComponent();
        }

        private void AjouterUtilisateur_Click(object sender, RoutedEventArgs e)
        {
            if (NomUtilisateur.Text != "")
            {
                if (PrenomUtilisateur.Text != "" || EmailUtilisateur.Text != "" || PseudoUtilisateur.Text != "" || MdpUtilisateur.Text != "")
                {
                    MessageBoxResult result = MessageBox.Show("Merci de confirmer l'ajout : " + NomUtilisateur.Text, "Confirmer", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            Utilisateur u = new Utilisateur(0, NomUtilisateur.Text, PrenomUtilisateur.Text, PseudoUtilisateur.Text, MdpUtilisateur.Text, EmailUtilisateur.Text, 2,false );
                            if (Contenu.AjouterContenu(c) != -1)
                            { MessageBox.Show("Utilisateur ajouté", "Ajout fait", MessageBoxButton.OK, MessageBoxImage.Information); }
                            break;
                        case MessageBoxResult.No:
                            Close();
                            break;
                        default:
                            break;
                    }
                    NomUtilisateur.Clear();
                    PrenomUtilisateur.Clear();
                    EmailUtilisateur.Clear();
                    PseudoUtilisateur.Clear();
                    MdpUtilisateur.Clear();

                    StatutActif.IsChecked = false;
                    StatutInactif.IsChecked = false;
                    AdminOui.IsChecked = false;
                    AdminNon.IsChecked = false;
                }
                else
                { MessageBox.Show("Aucun Utilisateur ajouté", "Utilisateur non ajouté", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
            else
            { MessageBox.Show("Vous devez donner un nom à votre Utilisateur", "Erreur titre", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void ModifierUtilisateur_Click(object sender, RoutedEventArgs e)
        { }

        private void ParcourirUtilisateur_Click(object sender, RoutedEventArgs e)
        { }
    }
}
