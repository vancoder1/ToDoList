using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace ToDoList.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Register value converters
            if (!Resources.Contains("BoolToVisibilityConverter"))
            {
                Resources.Add("BoolToVisibilityConverter", new BoolToVisibilityConverter());
            }
            if (!Resources.Contains("BoolToBoldConverter"))
            {
                Resources.Add("BoolToBoldConverter", new BoolToBoldConverter());
            }
            if (!Resources.Contains("BoolToStrikethroughConverter"))
            {
                Resources.Add("BoolToStrikethroughConverter", new BoolToStrikethroughConverter());
            }
        }
    }

    // Converters
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (bool)value ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (Visibility)value == Visibility.Visible;
        }
    }

    public class BoolToBoldConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (bool)value ? FontWeights.Bold : FontWeights.Normal;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value.Equals(FontWeights.Bold);
        }
    }

    public class BoolToStrikethroughConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((bool)value)
            {
                TextDecorationCollection decorations = new TextDecorationCollection();
                decorations.Add(TextDecorations.Strikethrough[0]);
                return decorations;
            }
            return null;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value != null && ((TextDecorationCollection)value).Count > 0;
        }
    }
}