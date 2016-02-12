using BusinessLayer;
using BusinessLayer.Core;
using JediTournamentWPF.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace EntitiesLayer.Core
{
    public class MatchPlayer
    {
        private Random rnd;
        private int nb;
        private int res;
        private int[] choix;
        private MatchPLayerGlobal mpg;
        private bool joueur = false;

        /// <summary>
        /// Enum Chi fou mi.
        /// </summary>
        enum ChiFouMi
        {
            Pierre = 0,
            Feuille = 1,
            Ciseau = 2
        };

        /// <summary>
        /// Match à jouer.
        /// </summary>
        private Match _match;
        /// <summary>
        /// Jedi Vainqueur.
        /// </summary>
        private Jedi _vainqueur;

        public bool Auto { get; set; }
        public int IdJ1 { get; set; }
        public int IdJ2 { get; set; }

        /// <summary>
        /// Détermine le jedi gagant, null si le match est non joué.
        /// </summary>
        public Jedi Vainqueur
        {
            get { return _vainqueur; }
            private set { _vainqueur = value; }
        }

        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="m">Match à jouer.</param>
        public MatchPlayer(Match m, bool auto, int idJ1, int idJ2, MatchPLayerGlobal mpg)
        {
            _match = m;
            _vainqueur = null;

            Auto = auto;
            IdJ1 = idJ1;
            IdJ2 = idJ2;
            rnd = new Random();

            this.mpg = mpg;
            nb = 0;
            res = -1;
            choix = new int[2];

            if (Auto)
                playAuto();
            else
            {
                playJouable();
            }
        }

        /// <summary>
        /// Joue le match et détermine son gagant.
        /// </summary>
        private void playAuto()
        {
            Random r = new Random();

            while (_vainqueur == null)
            {
                ChiFouMi j1 = (ChiFouMi)r.Next(0, 3);
                ChiFouMi j2 = (ChiFouMi)r.Next(0, 3);

                // Résultat  1 si le jedi 1 gagne, 2 si le jedi 2 gagne, 0 en cas d'égalité.
                int winner = (3 + j1 - j2) % 3;

                if (winner == 1)
                    _vainqueur = _match.Jedi1;
                else if (winner == 2)
                    _vainqueur = _match.Jedi2;
            }

            if (_match.PhaseTournoi == EPhaseTournoi.Finale)
            {
                EndTournoi();
            }
            else
            {
                mpg.playBis();
            }
        }

        private void playJouable()
        {
            if (_match.Jedi1.ID == IdJ1)
            {
                //popup
                ScanPFC sf = new ScanPFC(this,0);
                sf.ShowDialog();
                nb++;
                joueur = true;
            }
            else
            {
                if (_match.Jedi1.ID == IdJ2)
                {
                    //popup
                    ScanPFC sf = new ScanPFC(this, 0);
                    sf.ShowDialog();
                    nb++;
                    joueur = true;
                }
                else
                {
                    //auto
                    choix[0] = AutoPFC();
                }
            }

            if (_match.Jedi2.ID == IdJ1)
            {
                //popup
                ScanPFC sf = new ScanPFC(this,1);
                sf.ShowDialog();
                nb++;
                joueur = true;
            }
            else
            {
                if (_match.Jedi2.ID == IdJ2)
                {
                    //popup
                    ScanPFC sf = new ScanPFC(this, 1);
                    sf.ShowDialog();
                    nb++;
                    joueur = true;
                }
                else
                {
                    //auto
                    choix[1] = AutoPFC();
                }
            }

            if (nb == 0)
                jouerJouable();
        }

        public int AutoPFC()
        {
            int i = rnd.Next(0, 3);
            return i;

        }

        public void EndTournoi()
        {
            GagnantPopUp gpu = new GagnantPopUp(_vainqueur.Nom);
            gpu.ShowDialog();
            JediTournamentManager jtm = new JediTournamentManager();
            jtm.modTournoi(mpg.Tv.Tvm.Tournoi);
        }

        public void jouerJouable()
        {
            res = check(choix[0], choix[1]);
            Console.WriteLine(_match.PhaseTournoi);

            if (res != -1)
            {
                if (res == 1)
                    _vainqueur = _match.Jedi1;
                else if (res == 2)
                    _vainqueur = _match.Jedi2;

                if (_match.PhaseTournoi == EPhaseTournoi.Finale)
                {
                    EndTournoi();
                }
                else
                {
                    mpg.playBis();
                }
            }
            else
            {
                nb = 0;
                res = -1;
                choix = new int[2];

                if(joueur)
                { 
                    MessageBox.Show("Vous avez fait une égalité !");
                }

                joueur = false;

                playJouable();
            }
        }

        public void setChoice(int c, int j)
        {
            choix[j] = c;
            nb--;

            if (nb == 0)
            {
                jouerJouable();
            }
        }

        private int check(int J1, int J2)
        {
            if (J1 == 0 && J2 == 1)
                return 2;

            if (J1 == 1 && J2 == 0)
                return 1;

            if (J1 == 0 && J2 == 2)
                return 1;

            if (J1 == 2 && J2 == 0)
                return 2;

            if (J1 == 2 && J2 == 1)
                return 1;

            if (J1 == 1 && J2 == 2)
                return 2;

           return -1;
        }
    }
}
