using System.Windows;

namespace Carculator;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        // Set default culture
        Thread.CurrentThread.CurrentUICulture = 
            new System.Globalization.CultureInfo("en");
    }

}