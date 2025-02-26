using System.Xml.Serialization;

namespace Carculator.Models;

/// <summary>
/// Represents a car sale, including details such as car name,
/// selling price, VAT, and the date when the sale occurred.
/// This class is used to deserialize XML data related to car sales.
/// </summary>
[XmlType("CarSale")]
public class CarSale
{
    /// <summary>
    /// Name of the car model
    /// </summary>
    [XmlElement("ModelName")] 
    public required string ModelName { get; set; }

    /// <summary>
    /// The date and time when the car was sold, in UTC format.
    /// </summary>
    [XmlElement("SoldAtUtc")]
    public DateTime SoldAtUtc { get; set; }

    /// <summary>
    /// Selling price of the car in the sale, in CZK.
    /// </summary>
    [XmlElement("Price")]
    public double Price { get; set; }

    /// <summary>
    /// Value-added tax (VAT) rate applied to the car sale price, defined as a percentage.
    /// </summary>
    [XmlElement("VAT")]
    public double VAT { get; set; }

    public double PriceWithVAT => Price + Price * VAT / 100;
}