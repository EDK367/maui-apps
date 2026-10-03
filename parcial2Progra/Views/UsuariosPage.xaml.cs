using parcial2Progra.Data;
using parcial2Progra.Models;

namespace parcial2Progra.Views;

public partial class UsuariosPage : ContentPage
{
    private readonly DatabaseService _db;

    public UsuariosPage(DatabaseService db)
    {
        InitializeComponent();
        _db = db;
    }

    private async void OnGuardarClicked(object sender, EventArgs e)
    {
        var user = new User
        {
            Username = UsernameEntry.Text ?? string.Empty,
            Email = EmailEntry.Text ?? string.Empty,
            Password = PasswordEntry.Text ?? string.Empty
        };

        await _db.SaveUserAsync(user);

        MensajeLabel.Text = "Usuario guardado correctamente.";
        UsernameEntry.Text = string.Empty;
        EmailEntry.Text = string.Empty;
        PasswordEntry.Text = string.Empty;
    }
}
