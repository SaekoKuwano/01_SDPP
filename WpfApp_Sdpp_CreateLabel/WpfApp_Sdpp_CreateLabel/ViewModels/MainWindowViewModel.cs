using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace WpfApp_Sdpp_CreateLabel.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        //----------------------------
        // 初期設定
        //----------------------------
        // Regions
        private readonly IRegionManager _regionManager;

        //----------------------------
        // Window_Title
        //----------------------------
        private string _title = "組立ラベル発行";

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        //----------------------------
        // Button
        //----------------------------
        public DelegateCommand<string> CreateCommand { get; private set; }

        //----------------------------
        // コンストラクター
        //----------------------------
        public MainWindowViewModel(IRegionManager regionManager)
        {
            // Regions
            _regionManager = regionManager;

            // Button
            CreateCommand = new DelegateCommand<string>(Create);
        }

        //********************************************************************************
        //----------------------------
        // Button_Action
        //----------------------------
        private void Create(string CreatePath)
        {
            // Buttonの引数で、Controlを表示する
            if (CreatePath != null)
            {
                _regionManager.RequestNavigate("ContentRegion", CreatePath);
            }
        }
    }
}