using System;
using System.Collections.Generic;

namespace EntitiesLayer
{
    [Serializable]
    public class Jedi : EntityObject
    {
        public List<Caracteristique> Caracteristiques { get; set; }
        public bool IsSith { get; set; }
        public string JediState
        {
            get { return (IsSith ? "Sith" : "Jedi");  }
            set { JediState = value; }
        }
        public string Nom { get; set; }
        public string Image { get; set; }

        /// <summary>
        /// Constructeur par défaut.
        /// </summary>
        public Jedi()
            : base(0)
        {
            Nom = "Default Name";
            IsSith = false;
            Caracteristiques = null;
            Image = "";
        }

        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="id">ID du Jedi.</param>
        /// <param name="nom">Nom du jedi.</param>
        /// <param name="isSith">Booléen indiquant si c'est un sith ou non.</param>
        /// <param name="carac">Caractéristiques du jedi.</param>
        /// <param name="imagePath">Chemin vers l'image du jedi.</param>
        public Jedi(int id, string nom, bool isSith, List<Caracteristique> carac, string imagePath) 
            : base(id)
        {
            Nom = nom;
            IsSith = isSith;
            Caracteristiques = carac;
            Image = imagePath;
        }

        public override string ToString()
        {
            return Nom + " (" + JediState + ")";
        }
    }
}
