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
    //internal class CryptedList
    //{
    //    public List<CryptedValue> List { get; set; }
    //}

    //internal struct CryptedValue
    //{
    //    public ulong Value { get; set; }
    //}

    internal class MainWindowModelView : INotifyPropertyChanged
    {
        private RelayCommand send = null;

        private RelayCommand keysGenerate = null;

        private RelayCommand decrypt = null;

        public RelayCommand SendCommand
        {
            get => send ?? (send = new RelayCommand((obj) => 
            {
                Send();
            }));
        }

        public RelayCommand KeysGenerateCommand
        {
            get => keysGenerate ?? (keysGenerate = new RelayCommand((obj) =>
            {
                GenerateKeys();
            }));
        }

        public RelayCommand DecrypteCommand
        {
            get => decrypt ?? (decrypt = new RelayCommand((obj) =>
            {
                Read();
            }));
        }

        private ModelingRSACrypting model = new ModelingRSACrypting();

        private string decryptedMessage = "";

        private string logString = "";

        //private List<CryptedList> cryptedValues = new List<CryptedList>();
        private string[][] cryptedValues = new string[0][];

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
        //public ulong[][] CryptedMessage
        //public List<CryptedList> CryptedMessage
        public string[][] CryptedMessage
        {
            //get => model.CryptedMessage;
            //get => new ulong[][] { new ulong[] { 4, 5,7 ,}, new ulong[] { 4, 63,2 ,1}, new ulong[] { 4, 63, 5 } };
            get => cryptedValues;
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
            OnPropertyChanged(nameof(CryptedMessage));
        }

        public void Send()
        {
            model.SendMessage();

            //cryptedValues.Clear();

            //foreach (ImmutableArray<ulong> list in model.CryptedMessage)
            //{
            //    List<CryptedValue> innnerList = new List<CryptedValue>(list.Count());

            //    foreach (ulong item in list)
            //    {
            //        innnerList.Add(new CryptedValue() { Value = item });
            //    }

            //    cryptedValues.Add(new CryptedList() { List = innnerList });
            //}

            /*
            cryptedValues = new string[model.CryptedMessage.Length];

            for (int i = 0; i < model.CryptedMessage.Length; i++)
            {
                //List<CryptedValue> innnerList = new List<CryptedValue>(list.Count());
                StringBuilder stringBuilder = new StringBuilder(2);

                foreach (ulong item in model.CryptedMessage[i])
                {
                    //innnerList.Add(new CryptedValue() { Value = item });
                    stringBuilder.Append(item);
                    stringBuilder.Append(';');
                }

                //cryptedValues.Add(new CryptedList() { List = innnerList });
                cryptedValues[i] = stringBuilder.ToString();
            }
            */

            cryptedValues = new string[model.CryptedMessage.Length][];

            for (int i = 0; i < model.CryptedMessage.Length; i++)
            {
                string[] inner = new string[model.CryptedMessage[i].Length];

                for (int j = 0; j < model.CryptedMessage[i].Length; j++)
                {
                    inner[j] = model.CryptedMessage[i][j].ToString();
                }

                cryptedValues[i] = inner;
            }

            OnPropertyChanged(nameof(CryptedMessage));
        }

        public void Read()
        {
            this.DecryptedMessage = model.ReadMessage();
        }
    }
}
