using System;
using System.Windows.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MVVMBase.ViewModel;
using EntitiesLayer;
using BusinessLayer;

namespace JediTournamentWPF.ViewsModels
{
    class MatchsViewModel : ViewModelBase
    {
        /// <summary>
        /// Modèle encapsulé dans le ViewModel, collection de matchs view model.
        /// </summary>
        private ObservableCollection<MatchViewModel> _matches;
        /// <summary>
        /// Item sélectionné.
        /// </summary>
        private MatchViewModel _selectedItem;

        /// <summary>
        /// Liste des jedis disponibles.
        /// </summary>
        private List<Jedi> _jedis;
        /// <summary>
        /// Jedi sélectionné numéro 1.
        /// </summary>
        private Jedi _selectedJedi1;
        /// <summary>
        /// Jedi sélectionné numéro 2.
        /// </summary>
        private Jedi _selectedJedi2;
        /// <summary>
        /// Liste des jedis disponibles dans les combo box jedis.
        /// </summary>
        private List<Jedi> _jedisCombo;

        /// <summary>
        /// Liste des stades disponibles.
        /// </summary>
        private List<Stade> _stades;
        /// <summary>
        /// Stade sélectionné.
        /// </summary>
        private Stade _selectedStade;

        /// <summary>
        /// Constructeur.
        /// </summary>
        public MatchsViewModel()
        {
            JediTournamentManager manager = new JediTournamentManager();

            _jedis = manager.getAllJedis();
            _jedisCombo = new List<Jedi>(_jedis);
            _stades = manager.getAllStades();
            List<Match> matchsModel = manager.getAllMatchs();

            // Initialise la collection modèle
            _matches = new ObservableCollection<MatchViewModel>();
            foreach (Match m in matchsModel)
            {
                _matches.Add(new MatchViewModel(m));
            }

            // Auto sélectionne le premier match s'il y en a un
            if (_matches.Count > 0)
            {
                _selectedItem = _matches[0];
                _selectedJedi1 = _matches[0].Jedi1;
                _selectedJedi2 = _matches[0].Jedi2;
                _selectedStade = _matches[0].Stade;
            }
        }

        #region "Propriétés accessibles, mappables par la View

        /// <summary>
        /// Propiété Matchs (mappable par la view).
        /// </summary>
        public ObservableCollection<MatchViewModel> Matchs
        {
            get { return _matches; }
            private set
            {
                _matches = value;
                OnPropertyChanged("Matchs");
            }
        }

        /// <summary>
        /// Propiété SelectedItem (mappable par la view).
        /// </summary>
        public MatchViewModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                if (_selectedItem != null)
                {
                    _selectedJedi1 = _selectedItem.Jedi1;
                    _selectedJedi2 = _selectedItem.Jedi2;
                    _selectedStade = _selectedItem.Stade;
                }
                else
                {
                    _selectedJedi1 = null;
                    _selectedJedi2 = null;
                    _selectedStade = null;
                }
                
                OnPropertyChanged("SelectedItem");
                OnPropertyChanged("SelectedJedi1");
                OnPropertyChanged("SelectedJedi2");
                OnPropertyChanged("JedisCombo1");
                OnPropertyChanged("JedisCombo2");
                OnPropertyChanged("SelectedStade");
                OnPropertyChanged("StadesCombo");
            }
        }

        /// <summary>
        /// Propiété SelectedJedi1 (mappable par la view).
        /// </summary>
        public Jedi SelectedJedi1
        {
            get { return _selectedJedi1; }
            set
            {
                if (_selectedItem != null)
                {
                    _selectedJedi1 = value;
                    _selectedItem.Jedi1 = _selectedJedi1;

                    OnPropertyChanged("SelectedItem");
                    OnPropertyChanged("SelectedJedi1");
                }
            }
        }

        /// <summary>
        /// Propiété SelectedJedi2 (mappable par la view).
        /// </summary>
        public Jedi SelectedJedi2
        {
            get { return _selectedJedi2; }
            set
            {
                if (_selectedItem != null)
                {
                    _selectedJedi2 = value;
                    _selectedItem.Jedi2 = _selectedJedi2;

                    OnPropertyChanged("SelectedItem");
                    OnPropertyChanged("SelectedJedi2");
                }
            }
        }

        /// <summary>
        /// Propiété JedisCombo1 (mappable par la view).
        /// </summary>
        public List<Jedi> JedisCombo1
        {
            get { return _jedisCombo; }
            private set { return; }
        }

        /// <summary>
        /// Propiété JedisCombo2 (mappable par la view).
        /// </summary>
        public List<Jedi> JedisCombo2
        {
            get { return _jedisCombo; }
            private set { return; }
        }

        /// <summary>
        /// Propiété SelectedStade (mappable par la view).
        /// </summary>
        public Stade SelectedStade
        {
            get { return _selectedStade; }
            set
            {
                if (_selectedItem != null)
                {
                    _selectedStade = value;
                    _selectedItem.Stade = _selectedStade;

                    OnPropertyChanged("SelectedItem");
                    OnPropertyChanged("SelectedStade");
                }
            }
        }

        /// <summary>
        /// Propiété StadesCombo (mappable par la view).
        /// </summary>
        public List<Stade> StadesCombo
        {
            get { return _stades; }
            private set { return; }
        }

        #endregion
        #region "Commandes du formulaire"

        /// <summary>
        /// Commande Nouveau.
        /// </summary>
        private RelayCommand _newCommand;
        /// <summary>
        /// Id du prochain nouveau match créé.
        /// </summary>
        private static int _newNextId = -1;
        /// <summary>
        /// Commande Remove.
        /// </summary
        private RelayCommand _removeCommand;

        /// <summary>
        /// Commande Nouveau Match.
        /// </summary>
        public ICommand NewCommand
        {
            get
            {
                if (_newCommand == null)
                {
                    _newCommand = new RelayCommand(
                                                    () => this.New(),
                                                    () => this.CanNew()
                                                  );
                }
                return _newCommand;
            }
        }

        /// <summary>
        /// Définit si on est autorisé à appeler la commande nouveau match.
        /// </summary>
        /// <returns>True si on peut effectivement créer un nouveau match, sinon false.</returns>
        private bool CanNew()
        {
            return true;
        }

        /// <summary>
        /// Ajoute un nouveau match à la liste de matchs.
        /// </summary>
        private void New()
        {
            Match m = new Match(_newNextId--, null, null, EPhaseTournoi.QuartFinale, null);

            this.SelectedItem = new MatchViewModel(m);
            Matchs.Add(this.SelectedItem);
        }

        /// <summary>
        /// Commande de suppression du match courant.
        /// </summary>
        public ICommand RemoveCommand
        {
            get
            {
                if (_removeCommand == null)
                {
                    _removeCommand = new RelayCommand(
                                                        () => this.Remove(),
                                                        () => this.CanRemove()
                                                     );
                }
                return _removeCommand;
            }
        }

        /// <summary>
        /// Définit l'autorisation pour utiliser la commande de suppression.
        /// </summary>
        /// <returns>True si un élément de la liste est sélectionné, sinon faux.</returns>
        private bool CanRemove()
        {
            return (this.SelectedItem != null);
        }

        /// <summary>
        /// Applique la suppression du jedi courant.
        /// </summary>
        private void Remove()
        {
            if (this.SelectedItem != null)
            {
                if (this.SelectedItem.Match.ID >= 0)    // Notifie que pour les matchs déjà en BDD
                    OnRemoveMatch(this.SelectedItem.Match);

                Matchs.Remove(this.SelectedItem);
            }
        }

        #endregion
        #region "Evènements"

        // Event de suppression d'un match du conteneur
        public event EventHandler<MatchEventArgs> RemoveMatch;

        protected void OnRemoveMatch(Match m)
        {
            if (this.RemoveMatch != null)
                this.RemoveMatch(this, new MatchEventArgs(m));
        }

        /// <summary>
        /// Classe pour les arguments des évènements de match.
        /// </summary>
        public class MatchEventArgs : EventArgs
        {
            /// <summary>
            /// Match concerné par l'évènement.
            /// </summary>
            Match _match;

            public Match Match
            {
                get { return _match; }
                private set { _match = value; }
            }

            public MatchEventArgs(Match m)
            {
                _match = m;
            }
        }

        #endregion
    }
}
