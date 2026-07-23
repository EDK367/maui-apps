using Android.Database;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using mauiAppTask.Services;

namespace mauiAppTask.viewModels
{
    public partial class AddTaskViewModel : ObservableObject
    {
        [ObservableProperty]
        private string newTask;
        [ObservableProperty]
        private string newDescription;

        [RelayCommand]
        private void CreateTask()
        {
            if (!string.IsNullOrWhiteSpace(NewTask))
            {
                if (string.IsNullOrEmpty(NewDescription))
                {
                    NewDescription = "";
                }
                TaskService.CreateTask(NewTask, NewDescription);
                NewTask = string.Empty;
                NewDescription = string.Empty;
                
                WeakReferenceMessenger.Default.Send("TaskCreated");
                
                Shell.Current.GoToAsync("..");
            }
        }

        [RelayCommand]
        private async Task Cancel()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
