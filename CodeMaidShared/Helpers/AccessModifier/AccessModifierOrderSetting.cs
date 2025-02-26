using SteveCadwallader.CodeMaid.UI;

namespace SteveCadwallader.CodeMaid.Helpers.AccessModifier;

public class AccessModifierOrderSetting : Bindable
{
    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="AccessModifierOrderSetting"/> class.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="order">The order.</param>
    public AccessModifierOrderSetting(string name, int order)
    {
        Name = name;
        Order = order;
    }

    #endregion Constructors

    #region Properties

    /// <summary>
    /// Gets or sets the name associated with this access modifier.
    /// </summary>
    public string Name
    {
        get => GetPropertyValue<string>();
        set => SetPropertyValue(value);
    }

    /// <summary>
    /// Gets or sets the order associated with this access modifier.
    /// </summary>
    public int Order
    {
        get => GetPropertyValue<int>();
        set => SetPropertyValue(value);
    }

    #endregion Properties
}
