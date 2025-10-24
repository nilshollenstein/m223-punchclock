using System;
using System.Collections.Generic;

namespace M223PunchclockDotnet.Model;

public partial class Tag
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Entry> Entries { get; set; } = new List<Entry>();
}
