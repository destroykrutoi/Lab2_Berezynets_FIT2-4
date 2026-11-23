namespace StudentCabinet.Models;

// Модель предметної області: лише дані, без залежностей від Microsoft.Maui
public class Student
{
    public string FullName { get; set; } = string.Empty;
    public string Group { get; set; } = string.Empty;
    public double AverageScore { get; set; }
}
