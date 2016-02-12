using EntitiesLayer;
using EntitiesLayer.Core;
using JediTournamentWPF.ViewsModels;
using JediTournamentWPF.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;

namespace BusinessLayer.Core
{
    public class MatchPLayerGlobal
    {
        public TournoiViewer Tv { get; set; }
        private Dictionary<int, List<int>> IdMatchs { get; set; }
        private int i;
        private int j;
        private MatchPlayer mp;
        private List<Stade> stades;
        private Random rnd;
        private JediTournamentManager jtm;

        private int current = 4;

        public MatchPLayerGlobal(TournoiViewer tv)
        {

            Tv = tv;
            i = 0;
            j = 0;
            rnd = new Random();
            stades = new JediTournamentManager().getAllStades();
            jtm = new JediTournamentManager();

            IdMatchs = new Dictionary<int, List<int>>();
            List<int> l4 = new List<int>();
            l4.Add(0);
            l4.Add(1);
            IdMatchs.Add(4, l4);

            List<int> l5 = new List<int>();
            l5.Add(2);
            l5.Add(3);
            IdMatchs.Add(5, l5);

            List<int> l6 = new List<int>();
            l6.Add(4);
            l6.Add(5);
            IdMatchs.Add(6, l6);


        }

        public void play()
        {
            TournoiViewModel tvm = Tv.Tvm;
            mp = new MatchPlayer(tvm.Matchs[IdMatchs[current].ElementAt(i)], Tv.Auto, Tv.IdJ1, Tv.IdJ2, this);
        }

        public void playBis()
        {
            TournoiViewModel tvm = Tv.Tvm;
            BackgroundWorker bw = new BackgroundWorker();

            // this allows our worker to report progress during work
            bw.WorkerReportsProgress = true;

            // what to do in the background thread
            bw.DoWork += new DoWorkEventHandler(
            delegate (object o, DoWorkEventArgs args)
            {
                BackgroundWorker b = o as BackgroundWorker;

                // do some simple processing for 10 seconds
                Thread.Sleep(2000);

            });

            // what to do when progress changed (update the progress bar for example)
            bw.ProgressChanged += new ProgressChangedEventHandler(
            delegate (object o, ProgressChangedEventArgs args)
            {

            });

            // what to do when worker completes its task (notify the user)
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
            delegate (object o, RunWorkerCompletedEventArgs args)
            {
                if (i == 0)
                {

                    int st = rnd.Next(0, stades.Count);
                    Match match;
                    if (j != 2)
                    {
                        match = new Match(jtm.useAvailableIDMatch(), null, null, EPhaseTournoi.DemiFinale, tvm.Matchs.ElementAt(st).Stade);
                    }
                    else
                    {
                        match = new Match(jtm.useAvailableIDMatch(), null, null, EPhaseTournoi.Finale, tvm.Matchs.ElementAt(st).Stade);
                    }

                    tvm.Matchs.Add(match);
                    tvm.Matchs.ElementAt(current).Jedi1 = mp.Vainqueur;
                }
                else
                {
                    tvm.Matchs.ElementAt(current).Jedi2 = mp.Vainqueur;
                }

                i++;

                if (i == 2)
                {
                    i = 0;
                    j++;
                    current++;
                }

                Tv.TournoiController.DataContext = null;
                Tv.TournoiController.DataContext = tvm;


                if (current <= 6)
                {
                    play();
                }
                else
                {
                    mp = new MatchPlayer(tvm.Matchs[6], Tv.Auto, Tv.IdJ1, Tv.IdJ2, this);
                }
            });

            bw.RunWorkerAsync();
        }
    }
}
