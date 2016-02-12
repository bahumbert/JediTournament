using BusinessLayer;
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
    /// Logique d'interaction pour TournoiSelection.xaml
    /// </summary>
    public partial class TournoiSelection : Window
    {
        public TournoiSelection()
        {
            InitializeComponent();
            JediTournamentManager jtm = new JediTournamentManager();
            cbT.ItemsSource = jtm.getGoodTournois();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (cbT.SelectedItem != null)
            { 
                TournoiChoix tc = new TournoiChoix((Tournoi)cbT.SelectedItem);
                tc.ShowDialog();
                Close();
            }
        }
    }
}
