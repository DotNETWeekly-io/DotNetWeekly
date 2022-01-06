using System;
using System.Windows;

namespace DotNETWeeklyPublisher.Commands
{
    public  class ExCommandParameter
    {
        public DependencyObject Sender { get; set; }

        public EventArgs EventArgs { get; set; }


        public object Parameter { get; set; }
    }
}
