using System;
using System.Collections.Generic;

namespace MusicDb;

public partial class Artist
{
    public int Id { get; set; }

    public string ArtistName { get; set; } = null!;

    public virtual ICollection<Record> Records { get; set; } = new List<Record>();
}
