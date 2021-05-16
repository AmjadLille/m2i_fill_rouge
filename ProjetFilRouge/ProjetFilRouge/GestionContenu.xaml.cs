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
        int id;

        public int Id { get => id; set => id = value; }

        public GestionContenu(int id)
        {
            Id = id;
            InitializeComponent();
            AfficherListeContenu();
        }

        private void ParcourirContenu_Click(object sender, RoutedEventArgs e)
        {
            int s = 0;
            if (StatutActif.IsChecked == true)
            { s = 2; }
            if (StatutInactif.IsChecked == true)
            { s = 3; }

            string i = "";
            if (Img.IsChecked == true)
            { i = "*"; }
            if (NoImg.IsChecked == true)
            {}

            string l = "";
            if (Link.IsChecked == true)
            { l = "*"; }
            if (NoLink.IsChecked == true)
            { }

            List<Contenu> liste = Contenu.ContenuRecherche(-1,TitreContenu.Text, -1, -1, -1, l, i, s);
            contenus = liste;
            ListeViewContact.ItemsSource = contenus;
            StatutActif.IsChecked = false;
            StatutInactif.IsChecked = false;
        }

        private void AfficherListeContenu()
        {
            List<Contenu> liste = Contenu.ContenuRecherche(-1,"",-1,-1,-1,"","",0);
            contenus = liste;
            ListeViewContact.ItemsSource = contenus;
        }

        private void AjouterContenu_Click(object sender, RoutedEventArgs e)
        {
            if (TitreContenu.Text != "")
            {
                if (ImageContenu.Text != "" || LienContenu.Text != "" )
                {
                    MessageBoxResult result = MessageBox.Show("Merci de confirmer l'ajout : "+TitreContenu.Text,"Confirmer",MessageBoxButton.YesNo,MessageBoxImage.Warning);
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            Contenu c = new Contenu(0, TitreContenu.Text, Id, -1, -1, LienContenu.Text, ImageContenu.Text, 2);
                            if (Contenu.AjouterContenu(c) != -1)
                            { MessageBox.Show("Contenu ajouté", "Ajout fait", MessageBoxButton.OK, MessageBoxImage.Information); }
                            break;
                        case MessageBoxResult.No:
                            Close();
                            break;
                        default:
                            break;
                    }
                    TitreContenu.Clear();
                    ImageContenu.Clear();
                    LienContenu.Clear();
                    StatutActif.IsChecked = false; 
                    StatutInactif.IsChecked = false; 
                }
                else
                { MessageBox.Show("Vous n'avez fournit aucun fichier","Aucun fichier fournit",MessageBoxButton.OK,MessageBoxImage.Error); }
            }
            else
            { MessageBox.Show("Vous devez donner un nom à votre fichier","Erreur titre",MessageBoxButton.OK,MessageBoxImage.Error); }
        }

        private void ModifierContenu_Click(object sender, RoutedEventArgs e)
        {
            if (ListeViewContact.SelectedItem != null)
            {
                Contenu c = (Contenu)ListeViewContact.SelectedItem;
                //var radios = radioBtnMod.Children.OfType<RadioButton>();
                //RadioButton checkedRadio = radios.FirstOrDefault(rb => rb.GroupName == "TitreMod" && (string)rb.Content == p.Titre);
                //checkedRadio.IsChecked = true;
                TitreContenu.Text = c.Titre;
                LienContenu.Text = c.Link;
                ImageContenu.Text = c.Img;
                IdContenu.Text = Convert.ToString(c.Id);
                OwnerCanal.Text = c.PseudoOwnerCanal;
                OwnerContenu.Text = c.Pseudo;
                IdCommentaire.Text = Convert.ToString(c.IdCommentaires);
                if (c.IsStatut == 2)
                {StatutActif.IsChecked = true;}
                if (c.IsStatut == 3)
                { StatutInactif.IsChecked = true; }

                ParcourirContenu.Visibility = Visibility.Collapsed;
                AjouterContenu.Visibility = Visibility.Collapsed;
                ModifierContenu.Visibility = Visibility.Collapsed;
                ConfirmerModificationContenu.Visibility = Visibility.Visible;
                AnnulerModification.Visibility = Visibility.Visible;
            }
            else
                MessageBox.Show("Veuillez séléctionner une entrée de la liste", "Erreur de séléction...", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void ConfirmerModificationContenu_Click(object sender, RoutedEventArgs e)
        {
            List<User> ownerCanal = User.UserRecherche(-1, "", "", OwnerCanal.Text, "",-1,-1);
            List<User> ownerContenu = User.UserRecherche(-1, "", "", OwnerContenu.Text, "",-1,-1);

            User idOwnerCanal = ownerCanal.Find(user => user.Pseudo == OwnerCanal.Text);
            User idOwnerContenu = ownerContenu.Find(user => user.Pseudo == OwnerContenu.Text);

            Contenu c = new Contenu();
            c.Id = Convert.ToInt32(IdContenu.Text);
            c.Titre = TitreContenu.Text;
            c.IdUser = idOwnerContenu.Id;
            c.IdCana = idOwnerCanal.Id;
            c.IdCommentaires = Convert.ToInt32(IdCommentaire.Text);
            c.Link = LienContenu.Text;
            c.Img = ImageContenu.Text;
            if (StatutActif.IsChecked == true)
            {c.IsStatut = 2;}
            if (StatutInactif.IsChecked == true)
            {c.IsStatut = 3;}

            bool IsModify = Contenu.ModifierContenu(c);

            if (IsModify)
            {
                MessageBox.Show(c.Titre+" a été modifié","Contenu modifié",MessageBoxButton.OK,MessageBoxImage.Information);
                TitreContenu.Clear();
                LienContenu.Clear();
                ImageContenu.Clear();
                IdContenu.Text = "";
                OwnerCanal.Text = "";
                OwnerContenu.Text = "";
                IdCommentaire.Text = "";
                StatutActif.IsChecked = false;
                StatutInactif.IsChecked = false;

                ParcourirContenu.Visibility = Visibility.Visible;
                AjouterContenu.Visibility = Visibility.Visible;
                ModifierContenu.Visibility = Visibility.Visible;
                ConfirmerModificationContenu.Visibility = Visibility.Collapsed;
                AfficherListeContenu();
            }
            else
            {
                MessageBox.Show(c.Titre + " n'a pas été modifié", "Contenu non modifié", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AnnulerModification_Click(object sender, RoutedEventArgs e)
        {
            GestionContenu g = new GestionContenu(Id);
            g.Show();
            Close();
        }
    }
}
