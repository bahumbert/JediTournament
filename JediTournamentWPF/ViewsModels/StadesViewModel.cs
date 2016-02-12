using System;
using System.Windows.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MVVMBase.ViewModel;
using EntitiesLayer;
using System.Windows.Forms;
using JediTournamentWPF.Windows;
using BusinessLayer;

namespace JediTournamentWPF.ViewsModels
{
    class StadesViewModel : ViewModelBase
    {
        /// <summary>
        /// Modèle encapsulé dans le ViewModel, collection de stade view model.
        /// </summary>
        private ObservableCollection<StadeViewModel> _stades;
        /// <summary>
        /// Item sélectionné.
        /// </summary>
        private StadeViewModel _selectedItem;

        /// <summary>
        /// Constructeur.
        /// </summary>
        public StadesViewModel()
        {
            List<Stade> stadesModel = new JediTournamentManager().getAllStades();

            // Initialise la collection modèle
            _stades = new ObservableCollection<StadeViewModel>();
            foreach (Stade j in stadesModel)
            {
                _stades.Add(new StadeViewModel(j));
            }

            if (_stades.Count > 0)
                _selectedItem = _stades[0];
        }

        #region "Propriétés accessibles, mappables par la View

        /// <summary>
        /// Propiété Stades (mappable par la view).
        /// </summary>
        public ObservableCollection<StadeViewModel> Stades
        {
            get { return _stades; }
            private set
            {
                _stades = value;
                OnPropertyChanged("Stades");
            }
        }

        /// <summary>
        /// Propiété SelectedItem (mappable par la view).
        /// </summary>
        public StadeViewModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }

        #endregion
        #region "Commandes du formulaire"

        /// <summary>
        /// Commande Nouveau.
        /// </summary>
        private RelayCommand _newCommand;
        /// <summary>
        /// Id du prochain nouveau stade créé.
        /// </summary>
        private static int _newNextId = -1;
        /// <summary>
        /// Commande Remove.
        /// </summary
        private RelayCommand _removeCommand;
        /// <summary>
        /// Commande Parcourir.
        /// </summary
        private RelayCommand _browseCommand;
        /// <summary>
        /// Commande modification de caractéristiques.
        /// </summary
        private RelayCommand _modifyCaracsCommand;

        /// <summary>
        /// Commande Nouveau stade.
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
        /// Définit si on est autorisé à appeler la commande nouveau stade.
        /// </summary>
        /// <returns>True si on peut effectivement créer un nouveau stade, sinon false.</returns>
        private bool CanNew()
        {
            return true;
        }

        /// <summary>
        /// Ajoute un nouveau stade à la liste de stades.
        /// </summary>
        private void New()
        {
            Stade j = new Stade(_newNextId--, "<New>", 0, "<Planete>", null, "");

            this.SelectedItem = new StadeViewModel(j);
            Stades.Add(this.SelectedItem);
        }

        /// <summary>
        /// Commande de suppression du stade courant.
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
        /// Applique la suppression du stade courant.
        /// </summary>
        private void Remove()
        {
            if (this.SelectedItem != null)
            {
                if (this.SelectedItem.Stade.ID >= 0)    // Notifie que pour les stades déjà en BDD
                    OnRemoveStade(this.SelectedItem.Stade);

                Stades.Remove(this.SelectedItem);
            }
        }

        /// <summary>
        /// Commande de suppression du stade courant.
        /// </summary>
        public ICommand BrowseCommand
        {
            get
            {
                if (_browseCommand == null)
                {
                    _browseCommand = new RelayCommand(
                                                        () => this.Browse(),
                                                        () => this.CanBrowse()
                                                     );
                }
                return _browseCommand;
            }
        }

        /// <summary>
        /// Définit l'autorisation pour utiliser la commande parcourir.
        /// </summary>
        /// <returns>True si un élément de la liste est sélectionné, sinon faux.</returns>
        private bool CanBrowse()
        {
            return (this.SelectedItem != null);
        }

        /// <summary>
        /// Applique la suppression du stade courant.
        /// </summary>
        private void Browse()
        {
            if (this.SelectedItem != null)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Multiselect = false;
                openFileDialog.Filter = "Image PNG (*.png)|*.png|Image JPEG (*.jpeg)|*.jpeg|Image JPG (*.jpg)|*.jpg";
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                DialogResult res = openFileDialog.ShowDialog();
                if (res.ToString() == "OK")
                {
                    this.SelectedItem.Image = openFileDialog.FileName;
                }
            }
        }

        /// <summary>
        /// Commande pour la modification des caractéristiques du stade courant.
        /// </summary>
        public ICommand ModifyCaracsCommand
        {
            get
            {
                if (_modifyCaracsCommand == null)
                {
                    _modifyCaracsCommand = new RelayCommand(
                                                        () => this.ModifyCarac(),
                                                        () => this.CanModifyCarac()
                                                     );
                }
                return _modifyCaracsCommand;
            }
        }

        /// <summary>
        /// Définit l'autorisation pour utiliser la commande de modification des caractéristiques.
        /// </summary>
        /// <returns>True si un élément de la liste est sélectionné, sinon faux.</returns>
        private bool CanModifyCarac()
        {
            return (this.SelectedItem != null);
        }

        /// <summary>
        /// Applique la modification des caractéristiques au jedi courant.
        /// </summary>
        private void ModifyCarac()
        {
            if (this.SelectedItem != null)
            {
                CaracsViewer csv = new CaracsViewer(this.SelectedItem.Stade,
                                                    ETypeCaracteristique.Stade);

                // Abonnement à l'évènement de fermeture
                csv.CloseCaracsWindow += Csv_CloseCaracsWindow;

                csv.ShowDialog();

                // Désabonnement
                csv.CloseCaracsWindow -= Csv_CloseCaracsWindow;
            }
        }

        private void Csv_CloseCaracsWindow(object sender, CaracsViewer.CaracsModifEventArgs e)
        {
            // Affecte les nouvelles caras
            if (this._selectedItem != null)
                SelectedItem.Caracteristiques = e.NewCaracs;
        }

        #endregion
        #region "Evènements"

        // Event de suppression d'un stade du conteneur
        public event EventHandler<StadeEventArgs> RemoveStade;

        protected void OnRemoveStade(Stade s)
        {
            if (this.RemoveStade != null)
                this.RemoveStade(this, new StadeEventArgs(s));
        }

        /// <summary>
        /// Classe pour les arguments des évènements de stades.
        /// </summary>
        public class StadeEventArgs : EventArgs
        {
            /// <summary>
            /// Stade concerné par l'évènement.
            /// </summary>
            Stade _stade;

            public Stade Stade
            {
                get { return _stade; }
                private set { _stade = value; }
            }

            public StadeEventArgs(Stade s)
            {
                _stade = s;
            }
        }

        #endregion
    }
}