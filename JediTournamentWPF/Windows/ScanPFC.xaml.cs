using EntitiesLayer;
using EntitiesLayer.Core;
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
    /// Logique d'interaction pour ScanPFC.xaml
    /// </summary>
    public partial class ScanPFC : Window
    {
        private MatchPlayer mp;
        private int j;

        public ScanPFC(MatchPlayer mp,int j)
        {
            this.mp = mp;
            this.j = j;
            InitializeComponent();

            int joueur = j + 1;
            titre.Content = "Choix du joueur " + joueur + " :";
        }

        private void OnClickPierre(object sender, RoutedEventArgs e)
        {
            mp.setChoice(0, j);
            Close();
        }

        private void OnClickFeuille(object sender, RoutedEventArgs e)
        {
            mp.setChoice(1, j);
            Close();
        }

        private void OnClickCizeau(object sender, RoutedEventArgs e)
        {
            mp.setChoice(2,j);
            Close();
        }
    }
}
