using parcial2Progra.ViewModels;

namespace parcial2Progra.Views;

public partial class AsignacionesPage : ContentPage
{
    private readonly AsignacionesViewModel _vm;

    public AsignacionesPage(AsignacionesViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = _vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _vm.CargarDatos();
    }
}
