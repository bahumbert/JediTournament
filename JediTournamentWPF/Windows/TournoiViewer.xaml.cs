using BusinessLayer;
using EntitiesLayer;
using JediTournamentWPF.ViewsModels;
using BusinessLayer.Core;
using System;
using System.Windows;
using System.Windows.Threading;

namespace JediTournamentWPF.Windows
{
    /// <summary>
    /// Logique d'interaction pour LancerTournoiViewer.xaml
    /// </summary>
    public partial class TournoiViewer : Window
    {
        public bool Auto { get; set; }
        public int IdJ1 { get; set; }
        public int IdJ2 { get; set; }
        public Tournoi tournoi;
   
        public TournoiViewModel Tvm { get; set; }
        public TournoiViewer(Tournoi t,bool auto, int idJ1, int idJ2)
        {
            this.tournoi = t;
            Auto = auto;
            IdJ1 = idJ1;
            IdJ2 = idJ2;
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            Tvm = new TournoiViewModel(tournoi);
            TournoiController.DataContext = Tvm;

            MatchPLayerGlobal mpg = new MatchPLayerGlobal(this);
            Dispatcher.BeginInvoke(new Action(mpg.play), DispatcherPriority.ContextIdle);
        } 
    }
}
