using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using StudentCabinet.Models;

namespace StudentCabinet.ViewModels;

public class StudentViewModel : INotifyPropertyChanged, IQueryAttributable
{
    private readonly Student _student = new();

    public StudentViewModel()
    {
        AddStudentCommand = new Command(AddStudent, CanAddStudent);
        OpenDetailsCommand = new Command<Student>(async student => await OpenDetailsAsync(student));
    }

    public string FullName
    {
        get => _student.FullName;
        set
        {
            if (_student.FullName != value)
            {
                _student.FullName = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Greeting));
                ((Command)AddStudentCommand).ChangeCanExecute();
            }
        }
    }

    public string Group
    {
        get => _student.Group;
        set
        {
            if (_student.Group != value)
            {
                _student.Group = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Greeting));
            }
        }
    }

    public double AverageScore
    {
        get => _student.AverageScore;
        set
        {
            var rounded = Math.Round(value, 1);
            if (_student.AverageScore != rounded)
            {
                _student.AverageScore = rounded;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsExcellent));
            }
        }
    }

    public string Greeting =>
        string.IsNullOrWhiteSpace(FullName)
            ? "Введіть дані студента"
            : $"Студент: {FullName}, група {Group}";

    public bool IsExcellent => AverageScore >= 4.0;

    public ObservableCollection<Student> Students { get; } = new();

    public ICommand AddStudentCommand { get; }

    // Лабораторна робота № 2: навігаційна команда
    public ICommand OpenDetailsCommand { get; }

    private void AddStudent()
    {
        Students.Add(new Student
        {
            FullName = FullName,
            Group = Group,
            AverageScore = AverageScore
        });

        FullName = string.Empty;
        Group = string.Empty;
        AverageScore = 0;
    }

    private bool CanAddStudent() => !string.IsNullOrWhiteSpace(FullName);

    private async Task OpenDetailsAsync(Student? student)
    {
        if (student is null) return;

        var parameters = new Dictionary<string, object>
        {
            { "SelectedStudent", student }
        };
        await Shell.Current.GoToAsync("studentdetail", parameters);
    }

    // Отримання відредагованого студента після повернення зі сторінки деталей
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("OriginalStudent", out var o) && o is Student original
            && query.TryGetValue("UpdatedStudent", out var u) && u is Student updated)
        {
            var index = Students.IndexOf(original);
            if (index >= 0)
                Students[index] = updated; // заміна елемента оновлює CollectionView
        }
        query.Clear();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
