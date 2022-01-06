namespace DotNETWeeklyPublisher.ViewModels
{
    using Commands;

    using System.Windows;

    public class EpisodeIntroduceViewModel : NotificationObject
    {
        private string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; RaisePropertyChanged("Title"); }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set { _description = value; RaisePropertyChanged("Description"); }
        }

        public EpisodeIntroduceViewModel()
        {
            ClearCommand = new DelegateCommand();
            ClearCommand.ExecuteAction = (obj) =>
            {
                Title = string.Empty;
                Description = string.Empty;
            };
            CloseCommand = new DelegateCommand();
            CloseCommand.ExecuteAction = (obj) =>
            {
                if (obj is Window win)
                {
                    win.Close();
                }
            };
        }

        public DelegateCommand ClearCommand { get; set; }

        public DelegateCommand CloseCommand { get; set; }

    }
}
