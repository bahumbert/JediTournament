using System.Windows;
using System.Windows.Input;
using System.Windows.Forms;
using System.Reflection;
using BusinessLayer;
using JediTournamentWPF.Windows;
using System.IO;
using EntitiesLayer;

namespace JediTournamentWPF
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Business manager.
        /// </summary>
        private JediTournamentManager m_manager;

        /// <summary>
        /// Constructeur.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
           
            // Change le curseur de la souris
            string cursorPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Resources\\cursor.cur";
            if (File.Exists(cursorPath))
                Mouse.OverrideCursor  = new System.Windows.Input.Cursor(cursorPath);

            m_manager = new JediTournamentManager();
        }

        /// <summary>
        /// Gère le clic sur le bouton d'export des jedis.
        /// </summary>
        private void ExportJedi_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            // Configure la boite de dialogue
            dialog.InitialDirectory = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            dialog.DefaultExt = ".xml";
            dialog.Filter = "Fichier XML (*.xml)|*.xml";

            // Boite de dialogue de sauvegarde
            DialogResult result = dialog.ShowDialog();

            // Sauvegarde
            if (result.ToString() == "OK")
            {
                m_manager.serializeJedis(dialog.FileName);
            }
        }

        /// <summary>
        /// Gère l'affichage d'une popup MVVM, en cas de clic sur le bouton des jedis.
        /// </summary>
        private void buttonJedis_Click(object sender, RoutedEventArgs e)
        {
            JedisViewer jsv = new JedisViewer();
            jsv.ShowDialog();
        }

        /// <summary>
        /// Gère l'affichage d'une popup MVVM, en cas de clic sur le bouton des stades.
        /// </summary>
        private void buttonStades_Click(object sender, RoutedEventArgs e)
        {
            StadesViewer ssv = new StadesViewer();
            ssv.ShowDialog();
        }

        /// <summary>
        /// Gère l'affichage d'une popup MVVM, en cas de clic sur le bouton des matchs.
        /// </summary>
        private void buttonMatch_Click(object sender, RoutedEventArgs e)
        {
            MatchsViewer msv = new MatchsViewer();
            msv.ShowDialog();
        }

        /// <summary>
        /// Gère l'affichage d'une popup MVVM, en cas de clic sur le bouton des tournois.
        /// </summary>
        private void buttonTournois_Click(object sender, RoutedEventArgs e)
        {
            TournoisViewer tsv = new TournoisViewer();
            tsv.ShowDialog();
        }

        private void buttonLancer_Click(object sender, RoutedEventArgs e)
        {
            TournoiSelection tv = new TournoiSelection();
            tv.ShowDialog();
        }

        /// <summary>
        /// Gère la fermeture de la fenêtre, retour à la page de login.
        /// </summary>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Login win = new Login();
            win.Show();
        }
    }
}
