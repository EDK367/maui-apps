using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using mauiAppTask.Models.Entities;
using mauiAppTask.Services;

namespace mauiAppTask.viewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Tarea> tareas;

        public MainViewModel()
        {
            Tareas = new ObservableCollection<Tarea>(TaskService.GetAll());
            
            WeakReferenceMessenger.Default.Register<string>(this, (r, m) =>
            {
                if (m == "TaskCreated")
                {
                    UpdateTaskList();
                }
            });
        }

        [RelayCommand]
        private void ToggleStatus(Tarea tarea)
        {
            TaskService.ChangeStatus(tarea.TareaID);
            tarea.Realizada = !tarea.Realizada;
        }

    [RelayCommand]
    private async Task GoCreateTask()
    {
        await Shell.Current.GoToAsync("AddTaskPage");
    }

    [RelayCommand]
    private void UpdateTaskList()
    {
        Tareas.Clear();
        foreach (var t in TaskService.GetAll())
            Tareas.Add(t);
    }
    }
}
