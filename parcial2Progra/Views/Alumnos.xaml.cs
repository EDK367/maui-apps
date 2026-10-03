using parcial2Progra.ViewModels;

namespace parcial2Progra.Views;

public partial class AlumnosPage : ContentPage
{
    private readonly AlumnosViewModel _vm;

    public AlumnosPage(AlumnosViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = _vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _vm.CargarAlumnos();
    }
}
