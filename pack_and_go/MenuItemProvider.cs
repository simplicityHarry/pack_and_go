namespace PackAndGoPlugin
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Threading.Tasks;

    using Elektrobit.Guide.Studio.Workbench;
    using Elektrobit.Guide.Studio.Workbench.ViewModels;
    using Elektrobit.Guide.Ui.ViewModels;

    using Prism.Commands;

    using ReactiveUI;

    using Resources = PackAndGoPlugin.Properties.Resources;

    [Export(typeof(IMenuItemProvider))]
    internal class MenuItemProvider : IMenuItemProvider
    {
        private readonly IMenuFactory _menuFactory;

        private readonly IPackAndGo _packAndGo;

        // We're providing a new menu for the main menu bar
        public string MenuId => WorkbenchMenus.MAIN_MENU_BAR;

        [ImportingConstructor]
        public MenuItemProvider(IMenuFactory menuFactory, IPackAndGo packAndGo)
        {
            _menuFactory = menuFactory;
            _packAndGo = packAndGo;
        }

        public IEnumerable<IMenuItemViewModel> CreateMenuItems(object context)
        {
            var items = new List<IMenuItemViewModel>()
            {
                new MenuItemViewModel() {Header = Resources.MenuEntry_CreateZip, Command = ReactiveCommand.CreateFromTask<(IWorkbenchViewModel context, bool useMonitor)>(_packAndGo.Run), CommandParameter = (context:context as IWorkbenchViewModel, useMonitor:false)},
                new MenuItemViewModel() {Header = Resources.MenuEntry_CreateZipWithMonitor, Command = ReactiveCommand.CreateFromTask<(IWorkbenchViewModel context, bool useMonitor)>(_packAndGo.Run), CommandParameter = (context:context as IWorkbenchViewModel, useMonitor:true)}
            };
            // Create the items of the custom actions menu by using the IMenuFactory interface.
            // This will call the IMenuItemProvider implementations registered for the custom menu ID.
            yield return new MenuItemViewModel
            {
                 Header = Resources.MenuEntry_PackAndGo,
                 Items = items
            };          
        }
    }
}
