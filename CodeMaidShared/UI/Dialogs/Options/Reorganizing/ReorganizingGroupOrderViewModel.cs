using SteveCadwallader.CodeMaid.Helpers;
using SteveCadwallader.CodeMaid.Properties;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using SteveCadwallader.CodeMaid.Helpers.AccessModifier;

namespace SteveCadwallader.CodeMaid.UI.Dialogs.Options.Reorganizing
{
    /// <summary>
    /// The view model for reorganizing types options.
    /// </summary>
    public class ReorganizingGroupOrderViewModel : OptionsPageViewModel
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ReorganizingTypesViewModel" /> class.
        /// </summary>
        /// <param name="package">The hosting package.</param>
        /// <param name="activeSettings">The active settings.</param>
        public ReorganizingGroupOrderViewModel(CodeMaidPackage package, Settings activeSettings)
            : base(package, activeSettings)
        {
            Mappings = new SettingsToOptionsList(ActiveSettings, this)
            {
                new SettingToOptionMapping<string, GroupOrderListSetting>(x => ActiveSettings.Reorganizing_GroupOrderList, x => GroupList),
            };
        }

        #endregion Constructors

        #region Overrides of OptionsPageViewModel

        /// <summary>
        /// Gets the header.
        /// </summary>
        public override string Header => "Group Order";

        /// <summary>
        /// Loads the settings.
        /// </summary>
        public override void LoadSettings()
        {
            base.LoadSettings();

            CreateGroupOrderListCurrentState();
        }

        #endregion Overrides of OptionsPageViewModel

        #region Options

        /// <summary>
        /// Gets or sets the settings associated with public.
        /// </summary>
        protected GroupOrderListSetting GroupList { get; set; }

        #endregion Options

        #region Logic

        /// <summary>
        /// Gets an observable collection of the types.
        /// </summary>
        public ObservableCollection<object> GroupOrderList
        {
            get => GetPropertyValue<ObservableCollection<object>>();
            private set => SetPropertyValue(value);
        }

        /// <summary>
        /// Creates the member types collection from the current state.
        /// </summary>
        private void CreateGroupOrderListCurrentState()
        {
            GroupOrderList = new ObservableCollection<object>(GroupList.OrderBy(x => x.Order));

            GroupOrderList.CollectionChanged += (_, _) => UpdateGroupOrderSettings();
        }

        /// <summary>
        /// Updates the member type settings based on the current collection state.
        /// </summary>
        private void UpdateGroupOrderSettings()
        {
            for (var index = 0; index < GroupOrderList.Count; index++)
            {
                var groupOrder = GroupOrderList[index];

                if (groupOrder is GroupOrderSetting groupOrderSetting && groupOrderSetting.Order != index)
                {
                    groupOrderSetting.Order = index;
                    RaisePropertyChanged();
                }
            }

        }

        #endregion Logic
    }
}
