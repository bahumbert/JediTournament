using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using MVVMBase.ViewModel;
using EntitiesLayer;
using BusinessLayer;
using System.Windows.Forms;
using JediTournamentWPF.Windows;
using System.Collections.Generic;

namespace JediTournamentWPF.ViewsModels
{
    class JedisViewModel : ViewModelBase
    {
        /// <summary>
        /// Modèle encapsulé dans le ViewModel, collection de jedi view model.
        /// </summary>
        private ObservableCollection<JediViewModel> _jedis;
        /// <summary>
        /// Item sélectionné.
        /// </summary>
        private JediViewModel _selectedItem;

        /// <summary>
        /// Constructeur.
        /// </summary>
        public JedisViewModel()
        {
            List<Jedi> jedisModel = new JediTournamentManager().getAllJedis();

            // Initialise la collection modèle
            _jedis = new ObservableCollection<JediViewModel>();
            foreach (Jedi j in jedisModel)
            {
                _jedis.Add(new JediViewModel(j));
            }

            if (_jedis.Count > 0)
                _selectedItem = _jedis[0];
        }

        #region "Propriétés accessibles, mappables par la View

        /// <summary>
        /// Propiété Jedis (mappable par la view).
        /// </summary>
        public ObservableCollection<JediViewModel> Jedis
        {
            get { return _jedis; }
            private set
            {
                _jedis = value;
                OnPropertyChanged("Jedis");
            }
        }

        /// <summary>
        /// Propiété SelectedItem (mappable par la view).
        /// </summary>
        public JediViewModel SelectedItem
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
        /// Commande Remove.
        /// </summary
        private RelayCommand _removeCommand;
        /// <summary>
        /// Id du prochain nouveau jedi créé.
        /// </summary>
        private static int _newNextId = -1;
        /// <summary>
        /// Commande Parcourir.
        /// </summary
        private RelayCommand _browseCommand;
        /// <summary>
        /// Commande Modification de caractéristiques.
        /// </summary
        private RelayCommand _modifyCaracCommand;

        /// <summary>
        /// Commande Nouveau jedi.
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
        /// Définit si on est autorisé à appeler la commande nouveau jedi.
        /// </summary>
        /// <returns>True si on peut effectivement créer un nouveau jedi, sinon false.</returns>
        private bool CanNew()
        {
            return true;
        }

        /// <summary>
        /// Ajoute un nouveau jedi à la liste de jedis.
        /// </summary>
        private void New()
        {
            Jedi j = new Jedi(_newNextId--, "<New>", false, null, "");

            this.SelectedItem = new JediViewModel(j);
            Jedis.Add(this.SelectedItem);
        }

        /// <summary>
        /// Commande de suppression du jedi courant.
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
                if (this.SelectedItem.Jedi.ID >= 0)    // Notifie que pour les jedis déjà en BDD
                    OnRemoveJedi(this.SelectedItem.Jedi);

                Jedis.Remove(this.SelectedItem);
            }
        }

        /// <summary>
        /// Commande de suppression du jedi courant.
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
        /// Ouvre un explorer de fichier pour choisi l'image à affecter au jedi courant.
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
        /// Commande pour la modification des caractéristiques au jedi courant.
        /// </summary>
        public ICommand ModifyCaracsCommand
        {
            get
            {
                if (_modifyCaracCommand == null)
                {
                    _modifyCaracCommand = new RelayCommand(
                                                        () => this.ModifyCarac(),
                                                        () => this.CanModifyCarac()
                                                     );
                }
                return _modifyCaracCommand;
            }
        }

        /// <summary>
        /// Définit l'autorisation pour utiliser la commande de modifications des caractéristiques.
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
                CaracsViewer csv = new CaracsViewer(this.SelectedItem.Jedi, 
                                                    ETypeCaracteristique.Jedi);

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

        // Event de suppression d'un jedi du conteneur
        public event EventHandler<JediEventArgs> RemoveJedi;

        protected void OnRemoveJedi(Jedi j)
        {
            if (this.RemoveJedi != null)
                this.RemoveJedi(this, new JediEventArgs(j));
        }

        /// <summary>
        /// Classe pour les arguments des évènements de jedi.
        /// </summary>
        public class JediEventArgs : EventArgs
        {
            /// <summary>
            /// Jedi concerné par l'évènement.
            /// </summary>
            Jedi _jedi;

            public Jedi Jedi
            {
                get { return _jedi; }
                private set { _jedi = value; }
            }

            public JediEventArgs(Jedi j)
            {
                _jedi = j;
            }
        }

        #endregion
    }
}
