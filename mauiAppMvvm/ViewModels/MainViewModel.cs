using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace mauiAppMvvm.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private int count;

        [ObservableProperty]
        private string text = "Sin clicks";

        [RelayCommand]
        public void Plus()
        {
            Count++;
            Text = "Se ha hecho click " + Count + " veces";
        }
    }
}