namespace permisoMaui;

public partial class MainPage : ContentPage
{
    int count = 0;

    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnCounterClicked(object? sender, EventArgs e)
    {
        count++;

        if (count == 1)
        {
            var locateStatus = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            var contactStatus = await Permissions.CheckStatusAsync<Permissions.ContactsRead>();

            if (locateStatus != PermissionStatus.Granted)
            {
                locateStatus = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            }

            if (contactStatus != PermissionStatus.Granted)
            {
                contactStatus = await Permissions.RequestAsync<Permissions.ContactsRead>();
            }

            if (locateStatus != PermissionStatus.Granted || contactStatus != PermissionStatus.Granted)
            {
                await DisplayAlert(
                    "Permiso requerido",
                    "Necesitas permitir el acceso a la ubicación y al almacenamiento para continuar.",
                    "OK");
                count--;
                return;
            }
        }

        CounterBtn.Text = $"Clicked {count} time";
        SemanticScreenReader.Announce(CounterBtn.Text);
    }
}
