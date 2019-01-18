using System;
using System.Linq;
using System.Data;
using System.Collections.ObjectModel;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using Prism.Regions;
using Reactive.Bindings;
using WpfCtr_CreateLabel.Models;
using WpfCtr_CreateLabel.Views;

namespace WpfCtr_CreateLabel.ViewModels
{
    public class NewCreateLabelViewModel : BindableBase, INavigationAware
    {
        #region PopUP(ユーザ認証)

        public ReactiveProperty<string> OwnerInputValue { get; } = new ReactiveProperty<string>();

        public InteractionRequest<UserAuthenticationNotification> InputinteractionRequest { get; set; } = new InteractionRequest<UserAuthenticationNotification>();

        public DelegateCommand UserAuthenCommand { get; set; }

        #endregion PopUP(ユーザ認証)

        #region Input情報取得

        //--------------------------
        // [装置情報]
        //--------------------------
        // コンボボックス - アイテム
        private ObservableCollection<FMLProNameInfo> _fmlItem;

        public ObservableCollection<FMLProNameInfo> FmlItem
        {
            get { return _fmlItem; }
            set { SetProperty(ref _fmlItem, value); }
        }

        // コンボボックス - 選択
        private FMLProNameInfo _fmlSelectItem;

        public FMLProNameInfo FmlSelectItem
        {
            get { return _fmlSelectItem; }
            set { SetProperty(ref _fmlSelectItem, value); }
        }

        //--------------------------
        // [完成品情報]
        //--------------------------
        // 組立ロット名
        private string _fml_LotID;

        public string Fml_LotID
        {
            get { return _fml_LotID; }
            set { SetProperty(ref _fml_LotID, value); }
        }

        // ワークシリアル
        private string _fml_WorkSerial;

        public string Fml_WorkSerial
        {
            get { return _fml_WorkSerial; }
            set { SetProperty(ref _fml_WorkSerial, value); }
        }

        #endregion Input情報取得

        //--------------------------
        // 処理内容テキスト
        //--------------------------
        public ReactiveProperty<string> ComentText { get; private set; } = new ReactiveProperty<string>();

        //--------------------------
        // ラジオボタン
        //--------------------------
        public enum TestEnum { dummy, Left, Right }

        public TestEnum RadioEnum { get; set; }

        //--------------------------
        // Button
        //--------------------------
        // 装置側
        public DelegateCommand<string> AssemblyCommand { get; private set; }

        //--------------------------
        // DataGrid
        //--------------------------
        private ObservableCollection<FMLDataTabl> _fmlAddData;

        public ObservableCollection<FMLDataTabl> FmlAddData
        {
            get { return _fmlAddData; }
            set
            {
                if (_fmlAddData == value)
                    return;
                _fmlAddData = value;
                RaisePropertyChanged("FmlAddData");
            }
        }

        private DataTable Get_AssemblyLot;

        //--------------------------
        // 処理内容背景色
        //--------------------------
        private string _borderColor;

        public string BorderColor
        {
            get { return _borderColor; }
            set { SetProperty(ref _borderColor, value); }
        }

        //--------------------------
        // IRegionManager
        //--------------------------
        public IRegionManager _regionManager { get; set; }

        #region コンストラクター

        //--------------------------
        // コンストラクター
        //--------------------------
        public NewCreateLabelViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            // ComboBox
            Create_Combo_FlmList();

            // 装置検索ボタン
            AssemblyCommand = new DelegateCommand<string>(Button_Assembly);

            // 確定ボタン
            UserAuthenCommand = new DelegateCommand(Button_Authen);
        }

        #endregion コンストラクター

        //--------------------------
        // Button処理
        //--------------------------

        #region ロット情報抽出

        // 検索処理
        private void Button_Assembly(string assembly)
        {
            // 処理内容をNullにする
            ComentText.Value = null;

            // 装置側[ProNameInfo]
            if (assembly == "ProNameInfo")
            {
                #region << 装置 >>

                //----------------------
                // 選択確認
                //----------------------
                // 装置名
                if (FmlSelectItem == null)
                {
                    // エラーコメント欄に記入
                    ComentText.Value = " Error：：[装置情報] 装置名を選択してください。";

                    // 処理を中止
                    return;
                }

                // ステージ
                if (RadioEnum.ToString() == "dummy")
                {
                    // エラーコメント欄に記入
                    ComentText.Value = " Error：：[ステージ位置] 選択してください。";

                    // 処理を中止
                    return;
                }

                //----------------------
                // ロット情報抽出
                //----------------------
                // SQL FileName
                AssemblyLot_Extraction.FM_SQL = "MySQL_FML_LatestData_Lot.sql";

                // SQL実行
                // 引数：装置名、ステージ
                Get_AssemblyLot = AssemblyLot_Extraction.MySQL_AssemblyLot_Extra(FmlSelectItem.ProName, RadioEnum.ToString());

                #endregion << 装置 >>
            }
            // 完成品[AssemblyInfo]
            else if (assembly == "AssemblyInfo")
            {
                // nullチェック
                if (String.IsNullOrEmpty(_fml_LotID))
                {
                    // エラーコメント欄に記入
                    ComentText.Value = " Error：：[組立ロット名] 組立ロット名が入力されていません。";

                    // DataGrid値をクリア
                    FmlAddData.Clear();

                    // 処理を中止
                    return;
                }

                //----------------------
                // ロット情報抽出
                //----------------------
                // SQL FileName
                AssemblyLot_Extraction.FM_SQL = "MySQL_FML_Assembly_Lot.sql";

                // SQL実行
                // 引数：装置名、ステージ
                Get_AssemblyLot = AssemblyLot_Extraction.MySQL_AssemblyLot_Comp_Lot(_fml_LotID);
            }

            //--------------------------
            // DataGridに表示
            //--------------------------
            var data = new ObservableCollection<FMLDataTabl>();

            foreach (DataRow item in Get_AssemblyLot.Rows)
            {
                // Null確認
                if (item.ItemArray[0].ToString() == "")
                {
                    // エラーコメント欄に記入
                    ComentText.Value = " Error：：[装置情報] 最新の組立ロットを抽出できませんでした。選択条件を再度確認してください。";

                    // DataGrid値をクリア
                    FmlAddData.Clear();

                    // 処理を中止
                    return;
                }

                // 値を入れる
                data.Add(new FMLDataTabl()
                {
                    FmlAddData = Get_AssemblyLot.DefaultView,

                    ProName = item.ItemArray[0].ToString(),
                    AssemblyLot = item.ItemArray[1].ToString(),
                    Cnt = int.Parse(item.ItemArray[2].ToString()),
                    KouSei = item.ItemArray[3].ToString(),
                    DtPro = DateTime.Parse(item.ItemArray[4].ToString())
                });
            }

            // DataGridに反映する
            FmlAddData = data;
        }

        #endregion ロット情報抽出

        #region Button_確定

        //--------------------------
        // Button_確定
        //--------------------------
        public void Button_Authen()
        {
            var _userAuthenCommand = new UserAuthenticationNotification() { Title = "user authentication" };

            InputinteractionRequest.Raise(_userAuthenCommand);

            // nullチェック
            if (_userAuthenCommand.Input_UserID != null)
            {
                // PopUPから値を受け取り
                OwnerInputValue.Value = _userAuthenCommand.Input_UserID;

                // 出力
                Console.WriteLine(OwnerInputValue.Value);
                Console.WriteLine(_userAuthenCommand.Input_PassWD);
            }
            else
            {
                Console.WriteLine("認証 null");
            }

            _regionManager.RequestNavigate("ContentRegion", nameof(PrintOut_Label));
        }

        #endregion Button_確定

        //--------------------------
        // コンボボックス
        //--------------------------
        private void Create_Combo_FlmList()
        {
            var Items = new ObservableCollection<FMLProNameInfo>();
            for (int i = 1; i < 4; i++)
            {
                Items.Add(new FMLProNameInfo()
                {
                    ProName = String.Format("FML-{0}", i)
                });
            }

            // 値を格納する
            FmlItem = Items;
        }

        #region IsNavigationTargetメソッド

        //--------------------------
        // IsNavigationTarget
        //--------------------------
        // 画面に遷移してきたときに呼び出されます
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
        }

        // 引数で渡されたコンテキストのターゲットとなる画面かどうかを返します
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        // 画面から離れるときに呼び出されます
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            PrintOut_LabelViewModel.Lotid = FmlAddData[0].AssemblyLot;
            PrintOut_LabelViewModel.ProName = FmlAddData[0].ProName;
            PrintOut_LabelViewModel.Cnt = int.Parse(FmlAddData[0].Cnt.ToString());
            PrintOut_LabelViewModel.Flg = FmlAddData[0].KouSei;
        }

        #endregion IsNavigationTargetメソッド
    }
}