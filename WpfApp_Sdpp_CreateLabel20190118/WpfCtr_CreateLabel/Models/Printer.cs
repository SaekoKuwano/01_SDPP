using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Printing;
using System.Windows.Xps;
using System.Windows.Documents;
using System.Windows.Markup;
using WpfCtr_CreateLabel.ViewModels;
using WpfCtr_CreateLabel.Views;
using System.Text.RegularExpressions;
using System.Collections;

namespace WpfCtr_CreateLabel.Models
{
    public class Printer
    {
        public static void Print(PrintOut_LabelViewModel viewModel)
        {
            var fixedDoc = new FixedDocument();

            var objReportToPrint = new PrintOut_Label();

            var ReportToPrint = objReportToPrint as UserControl;
            ReportToPrint.DataContext = viewModel;

            var pageContent = new PageContent();
            var fixedPage = new FixedPage();

            //Create first page of document
            fixedPage.Children.Add(ReportToPrint);
            ((IAddChild)pageContent).AddChild(fixedPage);
            fixedDoc.Pages.Add(pageContent);

            SendFixedDocumentToPrinter(fixedDoc);
        }

        private static void SendFixedDocumentToPrinter(FixedDocument fixedDocument)
        {
            XpsDocumentWriter xpsdw;

            PrintDocumentImageableArea imgArea = null;
            //こちらのオーバーロードだと、プリンタ選択ダイアログが出る。
            xpsdw = PrintQueue.CreateXpsDocumentWriter(ref imgArea);

            //var ps = new LocalPrintServer();
            //var pq = ps.DefaultPrintQueue;
            //こちらのオーバーロードだと、プリンタ選択ダイアログを飛ばして既定のプリンタにスプールされる
            //xpsdw = PrintQueue.CreateXpsDocumentWriter(pq);
            xpsdw.Write(fixedDocument);

            // インストールされているプリンタ一覧を取得
            var ichiran = System.Drawing.Printing.PrinterSettings.InstalledPrinters;

            IEnumerator enu = ichiran.GetEnumerator();

            // 指定プリンタ名を取得
            while (enu.MoveNext())
            {
                if (Regex.IsMatch(enu.Current.ToString(), "Microsoft XPS Document Writer"))
                {
                    var strin = enu.Current.ToString();
                }
            }
        }
    }
}