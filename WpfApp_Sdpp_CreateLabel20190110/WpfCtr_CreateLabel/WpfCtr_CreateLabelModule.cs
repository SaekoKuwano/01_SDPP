using Prism.Ioc;
using Prism.Modularity;
using WpfCtr_CreateLabel.Views;

namespace WpfCtr_CreateLabel
{
    public class WpfCtr_CreateLabelModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NewCreateLabel>();
            containerRegistry.RegisterForNavigation<AgainCreateLabel>();
            containerRegistry.RegisterForNavigation<UserAuthentication>();
        }
    }
}