using System;
using System.Collections.Generic;
using System.Linq;
using EnvDTE;
using SteveCadwallader.CodeMaid.Model.CodeItems;
using SteveCadwallader.CodeMaid.Properties;

namespace SteveCadwallader.CodeMaid.Helpers.AccessModifier
{
    /// <summary>
    /// A helper class that simplifies access to <see cref="MemberTypeSetting"/> instances.
    /// </summary>
    public static class GroupOrderSettingHelper
    {
        #region Fields

        private static readonly CachedSetting<Dictionary<string, GroupOrderSetting>> GroupOrder;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// The static initializer for the <see cref="AccessModifierOrderSettingHelper"/> class.
        /// </summary>
        static GroupOrderSettingHelper()
        {
            GroupOrder = new CachedSetting<Dictionary<string, GroupOrderSetting>>(() => Settings.Default.Reorganizing_GroupOrderList, ParseGroupOrderList);
        }

        #endregion Constructors

        #region Properties

        internal static Dictionary<string, GroupOrderSetting> ParseGroupOrderList(string serializedSettingsOrder)
        {
            var groupOrderStrings = serializedSettingsOrder.Split(new[] { "||" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .Where(y => !string.IsNullOrEmpty(y));

            var groupOrderSettings = groupOrderStrings.Select((value, index) => new GroupOrderSetting(value, index))
                .ToDictionary(setting => setting.Name);

            return groupOrderSettings;
        }

        /// <summary>
        /// Gets the settings associated with the <see cref="KindCodeItem.Class"/> type.
        /// </summary>
        public static GroupOrderListSetting GroupOrderList => new(GroupOrder.Value.Values);

        #endregion Properties
    }
}
