using System.Windows;
using EntitiesLayer;
using BusinessLayer;
using JediTournamentWPF.ViewsModels;

namespace JediTournamentWPF.Windows
{
    /// <summary>
    /// Logique d'interaction pour JediViewer.xaml
    /// </summary>
    public partial class JediViewer : Window
    {
        private Jedi _source;

        /// <summary>
        /// Constructeur.
        /// </summary>
        public JediViewer() : this(null)
        {
        }

        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="source">Jedi source de l'affichage.</param>
        public JediViewer(Jedi source)
        {
            InitializeComponent();

            if (source == null)
            {
                // Récupération d'un jedi
                JediTournamentManager manager = new JediTournamentManager();
                _source = manager.getAllJedis()[0];
            }
            else
                _source = source;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Initialisation du jedi view Model
            JediViewModel jvm = new JediViewModel(_source);
            jediController.DataContext = jvm;
        }
    }
}
