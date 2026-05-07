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
        public List<int> a { get; }
        public List<int> b { get; }
        public string op { get; set; }
        public bool jd { get; set; } = true;

        public bool loop { get; set; } = false;

        public int prvi { get; set; } = 0;

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

        public CalculatorViewModel() {
            a = new List<int>();
            b = new List<int>();

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
            
            if (!loop)
            {
                prvi = list2int(a);
            }
            int drugi = list2int(b);
            int rez = 0;
            if (op == "+") { rez = prvi + drugi; }
            if (op == "*") { rez = prvi * drugi; }
            if (op == "-") { rez = prvi - drugi; }
            if (op == "/") { rez = prvi / drugi; }
            Output = rez.ToString();
            prvi = rez;
            a.Clear();
            b.Clear();
            loop = true;
        }
        private void GenerateC()
        {
            a.Clear();
            b.Clear();
           
            Output = "";
            jd = true;

            loop = false;
        }

        private void Generate1(object parameter)
        {
            int broj = int.Parse(parameter.ToString());
            int stringBroj = int.Parse(parameter.ToString());
            if (jd)
            {
                a.Add(broj);
            }
            else
            {
                b.Add(broj);
            }
            Output += stringBroj;
        }
        private void Generatep(object parameter)
        {
            op = parameter.ToString();
            jd = false;
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
