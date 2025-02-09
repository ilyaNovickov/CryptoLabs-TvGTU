using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lab2Lib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Lab2Wpf.ViewModel
{
    public class Turple
    {
        public int K { get; set; }

        public string Str { get; set; }
    }

    internal class MainWidowViewModel : ObservableObject
    {
        private ICommand analizeDecryptedMessage;
        private ICommand analazeAllKeys;
        private ICommand getPeriodCommand;
        private string cryptedMessage = null;
        private int lowBound = 0;
        private int upperBound = 32;
        private int period = 0;
        private int[] periodVals = null;

        private int k = 1;
        private int n = 1;

        public string CryptedMessage 
        {
            get => cryptedMessage;
            set
            {
                SetProperty(ref cryptedMessage, value);
            }
        }

        public ObservableCollection<Turple> DecryptedMessages { get; set; }
            = new ObservableCollection<Turple>();

        public int Period
        {
            get => period;
            set => SetProperty(ref period, value);
        }

        public int[] PeriodVals
        {
            get => periodVals;
            set => SetProperty(ref periodVals, value);
        }

        public int K
        {
            get => k;
            set
            {
                if (!(value is int))
                    return;
                if (value < 0 || value > 32)
                    return;
                SetProperty(ref k, value);
            }
        }

        public int N
        {
            get => n;
            set
            {
                if (!(value is int))
                    return;
                if (value < 0)
                    return;
                SetProperty(ref n, value);
            }
        }

        public int LowBound
        {
            get => lowBound;
            set
            {
                if (!(value is int))
                    return;
                if (value < 0 || value > 32)
                    return;
                SetProperty(ref lowBound, value);
            }
        }

        public int UpperBound
        {
            get => upperBound;
            set
            {
                if (!(value is int))
                    return;
                if (value < 0 || value > 32)
                    return;
                SetProperty(ref upperBound, value);
            }
        }

        public ICommand GetPeriodCommand
        {
            get => getPeriodCommand ?? (getPeriodCommand = new RelayCommand(GetPeriod));
        }

        public ICommand AnalizeDecryptedMessageCommand
        {
            get => analizeDecryptedMessage ?? (analizeDecryptedMessage = new RelayCommand(AnalizeDecryptedMessage));
        }

        public ICommand AnalizeAllKeysCommad
        {
            get => analazeAllKeys ?? (analazeAllKeys = new RelayCommand(AnalizeAllKeys));
        }


        private void AnalizeDecryptedMessage()
        {
            if (CryptedMessage == null)
                return;

            List<string> strings;

            IEnumerable<int> keys;

            try
            {
                keys = KeysGet.AnalizeKeys(CryptedMessage, lowBound, upperBound, out strings);
            }
            catch (Exception ex) 
            {
                return;
            }

            DecryptedMessages.Clear();

            for (int i = 0; i < keys.Count(); i++)
            {
                DecryptedMessages.Add(new Turple() 
                {
                    K = keys.ElementAt<int>(i), 
                    Str = strings[i]
                });
            }
        }

        private void AnalizeAllKeys()
        {
            if (CryptedMessage == null)
                return;

            List<(int, string)> keysAndText = new List<(int, string)>(1);

            try
            {
                if (lowBound > UpperBound)
                    throw new Exception("Нижняя граница ключа не может быть больше вверхней");

                for (int k = lowBound; k <= UpperBound; k++)
                {
                    string str = Lab2Modeling.Decrypt(CryptedMessage, k);

                    keysAndText.Add( (k, str) );
                }
            }
            catch (Exception ex)
            {
                return;
            }

            DecryptedMessages.Clear();

            for (int i = 0; i < keysAndText.Count; i++)
            {
                DecryptedMessages.Add(new Turple()
                {
                    K = keysAndText[i].Item1,
                    Str = keysAndText[i].Item2
                });
            }
        }

        private void GetPeriod()
        {
            Period = Lab2Modeling.PeriodAnalizer(k, n, out int[] vals);
            PeriodVals = vals;
        }

    }
}
