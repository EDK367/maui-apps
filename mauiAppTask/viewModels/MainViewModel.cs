using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using mauiAppTask.Models.Entities;
using mauiAppTask.Service;

namespace mauiAppTask.viewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Tarea> tareas;

        private readonly TaskService _taskService;
        public MainViewModel()
        {
            _taskService = new TaskService();
            Tareas = new ObservableCollection<Tarea>();

            WeakReferenceMessenger.Default.Register<string>(this, (r, m) =>
            {
                if (m == "TaskCreated")
                {
                    UpdateTaskList();
                }
            });

            // Carga inicial asíncrona (fire-and-forget intencional en constructor)
#pragma warning disable CS4014
            LoadTasksAsync();
#pragma warning restore CS4014
        }

        private async Task LoadTasksAsync()
        {
            var list = await _taskService.GetAllAsync();
            Tareas = new ObservableCollection<Tarea>(list);
        }

        [RelayCommand]
        private async Task ToggleStatus(Tarea tarea)
        {
            tarea.Realizada = !tarea.Realizada;
            await _taskService.UpdateTarea(tarea);
        }

    [RelayCommand]
    private async Task GoCreateTask()
    {
        await Shell.Current.GoToAsync("AddTaskPage");
    }

    [RelayCommand]
    private async Task UpdateTaskList()
    {
        var list = await _taskService.GetAllAsync();
        Tareas = new ObservableCollection<Tarea>(list);
    }
    }
}
