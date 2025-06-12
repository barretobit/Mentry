using System.Windows;
using Mentry.ViewModels;

namespace Mentry.Views;

public partial class MainWindow : Window
{
    public MainWindow(MainViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
}