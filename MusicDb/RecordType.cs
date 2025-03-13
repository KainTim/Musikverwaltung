using System;
using System.Collections.Generic;

namespace MusicDb;

public partial class RecordType
{
    public int Id { get; set; }

    public string Descr { get; set; } = null!;

    public virtual ICollection<Record> Records { get; set; } = new List<Record>();
}
