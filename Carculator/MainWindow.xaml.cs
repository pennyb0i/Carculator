using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;
using Carculator.Models;
using Microsoft.Win32;

namespace Carculator;

public partial class MainWindow : Window
{
    private IList<CarSale> CarSales = [];
    public ObservableCollection<CarSaleSummary> CarSaleSummaries { get; set; } = [];

    public MainWindow()
    {
        InitializeComponent();
        UpdateButtonVisibility();
        DataContext = this;
    }

    /// <summary>
    /// Handles the event triggered when the "Open XML File" button is clicked.
    /// Opens a file dialog to allow the user to select an XML file containing car sales data, deserializes the data,
    /// populates the CarSales list, and generates summaries based on weekend sales.
    /// Displays success or error messages depending on the outcome.
    /// </summary>
    /// <param name="sender">The source of the event, typically the button that was clicked.</param>
    /// <param name="e">The event arguments associated with the button click event.</param>
    private void OpenXmlFile_Click(object sender, RoutedEventArgs e)
    {
        var openFileDialog = new OpenFileDialog
        {
            Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*", 
            Title = "Open XML File" 
        };
    
        if (openFileDialog.ShowDialog() == true)
        {
            try
            {
                var filePath = openFileDialog.FileName;
                var fileContent = File.ReadAllText(filePath);
    
                var serializer = new XmlSerializer(typeof(ObservableCollection<CarSale>),
                    new XmlRootAttribute("CarSales")
                );
                using var stringReader = new StringReader(fileContent);
                CarSales = (IList<CarSale>?)serializer.Deserialize(stringReader) ?? [];

                GenerateCarSaleSummaries([DayOfWeek.Saturday, DayOfWeek.Sunday]);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while opening the file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    /// <summary>
    /// Handles the click event of the Clear button. Clears the CarSales list and the CarSaleSummaries collection,
    /// then updates the visibility of UI elements to reflect the cleared state.
    /// </summary>
    /// <param name="sender">The source of the event, typically the Clear button.</param>
    /// <param name="e">The event data associated with the RoutedEventArgs.</param>
    private void ClearButton_Click(object sender, RoutedEventArgs e)
    {
        CarSales.Clear();
        CarSaleSummaries.Clear();
        UpdateButtonVisibility();
    }
    
    /// <summary>
    /// Handles the change event of the language selector ComboBox.
    /// Updates the application's culture and UI elements to the language corresponding
    /// to the selected item in the ComboBox.
    /// </summary>
    /// <param name="sender">The ComboBox control that raised the event.</param>
    /// <param name="e">The event arguments containing details about the selection change.</param>
    private void LanguageSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedLanguage = ((ComboBoxItem)((ComboBox)sender).SelectedItem).Tag.ToString();
        Localization.TranslationSource.Instance.CurrentCulture = CultureInfo.GetCultureInfo(selectedLanguage ?? "en");
    }

    /// <summary>
    /// Generates summaries of car sales based on the provided day(s) of the week filter, if any.
    /// The summaries are grouped by car model, aggregating the total price, price with VAT, and the count of cars sold.
    /// The generated summaries are stored in the CarSaleSummaries observable collection.
    /// </summary>
    /// <param name="daysOfWeek">An optional list of days of the week to filter the car sales. If null, no filtering is applied.</param>
    private void GenerateCarSaleSummaries(IList<DayOfWeek>? daysOfWeek = null)
    {
        var summaries = CarSales.Where(c => daysOfWeek is null || daysOfWeek.Contains(c.SoldAtUtc.DayOfWeek) )
            .GroupBy(x => x.ModelName).Select(group => new CarSaleSummary
            {
                ModelName = group.Key,
                Price = group.Sum(x => x.Price),
                PriceWithVAT = group.Sum(x => x.PriceWithVAT),
                TotalCount = group.Count()
            });
                
        CarSaleSummaries.Clear();
        foreach (var summary in summaries)
        {
            CarSaleSummaries.Add(summary);
        }
        // Update button visibility once CarSaleSummaries is updated
        UpdateButtonVisibility();
    }
    
    /// <summary>
    /// Updates the visibility of UI elements based on the state of the CarSaleSummaries collection.
    /// If CarSaleSummaries is empty, shows the ImportPanel for importing data and hides the data grid.
    /// If CarSaleSummaries has items, hides the ImportPanel and displays the CarSaleSummariesTable to show the data.
    /// </summary>
    private void UpdateButtonVisibility()
    {
        ImportPanel.Visibility = CarSaleSummaries.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
        CarSaleSummariesTable.Visibility = CarSaleSummaries.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
        ClearButton.Visibility = CarSaleSummaries.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
    }
}