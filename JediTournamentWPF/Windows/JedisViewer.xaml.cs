using System.Collections.Generic;
using System.Windows;
using EntitiesLayer;
using BusinessLayer;
using JediTournamentWPF.ViewsModels;
using System.Collections.ObjectModel;

namespace JediTournamentWPF.Windows
{
    /// <summary>
    /// Logique d'interaction pour JedisViewer.xaml
    /// </summary>
    public partial class JedisViewer : Window
    {
        private JedisViewModel m_jsvm;

        public JedisViewer()
        {
            InitializeComponent();

            m_jsvm = null;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Initialisation du jedis view model
            m_jsvm = new JedisViewModel();

            // Abonnement
            m_jsvm.RemoveJedi += M_jsvm_RemoveJedi;

            jedisController.DataContext = m_jsvm;
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            // Applique les changements à la BDD
            JediTournamentManager manager = new JediTournamentManager();
            ObservableCollection<JediViewModel> jedis = m_jsvm.Jedis;

            foreach (JediViewModel jvm in jedis)
            {
                // Si < 0 alors ajout en BDD cas inexistant
                if (jvm.Jedi.ID < 0)
                {
                    manager.addJedi(jvm.Jedi);
                }
                else
                {
                    manager.modJedi(jvm.Jedi);
                }
            }

            // Désabonnement
            m_jsvm.RemoveJedi -= M_jsvm_RemoveJedi;
        }

        private void M_jsvm_RemoveJedi(object sender, JedisViewModel.JediEventArgs e)
        {
            JediTournamentManager manager = new JediTournamentManager();
            manager.delJedi(e.Jedi);
        }

    }
}
