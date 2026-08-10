using System.Globalization;

namespace articuloApp.converters;

public class BoolToEstadoConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is true ? "Activo" : "Inactivo";

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value?.ToString() == "Activo";
}
