using System;
using System.Collections.Generic;

namespace AirportAPI.Entities;

public partial class Formdatum
{
    public int Id? { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? Airport { get; set; }

    public string? Comments { get; set; }
}
