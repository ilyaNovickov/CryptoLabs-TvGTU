using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using Lab1Models;

namespace LabAvalApp.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
    {
            private RelayCommand send = null;

            private RelayCommand keysGenerate = null;

            private RelayCommand decrypt = null;

            public RelayCommand SendCommand
            {
                get => send ?? (send = new RelayCommand(() =>
                {
                    if (model.Message == null || model.Message == "")
                        return;
                    Send();
                }));
            }

            public RelayCommand KeysGenerateCommand
            {
                get => keysGenerate ?? (keysGenerate = new RelayCommand(() =>
                {
                    GenerateKeys();
                }));
            }

            public RelayCommand DecrypteCommand
            {
                get => decrypt ?? (decrypt = new RelayCommand(() =>
                {
                    Read();
                }));
            }

            private ModelingRSACrypting model = new ModelingRSACrypting();

            private string decryptedMessage = "";

            private string logString = "";

            //private List<CryptedList> cryptedValues = new List<CryptedList>();
            private string[][] cryptedValues = new string[0][];

            public MainWindowViewModel()
            {
                model.LoggingEvent += Model_LoggingEvent;
            }

            private void Model_LoggingEvent(object sender, LogEventArgs e)
            {
                LogString += (e.Message + '\n');
            }

            public uint P => model.P;
            public uint Q => model.Q;
            public BigInteger D => model.D;
            public BigInteger E => model.E;
            public BigInteger N => model.N;

            public string MessageToCrypt
            {
                get => model.Message;
                set
                {
                    model.Message = value;
                    OnPropertyChanged();
                }
            }


            public string[][] CryptedMessage
            {
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

            private void OnPropertyChanged([CallerMemberName] string message = "")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(message));
            }

            public void GenerateKeys()
            {
                model.GenerateKeys();

                OnPropertyChanged(nameof(P));
                OnPropertyChanged(nameof(Q));
                OnPropertyChanged(nameof(E));
                OnPropertyChanged(nameof(D));
                OnPropertyChanged(nameof(N));

                OnPropertyChanged(nameof(KeysGenerated));
                cryptedValues = new string[0][];
                OnPropertyChanged(nameof(CryptedMessage));
                this.DecryptedMessage = "";
            }

            public void Send()
            {
                model.SendMessage();

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
