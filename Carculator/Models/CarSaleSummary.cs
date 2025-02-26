namespace Carculator.Models;

/// <summary>
/// Represents a summary of car sales, including the car model, price,
/// price with VAT, and total number of cars sold.
/// </summary>
public class CarSaleSummary
{
    /// <summary>
    /// Name of the car model associated with the sales summary.
    /// <para/> Also serves as the key.
    /// </summary>
    public required string ModelName { get; set; }
    /// <summary>
    /// Summarized price of the cars sold.
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// Summarized price of the cars sold, with VAT included.
    /// </summary>
    public double PriceWithVAT { get; set; }
    /// <summary>
    /// Total count of the cars sold
    /// </summary>
    public double TotalCount { get; set; }
}