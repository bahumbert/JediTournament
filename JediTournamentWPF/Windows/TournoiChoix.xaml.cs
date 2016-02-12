using EntitiesLayer;
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
    /// Logique d'interaction pour TournoiChoix.xaml
    /// </summary>
    public partial class TournoiChoix : Window
    {
        private Tournoi t;
        public TournoiChoix(Tournoi t)
        {
            InitializeComponent();
            this.t = t;
        }

        private void btnAutomatique_Click(object sender, RoutedEventArgs e)
        {
            TournoiChoixSelection tv = new TournoiChoixSelection(true, t);
            tv.ShowDialog();
            Close();
        }

        private void btnJouable_Click(object sender, RoutedEventArgs e)
        {

            TournoiChoixSelection tv = new TournoiChoixSelection(false, t);
            tv.ShowDialog();
            Close();
        }
    }
}
