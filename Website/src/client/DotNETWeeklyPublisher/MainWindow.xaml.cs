using System.Windows;

namespace DotNETWeeklyPublisher
{
    using ViewModels;
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MainWindowViewModel vm)
        {
            this.DataContext= vm;
            InitializeComponent();
        }
    }
}
