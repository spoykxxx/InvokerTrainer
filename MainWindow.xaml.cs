using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace invoke
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public class Spell
    {
        public string ImagePath { get; set; }
        public char[] Combination { get; set; }
        public Spell(string imagePath, string combo)
        {
            ImagePath = imagePath;
            Combination = combo.ToUpper().ToCharArray();
        }
    }
    public partial class MainWindow : Window
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private bool _isTimerRunning = false;
        private List<Spell> _spells;
        private int _currentSpellIndex;
        private Random _random = new Random();
        private Spell _currentSpell;
        private List<char> _userInput = new();
        private Dictionary<Char, string> _orbIcons = new()
        {
        { 'Q',"assets/orbs/quas.png" },
        { 'W',"assets/orbs/wex.png" },
        { 'E',"assets/orbs/exort.png" },
        };
        public MainWindow()
        {   // Привет! Если у тебя есть советы, предложения по изменению моего кода, то напиши мне в телеграм: @spoykxxx
            // Hi! If you have any tips, suggestions for changing my code, then write to me in telegram: @spoykxxx
            InitializeComponent();
            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            HelpWindow helpWindow = new HelpWindow();
            helpWindow.Owner = this;
            helpWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            helpWindow.Show();
        }

        private void GitHubLink_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://github.com/spoykxxx")
            {
                UseShellExecute = true
            });
        }

        private void DiscordLink_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://discord.gg/BMuetKEKtr")
            {
                UseShellExecute = true
            });
        }

        private void LoadSpells()
        {
            _spells = new List<Spell>
                {
                    new Spell("assets/spells/alacrity.png", "WWE"),
                    new Spell("assets/spells/e.m.p.png", "WWW"),
                    new Spell("assets/spells/ghost_walk.png", "QQW"),
                    new Spell("assets/spells/chaos_meteor.png", "WEE"),
                    new Spell("assets/spells/cold_snap.png", "QQQ"),
                    new Spell("assets/spells/forge_spirit.png", "QEE"),
                    new Spell("assets/spells/tornado.png", "QWW"),
                    new Spell("assets/spells/ice_wall.png", "QQE"),
                    new Spell("assets/spells/sun_strike.png", "EEE"),
                    new Spell("assets/spells/deafening_blast.png", "QWE")
               };
            _spells = _spells.OrderBy(x => _random.Next()).ToList();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (!_isTimerRunning) return;

            if (e.Key == Key.Q || e.Key == Key.W || e.Key == Key.E)
            {
                if (_userInput.Count < 3)
                {
                    char orb = e.Key.ToString()[0];
                    _userInput.Add(orb);
                    UpdateOrbImages();
                }
            }
            else if (e.Key == Key.R)
            {
                if (_userInput.Count == 3)
                {
                    ValidateCombo();
                }
            }
        }

        private void UpdateOrbImages()
        {
            var orbImages = new List<Image> { Orb1, Orb2, Orb3 };
            for (int i = 0; i < 3; i++)
            {
                if (i < _userInput.Count)
                {
                    if (_orbIcons.TryGetValue(_userInput[i], out string orbPath))
                    {
                        orbImages[i].Source = new BitmapImage(new Uri(orbPath, UriKind.Relative));
                    }
                }
                else
                {
                    orbImages[i].Source = null;
                }
            }
            if (_isTimerRunning && _userInput.Count == 3)
            {
                InvokeImage.Source = new BitmapImage(new Uri("assets/orbs/invoke.png", UriKind.Relative));
            }
            else
            {
                InvokeImage.Source = null;
            }
        }

        private void ValidateCombo()
        {
            bool isCorrect = _userInput.SequenceEqual(_currentSpell.Combination);
            if (isCorrect)
            {
                _currentSpellIndex++;

                if (_currentSpellIndex < _spells.Count)
                {
                    _currentSpell = _spells[_currentSpellIndex];
                    SpellImage.Source = new BitmapImage(new Uri(_currentSpell.ImagePath, UriKind.Relative));
                    _userInput.Clear();
                    UpdateOrbImages();
                }
                else
                {
                    _stopwatch.Stop();
                    _isTimerRunning = false;
                    TimerText.Text = $"u did it in {TimerText.Text}";
                }
            }
            else
            {
                _userInput.Clear();
                UpdateOrbImages();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(this);
        }
        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void HideButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_isTimerRunning)
            {
                StartButton.Visibility = Visibility.Collapsed;

                LoadSpells();

                _currentSpellIndex = 0;
                _currentSpell = _spells[_currentSpellIndex];

                SpellImage.Source = new BitmapImage(new Uri(_currentSpell.ImagePath, UriKind.Relative));

                Orb1.Source = null;
                Orb2.Source = null;
                Orb3.Source = null;

                _stopwatch.Restart();
                _isTimerRunning = true;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            TimeSpan elapsed = _stopwatch.Elapsed;
            TimerText.Text = elapsed.ToString(@"mm\:ss\:ff");
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            if (_isTimerRunning)
            {
                TimeSpan elapsed = _stopwatch.Elapsed;
                TimerText.Text = elapsed.ToString(@"mm\:ss\:fff");
            }
        }
    }
}