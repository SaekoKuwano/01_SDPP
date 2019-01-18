using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Reactive.Bindings;
using System.Windows.Media.Imaging;
using WpfCtr_CreateLabel.Models;

namespace WpfCtr_CreateLabel.ViewModels
{
    public class PrintOut_LabelViewModel : BindableBase, INavigationAware, IRegionMemberLifetime
    {
        public static string Lotid;
        public static string ProName;
        public static string Flg;
        public static int Cnt;

        //---------------------------
        // ロット名
        //---------------------------
        private string _fml_LotID;

        public string Fml_LotID
        {
            get { return _fml_LotID; }
            set { SetProperty(ref _fml_LotID, value); }
        }

        //---------------------------
        // バーコード画像
        //---------------------------
        private BitmapImage _barCodeLot;

        public BitmapImage BarCodeLot
        {
            get { return _barCodeLot; }
            set { SetProperty(ref _barCodeLot, value); }
        }

        //---------------------------
        // 製品名
        //---------------------------
        private string _fml_ProName;

        public string Fml_ProName
        {
            get { return _fml_ProName; }
            set { SetProperty(ref _fml_ProName, value); }
        }

        //---------------------------
        // ロットフラグ
        //---------------------------
        private string _fml_Flg;

        public string Fml_Flg
        {
            get { return _fml_Flg; }
            set { SetProperty(ref _fml_Flg, value); }
        }

        //---------------------------
        // 数量
        //---------------------------
        private int _rml_Cnt;

        public int Fml_Cnt
        {
            get { return _rml_Cnt; }
            set { SetProperty(ref _rml_Cnt, value); }
        }

        //---------------------------
        // Viewライフタイム
        //---------------------------
        public bool KeepAlive { get; set; } = false;

        //---------------------------
        // コンストラクター
        //---------------------------
        public PrintOut_LabelViewModel()
        {
            string fm = "C:\\Users\\ethna\\OneDrive\\デスクトップ\\CODE128test.jpg";

            var source = new BitmapImage();
            source.BeginInit();
            source.UriSource = new Uri(fm);
            source.EndInit();

            BarCodeLot = source;

            // Printアウト
        }

        #region メソッド

        //--------------------------
        // IsNavigationTarget
        //--------------------------
        // 画面に遷移してきたときに呼び出されます
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Fml_LotID = Lotid;
            Fml_ProName = ProName;
            Fml_Flg = Flg;
            Fml_Cnt = Fml_Cnt;

            Print();
        }

        // 引数で渡されたコンテキストのターゲットとなる画面かどうかを返します
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        // 画面から離れるときに呼び出されます
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        #endregion メソッド

        private void Print()
        {
            Printer.Print(this);
        }
    }
}