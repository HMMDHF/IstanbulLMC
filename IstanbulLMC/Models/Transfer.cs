using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IstanbulLMC.Models;

public partial class Transfer
{
    public long ID { get; set; }

    public int VehicleCategoryID { get; set; }

    [Required(ErrorMessage = " ")]
    public string FromPlaceID { get; set; } = null!;

    [Required(ErrorMessage = " ")]
    public string FromPlace { get; set; } = null!;

    [Required(ErrorMessage = " ")]
    public string ToPlaceID { get; set; } = null!;

    [Required(ErrorMessage = " ")]
    public string ToPlace { get; set; } = null!;

    public decimal KMPrice { get; set; }

    public decimal TotalPrice { get; set; }

    public decimal Distance { get; set; }

    public bool? IsActive { get; set; }

    public bool IsComplated { get; set; }

    public string? InsertID { get; set; }

    public DateTime? InsertDate { get; set; }

    public string? UpdateID { get; set; }

    public int? UpdateDate { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required]
    public string? Tel { get; set; }

    public string? FlieghtNo { get; set; }

    public string? Message { get; set; }

    public string? NO { get; set; }

    [Required(ErrorMessage = " ")]
    public DateTime Date { get; set; }

    public DateTime? RoundTripDate { get; set; }

    public decimal? DriverTrip { get; set; }



    public virtual VehicleCategory VehicleCategory { get; set; } = null!;
    public virtual ICollection<TransferService> TransferServices { get; set; } = new List<TransferService>();
}
