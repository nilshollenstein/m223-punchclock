using M223PunchclockDotnet.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace M223PunchclockDotnet.Model;

public class Category
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Title { get; set; } = null!;
    [JsonIgnore] 
    public virtual ICollection<Entry> Entries { get; set; } = new List<Entry>();
}
