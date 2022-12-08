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
    public class ReorganizingAccessModifierViewModel : OptionsPageViewModel
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ReorganizingTypesViewModel" /> class.
        /// </summary>
        /// <param name="package">The hosting package.</param>
        /// <param name="activeSettings">The active settings.</param>
        public ReorganizingAccessModifierViewModel(CodeMaidPackage package, Settings activeSettings)
            : base(package, activeSettings)
        {
            Mappings = new SettingsToOptionsList(ActiveSettings, this)
            {
                new SettingToOptionMapping<string, AccessModifierOrderListSetting>(x => ActiveSettings.Reorganizing_AccessModifierList, x => AccessModifierList),
            };
        }

        #endregion Constructors

        #region Overrides of OptionsPageViewModel

        /// <summary>
        /// Gets the header.
        /// </summary>
        public override string Header => Resources.ReorganizingAccessModifierViewModel_AccessModifiers;

        /// <summary>
        /// Loads the settings.
        /// </summary>
        public override void LoadSettings()
        {
            base.LoadSettings();

            CreateAccessModifierListCurrentState();
        }

        #endregion Overrides of OptionsPageViewModel

        #region Options

        /// <summary>
        /// Gets or sets the settings associated with public.
        /// </summary>
        protected AccessModifierOrderListSetting AccessModifierList { get; set; }

        #endregion Options

        #region Logic

        /// <summary>
        /// Gets an observable collection of the types.
        /// </summary>
        public ObservableCollection<object> AccessModifierOrderList
        {
            get => GetPropertyValue<ObservableCollection<object>>();
            private set => SetPropertyValue(value);
        }

        /// <summary>
        /// Creates the member types collection from the current state.
        /// </summary>
        private void CreateAccessModifierListCurrentState()
        {
            AccessModifierOrderList = new ObservableCollection<object>(AccessModifierList.OrderBy(x => x.Order));

            AccessModifierOrderList.CollectionChanged += (_, _) => UpdateAccessModifierSettings();
        }

        /// <summary>
        /// Updates the member type settings based on the current collection state.
        /// </summary>
        private void UpdateAccessModifierSettings()
        {
            for (var index = 0; index < AccessModifierOrderList.Count; index++)
            {
                var accessModifier = AccessModifierOrderList[index];

                if (accessModifier is AccessModifierOrderSetting accessModifierOrderSetting && accessModifierOrderSetting.Order != index)
                {
                    accessModifierOrderSetting.Order = index;
                    RaisePropertyChanged();
                }
            }

        }

        #endregion Logic
    }
}
