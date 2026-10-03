using parcial2Progra.ViewModels;

namespace parcial2Progra.Views;

public partial class CursosPage : ContentPage
{
    private readonly CursosViewModel _vm;

    public CursosPage(CursosViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = _vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _vm.CargarCursos();
    }
}
