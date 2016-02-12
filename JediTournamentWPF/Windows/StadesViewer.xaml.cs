using System.Collections.Generic;
using System.Windows;
using EntitiesLayer;
using BusinessLayer;
using JediTournamentWPF.ViewsModels;
using System.Collections.ObjectModel;

namespace JediTournamentWPF.Windows
{
    /// <summary>
    /// Logique d'interaction pour StadesViewer.xaml
    /// </summary>
    public partial class StadesViewer : Window
    {
        private StadesViewModel m_ssvm;

        public StadesViewer()
        {
            InitializeComponent();

            m_ssvm = null;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Initialisation du stades view model
            m_ssvm = new StadesViewModel();

            // Abonnement
            m_ssvm.RemoveStade += M_ssvm_RemoveStade;

            stadesController.DataContext = m_ssvm;
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            // Applique les changements à la BDD
            JediTournamentManager manager = new JediTournamentManager();
            ObservableCollection<StadeViewModel> stades = m_ssvm.Stades;

            foreach (StadeViewModel svm in stades)
            {
                // Si < 0 alors ajout en BDD cas inexistant
                if (svm.Stade.ID < 0)
                {
                    manager.addStade(svm.Stade);
                }
                else
                {
                    manager.modStade(svm.Stade);
                }
            }

            // Désabonnement
            m_ssvm.RemoveStade -= M_ssvm_RemoveStade;
        }

        private void M_ssvm_RemoveStade(object sender, StadesViewModel.StadeEventArgs e)
        {
            JediTournamentManager manager = new JediTournamentManager();
            manager.delStade(e.Stade);
        }
    }
}
