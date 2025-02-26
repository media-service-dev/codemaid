using System;
using System.Collections.Generic;
using System.Linq;

namespace SteveCadwallader.CodeMaid.Helpers.AccessModifier;

public class AccessModifierOrderListSetting : List<AccessModifierOrderSetting>
{
    #region Constructors

    /// <inheritdoc />
    public AccessModifierOrderListSetting()
    {
    }

    /// <inheritdoc />
    public AccessModifierOrderListSetting(int capacity) : base(capacity)
    {
    }

    /// <inheritdoc />
    public AccessModifierOrderListSetting(IEnumerable<AccessModifierOrderSetting> collection) : base(collection)
    {
    }

    #endregion Methods

    #region Methods

    /// <summary>
    /// Deserializes the specified string into a new instance of <see cref="AccessModifierOrderSetting" />.
    /// </summary>
    /// <param name="serializedString">The serialized string to deserialize.</param>
    /// <returns>A new instance of <see cref="AccessModifierOrderSetting" />.</returns>
    public static explicit operator AccessModifierOrderListSetting(string serializedString)
    {
        try
        {
            var accessModifiers = AccessModifierOrderSettingHelper.ParseAccessModifierOrderList(serializedString).Values;

            return new AccessModifierOrderListSetting(accessModifiers);
        }
        catch (Exception exception)
        {
            OutputWindowHelper.ExceptionWriteLine("Unable to deserialize access modifier order setting", exception);
            return null;
        }
    }

    /// <summary>
    /// Serializes the specified <see cref="AccessModifierOrderSetting"/> into a string (e.g. for persistence to settings).
    /// </summary>
    /// <returns>A serialized string representing the object.</returns>
    public static explicit operator string(AccessModifierOrderListSetting accessModifierOrderListSetting)
    {
        var orderedAccessModifiers = accessModifierOrderListSetting.OrderBy(setting => setting.Order).Select(setting => setting.Name);

        return string.Join("||", orderedAccessModifiers);
    }

    #endregion Methods
}
