﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfCtr_CreateLabel.Views
{
    /// <summary>
    /// NewCreateLabel.xaml の相互作用ロジック
    /// </summary>
    public partial class NewCreateLabel : UserControl
    {
        public NewCreateLabel()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //アプリケーションを強制終了します
            Environment.Exit(0);
        }
    }
}