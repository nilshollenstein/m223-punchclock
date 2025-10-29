using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace M223PunchclockDotnet.Model;

public class Tag
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Entry> Entries { get; set; } = new List<Entry>();
}
