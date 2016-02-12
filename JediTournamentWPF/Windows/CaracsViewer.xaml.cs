using System.Windows;
using System.Collections.Generic;
using BusinessLayer;
using EntitiesLayer;
using JediTournamentWPF.ViewsModels;
using System;
using System.Collections.ObjectModel;

namespace JediTournamentWPF.Windows
{
    /// <summary>
    /// Logique d'interaction pour CaracsViewer.xaml
    /// </summary>
    public partial class CaracsViewer : Window
    {
        private CaracsModifViewModel m_csmvm;
        private EntityObject m_entity;
        private ETypeCaracteristique m_type;

        public CaracsViewer(EntityObject entity, ETypeCaracteristique type)
        {
            InitializeComponent();

            m_csmvm = null;
            m_entity = entity;
            m_type = type;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            JediTournamentManager manager = new JediTournamentManager();
            // Listes des caracs
            List<Caracteristique> availableCaracs;

            // Initialisation du caracs view model
            switch (m_type)
            {
                case ETypeCaracteristique.Jedi:
                    availableCaracs = manager.getAllJediCaracs();
                    m_csmvm = new CaracsModifViewModel(availableCaracs, ((Jedi)m_entity).Caracteristiques);
                    break;
                case ETypeCaracteristique.Stade:
                    availableCaracs = manager.getAllStadeCaracs();
                    m_csmvm = new CaracsModifViewModel(availableCaracs, ((Stade)m_entity).Caracteristiques);
                    break;
                default:
                    availableCaracs = manager.getAllJediCaracs();
                    m_csmvm = new CaracsModifViewModel(availableCaracs, ((Jedi)m_entity).Caracteristiques);
                    break;
            }

            caracsModifController.DataContext = m_csmvm;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            OnCloseCaracsWindows();
        }

        #region "Lié aux évènements lancés"

        // Event destiné à réclamer la fermeture du conteneur
        public event EventHandler<CaracsModifEventArgs> CloseCaracsWindow;

        protected void OnCloseCaracsWindows()
        {
            if (this.CloseCaracsWindow != null)
                this.CloseCaracsWindow(this, new CaracsModifEventArgs(m_csmvm));
        }

        /// <summary>
        /// Classe pour les arguments des évènements de fermeture de la fenêtre des caracs.
        /// </summary>
        public class CaracsModifEventArgs : EventArgs
        {
            /// <summary>
            /// Liste des caractéristiques à la fermeture de la fenêtre d'édition de celle-ci.
            /// </summary>
            List<Caracteristique> m_newCaracs;

            public List<Caracteristique> NewCaracs
            {
                get { return m_newCaracs; }
                private set { m_newCaracs = value; }
            }

            public CaracsModifEventArgs(CaracsModifViewModel vm)
            {
                m_newCaracs = new List<Caracteristique>();

                ObservableCollection<CaracViewModel> caracVms = vm.CurrentCaracs;
                foreach (CaracViewModel cvm in caracVms)
                {
                    m_newCaracs.Add(cvm.Caracteristique);
                }
            }
        }

        #endregion
    }
}
