using System.Globalization;

namespace StudentCabinet.Converters;

// Перетворює середній бал на колір: >= 4.0 — зелений, інакше — помаранчевий
public class ScoreToColorConverter : IValueConverter
{
    public double Threshold { get; set; } = 4.0;

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is double score && score >= Threshold)
            return Color.FromArgb("#2E7D32");
        return Color.FromArgb("#E65100");
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}
