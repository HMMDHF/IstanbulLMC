using System;
using System.Collections.Generic;

namespace IstanbulLMC.Models;

public partial class VehicleCategory
{
    public int ID { get; set; }

    public string Name { get; set; } = null!;

    public int SeateCount { get; set; }

    public int? SuitcaseCount { get; set; }

    public decimal MaxDistance { get; set; }

    public decimal KMPrice { get; set; }

    public string? Image { get; set; }

    public bool IsActive { get; set; }

    public string? InsertID { get; set; }

    public int? InsertDate { get; set; }

    public string? UpdateID { get; set; }

    public int? UpdateDate { get; set; }

    public virtual ICollection<Transfer> Transfer { get; set; } = new List<Transfer>();
}
