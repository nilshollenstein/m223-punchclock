using M223PunchclockDotnet.Model;
using System;
using System.Collections.Generic;

namespace M223PunchclockDotnet.Model;

public partial class TagEntry
{
    public int TagEntryId { get; set; }

    public int TagId { get; set; }

    public int EntryId { get; set; }

    public virtual Entry Entry { get; set; } = null!;

    public virtual Tag Tag { get; set; } = null!;
}
