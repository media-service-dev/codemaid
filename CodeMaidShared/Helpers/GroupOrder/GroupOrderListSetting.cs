using System;
using System.Collections.Generic;
using System.Linq;

namespace SteveCadwallader.CodeMaid.Helpers.AccessModifier;

public class GroupOrderListSetting : List<GroupOrderSetting>
{
    #region Constructors

    /// <inheritdoc />
    public GroupOrderListSetting()
    {
    }

    /// <inheritdoc />
    public GroupOrderListSetting(int capacity) : base(capacity)
    {
    }

    /// <inheritdoc />
    public GroupOrderListSetting(IEnumerable<GroupOrderSetting> collection) : base(collection)
    {
    }

    #endregion Methods

    #region Methods

    /// <summary>
    /// Deserializes the specified string into a new instance of <see cref="GroupOrderListSetting" />.
    /// </summary>
    /// <param name="serializedString">The serialized string to deserialize.</param>
    /// <returns>A new instance of <see cref="GroupOrderListSetting" />.</returns>
    public static explicit operator GroupOrderListSetting(string serializedString)
    {
        try
        {
            var groups = GroupOrderSettingHelper.ParseGroupOrderList(serializedString).Values;

            return new GroupOrderListSetting(groups);
        }
        catch (Exception exception)
        {
            OutputWindowHelper.ExceptionWriteLine("Unable to deserialize group order setting", exception);
            return null;
        }
    }

    /// <summary>
    /// Serializes the specified <see cref="GroupOrderListSetting"/> into a string (e.g. for persistence to settings).
    /// </summary>
    /// <returns>A serialized string representing the object.</returns>
    public static explicit operator string(GroupOrderListSetting groupOrderListSetting)
    {
        var orderedGroups = groupOrderListSetting.OrderBy(setting => setting.Order).Select(setting => setting.Name);

        return string.Join("||", orderedGroups);
    }

    #endregion Methods
}
