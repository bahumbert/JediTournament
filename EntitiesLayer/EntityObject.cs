namespace EntitiesLayer
{
    /// <summary>
    /// Classe mère de toutes les classes.
    /// </summary>
    public abstract class EntityObject
    {
        /// <summary>
        /// ID de l'instance.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="id">ID associé à l'instance.</param>
        public EntityObject(int id)
        {
            ID = id;
        }

        public override bool Equals(object obj)
        {
            EntityObject e = obj as EntityObject;
            if (e != null)
                return ID == e.ID;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    
}
