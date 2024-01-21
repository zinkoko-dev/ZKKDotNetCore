using System;
using System.Collections.Generic;

namespace ZKKDotNetCore.DbFirstCommandApp.Models;

public partial class TblStudent
{
    public int StudentId { get; set; }

    public string StudentName { get; set; } = null!;

    public string StudentCity { get; set; } = null!;

    public string StudentGender { get; set; } = null!;
}
