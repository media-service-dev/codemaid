using SteveCadwallader.CodeMaid.UI;

namespace SteveCadwallader.CodeMaid.Helpers.AccessModifier;

public class GroupOrderSetting : Bindable
{
    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="AccessModifierOrderSetting"/> class.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="order">The order.</param>
    public GroupOrderSetting(string name, int order)
    {
        Name = name;
        Order = order;
    }

    #endregion Constructors

    #region Properties

    /// <summary>
    /// Gets or sets the name associated with this group.
    /// </summary>
    public string Name
    {
        get => GetPropertyValue<string>();
        set => SetPropertyValue(value);
    }

    /// <summary>
    /// Gets or sets the order associated with this group.
    /// </summary>
    public int Order
    {
        get => GetPropertyValue<int>();
        set => SetPropertyValue(value);
    }

    #endregion Properties
}
