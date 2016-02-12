using System.Windows;
using JediTournamentWPF.ViewsModels;
using BusinessLayer;
using System.Collections.ObjectModel;

namespace JediTournamentWPF.Windows
{
    /// <summary>
    /// Logique d'interaction pour TournoisViewer.xaml
    /// </summary>
    public partial class TournoisViewer : Window
    {
        private TournoisViewModel m_tsvm;

        public TournoisViewer()
        {
            InitializeComponent();

            m_tsvm = null;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Initialisation du tournoi view model
            m_tsvm = new TournoisViewModel();

            // Abonnement
            m_tsvm.RemoveTournoi += M_tsvm_RemoveTournoi; ;

            tournoisController.DataContext = m_tsvm;
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            // Applique les changements à la BDD
            JediTournamentManager manager = new JediTournamentManager();
            ObservableCollection<TournoiViewModel> tournois = m_tsvm.Tournois;

            foreach (TournoiViewModel tvm in tournois)
            {
                // Vérifie la validité de la saisie
                if (tvm.Tournoi.Matchs != null && tvm.Tournoi.Matchs.Count == 4)
                {
                    // Si < 0 alors ajout en BDD cas inexistant
                    if (tvm.Tournoi.ID < 0)
                    {
                        manager.addTournoi(tvm.Tournoi);
                    }
                    else
                    {
                        manager.modTournoi(tvm.Tournoi);
                    }
                }
            }

            // Désabonnement
            m_tsvm.RemoveTournoi -= M_tsvm_RemoveTournoi;
        }

        private void M_tsvm_RemoveTournoi(object sender, TournoisViewModel.TournoiEventArgs e)
        {
            JediTournamentManager manager = new JediTournamentManager();
            manager.delTournoi(e.Tournoi);
        }
    }
}
