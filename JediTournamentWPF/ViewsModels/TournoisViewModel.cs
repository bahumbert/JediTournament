using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MVVMBase.ViewModel;
using EntitiesLayer;
using BusinessLayer;
using System.Windows.Input;
using System;

namespace JediTournamentWPF.ViewsModels
{
    class TournoisViewModel : ViewModelBase
    {
        /// <summary>
        /// Modèle encapsulé dans le ViewModel, collection de tournoi.
        /// </summary>
        private ObservableCollection<TournoiViewModel> _tournois;
        /// <summary>
        /// Item sélectionné.
        /// </summary>
        private TournoiViewModel _selectedTournoi;

        /// <summary>
        /// Liste des matchs disponibles.
        /// </summary>
        private ObservableCollection<Match> _availableMatchs;
        /// <summary>
        /// Match disponible sélectionné.
        /// </summary>
        private Match _selectedAvailableMatch;

        /// <summary>
        /// Liste des matchs affectés.
        /// </summary>
        private ObservableCollection<Match> _affectedMatchs;
        /// <summary>
        /// Match affecté sélectionné.
        /// </summary>
        private Match _selectedAffectedMatch;

        /// <summary>
        /// Constructeur.
        /// </summary>
        public TournoisViewModel()
        {
            JediTournamentManager manager = new JediTournamentManager();

            _tournois = new ObservableCollection<TournoiViewModel>();
            List<Tournoi> tournoisModel = manager.getAllTournois();
            _availableMatchs = new ObservableCollection<Match>(manager.getAllQuartFinaleMatch());
            _affectedMatchs = new ObservableCollection<Match>();

            // Liste des tournois + forme la liste des matchs disponibles
            foreach (Tournoi t in tournoisModel)
            {
                _tournois.Add(new TournoiViewModel(t));

                if (t.Matchs != null)
                {
                    // Supprime les matchs qui ne sont plus disponibles
                    foreach (Match m in t.Matchs)
                    {
                        _availableMatchs.Remove(m);
                    }
                }
            }

            // Auto sélection du premier tournoi
            if (_tournois.Count > 0)
            {
                _selectedTournoi = _tournois[0];
                _affectedMatchs = new ObservableCollection<Match>(_selectedTournoi.Matchs);
            }

            if (_availableMatchs.Count > 0)
            {
                _selectedAvailableMatch = _availableMatchs[0];
            }

            if (_affectedMatchs.Count > 0)
            {
                _selectedAffectedMatch = _affectedMatchs[0];
            }
        }

        #region "Propriétés accessibles, mappables par la View

        /// <summary>
        /// Propiété Tournois (mappable par la view).
        /// </summary>
        public ObservableCollection<TournoiViewModel> Tournois
        {
            get { return _tournois; }
            private set
            {
                _tournois = value;
                OnPropertyChanged("Tournois");
            }
        }

        /// <summary>
        /// Propiété SelectedTournoi (mappable par la view).
        /// </summary>
        public TournoiViewModel SelectedTournoi
        {
            get { return _selectedTournoi; }
            set
            {
                _selectedTournoi = value;

                if (_selectedTournoi != null && _selectedTournoi.Matchs != null)
                {
                    AffectedMatchs = new ObservableCollection<Match>(_selectedTournoi.Matchs);
                }
                else
                {
                    AffectedMatchs = new ObservableCollection<Match>();
                }

                OnPropertyChanged("SelectedTournoi");
            }
        }

        /// <summary>
        /// Propiété AvailableMatchs (mappable par la view).
        /// </summary>
        public ObservableCollection<Match> AvailableMatchs
        {
            get { return _availableMatchs; }
            set
            {
                _availableMatchs = value;
                OnPropertyChanged("AvailableMatchs");
            }
        }

        /// <summary>
        /// Propiété SelectedAvailableMatch (mappable par la view).
        /// </summary>
        public Match SelectedAvailableMatch
        {
            get { return _selectedAvailableMatch; }
            set
            {
                _selectedAvailableMatch = value;
                OnPropertyChanged("SelectedAvailableMatch");
            }
        }

        /// <summary>
        /// Propiété AffectedMatchs (mappable par la view).
        /// </summary>
        public ObservableCollection<Match> AffectedMatchs
        {
            get { return _affectedMatchs; }
            set
            {
                _affectedMatchs = value;
                OnPropertyChanged("AffectedMatchs");
            }
        }

        /// <summary>
        /// Propiété SelectedAffectedMatch (mappable par la view).
        /// </summary>
        public Match SelectedAffectedMatch
        {
            get { return _selectedAffectedMatch; }
            set
            {
                _selectedAffectedMatch = value;
                OnPropertyChanged("SelectedAffectedMatch");
            }
        }

        #endregion
        #region "Commandes du formulaire"

        /// <summary>
        /// Commande Nouveau.
        /// </summary>
        private RelayCommand _newCommand;
        /// <summary>
        /// Id du prochain nouveau tournoi créé.
        /// </summary>
        private static int _newNextId = -1;
        /// <summary>
        /// Commande delete.
        /// </summary>
        private RelayCommand _delCommand;
        /// <summary>
        /// Commande Remove.
        /// </summary
        private RelayCommand _addCommand;
        /// <summary>
        /// Commande Remove.
        /// </summary
        private RelayCommand _removeCommand;

        /// <summary>
        /// Commande Nouveau Tournoi.
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
        /// Définit si on est autorisé à appeler la commande nouveau tournoi.
        /// </summary>
        /// <returns>True si on peut effectivement créer un nouveau tournoi, sinon false.</returns>
        private bool CanNew()
        {
            return true;
        }

        /// <summary>
        /// Ajoute un nouveau tounoi à la liste de tournoi.
        /// </summary>
        private void New()
        {
            Tournoi t = new Tournoi(_newNextId--, "<New>", null);

            this.SelectedTournoi = new TournoiViewModel(t);
            Tournois.Add(this.SelectedTournoi);
        }

        /// <summary>
        /// Commande Nouveau Tournoi.
        /// </summary>
        public ICommand DelCommand
        {
            get
            {
                if (_delCommand == null)
                {
                    _delCommand = new RelayCommand(
                                                    () => this.Del(),
                                                    () => this.CanDel()
                                                  );
                }
                return _delCommand;
            }
        }

        /// <summary>
        /// Définit si on est autorisé à appeler la commande delete tournoi.
        /// </summary>
        /// <returns>True si on peut effectivement supprimer un tournoi, sinon false.</returns>
        private bool CanDel()
        {
            return this._selectedTournoi != null;
        }

        /// <summary>
        /// Supprime un tounoi à la liste de tournoi.
        /// </summary>
        private void Del()
        {
            // Rend disponible les matchs affectés au tournoi
            if (_selectedTournoi.Matchs != null && _selectedTournoi.Matchs.Count > 0)
            {
                foreach (Match m in _affectedMatchs)
                {
                    AvailableMatchs.Add(m);
                }

                AffectedMatchs.Clear();
            }

            if (_selectedTournoi.Tournoi.ID >= 0)
                OnRemoveTournoi(this.SelectedTournoi.Tournoi);

            Tournois.Remove(_selectedTournoi);
        }

        /// <summary>
        /// Commande Ajout de match au tournoi courant.
        /// </summary>
        public ICommand AddCommand
        {
            get
            {
                if (_addCommand == null)
                {
                    _addCommand = new RelayCommand(
                                                    () => this.Add(),
                                                    () => this.CanAdd()
                                                  );
                }
                return _addCommand;
            }
        }

        /// <summary>
        /// Définit si on est autorisé à appeler la commande ajout de match.
        /// </summary>
        /// <returns>True si on peut effectivement ajouter un match, sinon false.</returns>
        private bool CanAdd()
        {
            return _selectedTournoi != null && _selectedAvailableMatch != null && _affectedMatchs.Count < 4;
        }

        /// <summary>
        /// Ajoute un match au tounoi courant.
        /// </summary>
        private void Add()
        {
            // Mise à jour de la sélection affectée
            SelectedAffectedMatch = _selectedAvailableMatch;

            // Ajoute le match aux affectés (supp des disponibles)
            AffectedMatchs.Add(_selectedAvailableMatch);
            AvailableMatchs.Remove(_selectedAvailableMatch);

            // Mise à jour du tournoi
            if (SelectedTournoi.Matchs == null)
                SelectedTournoi.Matchs = new List<Match>();
            SelectedTournoi.Matchs.Add(_selectedAffectedMatch);

            // Met à jour la sélection available
            if (_availableMatchs.Count > 0)
            {
                SelectedAvailableMatch = _availableMatchs[0];
            }
            else
            {
                SelectedAvailableMatch = null;
            }
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
            return _selectedTournoi != null && _selectedAffectedMatch != null && _selectedAffectedMatch.IdJediVainqueur == -1 /* Match non joué */;
        }

        /// <summary>
        /// Applique la suppression du jedi courant.
        /// </summary>
        private void Remove()
        {
            // Mise à jour de la sélection disponible
            SelectedAvailableMatch = SelectedAffectedMatch;

            // Ajoute le match aux availables (supp des disponibles)
            AvailableMatchs.Add(_selectedAffectedMatch);
            SelectedTournoi.Matchs.Remove(_selectedAffectedMatch);
            AffectedMatchs.Remove(_selectedAffectedMatch);

            // Met à jour la sélection affectée
            if (_affectedMatchs.Count > 0)
            {
                SelectedAffectedMatch = _affectedMatchs[0];
            }
            else
            {
                SelectedAffectedMatch = null;
            }
        }

        #endregion
        #region "Evènements"

        // Event de suppression d'un tournoi du conteneur
        public event EventHandler<TournoiEventArgs> RemoveTournoi;

        protected void OnRemoveTournoi(Tournoi t)
        {
            if (this.RemoveTournoi != null)
                this.RemoveTournoi(this, new TournoiEventArgs(t));
        }

        /// <summary>
        /// Classe pour les arguments des évènements de tournoi.
        /// </summary>
        public class TournoiEventArgs : EventArgs
        {
            /// <summary>
            /// Tournoi concerné par l'évènement.
            /// </summary>
            Tournoi _tournoi;

            public Tournoi Tournoi
            {
                get { return _tournoi; }
                private set { _tournoi = value; }
            }

            public TournoiEventArgs(Tournoi t)
            {
                _tournoi = t;
            }
        }

        #endregion
    }
}
