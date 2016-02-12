using EntitiesLayer;
using MVVMBase.ViewModel;
using System.Collections.Generic;

namespace JediTournamentWPF.ViewsModels
{
    class MatchViewModel : ViewModelBase
    {
        // Model encapsulé dans le ViewModel
        private Match _match;

        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="matchModel">Match servant de modèle.</param>
        public MatchViewModel(Match matchModel)
        {
            _match = matchModel;
        }

        #region "Propriétés accessibles, mappables par la View"

        /// <summary>
        /// Accesseur sur le match modèle.
        /// </summary>
        public Match Match
        {
            get { return _match; }
            private set { _match = value; }
        }

        /// <summary>
        /// Propiété Jedi1 (mappable par la view).
        /// </summary>
        public Jedi Jedi1
        {
            get { return _match.Jedi1; }
            set
            {
                if (value == _match.Jedi1)
                    return;

                _match.Jedi1 = value;
                // Notifie le changement de la propriété
                base.OnPropertyChanged("Jedi1");
            }
        }

        /// <summary>
        /// Propiété Jedi2 (mappable par la view).
        /// </summary>
        public Jedi Jedi2
        {
            get { return _match.Jedi2; }
            set
            {
                if (value == _match.Jedi2)
                    return;

                _match.Jedi2 = value;
                // Notifie le changement de la propriété
                base.OnPropertyChanged("Jedi2");
            }
        }

        /// <summary>
        /// Propiété Stade (mappable par la view).
        /// </summary>
        public Stade Stade
        {
            get { return _match.Stade; }
            set
            {
                if (value == _match.Stade)
                    return;

                _match.Stade = value;
                // Notifie le changement de la propriété
                base.OnPropertyChanged("Stade");
            }
        }

        /// <summary>
        /// Propiété PhaseTournoi (mappable par la view).
        /// </summary>
        public EPhaseTournoi PhaseTournoi
        {
            get { return _match.PhaseTournoi; }
            set
            {
                if (value == PhaseTournoi)
                    return;

                _match.PhaseTournoi = value;
                // Notifie le changement de la propriété
                base.OnPropertyChanged("PhaseTournoi");
            }
        }

        #endregion
    }
}
