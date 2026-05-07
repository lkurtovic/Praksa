using GalaSoft.MvvmLight.Command;
using PraksaClient.Helpers;
using PraksaClient.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PraksaClient.ViewModels
{
    public class CalculatorViewModel : INotifyPropertyChanged
    {
        private string output;
        public List<List<int>> a { get; }
        public List<string> b { get; }
        public string op { get; set; }
        public bool jd { get; set; } = true;

        public bool loop { get; set; } = false;

        public int prvi { get; set; } = 0;

        public List<int> pomocna { get; set; }
        public List<int> brojevi { get; set; }
        public List<int> indeksi { get; set; }
        public string Output
        {
            get { return output; }
            set
            {
                output = value;
                OnPropertyChanged(nameof(Output));
            }
        }

        public ICommand GenerateCommandj { get; }
        public ICommand GenerateCommand1 { get; }
        public ICommand GenerateCommandp { get; }
        public ICommand GenerateCommandC { get; }

        public CalculatorViewModel()
        {
            a = new List<List<int>>();
            b = new List<string>();
            brojevi = new List<int>();
            pomocna = new List<int>();

            GenerateCommandj = new GenericCommand(Generate, CanGenerate);
            GenerateCommand1 = new RelayCommand<object>(Generate1);
            GenerateCommandp = new RelayCommand<object>(Generatep);
            GenerateCommandC = new GenericCommand(GenerateC, CanGenerate);
        }

        public int list2int(List<int> list)
        {
            int result = 0;
            for (int i = 0; i < list.Count; i++)
            {
                result += list[i] * (int)Math.Pow(10, list.Count - 1 - i);
            }
            return result;
        }

        private void Generate()
        {
            a.Add(new List<int>(pomocna));
            pomocna.Clear();

            brojevi.Clear(); // Changed: Added clear to prevent list accumulation from previous runs
            for (int j = 0; j < a.Count(); j++)
            {
                brojevi.Add(list2int(a[j]));
            }

            int i = 0;
            while (i < b.Count())
            {
                if (b[i] == "*")
                {
                    int novi = brojevi[i] * brojevi[i + 1];
                    brojevi[i] = novi; // Changed: Update current index instead of using Insert(i-1)
                    brojevi.RemoveAt(i + 1);
                    b.RemoveAt(i);
                    // Changed: Removed i++ here because the list shifted; checking current index again
                }
                else if (b[i] == "/")
                {
                    int novi = brojevi[i] / brojevi[i + 1];
                    brojevi[i] = novi; // Changed: Update current index instead of using Insert(i-1)
                    brojevi.RemoveAt(i + 1);
                    b.RemoveAt(i);
                    // Changed: Removed i++ here because the list shifted; checking current index again
                }
                else
                {
                    i++; // Changed: Only increment if no operation was performed
                }
            }

            i = 0;
            while (i < b.Count())
            {
                if (b[i] == "+")
                {
                    int novi = brojevi[i] + brojevi[i + 1];
                    brojevi[i] = novi; // Changed: Update current index instead of using Insert(i-1)
                    brojevi.RemoveAt(i + 1);
                    b.RemoveAt(i);
                    // Changed: Removed i++ here because the list shifted; checking current index again
                }
                else if (b[i] == "-")
                {
                    int novi = brojevi[i] - brojevi[i + 1];
                    brojevi[i] = novi; // Changed: Update current index instead of using Insert(i-1)
                    brojevi.RemoveAt(i + 1);
                    b.RemoveAt(i);
                    // Changed: Removed i++ here because the list shifted; checking current index again
                }
                else
                {
                    i++; // Changed: Only increment if no operation was performed
                }
            }

            if (brojevi.Count > 0)
            {
                Output = brojevi[0].ToString();
            }

            // Changed: Clear state so next calculation starts fresh
            a.Clear();
            b.Clear();
        }

        private void GenerateC()
        {
            b.Clear();
            a.Clear(); // Changed: Added clear for 'a'
            pomocna.Clear(); // Changed: Added clear for 'pomocna'
            brojevi.Clear(); // Changed: Added clear for 'brojevi'
            Output = "";
        }

        private void Generate1(object parameter)
        {
            int broj = int.Parse(parameter.ToString());
            int stringBroj = int.Parse(parameter.ToString());
            pomocna.Add(broj);
            Output += stringBroj;
        }

        private void Generatep(object parameter)
        {
            op = parameter.ToString();
            b.Add(op);
            a.Add(new List<int>(pomocna));
            pomocna.Clear();
            Output += op;
        }

        private bool CanGenerate()
        {
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}