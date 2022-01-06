using Microsoft.Extensions.Configuration;

namespace DotNETWeeklyPublisher.ViewModels
{
    using Commands;
    using Views;

    using System.Windows;

    public class MainWindowViewModel : NotificationObject
    {

        private readonly IConfiguration _configuration;

        private EpisodeIntroduceViewModel _episodeIntroduceVm;

        public MainWindowViewModel(IConfiguration configuration)
        {
            _configuration = configuration;
            EpisodePreviewText = "";
            OpenEpisodeCommand = new DelegateCommand();
            OpenEpisodeCommand.ExecuteAction = new System.Action<object>(OpenEpisode);
            _episodeIntroduceVm = new EpisodeIntroduceViewModel();

        }

        private string _episodePreviewText;

        public string EpisodePreviewText
        {
            get { return _episodePreviewText; }
            set { _episodePreviewText = value; this.RaisePropertyChanged("EpisodePreviewText"); }
        }

        public DelegateCommand OpenEpisodeCommand { get; set; }


        private void OpenEpisode(object parameter)
        {
            var view = new EpisodeIntroduceView(_episodeIntroduceVm);
            view.Show();
        }
    }
}
