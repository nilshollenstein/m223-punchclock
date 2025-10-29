using M223PunchclockDotnet.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace M223PunchclockDotnet.Model;

public partial class Entry
{
    public int Id { get; set; }

    public DateTime CheckIn { get; set; }

    public DateTime CheckOut { get; set; }
    [MaxLength(1000)]
    public string? Description { get; set; }
    
    public int? CategoryId { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
}
