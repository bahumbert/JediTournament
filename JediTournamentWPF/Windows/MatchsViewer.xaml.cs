using System.Windows;
using BusinessLayer;
using JediTournamentWPF.ViewsModels;
using System.Collections.ObjectModel;

namespace JediTournamentWPF.Windows
{
    /// <summary>
    /// Logique d'interaction pour MatchsViewer.xaml
    /// </summary>
    public partial class MatchsViewer : Window
    {
        private MatchsViewModel m_msvm;

        public MatchsViewer()
        {
            InitializeComponent();

            m_msvm = null;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Initialisation du matchs view model
            m_msvm = new MatchsViewModel();

            // Abonnement
            m_msvm.RemoveMatch += M_msvm_RemoveMatch; ;

            matchsController.DataContext = m_msvm;
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            // Applique les changements à la BDD
            JediTournamentManager manager = new JediTournamentManager();
            ObservableCollection<MatchViewModel> matchs = m_msvm.Matchs;

            foreach (MatchViewModel mvm in matchs)
            {
                // Test la validité de la saisie
                if (mvm.Jedi1 != null && mvm.Jedi2 != null && mvm.Stade != null)
                {
                    // Si < 0 alors ajout en BDD cas inexistant
                    if (mvm.Match.ID < 0)
                        manager.addMatch(mvm.Match);
                    else
                        manager.modMatch(mvm.Match);
                }
            }

            // Désabonnement
            m_msvm.RemoveMatch -= M_msvm_RemoveMatch;
        }

        private void M_msvm_RemoveMatch(object sender, MatchsViewModel.MatchEventArgs e)
        {
            JediTournamentManager manager = new JediTournamentManager();
            manager.delMatch(e.Match);
        }
    }
}
