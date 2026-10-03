using System.Collections.ObjectModel;
using System.Windows.Input;
using parcial2Progra.Models;
using parcial2Progra.Service;

namespace parcial2Progra.ViewModels
{
    public class AlumnosViewModel : BaseViewModel
    {
        private readonly AlumnoService _alumnoService;

        string _nombre = string.Empty;
        string _apellido = string.Empty;
        string _edad = string.Empty;
        string _genero = string.Empty;
        string _correo = string.Empty;
        string _telefono = string.Empty;
        string _mensaje = string.Empty;

        public string Nombre { get => _nombre; set => SetProperty(ref _nombre, value); }
        public string Apellido { get => _apellido; set => SetProperty(ref _apellido, value); }
        public string Edad { get => _edad; set => SetProperty(ref _edad, value); }
        public string Genero { get => _genero; set => SetProperty(ref _genero, value); }
        public string Correo { get => _correo; set => SetProperty(ref _correo, value); }
        public string Telefono { get => _telefono; set => SetProperty(ref _telefono, value); }
        public string Mensaje { get => _mensaje; set => SetProperty(ref _mensaje, value); }

        public ObservableCollection<Alumno> Alumnos { get; } = new();

        public ICommand GuardarCommand { get; }
        public ICommand CargarCommand { get; }

        public AlumnosViewModel(AlumnoService alumnoService)
        {
            _alumnoService = alumnoService;
            Title = "Alumnos";
            GuardarCommand = new Command(async () => await GuardarAlumno());
            CargarCommand = new Command(async () => await CargarAlumnos());
        }

        public async Task CargarAlumnos()
        {
            if (IsBusy) return;
            IsBusy = true;
            try
            {
                Alumnos.Clear();
                var lista = await _alumnoService.GetAllAsync();
                foreach (var a in lista)
                    Alumnos.Add(a);
            }
            finally { IsBusy = false; }
        }

        async Task GuardarAlumno()
        {
            if (string.IsNullOrWhiteSpace(Nombre) || string.IsNullOrWhiteSpace(Apellido))
            {
                Mensaje = "Nombre y Apellido son requeridos.";
                return;
            }

            IsBusy = true;
            try
            {
                var alumno = new Alumno
                {
                    Nombre = Nombre,
                    Apellido = Apellido,
                    Edad = Edad,
                    Genero = Genero,
                    Correo = Correo,
                    Telefono = Telefono,
                    FechaMod = DateTime.Now,
                    Activo = true
                };

                await _alumnoService.CreateAsync(alumno);
                Mensaje = $"Alumno '{Nombre} {Apellido}' guardado correctamente.";

                Nombre = Apellido = Edad = Genero = Correo = Telefono = string.Empty;
                await CargarAlumnos();
            }
            finally { IsBusy = false; }
        }
    }
}
