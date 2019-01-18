using System.Windows;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using WpfApp_Sdpp_CreateLabel.Views;

namespace WpfApp_Sdpp_CreateLabel
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);

            moduleCatalog.AddModule<WpfCtr_CreateLabel.WpfCtr_CreateLabelModule>();
        }
    }
}