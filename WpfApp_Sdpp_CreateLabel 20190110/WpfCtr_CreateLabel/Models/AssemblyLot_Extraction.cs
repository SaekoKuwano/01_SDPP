using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Data;
using ClassLibrary_DbConection;
using WpfCtr_CreateLabel.SQL;

namespace WpfCtr_CreateLabel.Models
{
    public class AssemblyLot_Extraction
    {
        #region 情報受取

        public static string NM_ProName;    // 装置名
        public static string NM_Stage;      // ステージ

        #endregion 情報受取

        #region 組立ロット抽出

        /// <summary>
        /// 組立ロット抽出（最新）
        /// </summary>
        /// <returns></returns>
        public static DataTable MySQL_AssemblyLot_Extra()
        {
            #region SQL作成

            // SQLファイル名
            string SQL_LtDt = "MySQL_FML_LatestData_Lot.sql";

            // SQL展開
            string SQL_AssemblyLot_Now_St = SQL_FileReading.OpenSQLFile_Select(SQL_LtDt);

            // SQL置換
            string SQL_AssemblyLot_Now_Qy = SQL_AssemblyLot_Now_St;
            SQL_AssemblyLot_Now_Qy = Regex.Replace(SQL_AssemblyLot_Now_Qy, "_proNm", NM_ProName);
            SQL_AssemblyLot_Now_Qy = Regex.Replace(SQL_AssemblyLot_Now_Qy, "_sTage", NM_Stage);

            #endregion SQL作成

            #region MySQL実行

            // DB接続開始
            MySQL_Select.DatabaseConnect();

            // SQL実行
            DataTable MySQL_DataSet = MySQL_Select.DataBaseExcute(SQL_AssemblyLot_Now_Qy);

            // DB接続終了
            MySQL_Select.DatabaseClose();

            #endregion MySQL実行

            //---------------------
            // データアクセス
            //---------------------
            // DataSetからテーブルを取得する
            //DataTable AssemblyLot_Tbl = MySQL_DataSet.Tables["Product"];

            // 戻り値
            return MySQL_DataSet;
        }

        #endregion 組立ロット抽出
    }
}