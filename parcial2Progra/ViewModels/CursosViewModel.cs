using System.Collections.ObjectModel;
using System.Windows.Input;
using parcial2Progra.Models;
using parcial2Progra.Service;

namespace parcial2Progra.ViewModels
{
    public class CursosViewModel : BaseViewModel
    {
        private readonly CursoService _cursoService;

        string _nombre = string.Empty;
        string _descripcion = string.Empty;
        string _mensaje = string.Empty;

        public string Nombre { get => _nombre; set => SetProperty(ref _nombre, value); }
        public string Descripcion { get => _descripcion; set => SetProperty(ref _descripcion, value); }
        public string Mensaje { get => _mensaje; set => SetProperty(ref _mensaje, value); }

        public ObservableCollection<Curso> Cursos { get; } = new();

        public ICommand GuardarCommand { get; }
        public ICommand CargarCommand { get; }

        public CursosViewModel(CursoService cursoService)
        {
            _cursoService = cursoService;
            Title = "Cursos";
            GuardarCommand = new Command(async () => await GuardarCurso());
            CargarCommand = new Command(async () => await CargarCursos());
        }

        public async Task CargarCursos()
        {
            if (IsBusy) return;
            IsBusy = true;
            try
            {
                Cursos.Clear();
                var lista = await _cursoService.GetAllAsync();
                foreach (var c in lista)
                    Cursos.Add(c);
            }
            finally { IsBusy = false; }
        }

        async Task GuardarCurso()
        {
            if (string.IsNullOrWhiteSpace(Nombre))
            {
                Mensaje = "El nombre del curso es requerido.";
                return;
            }

            IsBusy = true;
            try
            {
                var curso = new Curso
                {
                    Nombre = Nombre,
                    Descripcion = Descripcion,
                    FechaMod = DateTime.Now,
                    Activo = true
                };

                await _cursoService.CreateAsync(curso);
                Mensaje = $"Curso '{Nombre}' guardado correctamente.";

                Nombre = Descripcion = string.Empty;
                await CargarCursos();
            }
            finally { IsBusy = false; }
        }
    }
}
