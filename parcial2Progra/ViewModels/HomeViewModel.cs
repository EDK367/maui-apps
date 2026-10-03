using System.Collections.ObjectModel;
using System.Windows.Input;
using parcial2Progra.Models;
using parcial2Progra.Service;

namespace parcial2Progra.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private readonly AlumnoService _alumnoService;

        public ObservableCollection<Alumno> UltimosAlumnos { get; } = new();

        public ICommand CargarCommand { get; }

        public HomeViewModel(AlumnoService alumnoService)
        {
            _alumnoService = alumnoService;
            Title = "Inicio";
            CargarCommand = new Command(async () => await CargarUltimosAlumnos());
        }

        public async Task CargarUltimosAlumnos()
        {
            if (IsBusy) return;
            IsBusy = true;
            try
            {
                UltimosAlumnos.Clear();
                var lista = await _alumnoService.GetUltimos10Async();
                foreach (var a in lista)
                    UltimosAlumnos.Add(a);
            }
            finally { IsBusy = false; }
        }
    }
}
