using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace JediWebService
{
    [ServiceContract]
    public interface IService1
    {
        #region "Liés aux Jedis"
        [OperationContract]
        List<JediContract> GetJedis();

        [OperationContract]
        List<CaracteristiqueContract> GetCaracteristiquesByJedi(int id);

        [OperationContract]
        void AddJedi(string nom, bool isSith, CaracteristiqueContract force, CaracteristiqueContract defense, CaracteristiqueContract chance, CaracteristiqueContract sante);

        [OperationContract]
        void modJedi(int id, string nom, bool isSith, CaracteristiqueContract force, CaracteristiqueContract defense, CaracteristiqueContract chance, CaracteristiqueContract sante);

        [OperationContract]
        void delJedi(int id);
            #endregion
        #region "Liés aux Stades"
        [OperationContract]
        List<StadeContract> GetStades();

        [OperationContract]
        List<CaracteristiqueContract> GetCaracteristiquesByStade(int id);

        [OperationContract]
        void AddStade(int nbPlaces, string nom, string planete, CaracteristiqueContract force, CaracteristiqueContract defense, CaracteristiqueContract chance, CaracteristiqueContract sante);

        [OperationContract]
        void modStade(int id, int nbPlaces, string nom, string planete, CaracteristiqueContract force, CaracteristiqueContract defense, CaracteristiqueContract chance, CaracteristiqueContract sante);

        [OperationContract]
        void delStade(int id);
        #endregion
        #region "Liés aux Matchs"
        [OperationContract]
        List<MatchContract> GetMatchs();

        [OperationContract]
        void AddMatch(int idJediVainqueur, string jedic1, string jedic2, string stadec);

        [OperationContract]
        void modMatch(int id, int idJediVainqueur, string jedic1, string jedic2, string stadec, EPhaseTournoi phase);

        [OperationContract]
        void delMatch(int id);
        #endregion
        #region "Liés aux Tournois"
        [OperationContract]
        List<TournoiContract> GetTournois();

        [OperationContract]
        void AddTournoi(string nom, List<MatchContract> listMatchc);

        [OperationContract]
        void modTournoi(int id, string nom, List<MatchContract> listMatchc);

        [OperationContract]
        void delTournoi(int id);
        #endregion
        #region "Liés aux Caracteristiques"
        [OperationContract]
        List<CaracteristiqueContract> GetCaracteristiques();

        [OperationContract]
        List<CaracteristiqueContract> GetCaracteristiquesJediForce();
        [OperationContract]
        List<CaracteristiqueContract> GetCaracteristiquesJediDefense();
        [OperationContract]
        List<CaracteristiqueContract> GetCaracteristiquesJediChance();
        [OperationContract]
        List<CaracteristiqueContract> GetCaracteristiquesJediSante();

        [OperationContract]
        List<CaracteristiqueContract> GetCaracteristiquesStadeForce();
        [OperationContract]
        List<CaracteristiqueContract> GetCaracteristiquesStadeDefense();
        [OperationContract]
        List<CaracteristiqueContract> GetCaracteristiquesStadeChance();
        [OperationContract]
        List<CaracteristiqueContract> GetCaracteristiquesStadeSante();

        [OperationContract]
        void AddCarac(string nom, int valeur, EDefCaracteristique definition, ETypeCaracteristique type);

        [OperationContract]
        void modCarac(int id, string nom, int valeur, EDefCaracteristique definition, ETypeCaracteristique type);

        [OperationContract]
        void delCarac(int id);
        #endregion

    }
    [DataContract]
    public class JediContract
    {
        int id;
        string nom;
        List<CaracteristiqueContract> caracteristiques;
        bool isSith;

        public JediContract(int id, string nom, List<CaracteristiqueContract> caracteristiques, bool isSith)
        {
            this.id = id;
            this.nom = nom;
            this.caracteristiques = caracteristiques;
            this.isSith = isSith;
        }

        public JediContract(){}

        [DataMember]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public List<CaracteristiqueContract> Caracteristiques
        {
            get { return caracteristiques; }
            set { caracteristiques = value; }
        }
        [DataMember]
        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }

        [DataMember]
        public bool IsSith
        {
            get { return isSith; }
            set { isSith = value; }
        }
    }
    [DataContract]
    public class StadeContract
    {
        int id;
        int nbPlace;
        string nom;
        string planete;
        List<CaracteristiqueContract> caracs;

        public StadeContract() { }

        public StadeContract(int id, int nbPlace, string nom, string planete, List<CaracteristiqueContract> caracs)
        {
            this.id = id;
            this.nbPlace = nbPlace;
            this.nom = nom;
            this.planete = planete;
            this.caracs = caracs;
        }

        [DataMember]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public int NbPlace
        {
            get { return nbPlace; }
            set { nbPlace = value; }
        }

        [DataMember]
        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }

        [DataMember]
        public string Planete
        {
            get { return planete; }
            set { planete = value; }
        }

        [DataMember]
        public List<CaracteristiqueContract> Caracs
        {
            get { return caracs; }
            set { caracs = value; }
        }
    }
    [DataContract]
    public class MatchContract
    {
        int id;
        int idJediVainqueur;
        JediContract jedi1;
        JediContract jedi2;
        StadeContract stade;

        public MatchContract() { }

        public MatchContract(int id, int idJediVainqueur, JediContract jedi1, JediContract jedi2, StadeContract stade)
        {
            this.id = id;
            this.idJediVainqueur = idJediVainqueur;
            this.jedi1 = jedi1;
            this.jedi2 = jedi2;
            this.stade = stade;
        }

        [DataMember]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public int IdJediVainqueur
        {
            get { return idJediVainqueur; }
            set {idJediVainqueur = value; }
        }

        [DataMember]
        public JediContract Jedi1
        {
            get { return jedi1; }    
            set { jedi1 = value; }
        }

        [DataMember]
        public JediContract Jedi2
        {
            get { return jedi2; }
            set { jedi2 = value; }
        }

        [DataMember]
        public StadeContract Stade
        {
            get { return stade; }
            set { stade = value; }
        }
    }
    [DataContract]
    public class TournoiContract
    {
        int id;
        List<MatchContract> matchs;
        string nom;

        public TournoiContract() { }

        public TournoiContract(int id, List<MatchContract> matchs, string nom)
        {
            this.id = id;
            this.matchs = matchs;
            this.nom = nom;
        }

        [DataMember]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public List<MatchContract> Matchs
        {
            get { return matchs; }
            set { matchs = value; }
        }

        [DataMember]
        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }
    }
    [DataContract]
    public class CaracteristiqueContract
    {
        int id;
        string nom;
        EDefCaracteristique definition;
        ETypeCaracteristique type;
        int valeur;

        public CaracteristiqueContract(int id, string nom, EDefCaracteristique definition, ETypeCaracteristique type, int valeur)
        {
            this.id = id;
            this.nom = nom;
            this.definition = definition;
            this.type = type;
            this.valeur = valeur;
        }

        public CaracteristiqueContract()
        {

        }

        [DataMember]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }

        [DataMember]
        public EDefCaracteristique Definition
        {
            get { return definition ; }
            set { definition = value; }
        }

        [DataMember]
        public ETypeCaracteristique Type
        {
            get { return type; }
            set { type = value; }
        }

        [DataMember]
        public int Valeur
        {
            get { return valeur; }
            set { valeur = value; }
        }
    }
}
