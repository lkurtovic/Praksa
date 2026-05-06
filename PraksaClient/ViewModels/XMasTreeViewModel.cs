using PraksaClient.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace PraksaClient.ViewModels
{
	public class XMasTreeViewModel : INotifyPropertyChanged
	{
		#region private fields

		private string header;
		private string footer;
		private string output;
		private int height;
		private Brush olovka;

		#endregion

		#region public properties

		public Brush Olovka {
			get => olovka; 
			set { 
				olovka = value;
				OnPropertyChanged(nameof(Olovka)); 
			}
		}

		public string Header
		{
			get => header;
			set
			{
				header = value;
				OnPropertyChanged(nameof(Header));
			}
		}

		public string Footer
		{
			get { return footer; }
			set
			{
				footer = value;
				OnPropertyChanged(nameof(Footer));
			}
		}

		public string Output
		{
			get { return output; }
			set
			{
				output = value;
				OnPropertyChanged(nameof(Output));
			}
		}

		public int Height
		{
			get { return height; }
			set
			{
				height = value;
				OnPropertyChanged(nameof(Height));
			}
		}

		#endregion

		#region Commands

		public ICommand GenerateCommand { get; }

        public ICommand OnOffCommand { get; }

        #endregion

        #region Constructor

        public XMasTreeViewModel()
		{
			GenerateCommand = new GenericCommand(Generate, CanGenerate);
			OnOffCommand = new GenericCommand(Obojaj, CanGenerate);
            Olovka = Brushes.Black;
        }

		#endregion
		private void Obojaj()
		{
            if (Olovka == Brushes.Black)
				Olovka = Brushes.AliceBlue;
			else
				Olovka = Brushes.Black;
        }

		private void Generate()
		{
			Output = $"Generated a Christmas tree with header: {Header}, footer: {Footer}, and height: {Height}";
			string tree = "";
			tree += "\n";
			tree += $"{Header}";
			tree += "\n";
            int width = (height - 2) * 2 + 1;
			for (int i = 1; i <= height; i++)
			{
				for (int j = 1; j <= width; j++)
				{
					//tree += "*";
                    
					if (i+1 == height)
						tree += "*";
					else if (i == height)
						if (j == (width / 2) + 1)
							tree += "*";
						else
							tree += " ";
					else if ((j == (width / 2) - (i - 1) + 1) || (j == (width / 2) + (i - 1) + 1))
						tree += "*";
					else
						tree += " ";
                }
                tree += "\n";
			}
            tree += "\n";
            tree += $"{Footer}";
            tree += "\n";
            Output = tree;
		}

		private bool CanGenerate()
		{
			return Height > 0;
		}

		#region Interface implementation
		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		#endregion
	}
}
