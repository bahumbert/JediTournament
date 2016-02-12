using EntitiesLayer;
using MVVMBase.ViewModel;
using System.Collections.Generic;

namespace JediTournamentWPF.ViewsModels
{
    public class TournoiViewModel : ViewModelBase
    {
        // Model encapsulé dans le ViewModel
        private Tournoi _tournoi;

        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="tournoiModel">Tournoi servant de modèle.</param>
        public TournoiViewModel(Tournoi tournoiModel)
        {
            _tournoi = tournoiModel;
        }

        #region "Propriétés accessibles, mappables par la View"

        /// <summary>
        /// Accesseur sur le tournoi modèle.
        /// </summary>
        public Tournoi Tournoi
        {
            get { return _tournoi; }
            private set { _tournoi = value; }
        }

        /// <summary>
        /// Propiété Nom (mappable par la view).
        /// </summary>
        public string Nom
        {
            get { return _tournoi.Nom; }
            set
            {
                if (value == _tournoi.Nom)
                    return;

                _tournoi.Nom = value;
                // Notifie le changement de la propriété
                base.OnPropertyChanged("Nom");
            }
        }

        /// <summary>
        /// Propiété Matchs (mappable par la view).
        /// </summary>
        public List<Match> Matchs
        {
            get { return _tournoi.Matchs; }
            set
            {
                if (value == _tournoi.Matchs)
                    return;

                _tournoi.Matchs = value;
                // Notifie le changement de la propriété
                base.OnPropertyChanged("Matchs");
            }
        }

        #endregion
    }
}
