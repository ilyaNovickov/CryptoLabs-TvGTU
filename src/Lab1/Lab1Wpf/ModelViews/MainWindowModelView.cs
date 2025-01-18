using Lab1Models;
using Lab1Wpf.Commands;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab1Wpf.ModelViews
{
    internal class MainWindowModelView : INotifyPropertyChanged
    {
        private ModelingRSACrypting model = new ModelingRSACrypting();

        private string decryptedMessage = "";

        private string logString = "";

        public MainWindowModelView()
        {
            model.LoggingEvent += Model_LoggingEvent; 
        }

        private void Model_LoggingEvent(object sender, LogEventArgs e)
        {
            LogString += (e.Message + '\n');
        }

        public string MessageToCrypt
        {
            get => model.Message;
            set
            {
                model.Message = value;
                OnPropertyChanged();
            }
        }

        //public ImmutableArray<ImmutableArray<ulong>> CryptedMessage
        public ulong[][] CryptedMessage
        {
            //get => model.CryptedMessage;
            get => new ulong[][] { new ulong[] { 4, 5,7 ,}, new ulong[] { 4, 63,2 ,1}, new ulong[] { 4, 63, 5 } };
        }

        public string DecryptedMessage
        {
            get => decryptedMessage;
            private set
            {
                decryptedMessage = value;
                OnPropertyChanged();
            }
        }

        public string LogString
        {
            get => logString;
            set
            {
                logString = value;
                OnPropertyChanged();
            }
        }

        public int MaxMessageLength => model.MessageMaxLength;

        public int OneMessageLength => model.MessageMaxLength;

        public bool KeysGenerated
        {
            get => model.KeysGenerated;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string message = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(message));
        }

        public void GenerateKeys()
        {
            model.GenerateKeys();

            OnPropertyChanged(nameof(KeysGenerated));
        }

        public void Send()
        {
            model.SendMessage();

            OnPropertyChanged(nameof(CryptedMessage));
        }

        public void Read()
        {
            this.DecryptedMessage = model.ReadMessage();
        }
    }
}
