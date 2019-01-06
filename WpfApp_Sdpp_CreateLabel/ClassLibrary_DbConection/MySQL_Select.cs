using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace ClassLibrary_DbConection
{
    public class MySQL_Select
    {
        // 接続文字
        public static MySqlConnection MySqlInstance;

        /// <summary>
        /// MySQL接続
        /// </summary>
        public static void DatabaseConnect()
        {
            string str = "Server=127.0.0.1;User ID=root;Password=root;Database=sdpp";

            MySqlInstance = new MySqlConnection(str);

            try
            {
                MySqlInstance.Open();

                // ServerVer
                //Console.WriteLine(MySqlInstance.ServerVersion);
            }
            catch (Exception ex)
            {
                // 例外の説明を表示する
                Console.WriteLine(ex.Message);

                // DBを切断する
                DatabaseClose();

                // 処理を中止する
                return;
            }
        }

        /// <summary>
        /// MySQL検索
        /// </summary>
        /// <param name="SqlCmd"></param>
        /// <returns></returns>
        public static DataSet DataBaseExcute(string SqlCmd)
        {
            try
            {
                // SQL 実行
                MySqlCommand cmd = new MySqlCommand(SqlCmd, MySqlInstance);

                // DataSet
                MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                ada.Fill(ds, "Product");

                // 抽出結果を返す
                return ds;
            }
            catch (Exception ex)
            {
                // 例外の説明を表示する
                Console.WriteLine(ex.Message);

                // DBを切断する
                DatabaseClose();

                // 抽出結果を返す
                return null;
            }
        }

        /// <summary>
        /// MySQL接続解除
        /// </summary>
        public static void DatabaseClose()
        {
            MySqlInstance.Dispose();
            MySqlInstance.Close();
        }
    }
}