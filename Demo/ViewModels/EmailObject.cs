// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System;
using System.ComponentModel;

namespace Demo.ViewModels
{
    public class EmailObject : INotifyPropertyChanged
    {
        private bool _unread;

        public string Sender { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public string Time { get; set; }

        public bool Unread
        {
            get { return _unread; }
            set
            {
                _unread = value;
                NotifyPropertyChanged("Unread");
            }
        }

        public EmailObject(string sender, string subject, string body)
        {
            Sender = sender;
            Subject = subject;
            Body = body;
        }

        public EmailObject(string sender, string subject, string body, string time, bool unread)
        {
            Sender = sender;
            Subject = subject;
            Body = body;
            Time = time;
            Unread = unread;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
