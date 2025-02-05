using System;
using System.Collections.Generic;

namespace BlazorApp2.DB;

public partial class TypeMaterial
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public virtual ICollection<Material> Materials { get; set; } = new List<Material>();
}
