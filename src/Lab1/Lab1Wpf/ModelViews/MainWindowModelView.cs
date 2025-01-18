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

        public string MessageToCrypt
        {
            get => model.Message;
            set
            {
                model.Message = value;
                OnPropertyChanged();
            }
        }

        public ImmutableArray<ImmutableArray<ulong>> CryptedMessage
        {
            get => model.CryptedMessage;
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
