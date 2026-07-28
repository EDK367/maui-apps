using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using mauiAppTask.Models.Entities;
using mauiAppTask.Service;

namespace mauiAppTask.viewModels
{
    public partial class AddTaskViewModel : ObservableObject
    {
        [ObservableProperty]
        private string? newTask;
        [ObservableProperty]
        private string? newDescription;
        private readonly TaskService _taskService;

        public AddTaskViewModel()
        {
            _taskService = new TaskService();
        }

        [RelayCommand]
        private async Task CreateTask()
        {
            if (!string.IsNullOrWhiteSpace(NewTask))
            {
                if (string.IsNullOrEmpty(NewDescription))
                {
                    NewDescription = "";
                }
                await _taskService.CreateTask(new Tarea { Nombre = NewTask, Descripcion = NewDescription, Activa = true });
                NewTask = string.Empty;
                NewDescription = string.Empty;

                WeakReferenceMessenger.Default.Send("TaskCreated");

                await Shell.Current.GoToAsync("..");
            }
        }

        [RelayCommand]
        private async Task Cancel()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
