using EntitiesLayer;
using MVVMBase.ViewModel;

namespace JediTournamentWPF.ViewsModels
{
    public class CaracViewModel : ViewModelBase
    {
        // Model encapsulé dans le ViewModel
        private Caracteristique _carac;

        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="caracModel">Caractéristique servant de modèle.</param>
        public CaracViewModel(Caracteristique caracModel)
        {
            _carac = caracModel;
        }

        #region "Propriétés accessibles, mappables par la View"

        /// <summary>
        /// Accesseur sur le match modèle.
        /// </summary>
        public Caracteristique Caracteristique
        {
            get { return _carac; }
            set { _carac = value; }
        }

        /// <summary>
        /// Propiété Nom (mappable par la view).
        /// </summary>
        public string Nom
        {
            get { return _carac.Nom; }
            set
            {
                if (value == _carac.Nom)
                    return;

                _carac.Nom = value;
                // Notifie le changement de la propriété
                base.OnPropertyChanged("Nom");
            }
        }

        /// <summary>
        /// Propiété Type (mappable par la view).
        /// </summary>
        public ETypeCaracteristique Type
        {
            get { return _carac.Type; }
            set
            {
                if (value == _carac.Type)
                    return;

                _carac.Type = value;
                // Notifie le changement de la propriété
                base.OnPropertyChanged("Type");
            }
        }

        /// <summary>
        /// Propiété Definition (mappable par la view).
        /// </summary>
        public EDefCaracteristique Definition
        {
            get { return _carac.Definition; }
            set
            {
                if (value == _carac.Definition)
                    return;

                _carac.Definition = value;
                // Notifie le changement de la propriété
                base.OnPropertyChanged("Definition");
            }
        }

        /// <summary>
        /// Propiété Value (mappable par la view).
        /// </summary>
        public int Valeur
        {
            get { return _carac.Valeur; }
            set
            {
                if (value == _carac.Valeur)
                    return;

                _carac.Valeur = value;
                // Notifie le changement de la propriété
                base.OnPropertyChanged("Valeur");
            }
        }

        #endregion

        /// <summary>
        /// Méthode d'affichage.
        /// </summary>
        /// <returns>Affichage d'une caractéristique : Nom Valeur (Definition).</returns>
        public override string ToString()
        {
            return Nom + " " + Valeur + " (" + Definition + ")";
        }
    }
}
