using System.Windows;

namespace DotNETWeeklyPublisher.Views
{
    using ViewModels;
    /// <summary>
    /// Interaction logic for EpisodeIntroduceView.xaml
    /// </summary>
    public partial class EpisodeIntroduceView : Window
    {

        public EpisodeIntroduceView(EpisodeIntroduceViewModel vm)
        {
            InitializeComponent();
            this.DataContext = vm;
        }
    }
}
