﻿using EmailAgent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CryptoBird.ViewModels
{
    class MailSendViewModel : BasicViewModel
    {
        private string _from;
        public string From
        {
            get => _from;
            set => SetProperty(ref _from, value, "From");
        }

        private string _to;
        public string To
        {
            get => _to;
            set => SetProperty(ref _to, value, "To");
        }

        private string _subject;
        public string Subject
        {
            get => _subject;
            set => SetProperty(ref _subject, value, "Subject");
        }

        private string _body;
        public string Body
        {
            get => _body;
            set => SetProperty(ref _body, value, "Body");
        }

        public ICommand SendMessageCommand { get; }

        public MailSendViewModel()
        {
            SendMessageCommand = new RelayCommand(SendMessage);

            From = UserData.Login;
        }

        private void SendMessage()
        {
            var messageSender = new MailSender("smtp.gmail.com", 587);

            messageSender.Send(From, To, Body, Subject, UserData.Login, UserData.Password, "smtp.gmail.com", 587);
        }

        public class RelayCommand<T> : ICommand
        {
            private Action<T> action;
            public RelayCommand(Action<T> action) => this.action = action;
            public bool CanExecute(object parameter) => true;
#pragma warning disable CS0067
            public event EventHandler CanExecuteChanged;
#pragma warning restore CS0067
            public void Execute(object parameter) => action((T)parameter);
        }

        public class RelayCommand : ICommand
        {
            private Action action;
            public RelayCommand(Action action) => this.action = action;
            public bool CanExecute(object parameter) => true;
#pragma warning disable CS0067
            public event EventHandler CanExecuteChanged;
#pragma warning restore CS0067
            public void Execute(object parameter) => action();
        }
    }
}