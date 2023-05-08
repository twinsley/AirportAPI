using System;
using System.Collections.Generic;

namespace AirportAPI.Entities;

public partial class Wxdatum
{
    public long? Id { get; set; }

    public float? Humidity { get; set; }

    public float? Temperature { get; set; }

    public float? Windspeed { get; set; }

    public float? Winddirection { get; set; }

    public DateTime? Date { get; set; }

    public string? Identifier { get; set; }
}
