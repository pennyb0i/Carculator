using System.Windows.Data;
using Carculator.Localization;

namespace Carculator.Extensions;

public class LocExtension : Binding
{
    public LocExtension(string name) : base("[" + name + "]")
    {
        Mode = BindingMode.OneWay;
        Source = TranslationSource.Instance;
    }
}