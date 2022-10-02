using FakeDeleter.ViewModels;
using System;
using System.Windows.Controls;

namespace FakeDeleter.Views
{
    /// <summary>
    /// Interaction logic for FilesView.xaml
    /// </summary>
    public partial class FilesView : UserControl
    {
        public FilesView()
        {
            InitializeComponent();

            DataContextChanged += (s, e) => Jump.MouseOver += ((FilesViewModel)DataContext).Jump;
        }
    }
}
