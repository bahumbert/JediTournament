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
        [OperationContract]
        List<JediContract> GetJedis();

        [OperationContract]
        List<StadeContract> GetStades();

        [OperationContract]
        List<MatchContract> GetMatchs();

        [OperationContract]
        List<TournoiContract> GetTournois();

        [OperationContract]
        List<CaracteristiqueContract> GetCaracteristiquesByJedi(JediContract j);

        [OperationContract]
        void AddJedi(string nom, bool isSith, CaracteristiqueContract force, CaracteristiqueContract defense, CaracteristiqueContract chance, CaracteristiqueContract sante);
    }
    [DataContract]
    public class JediContract
    {
        string nom;
        List<CaracteristiqueContract> caracteristiques;
        bool isSith;

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
        int nbPlace;
        string planete;
        [DataMember]
        public int NbPlace
        {
            get { return nbPlace; }
            set { nbPlace = value; }
        }

        [DataMember]
        public string Planete
        {
            get { return planete; }
            set { planete = value; }
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
        List<MatchContract> matchs;
        string nom;

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
        string nom;
        int valeur;

        [DataMember]
        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }

        [DataMember]
        public int Valeur
        {
            get { return valeur; }
            set { valeur = value; }
        }
    }
}
