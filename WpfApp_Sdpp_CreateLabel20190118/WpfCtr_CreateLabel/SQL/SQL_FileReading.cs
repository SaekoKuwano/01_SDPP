using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace WpfCtr_CreateLabel.SQL
{
    /// <summary>
    /// SQLファイルを読み込む
    /// ※ビルドアクション=>組込みリソース
    /// </summary>
    public class SQL_FileReading
    {
        #region 初期設定

        // 名前空間
        private static string SQL_NameSpace = "WpfCtr_CreateLabel.SQL";

        // [Directory] 01_Select
        private static string SQL_DirSelect = "Select";

        // [Directory] 02_DML
        private static string SQL_DirDML = "DML";

        #endregion 初期設定

        //-----------------------
        // SQL展開
        //-----------------------
        public static string OpenSQLFile_Select(string SQLFilePath)
        {
            // Pathを作成
            // 名前空間.フォルダ名.ファイル名
            string OpenSQLPath = String.Format("{0}.{1}.{2}", SQL_NameSpace, SQL_DirSelect, SQLFilePath);

            // 実行ファイルのAssemblyを取得
            Assembly _assembly = Assembly.GetExecutingAssembly();

            // Assemblyに埋め込まれてる、リソースのStreamを取得
            Stream _stream = _assembly.GetManifestResourceStream(OpenSQLPath);

            // リソースが取得できてるか確認
            if (_stream == null)
            {
                return null;
            }

            // ファイルの中身を展開
            StreamReader _stringReader = new StreamReader(_stream);

            // 文字に変換
            string _sqlReaderSt = _stringReader.ReadToEnd();

            // Streamを閉じる
            _stringReader.Close();

            //-----------------------
            // フォーマット
            //-----------------------
            string S1 = _sqlReaderSt.Replace(Environment.NewLine, " ");

            //var S2 = S1.Split('\r');

            return S1;
        }
    }
}