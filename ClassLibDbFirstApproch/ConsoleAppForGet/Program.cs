using System.Net.Http.Json;

// Fake API call from Program.cs
using (var client = new HttpClient())
{
    client.BaseAddress = new Uri("http://localhost:5137/api/student");

    var users = await client.GetFromJsonAsync<List<Student>>("");

    if (users != null)
    {
        foreach (var user in users)
        {
            Console.WriteLine($"{user.Email} - {user.FullName} - {user.StudentId} - {user.Phone}");
        }
    }
}

public class Student
{
    public int StudentId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public string Status { get; set; } = null!;

    public DateOnly JoinDate { get; set; }
}