using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Timers;
using System.Windows;
using Game_Explorer.Class;

namespace Game_Explorer.Components
{
    /// <summary>
    /// Interaction logic for CircleProgress.xaml
    /// </summary>
    public sealed partial class CircleProgress : INotifyPropertyChanged
    {
        private double _value1;
        private double _value2;

        public double Value1
        {
            get => _value1;
            set
            {
                _value1 = value;
                OnPropertyChanged();
            }
        }

        public double Value2
        {
            get => _value2;
            set
            {
                _value2 = value;
                OnPropertyChanged();
            }
        }

        public CircleProgress()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void InitializeTimer()
        {
            var timer1 = new Timer(1200);
            timer1.Elapsed += (sender, args) => UpdateInformation();
            timer1.Start();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void UpdateInformation()
        {
            Value1 = SystemInfo.GetCpuUsage();

            Value2 = SystemInfo.GetUsedRamAsPercentage();
            Ram.Text = $"{SystemInfo.GetUsedRam()}Gib / {SystemInfo.GetTotalRam()}Gib";
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateInformation();
            Ram.Text = $"{SystemInfo.GetUsedRam()}Gib / {SystemInfo.GetTotalRam()}Gib";
            CpuModel.Text = SystemInfo.GetCpuModel();
            InitializeTimer();
        }
    }
}