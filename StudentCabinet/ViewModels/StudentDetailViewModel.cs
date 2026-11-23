using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using StudentCabinet.Models;

namespace StudentCabinet.ViewModels;

// ViewModel сторінки деталей: отримує студента через IQueryAttributable
public class StudentDetailViewModel : IQueryAttributable, INotifyPropertyChanged
{
    private Student? _original;
    private string _fullName = string.Empty;
    private string _group = string.Empty;
    private double _averageScore;

    public StudentDetailViewModel()
    {
        GoBackCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
        SaveCommand = new Command(async () => await SaveAsync(), () => _original is not null);
    }

    public string FullName
    {
        get => _fullName;
        set { if (_fullName != value) { _fullName = value; OnPropertyChanged(); } }
    }

    public string Group
    {
        get => _group;
        set { if (_group != value) { _group = value; OnPropertyChanged(); } }
    }

    public double AverageScore
    {
        get => _averageScore;
        set
        {
            var rounded = Math.Round(Math.Clamp(value, 0, 5), 1);
            if (_averageScore != rounded)
            {
                _averageScore = rounded;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsChanged));
            }
        }
    }

    public bool IsChanged => _original is not null && _original.AverageScore != AverageScore;

    public ICommand GoBackCommand { get; }
    public ICommand SaveCommand { get; }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("SelectedStudent", out var value) && value is Student student)
        {
            _original = student;
            FullName = student.FullName;
            Group = student.Group;
            AverageScore = student.AverageScore;
            OnPropertyChanged(nameof(IsChanged));
            ((Command)SaveCommand).ChangeCanExecute();
        }
    }

    private async Task SaveAsync()
    {
        if (_original is null) return;

        var updated = new Student
        {
            FullName = FullName,
            Group = Group,
            AverageScore = AverageScore
        };

        // повернення на попередню сторінку з передачею оновленого об'єкта
        var parameters = new Dictionary<string, object>
        {
            { "OriginalStudent", _original },
            { "UpdatedStudent", updated }
        };
        await Shell.Current.GoToAsync("..", parameters);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
