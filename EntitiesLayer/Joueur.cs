namespace EntitiesLayer
{
    public class Joueur : EntityObject
    {
        public string Nom { get; set; }
        public int Score { get; set; }

        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="id">Id du joueur.</param>
        /// <param name="nom">Nom du joueur.</param>
        public Joueur(int id, string nom) 
            : base(id)
        {
            Nom = nom;
            Score = 0;
        }
    }
}
