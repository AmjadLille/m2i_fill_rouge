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
    /// Logique d'interaction pour GestionUtilisateur.xaml
    /// </summary>
    public partial class GestionUtilisateur : Window
    {
        private static List<User> utilisateurs;
        int id;

        public int Id { get => id; set => id = value; }
        public GestionUtilisateur(int id)
        {
            Id = id;
            InitializeComponent();
            AfficherListeContenu();
        }

        private void AjouterUtilisateur_Click(object sender, RoutedEventArgs e)
        {
            if (NomUtilisateur.Text != "" && PrenomUtilisateur.Text != "" && 
                EmailUtilisateur.Text != "" && PseudoUtilisateur.Text != "" && 
                MdpUtilisateur.Text != "" )
            {
                if (NomUtilisateur.Text != "" && PrenomUtilisateur.Text != "" && EmailUtilisateur.Text != "" && 
                    PseudoUtilisateur.Text != "" && MdpUtilisateur.Text != "")
                {
                    MessageBoxResult result = MessageBox.Show("Merci de confirmer l'ajout : " + NomUtilisateur.Text + " "+ PrenomUtilisateur , "Confirmer", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    
                    bool isAdmin = false;
                    if (AdminOui.IsChecked == true)
                    {isAdmin = true;}

                    int isActif = 3;
                    if (StatutActif.IsChecked == true)
                    { isActif = 2; }

                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            User u = new User(0, NomUtilisateur.Text, PrenomUtilisateur.Text, PseudoUtilisateur.Text, MdpUtilisateur.Text, EmailUtilisateur.Text, isActif,isAdmin );
                            if (User.AjouterUser(u) != -1)
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
            { MessageBox.Show("Merci de remplir les champs", "Information manquante", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void ModifierUtilisateur_Click(object sender, RoutedEventArgs e)
        {
            if (ListeViewContact.SelectedItem != null)
            {
                User u = (User)ListeViewContact.SelectedItem;
                //var radios = radioBtnMod.Children.OfType<RadioButton>();
                //RadioButton checkedRadio = radios.FirstOrDefault(rb => rb.GroupName == "TitreMod" && (string)rb.Content == p.Titre);
                //checkedRadio.IsChecked = true;
                NomUtilisateur.Text = u.Nom;
                PrenomUtilisateur.Text = u.Nom;
                PseudoUtilisateur.Text = u.Pseudo;
                IdUtilisateur.Text = Convert.ToString(u.Id);
                EmailUtilisateur.Text = u.Email;
                MdpUtilisateur.Text = u.Mdp;

                if (u.IsStatut == 1)
                { StatutEnCours.IsChecked = true; }
                if (u.IsStatut == 2)
                { StatutActif.IsChecked = true; }
                if (u.IsStatut == 3)
                { StatutInactif.IsChecked = true; }
                
                if (u.IsAdmin == true)
                { AdminOui.IsChecked = true; }
                if (u.IsAdmin == false)
                { AdminNon.IsChecked = true; }

                parcourirutilisateur.Visibility = Visibility.Collapsed;
                ajouterutilisateur.Visibility = Visibility.Collapsed;
                modifierutilisateur.Visibility = Visibility.Collapsed;
                ConfirmerModificationUtilisateur.Visibility = Visibility.Visible;
                AnnulerModification.Visibility = Visibility.Visible;
            }
            else
                MessageBox.Show("Veuillez séléctionner une entrée de la liste", "Erreur de séléction...", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void ParcourirUtilisateur_Click(object sender, RoutedEventArgs e)
        {
            if (NomUtilisateur.Text != "" ||
                PrenomUtilisateur.Text != "" ||
                EmailUtilisateur.Text != "" ||
                PseudoUtilisateur.Text != "" ||
                StatutActif.IsChecked == true ||
                StatutEnCours.IsChecked == true ||
                StatutInactif.IsChecked == true ||
                AdminNon.IsChecked == true ||
                AdminOui.IsChecked == true
                )
            {
                int isAdmin = -1;
                int isStatut = 0;

                if (AdminOui.IsChecked == true)
                {isAdmin = 1;}
                else if (AdminNon.IsChecked == true)
                { isAdmin = 0; }

                if (StatutActif.IsChecked == true)
                { isStatut = 2; }
                else if (StatutEnCours.IsChecked == true)
                { isStatut = 1; }
                else if (StatutInactif.IsChecked == true)
                { isStatut = 3; }

                utilisateurs = User.UserRecherche(-1,NomUtilisateur.Text,PrenomUtilisateur.Text,PseudoUtilisateur.Text,EmailUtilisateur.Text,isAdmin,isStatut);
            }
            else
            { utilisateurs = User.UserRecherche(-1, "", "", "", "", -1, 0); }
        }

        private void AfficherListeContenu()
        {
            utilisateurs = User.UserRecherche(-1, "", "", "", "",-1,0);
            ListeViewContact.ItemsSource = utilisateurs;
        }

        private void ConfirmerModificationUtilisateur_Click(object sender, RoutedEventArgs e)
        {
            int s = 0;
            bool a = false;

            if (AdminOui.IsChecked == true)
            { a = true; }

            if (StatutActif.IsChecked == true)
            { s = 2; }
            if (StatutEnCours.IsChecked == true)
            { s = 1; }
            if (StatutInactif.IsChecked == true)
            { s = 3; }

            User u = new User(Convert.ToInt32(IdUtilisateur.Text),
                              NomUtilisateur.Text,
                              PrenomUtilisateur.Text,
                              PseudoUtilisateur.Text,
                              MdpUtilisateur.Text,
                              EmailUtilisateur.Text,
                              s,
                              a
                              );

            MessageBoxResult result =  MessageBox.Show("Confirmez la modification de "+u.Nom+" "+u.Prenom,"Confirmer la modification",MessageBoxButton.YesNo,MessageBoxImage.Information);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    User.ModifierUser(u);
                    AfficherListeContenu();
                    break;
                case MessageBoxResult.No:
                    Close();
                    break;
                default:
                    break;
            }
        }

        private void AnnulerModification_Click(object sender, RoutedEventArgs e)
        {
            GestionUtilisateur g = new GestionUtilisateur(Id);
            g.Show();
            Close();
        }
    }
}
