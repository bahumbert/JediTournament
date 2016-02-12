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
using System.Windows.Shapes;
using BusinessLayer;
using System.Reflection;

namespace JediTournamentWPF {
    /// <summary>
    /// Logique d'interaction pour login.xaml
    /// </summary>
    public partial class Login : Window
    {
        /// <summary>
        /// Business manager.
        /// </summary>
        private JediTournamentManager m_manager;

        public Login()
        {
            InitializeComponent();

            m_manager = new JediTournamentManager();
        }

        /// <summary>
        /// Appelé lors du clic sur le bouton connexion.
        /// </summary>
        /// <param name="sender">Objet envoyant l'évènement.</param>
        /// <param name="e">Arguments</param>
        private void connexionButton_Click(object sender, RoutedEventArgs e)
        {
            // Vérifie le mot de passe entré
            if (m_manager.checkConnexionUser(loginForm.Text, passwdForm.Password))
            {
                MainWindow win = new MainWindow();
                win.Show();

                this.Close();
            }
            // Fail identification
            else
            {
                MessageBox.Show("Identification incorrecte",
                                Assembly.GetExecutingAssembly().GetName().Name,
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }
    }
}
