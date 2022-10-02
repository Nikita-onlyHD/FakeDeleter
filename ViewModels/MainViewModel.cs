using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using FakeDeleter.Models;

namespace FakeDeleter.ViewModels
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        private readonly StartupViewModel startupViewModel = new();
        private readonly FilesViewModel filesViewModel = new();

        private static readonly Random rnd = new();

        #region Properties
        public event PropertyChangedEventHandler? PropertyChanged;

        private void Changed(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        private object? content;
        public object? Content
        {
            get => content;
            set
            {
                if (content != value)
                {
                    content = value;
                    Changed(nameof(Content));
                }
            }
        }

        private double randomLeft;
        public double RandomLeft
        {
            get => randomLeft;
            set
            {
                if (randomLeft != value)
                {
                    randomLeft = value;
                    Changed(nameof(RandomLeft));
                }
            }
        }

        private double randomTop;
        public double RandomTop
        {
            get => randomTop;
            set
            {
                if (randomTop != value)
                {
                    randomTop = value;
                    Changed(nameof(RandomTop));
                }
            }
        }
        #endregion

        public MainViewModel()
        {
            Content = startupViewModel;

            startupViewModel.ButtonPressed += () =>
            {
                Content = filesViewModel;
                filesViewModel.LookupDirectoryCommand.Execute(null);
            };

            filesViewModel.Jumped += () =>
            {
                RandomLeft = rnd.NextDouble()* (System.Windows.SystemParameters.PrimaryScreenWidth - 600.0);
                RandomTop = rnd.NextDouble()* (System.Windows.SystemParameters.PrimaryScreenHeight - 440.0);
            };
        }
    }
}
