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
            //AddToList();
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
                if (c.IsStatut == 2)
                {StatutActif.IsChecked = true;}
                if (c.IsStatut == 3)
                { StatutActif.IsChecked = false; }
            }
            else
                MessageBox.Show("Veuillez séléctionner une entrée de la liste", "Erreur de séléction...", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
