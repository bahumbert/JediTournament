using EntitiesLayer;
using MVVMBase.ViewModel;
using System.Collections.Generic;

namespace JediTournamentWPF.ViewsModels
{
    class StadeViewModel : ViewModelBase
    {
        // Model encapsulé dans le ViewModel
        private Stade _stade;

        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="stadeModel">Stade servant de modèle.</param>
        public StadeViewModel(Stade stadeModel)
        {
            _stade = stadeModel;
        }

        #region "Propriétés accessibles, mappables par la View"

        /// <summary>
        /// Accesseur sur le stade modèle.
        /// </summary>
        public Stade Stade
        {
            get { return _stade; }
            private set { _stade = value; }
        }

        /// <summary>
        /// Propiété Nom (mappable par la view).
        /// </summary>
        public string Nom
        {
            get { return _stade.Nom; }
            set
            {
                if (value == _stade.Nom)
                    return;

                _stade.Nom = value;
                // Notifie le changement de la propriété
                base.OnPropertyChanged("Nom");
            }
        }

        /// <summary>
        /// Propiété NbPlaces (mappable par la view).
        /// </summary>
        public int NbPlaces
        {
            get { return _stade.NbPlaces; }
            set
            {
                if (value == _stade.NbPlaces)
                    return;

                _stade.NbPlaces = value;
                // Notifie le changement des propriétés
                base.OnPropertyChanged("NbPlaces");
                //base.OnPropertyChanged("JediState");
            }
        }

        /// <summary>
        /// Propiété Planete (mappable par la view).
        /// </summary>
        public string Planete
        {
            get { return _stade.Planete; }
            set
            {
                if (value == _stade.Planete)
                    return;

                _stade.Planete = value;
                // Notifie le changement des propriétés
                base.OnPropertyChanged("Planete");
            }
        }

        /// <summary>
        /// Propiété Image (mappable par la view).
        /// </summary>
        public string Image
        {
            get { return _stade.Image; }
            set
            {
                if (value == _stade.Image)
                    return;

                _stade.Image = value;
                // Notifie le changement de la propriété
                base.OnPropertyChanged("Image");
            }
        }

        /// <summary>
        /// Propiété Caracteristiques (mappable par la view).
        /// </summary>
        public List<Caracteristique> Caracteristiques
        {
            get { return _stade.Caracteristiques; }
            set
            {
                if (value == _stade.Caracteristiques)
                    return;

                _stade.Caracteristiques = value;
                // Notifie le changement de la propriété
                base.OnPropertyChanged("Caracteristiques");
            }
        }

        #endregion
    }
}
