using StudentCabinet.Views;

namespace StudentCabinet;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        // маршрут сторінки деталей студента
        Routing.RegisterRoute("studentdetail", typeof(StudentDetailPage));
    }
}
