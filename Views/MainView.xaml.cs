using System;
using System.Windows;

namespace FakeDeleter.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        private static readonly Random rnd = new();

        public MainView()
        {
            InitializeComponent();
        }
    }
}
