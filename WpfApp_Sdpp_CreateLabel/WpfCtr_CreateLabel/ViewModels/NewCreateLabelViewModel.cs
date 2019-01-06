using System;
using System.Linq;
using System.Data;
using System.Collections.ObjectModel;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using Reactive.Bindings;
using WpfCtr_CreateLabel.Models;
using System.Collections.Generic;

namespace WpfCtr_CreateLabel.ViewModels
{
    public class NewCreateLabelViewModel : BindableBase
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
        public enum TestEnum { Left, Right }

        public TestEnum RadioEnum { get; set; }

        //--------------------------
        // Button
        //--------------------------
        // 装置側
        public DelegateCommand<string> AssemblyCommand { get; private set; }

        //--------------------------
        // DataGrid
        //--------------------------
        public DataSet FmlAddData { get; set; }

        //--------------------------
        // コンストラクター
        //--------------------------
        public NewCreateLabelViewModel()
        {
            // ComboBox
            Create_Combo_FlmList();

            // ラジオボタン

            // 装置検索ボタン
            AssemblyCommand = new DelegateCommand<string>(Button_Assembly);

            // 確定ボタン
            UserAuthenCommand = new DelegateCommand(Button_Authen);
        }

        //--------------------------
        // Button処理
        //--------------------------
        // 検索処理
        private void Button_Assembly(string assembly)
        {
            // 処理内容をNullにする
            ComentText.Value = null;

            // 装置側[ProNameInfo]
            if (assembly == "ProNameInfo")
            {
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

                //----------------------
                // ロット情報抽出
                //----------------------
                // 装置名
                AssemblyLot_Extraction.NM_ProName = FmlSelectItem.ProName;

                // ステージ　RadioEnum
                AssemblyLot_Extraction.NM_Stage = RadioEnum.ToString();

                // SQL実行
                DataSet Get_AssemblyLot = AssemblyLot_Extraction.MySQL_AssemblyLot_Extra();

                // Null確認
                if (Get_AssemblyLot == null)
                {
                    // エラーコメント欄に記入
                    ComentText.Value = " Error：：[装置情報] 最新の組立ロットを抽出できませんでした。選択条件を再度確認してください。";

                    // 処理を中止
                    return;
                }

                //// datagridに表示

                FmlAddData = Get_AssemblyLot;

                //List<FMLAssemblyLotGrid> sources = new List<FMLAssemblyLotGrid>();

                //foreach (DataRow row in Get_AssemblyLot.Rows)
                //{
                //    FMLAssemblyLotGrid f = new FMLAssemblyLotGrid();

                //    F.ProName = row["ProName"].ToString();
                //    F.AssemblyLot = row["AssemblyLot"].ToString();
                //    F.Cnt = int.Parse(row["Cnt"].ToString());
                //    F.KouSei = row["KouSei"].ToString();
                //    F.DtPro = DateTime.Parse(row["DtPro"].ToString());
                //}
            }
        }

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
        }

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
    }
}