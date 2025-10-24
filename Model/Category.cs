using M223PunchclockDotnet.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace M223PunchclockDotnet.Model;

public partial class Category
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Title { get; set; } = null!;

    public virtual ICollection<Entry> Entries { get; set; } = new List<Entry>();
}
