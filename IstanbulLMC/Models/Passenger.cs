using System;
using System.Collections.Generic;

namespace IstanbulLMC.Models;

public partial class Passenger
{
    public long ID { get; set; }

    public long? TransferID { get; set; }

    public string? Name { get; set; }

    public string? Phone { get; set; }

    public string? PassportNO { get; set; }

    public string? InsertID { get; set; }

    public int? InsertDate { get; set; }

    public string? UpdateID { get; set; }

    public int? UpdateDate { get; set; }

    public virtual Transfer? Transfer { get; set; }
}
