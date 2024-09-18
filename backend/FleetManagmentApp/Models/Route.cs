using FleetManagmentApp.Models;

public class Route
{
    public int Id { get; set; }
    public int CarId { get; set; }
    public Car? Car { get; set; }  
    public string StartLocation { get; set; }
    public string EndLocation { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public double Distance { get; set; }
    public double FuelUsed { get; set; }
}
