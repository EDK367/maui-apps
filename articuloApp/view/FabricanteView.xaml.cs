using articuloApp.data;
using articuloApp.model;
using Microsoft.EntityFrameworkCore;

namespace articuloApp.view;

public partial class FabricanteView : ContentPage
{
    private readonly AppDbContext _db;
    private Fabricante? _seleccionado;

    public FabricanteView(AppDbContext db)
    {
        InitializeComponent();
        _db = db;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        CargarLista();
    }

    private void CargarLista()
    {
        listaFabricantes.ItemsSource = _db.Fabricantes.AsNoTracking().ToList();
    }

    private void OnSeleccion(object sender, SelectionChangedEventArgs e)
    {
        _seleccionado = e.CurrentSelection.FirstOrDefault() as Fabricante;
        if (_seleccionado is null) return;

        txtCodigo.Text = _seleccionado.Codigo.ToString();
        txtNombre.Text = _seleccionado.Nombre;
        swEstado.IsToggled = _seleccionado.Estado;
        btnEliminar.IsEnabled = true;
    }

    private async void OnGuardar(object sender, EventArgs e)
    {
        if (!int.TryParse(txtCodigo.Text, out int codigo) || string.IsNullOrWhiteSpace(txtNombre.Text))
        {
            await DisplayAlert("Validación", "Código y Nombre son requeridos.", "OK");
            return;
        }

        if (_seleccionado is null)
        {
            var nuevo = new Fabricante
            {
                Codigo = codigo,
                Nombre = txtNombre.Text.Trim(),
                Estado = swEstado.IsToggled,
                FechaMod = DateTime.Now
            };
            _db.Fabricantes.Add(nuevo);
        }
        else
        {
            var fab = _db.Fabricantes.Find(_seleccionado.FabricanteId)!;
            fab.Codigo = codigo;
            fab.Nombre = txtNombre.Text.Trim();
            fab.Estado = swEstado.IsToggled;
            fab.FechaMod = DateTime.Now;
        }

        _db.SaveChanges();
        OnLimpiar(sender, e);
        CargarLista();
    }

    private void OnLimpiar(object sender, EventArgs e)
    {
        txtCodigo.Text = string.Empty;
        txtNombre.Text = string.Empty;
        swEstado.IsToggled = false;
        btnEliminar.IsEnabled = false;
        _seleccionado = null;
        listaFabricantes.SelectedItem = null;
    }

    private async void OnEliminar(object sender, EventArgs e)
    {
        if (_seleccionado is null) return;

        bool confirm = await DisplayAlert("Eliminar", $"¿Eliminar fabricante '{_seleccionado.Nombre}'?", "Sí", "No");
        if (!confirm) return;

        var fab = _db.Fabricantes.Find(_seleccionado.FabricanteId);
        if (fab != null)
        {
            _db.Fabricantes.Remove(fab);
            _db.SaveChanges();
        }

        OnLimpiar(sender, e);
        CargarLista();
    }
}
