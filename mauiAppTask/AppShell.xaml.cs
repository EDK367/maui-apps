using mauiAppTask.Views;

namespace mauiAppTask;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute("AddTaskPage", typeof(AddTaskPage));
    }
}
