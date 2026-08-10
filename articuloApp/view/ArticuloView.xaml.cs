using articuloApp.data;
using articuloApp.model;
using Microsoft.EntityFrameworkCore;

namespace articuloApp.view;

public partial class ArticuloView : ContentPage
{
    private readonly AppDbContext _db;
    private Articulo? _seleccionado;

    public ArticuloView(AppDbContext db)
    {
        InitializeComponent();
        _db = db;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        pkFabricante.ItemsSource = _db.Fabricantes.AsNoTracking().ToList();
        CargarLista();
    }

    private void CargarLista()
    {
        listaArticulos.ItemsSource = _db.Articulos
            .Include(a => a.Fabricante)
            .AsNoTracking()
            .ToList();
    }

    private void OnSeleccion(object sender, SelectionChangedEventArgs e)
    {
        _seleccionado = e.CurrentSelection.FirstOrDefault() as Articulo;
        if (_seleccionado is null) return;

        txtCodigo.Text = _seleccionado.Codigo.ToString();
        txtNombre.Text = _seleccionado.Nombre;
        txtPrecio.Text = _seleccionado.Precio.ToString("F2");
        swEstado.IsToggled = _seleccionado.Estado;

        var fabricantes = pkFabricante.ItemsSource as List<Fabricante>;
        pkFabricante.SelectedItem = fabricantes?.FirstOrDefault(f => f.FabricanteId == _seleccionado.FabricanteId);
        btnEliminar.IsEnabled = true;
    }

    private async void OnGuardar(object sender, EventArgs e)
    {
        if (!int.TryParse(txtCodigo.Text, out int codigo) ||
            string.IsNullOrWhiteSpace(txtNombre.Text) ||
            !decimal.TryParse(txtPrecio.Text, out decimal precio) ||
            pkFabricante.SelectedItem is not Fabricante fabSeleccionado)
        {
            await DisplayAlert("Validación", "Todos los campos son requeridos.", "OK");
            return;
        }

        if (_seleccionado is null)
        {
            var nuevo = new Articulo
            {
                Codigo = codigo,
                Nombre = txtNombre.Text.Trim(),
                Precio = precio,
                Estado = swEstado.IsToggled,
                FechaMod = DateTime.Now,
                FabricanteId = fabSeleccionado.FabricanteId
            };
            _db.Articulos.Add(nuevo);
        }
        else
        {
            var art = _db.Articulos.Find(_seleccionado.ArticuloId)!;
            art.Codigo = codigo;
            art.Nombre = txtNombre.Text.Trim();
            art.Precio = precio;
            art.Estado = swEstado.IsToggled;
            art.FechaMod = DateTime.Now;
            art.FabricanteId = fabSeleccionado.FabricanteId;
        }

        _db.SaveChanges();
        OnLimpiar(sender, e);
        CargarLista();
    }

    private void OnLimpiar(object sender, EventArgs e)
    {
        txtCodigo.Text = string.Empty;
        txtNombre.Text = string.Empty;
        txtPrecio.Text = string.Empty;
        swEstado.IsToggled = false;
        pkFabricante.SelectedItem = null;
        btnEliminar.IsEnabled = false;
        _seleccionado = null;
        listaArticulos.SelectedItem = null;
    }

    private async void OnEliminar(object sender, EventArgs e)
    {
        if (_seleccionado is null) return;

        bool confirm = await DisplayAlert("Eliminar", $"¿Eliminar artículo '{_seleccionado.Nombre}'?", "Sí", "No");
        if (!confirm) return;

        var art = _db.Articulos.Find(_seleccionado.ArticuloId);
        if (art != null)
        {
            _db.Articulos.Remove(art);
            _db.SaveChanges();
        }

        OnLimpiar(sender, e);
        CargarLista();
    }
}
