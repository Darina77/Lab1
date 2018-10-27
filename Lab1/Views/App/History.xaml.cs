using Lab1.ViewModels.App;

namespace Lab1.Views.App
{
    /// <inheritdoc />
    /// <summary>
    /// Логика взаимодействия для History.xaml
    /// </summary>
    public partial class History
    {
        public History()
        {
            InitializeComponent();
            var history = new HistoryViewModel();
            DataContext = history;
        }
    }
}
