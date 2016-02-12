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
    /// Logique d'interaction pour TournoiChoixAutomatique.xaml
    /// </summary>
    public partial class TournoiChoixSelection : Window
    {
        public bool Auto { get; set; }
        private Tournoi t;

        public TournoiChoixSelection(bool auto, Tournoi t)
        {
            InitializeComponent();
            this.t = t;

            Auto = auto;

            List<Jedi> jedis = new List<Jedi>();
            JediTournamentManager manager = new JediTournamentManager();

            foreach(Match match in t.Matchs)
            {
                jedis.Add(match.Jedi1);
                jedis.Add(match.Jedi2);
            }

            cbJ1.ItemsSource = jedis;
            cbJ2.ItemsSource = jedis;
        }

        private void btnLancer_Click(object sender, RoutedEventArgs e)
        {
            if (cbJ1.SelectedItem != null && cbJ2.SelectedItem != null && ((Jedi)cbJ1.SelectedItem).ID != ((Jedi)cbJ2.SelectedItem).ID)
            {
                TournoiViewer tv = new TournoiViewer(t,Auto, ((Jedi)cbJ1.SelectedItem).ID, ((Jedi)cbJ2.SelectedItem).ID);
                tv.ShowDialog();
                Close();
            }
        }
    }
}
