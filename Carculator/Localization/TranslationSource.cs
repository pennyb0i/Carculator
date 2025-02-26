using System.ComponentModel;
using System.Globalization;
using System.Resources;

namespace Carculator.Localization;

public class TranslationSource : INotifyPropertyChanged
{
    private static readonly TranslationSource instance = new();

    public static TranslationSource Instance => instance;

    private readonly ResourceManager _resManager = Strings.ResourceManager;
    private CultureInfo currentCulture;

    public string this[string key] => _resManager.GetString(key, currentCulture) ?? "";

    public CultureInfo CurrentCulture
    {
        get => currentCulture;
        set
        {
            if (!Equals(currentCulture, value))
            {
                currentCulture = value;
                var @event = PropertyChanged;
                if (@event != null)
                {
                    @event.Invoke(this, new PropertyChangedEventArgs(string.Empty));
                }
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
}