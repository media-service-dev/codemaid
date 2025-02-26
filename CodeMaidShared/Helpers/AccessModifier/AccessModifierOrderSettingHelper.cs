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
    public static class AccessModifierOrderSettingHelper
    {
        #region Fields

        private static readonly CachedSetting<Dictionary<string, AccessModifierOrderSetting>> AccessModifierOrder;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// The static initializer for the <see cref="AccessModifierOrderSettingHelper"/> class.
        /// </summary>
        static AccessModifierOrderSettingHelper()
        {
            AccessModifierOrder = new CachedSetting<Dictionary<string, AccessModifierOrderSetting>>(() => Settings.Default.Reorganizing_AccessModifierList, ParseAccessModifierOrderList);
        }

        #endregion Constructors

        #region Properties

        internal static Dictionary<string, AccessModifierOrderSetting> ParseAccessModifierOrderList(string serializedSettingsOrder)
        {
            var accessModifierStrings = serializedSettingsOrder.Split(new[] { "||" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .Where(y => !string.IsNullOrEmpty(y));

            if (Settings.Default.Reorganizing_ReverseOrderByAccessLevel)
            {
                accessModifierStrings = accessModifierStrings.Reverse();
            }

            var accessModifierList = accessModifierStrings.Select((value, index) => new AccessModifierOrderSetting(value, index))
                .ToDictionary(setting => setting.Name);

            return accessModifierList;
        }

        /// <summary>
        /// Gets the settings associated with the <see cref="KindCodeItem.Class"/> type.
        /// </summary>
        public static AccessModifierOrderListSetting AccessModifierOrderList => new(AccessModifierOrder.Value.Values);

        #endregion Properties

        #region Methods

        /// <summary>
        /// Looks up the <see cref="AccessModifierOrderSetting"/> associated with the specified vsCMAccess, otherwise null.
        /// </summary>
        /// <param name="cmAccess">Visual Studio representation of a access modifier.</param>
        /// <returns>The associated <see cref="AccessModifierOrderSetting"/>, otherwise null.</returns>
        public static AccessModifierOrderSetting LookupByVsCmAccess(vsCMAccess cmAccess)
        {
            var accessModifier = CmAccessToAccessModifier(cmAccess);

            if (accessModifier == null)
            {
                return null;
            }

            return AccessModifierOrder.Value.TryGetValue(accessModifier, out var accessModifierOrder) ? accessModifierOrder : null;
        }

        private static string CmAccessToAccessModifier(vsCMAccess cmAccess)
        {
            switch (cmAccess)
            {
                case vsCMAccess.vsCMAccessPublic:
                {
                    return "Public";
                }
                case vsCMAccess.vsCMAccessPrivate:
                {
                    return "Private";
                }
                case vsCMAccess.vsCMAccessProject:
                {
                    return "Internal";
                }
                case vsCMAccess.vsCMAccessProtected:
                {
                    return "Protected";
                }
                case vsCMAccess.vsCMAccessProjectOrProtected:
                {
                    return "ProtectedInternal";
                }
                case vsCMAccess.vsCMAccessDefault:
                case vsCMAccess.vsCMAccessAssemblyOrFamily:
                case vsCMAccess.vsCMAccessWithEvents:
                default:
                    return null;
            }
        }

        #endregion Methods
    }
}