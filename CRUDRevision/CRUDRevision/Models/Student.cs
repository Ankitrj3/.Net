using System;
using System.Collections.Generic;

namespace CRUDRevision.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public DateTime? CreatedDate { get; set; }
}
