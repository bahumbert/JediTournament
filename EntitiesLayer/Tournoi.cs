using System.Collections.Generic;

namespace EntitiesLayer
{
    public class Tournoi : EntityObject
    {
        public List<Match> Matchs { get; set; }
        public string Nom { get; set; }

        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="id">Id du tournoi.</param>
        /// <param name="nom">Nom du tournoi.</param>
        /// <param name="matchs">Match qui ont eu ou auront lieu pendant le tournoi.</param>
        public Tournoi(int id, string nom, List<Match> matchs) 
            : base(id)
        {
            Nom = nom;
            Matchs = matchs;
        }

        public override string ToString()
        {
            return Nom;
        }
    }
}
