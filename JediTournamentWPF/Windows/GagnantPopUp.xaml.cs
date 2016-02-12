using BusinessLayer;
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

namespace JediTournamentWPF.Windows
{
    /// <summary>
    /// Logique d'interaction pour GagnantPopUp.xaml
    /// </summary>
    public partial class GagnantPopUp : Window
    {
        public GagnantPopUp(string vainqueur)
        {
            InitializeComponent();
            labelVainqueur.Content = "Le gagnant est : " + vainqueur;
        }

        private void btnEnd_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
