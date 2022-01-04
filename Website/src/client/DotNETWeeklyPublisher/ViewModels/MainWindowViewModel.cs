using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using Microsoft.Extensions.Configuration;

namespace DotNETWeeklyPublisher.ViewModels
{
    public class MainWindowViewModel : NotificationObject
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; this.RaisePropertyChanged("Name"); }
        }

        private readonly IConfiguration _configuration;

        public MainWindowViewModel(IConfiguration configuration)
        {
            _configuration = configuration;
            Name = configuration.GetSection("ConnectionStrings")["DefaultConnection"];
        }

    }
}
