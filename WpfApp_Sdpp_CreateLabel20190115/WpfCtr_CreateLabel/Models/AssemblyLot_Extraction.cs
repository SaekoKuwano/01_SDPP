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

        public static string NM_Work;       // ワークシリアル
        public static string FM_SQL;        // SQLファイル名

        #endregion 情報受取

        #region 装置情報から最新ロットを抽出

        /// <summary>
        /// 装置情報から最新の組立ロットを抽出
        /// </summary>
        /// <returns>MySQL_DataSet</returns>
        public static DataTable MySQL_AssemblyLot_Extra(string NM_ProName, string NM_Stage)
        {
            #region SQL作成

            // SQLファイル名
            string SQL_LtDt = FM_SQL;

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

            // 戻り値
            return MySQL_DataSet;
        }

        #endregion 装置情報から最新ロットを抽出

        #region 組立ロットから情報抽出

        /// <summary>
        /// 組立ロット名から情報を抽出
        /// </summary>
        /// <returns>MySQL_DataSet</returns>
        public static DataTable MySQL_AssemblyLot_Comp_Lot(string NM_LotID)
        {
            #region SQL作成

            // SQLファイル名
            string SQL_LtDt = FM_SQL;

            // SQL展開
            string SQL_AssemblyLot_Now_St = SQL_FileReading.OpenSQLFile_Select(SQL_LtDt);

            // SQL置換
            string SQL_AssemblyLot_Now_Qy = SQL_AssemblyLot_Now_St;
            SQL_AssemblyLot_Now_Qy = Regex.Replace(SQL_AssemblyLot_Now_Qy, "_LotId", NM_LotID);

            #endregion SQL作成

            #region MySQL実行

            // DB接続開始
            MySQL_Select.DatabaseConnect();

            // SQL実行
            DataTable MySQL_DataSet = MySQL_Select.DataBaseExcute(SQL_AssemblyLot_Now_Qy);

            // DB接続終了
            MySQL_Select.DatabaseClose();

            #endregion MySQL実行

            // 戻り値
            return MySQL_DataSet;
        }

        #endregion 組立ロットから情報抽出
    }
}