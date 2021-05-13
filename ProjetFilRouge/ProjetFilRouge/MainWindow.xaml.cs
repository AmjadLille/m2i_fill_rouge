using ProjetFilRouge.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjetFilRouge
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Valider_Click(object sender, RoutedEventArgs e)
        {
            string prenom = Prenom.Text;
            string nom = Nom.Text;
            string pseudo = NomUtilisateur.Text;
            string mail = Mail.Text;
            string confirmMail = ConfirmationMail.Text;
            string mdp = MdpUtilisateur.Text;

            if (prenom != "" || nom != "" || pseudo != "" || mail != "" || confirmMail != "" || mdp != "")
            {
                if (mail == confirmMail)
                {
                    if (mdp.Length >= 6)
                    {
                        User u = new User(0, nom, prenom, pseudo, mdp, mail, 1, false);
                        if(User.AjouterUser(u) != -1)
                        {
                            { MessageBox.Show("Un administrateur va valider votre inscription","Inscription validée",MessageBoxButton.OK,MessageBoxImage.Information); }
                        }
                    }
                    else
                    { MessageBox.Show("Le mot de passe doit contenir au moins 5 caractères et un chiffre","Erreur mot de pass",MessageBoxButton.OK,MessageBoxImage.Error); }
                }
                else
                { MessageBox.Show("La confirmation de mail doit être identique au mail","Erreur mail", MessageBoxButton.OK,MessageBoxImage.Error); }
            }
            else
            { MessageBox.Show("Merci de remplir tous les champs", "Champs vides", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void Connexion_Click(object sender, RoutedEventArgs e)
        {
            Connexion g = new Connexion();
            g.Show();
            Close();
        }

        private void Infos_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("C'est un blague, il n'y a aucune info","Parce tous les sites ont cet onglet",MessageBoxButton.OK,MessageBoxImage.Exclamation);
        }
    }
}
