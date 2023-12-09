using System;
using System.Collections.Generic;

namespace IstanbulLMC.Models;

public partial class Transfer
{
    public long ID { get; set; }

    public int VehicleCategoryID { get; set; }

    public string FromPlaceID { get; set; } = null!;

    public string FromPlace { get; set; } = null!;

    public string ToPlaceID { get; set; } = null!;

    public string ToPlace { get; set; } = null!;

    public decimal KMPrice { get; set; }

    public decimal TotalPrice { get; set; }

    public decimal Distance { get; set; }

    public bool? IsActive { get; set; }

    public bool IsComplated { get; set; }

    public string? InsertID { get; set; }

    public int? InsertDate { get; set; }

    public string? UpdateID { get; set; }

    public int? UpdateDate { get; set; }

    public string? Name { get; set; }

    public string? Tel { get; set; }

    public string? FlieghtNo { get; set; }

    public string? Message { get; set; }

    public string? NO { get; set; }

    public virtual ICollection<Passenger> Passenger { get; set; } = new List<Passenger>();

    public virtual VehicleCategory VehicleCategory { get; set; } = null!;
}
