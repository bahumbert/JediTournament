using System.Windows.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MVVMBase.ViewModel;
using EntitiesLayer;
using System;

namespace JediTournamentWPF.ViewsModels
{
    public class CaracsModifViewModel : ViewModelBase
    {
        /// <summary>
        /// Modèle encapsulé dans le ViewModel, collection de caractéristiques view model.
        /// </summary>
        private ObservableCollection<CaracViewModel> _availablesCaracs;
        /// <summary>
        /// Item sélectionné liste caractéristiques disponibles.
        /// </summary>
        private CaracViewModel _selectedAvailableItem;

        /// <summary>
        /// Modèle encapsulé dans le ViewModel, collection de caractéristiques view model.
        /// </summary>
        private ObservableCollection<CaracViewModel> _currentCaracs;
        /// <summary>
        /// Item sélectionné liste caractéristiques déjà utilisées.
        /// </summary>
        private CaracViewModel _selectedCurrentItem;

        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="caracsAvailableModel">Liste de caractéristiques disponibles contenus dans le modèle.</param>
        /// <param name="caracsModel">Liste de caractéristiques possédées.</param>
        public CaracsModifViewModel(List<Caracteristique> caracsAvailableModel, List<Caracteristique> caracsModel)
        {
            // Initialise les collections du modèle
            _availablesCaracs = new ObservableCollection<CaracViewModel>();
            _currentCaracs = new ObservableCollection<CaracViewModel>();

            if (caracsModel != null)
            {
                // Liste des caracs déjà utilisées
                foreach (Caracteristique carac in caracsModel)
                {
                    _currentCaracs.Add(new CaracViewModel(carac));

                    int index;
                    if ((index = caracsAvailableModel.FindIndex(c => c.ID == carac.ID)) > -1)
                    {
                        caracsAvailableModel.RemoveAt(index);
                    }
                }
            }

            // Reste de la liste des compétences disponibles
            foreach (Caracteristique carac in caracsAvailableModel)
            {
                _availablesCaracs.Add(new CaracViewModel(carac));
            }


            // Auto sélectionne la première caractéristique disponible s'il y en a un
            if (_availablesCaracs.Count > 0)
            {
                _selectedAvailableItem = _availablesCaracs[0];
            }
            // Auto sélectionne la première caractéristique utilisée s'il y en a un
            if (_currentCaracs.Count > 0)
            {
                _selectedCurrentItem = _currentCaracs[0];
            }
        }

        #region "Propriétés accessibles, mappables par la View

        /// <summary>
        /// Propiété AvailableCaracs (mappable par la view).
        /// </summary>
        public ObservableCollection<CaracViewModel> AvailableCaracs
        {
            get { return _availablesCaracs; }
            private set
            {
                _availablesCaracs = value;
                OnPropertyChanged("AvailableCaracs");
            }
        }

        /// <summary>
        /// Propiété SelectedAvailableCarac (mappable par la view).
        /// </summary>
        public CaracViewModel SelectedAvailableCarac
        {
            get { return _selectedAvailableItem; }
            set
            {
                _selectedAvailableItem = value;
                OnPropertyChanged("SelectedAvailableCarac");
            }
        }

        /// <summary>
        /// Propiété CurrentCaracs (mappable par la view).
        /// </summary>
        public ObservableCollection<CaracViewModel> CurrentCaracs
        {
            get { return _currentCaracs; }
            private set
            {
                _currentCaracs = value;
                OnPropertyChanged("CurrentCaracs");
            }
        }

        /// <summary>
        /// Propiété SelectedCurrentCarac (mappable par la view).
        /// </summary>
        public CaracViewModel SelectedCurrentCarac
        {
            get { return _selectedCurrentItem; }
            set
            {
                _selectedCurrentItem = value;
                OnPropertyChanged("SelectedCurrentCarac");
            }
        }

        #endregion
        #region "Commandes du formulaire"

        /// <summary>
        /// Commande Ajout.
        /// </summary>
        private RelayCommand _addCommand;
        /// <summary>
        /// Commande Remove.
        /// </summary
        private RelayCommand _removeCommand;

        /// <summary>
        /// Commande Ajout de caractéristique.
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
        /// Définit si on est autorisé à appeler la commande ajout caractéristique.
        /// </summary>
        /// <returns>True si on peut effectivement ajouter une caractéristique, sinon false.</returns>
        private bool CanAdd()
        {
            return (this._selectedAvailableItem != null);
        }

        /// <summary>
        /// Ajoute une caractéristique aux caractéristiques déjà utilisées.
        /// </summary>
        private void Add()
        {
            // Ajoute la carac aux courante (supp des disponibles)
            _selectedCurrentItem = _selectedAvailableItem;
            _currentCaracs.Add(_selectedAvailableItem);
            _availablesCaracs.Remove(_selectedAvailableItem);

            // Met à jour la sélection
            if (_availablesCaracs.Count > 0)
            {
                _selectedAvailableItem = _availablesCaracs[0];
            }
            else
            {
                _selectedAvailableItem = null;
            }

            OnPropertyChanged("SelectedAvailableCarac");
            OnPropertyChanged("SelectedCurrentCarac");
        }

        /// <summary>
        /// Commande de suppression de la caractéristique sélectionnée.
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
            return (this._selectedCurrentItem != null);
        }

        /// <summary>
        /// Applique la suppression de la caractéristique courante.
        /// </summary>
        private void Remove()
        {
            // Ajoute la carac aux disponibles (supp des courantes)
            _selectedAvailableItem = _selectedCurrentItem;
            _availablesCaracs.Add(_selectedCurrentItem);
            _currentCaracs.Remove(_selectedCurrentItem);

            // Met à jour la sélection
            if (_currentCaracs.Count > 0)
            {
                _selectedCurrentItem = _currentCaracs[0];
            }
            else
            {
                _selectedCurrentItem = null;
            }

            OnPropertyChanged("SelectedAvailableCarac");
            OnPropertyChanged("SelectedCurrentCarac");
        }

        #endregion
    }
}
