using System.Collections.Generic;

namespace EntitiesLayer
{
    public class Stade : EntityObject
    {
        public string Nom { get; set; }
        public int NbPlaces { get; set; }
        public List<Caracteristique> Caracteristiques { get; set; }
        public string Planete { get; set; }
        public string Image { get; set; }

        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="id">Id du stade.</param>
        /// <param name="nbPlaces">Nombre de places du stade.</param>
        /// <param name="planete">Nom de la planète sur laquelle se situe le stade.</param>
        /// <param name="carac">Caractéritiques associées au stade.</param>
        /// <param name="imagePath">Chemin vers l'image du stade.</param>
        public Stade(int id, string nom, int nbPlaces, string planete, List<Caracteristique> carac, string imagePath) 
            : base(id)
        {
            Nom = nom;
            NbPlaces = nbPlaces;
            Planete = planete;
            Caracteristiques = carac;
            Image = imagePath;
        }

        public override string ToString()
        {
            return Nom + " (" + Planete + ")";
        }
    }
}
