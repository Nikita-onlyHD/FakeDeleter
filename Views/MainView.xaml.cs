using FakeDeleter.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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

            DataContext = new MainViewModel();

            Jump.MouseOver += () => 
            {
                Left = rnd.NextDouble() * (System.Windows.SystemParameters.PrimaryScreenWidth - Width);
                Top = rnd.NextDouble() * (System.Windows.SystemParameters.PrimaryScreenHeight - Height);
            };
        }
    }
}
