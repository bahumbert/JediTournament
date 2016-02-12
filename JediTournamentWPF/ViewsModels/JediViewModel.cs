using EntitiesLayer;
using MVVMBase.ViewModel;
using System.Collections.Generic;

namespace JediTournamentWPF.ViewsModels
{
    class JediViewModel : ViewModelBase
    {
        // Model encapsulé dans le ViewModel
        private Jedi _jedi;

        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="jediModel">Jedi servant de modèle.</param>
        public JediViewModel(Jedi jediModel)
        {
            _jedi = jediModel;
        }

        #region "Propriétés accessibles, mappables par la View"

        /// <summary>
        /// Accesseur sur le jedi modèle.
        /// </summary>
        public Jedi Jedi
        {
            get { return _jedi; }
            private set { _jedi = value; }
        }

        /// <summary>
        /// Propiété Nom (mappable par la view).
        /// </summary>
        public string Nom
        {
            get { return _jedi.Nom; }
            set
            {
                if (value == _jedi.Nom)
                    return;

                _jedi.Nom = value;
                // Notifie le changement de la propriété
                base.OnPropertyChanged("Nom");
            }
        }

        /// <summary>
        /// Propiété IsSith (mappable par la view).
        /// </summary>
        public bool IsSith
        {
            get { return _jedi.IsSith; }
            set
            {
                if (value == _jedi.IsSith)
                    return;

                _jedi.IsSith = value;
                // Notifie le changement des propriétés
                base.OnPropertyChanged("IsSith");
                base.OnPropertyChanged("JediState");
            }
        }

        /// <summary>
        /// Propiété JediState (mappable par la view).
        /// </summary>
        public string JediState
        {
            get { return _jedi.JediState; }
            private set { return; }
        }

        /// <summary>
        /// Propiété Image (mappable par la view).
        /// </summary>
        public string Image
        {
            get { return _jedi.Image; }
            set
            {
                if (value == _jedi.Image)
                    return;

                _jedi.Image = value;
                // Notifie le changement de la propriété
                base.OnPropertyChanged("Image");
            }
        }

        /// <summary>
        /// Propiété Caracteristiques (mappable par la view).
        /// </summary>
        public List<Caracteristique> Caracteristiques
        {
            get { return _jedi.Caracteristiques; }
            set
            {
                if (value == _jedi.Caracteristiques)
                    return;

                _jedi.Caracteristiques = value;
                // Notifie le changement de la propriété
                base.OnPropertyChanged("Caracteristiques");
            }
        }

        #endregion
    }
}
