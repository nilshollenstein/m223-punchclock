using M223PunchclockDotnet.Model;
using System;
using System.Collections.Generic;

namespace M223PunchclockDotnet.Model;

public partial class Entry
{
    public int Id { get; set; }

    public DateTime CheckIn { get; set; }

    public DateTime CheckOut { get; set; }

    public int? CategoryId { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<TagEntry> TagEntries { get; set; } = new List<TagEntry>();
}
