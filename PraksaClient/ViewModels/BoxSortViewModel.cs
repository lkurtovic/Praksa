using PraksaClient.Helpers;
using PraksaClient.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace PraksaClient.ViewModels
{
    public class BoxSortViewModel
    {
        #region private fields
        
        Random _random = new Random();

        #endregion

        #region Public properties
        public ObservableCollection<Box> Boxes { get; }
        public ObservableCollection<Box> SortedBoxes { get; }

        public ObservableCollection<Box> TallestBoxes { get; }
        public ObservableCollection<Box> TallestBoxesTracker { get; }

        public ObservableCollection<Box> MinHeap { get; }


        #endregion

        #region Commands

        public ICommand GenerateBoxesCommand { get; }

        public ICommand SortBoxesCommand { get; }

        public ICommand TallestBoxesCommand { get; }

        #endregion

        #region Constructor

        public BoxSortViewModel()
        {
            Boxes = new ObservableCollection<Box>();
            SortedBoxes = new ObservableCollection<Box>();
            TallestBoxes = new ObservableCollection<Box>();
            TallestBoxesTracker = new ObservableCollection<Box>();

            GenerateBoxesCommand = new GenericCommand(GenerateBoxes, CanGenerateBoxes);
            SortBoxesCommand = new GenericCommand(SortBoxes, CanSortBoxes);
            TallestBoxesCommand = new GenericCommand(FindTallestBoxes2, CanMakeTallestBoxes);
        }

        #endregion

        private void FindTallestBoxes2()
        {
            TallestBoxes.Clear();
            TallestBoxesTracker.Clear();
            int max_height = 0;
            for (int i = 0; i < SortedBoxes.Count; i++)
            {
                TallestBoxesTracker.Clear();
                int width = SortedBoxes[i].Width;
                int length = SortedBoxes[i].Length;
                int height = SortedBoxes[i].Height;
                int height_tracker = height;
                TallestBoxesTracker.Add(SortedBoxes[i]);

                for (int j = i+1; j < SortedBoxes.Count; j++)
                {
                    if (SortedBoxes[j].Width <= width && SortedBoxes[j].Length <= length)
                    {
                        TallestBoxesTracker.Add(SortedBoxes[j]);
                        width = SortedBoxes[j].Width;
                        length = SortedBoxes[j].Length;
                        height = SortedBoxes[j].Height;
                        height_tracker += height;
                    }
                }

                if (height_tracker > max_height)
                {
                    TallestBoxes.Clear();
                    max_height = height_tracker;
                    foreach (Box box in TallestBoxesTracker)
                    {
                        TallestBoxes.Add(box);
                    }
                }
            }
        }


        private void FindTallestBoxes() {
            TallestBoxes.Clear();
            TallestBoxesTracker.Clear();
            int max_height = 0;
            int max_width = 11;
            int max_length = 11;
            int current_height_tracker = 0;
            for (int i = 0; i < SortedBoxes.Count; i++)
            {
                int current_height = SortedBoxes[i].Height;
                int current_length = SortedBoxes[i].Length;
                int current_width = SortedBoxes[i].Width;

                    if (current_width <= max_width && current_length <= max_length)
                    {
                        current_height_tracker += current_height;
                        max_width = current_width;
                        max_length = current_length;
                        TallestBoxesTracker.Add(SortedBoxes[i]);
                    } else
                    {
                        if (current_height_tracker > max_height)
                        {
                            TallestBoxes.Clear();
                            max_height = current_height_tracker;
                            foreach (Box box in TallestBoxesTracker)
                            {
                                TallestBoxes.Add(box);
                            }
                            TallestBoxesTracker.Clear();
                            current_height_tracker = 0;
                        }
                        else
                        {
                            TallestBoxesTracker.Clear();
                            current_height_tracker = 0;
                        }
                        max_width = 11;
                        max_length = 11;
                    }
                }
            }





        private bool CanMakeTallestBoxes()
        {
            return SortedBoxes.Any();
        }

        #region Private methods

        private void SortBoxes()
        {
            var temp_sortedBoxes = Boxes.OrderByDescending(b => b.Width).ThenByDescending(b => b.Length).ToList();
            SortedBoxes.Clear();
            foreach (Box box in temp_sortedBoxes)
            {
                SortedBoxes.Add(box);
            }
        }

        private bool CanSortBoxes()
        {
            return Boxes.Any();
        }

        private void GenerateBoxes()
        {
            Boxes.Clear();

            for (int i = 0; i < 10; i++)
            {
                Box box = new Box();
                box.Height = _random.Next(1, 10);
                box.Width = _random.Next(1, 10);
                box.Length = _random.Next(1, 10);
                Boxes.Add(box);
            }
        }


        private bool CanGenerateBoxes()
        {
            return true;
        }

        #endregion
    }
}
