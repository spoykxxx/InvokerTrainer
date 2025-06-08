using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
namespace invoke
{
    /// <summary>
    /// Логика взаимодействия для HelpWindow.xaml
    /// </summary>
    public partial class HelpWindow : Window
    {
        public HelpWindow()
        {
            InitializeComponent();
            GenerateHelpContent();
        }
        private void GenerateHelpContent()
        {
            var spells = new List<SpellHelp>
            {
                new SpellHelp("cold_snap.png", new[] {"quas", "quas", "quas"}),
                new SpellHelp("e.m.p.png", new[] {"wex", "wex", "wex"}),
                new SpellHelp("sun_strike.png", new[] {"exort", "exort", "exort"}),
                new SpellHelp("tornado.png", new[] {"quas", "wex", "wex"}),
                new SpellHelp("chaos_meteor.png", new[] {"wex","exort","exort"}),
                new SpellHelp("ice_wall.png", new[] {"quas", "quas", "exort"}),
                new SpellHelp("forge_spirit.png", new[] {"quas", "exort", "exort"}),
                new SpellHelp("deafening_blast.png", new[] {"quas", "wex", "exort"}),
                new SpellHelp("alacrity.png", new[] {"wex", "wex", "exort"}),
                new SpellHelp("ghost_walk.png",new[]{"quas","quas","wex"})
            };
            foreach (var spell in spells)
            {
                StackPanel row = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    Margin = new Thickness(5)
                };

                Border spellBorder = new Border
                {
                    Background = new SolidColorBrush(Color.FromArgb(100, 0, 0, 0)),
                    CornerRadius = new CornerRadius(5),
                    Width = 60,
                    Height = 60,
                    Margin = new Thickness(5)
                };
                Image spellImage = new Image
                {
                    Source = new BitmapImage(new Uri($"pack://application:,,,/assets/spells/{spell.SpellIcon}")),
                    Width = 60,
                    Height = 60,
                    Margin = new Thickness(2)
                };

                spellBorder.Child = spellImage;

                row.Children.Add(spellBorder);

                foreach (var orb in spell.Orbs)
                {
                    Border orbBorder = new Border
                    {
                        Background = new SolidColorBrush(Color.FromArgb(100, 0, 0, 0)),
                        CornerRadius = new CornerRadius(5),
                        Width = 45,
                        Height = 45,
                        Margin = new Thickness(2)
                    };

                    Image orbImage = new Image
                    {
                        Source = new BitmapImage(new Uri($"pack://application:,,,/assets/orbs/{orb}.png")),
                        Width = 40,
                        Height = 40,
                        Margin = new Thickness(2)
                    };

                    orbBorder.Child = orbImage;
                    row.Children.Add(orbBorder);
                }
                HelpContentPanel.Children.Add(row);

            }
        }
        private void Alt_CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Alt_HideButton_Click(Object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Alt_TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
