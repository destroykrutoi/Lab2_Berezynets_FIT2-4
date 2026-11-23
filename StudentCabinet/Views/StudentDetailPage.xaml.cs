using StudentCabinet.ViewModels;

namespace StudentCabinet.Views;

public partial class StudentDetailPage : ContentPage
{
    // ViewModel надходить через Dependency Injection
    public StudentDetailPage(StudentDetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
