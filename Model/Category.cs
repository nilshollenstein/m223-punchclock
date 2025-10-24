using M223PunchclockDotnet.Model;
using System;
using System.Collections.Generic;

namespace M223PunchclockDotnet.Model;

public partial class Category
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Entry> Entries { get; set; } = new List<Entry>();
}
