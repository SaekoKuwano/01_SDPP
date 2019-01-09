using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Data;
using System.Data;

namespace WpfCtr_CreateLabel.Models
{
    /// <summary>
    /// 装置名選択
    /// </summary>
    public class FMLProNameInfo : INotifyPropertyChanged
    {
        private string _proName;

        public string ProName
        {
            get { return _proName; }
            set
            {
                _proName = value;
                OnPropertyChanged();
            }
        }

        // INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]string propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
    }

    public class FMLDataTabl : INotifyPropertyChanged
    {
        private DataView _fmlAddData;

        public DataView FmlAddData
        {
            get { return _fmlAddData; }
            set
            {
                _fmlAddData = value;
                OnPropertyChanged("FmlAddData");
            }
        }

        private string _proName;

        public string ProName
        {
            get { return _proName; }
            set
            {
                _proName = value;
                OnPropertyChanged("ProName");
            }
        }

        private string _assemblyLot;

        public string AssemblyLot
        {
            get { return _assemblyLot; }
            set
            {
                _assemblyLot = value;
                OnPropertyChanged("AssemblyLot");
            }
        }

        private int _cnt;

        public int Cnt
        {
            get { return _cnt; }
            set
            {
                _cnt = value;
                OnPropertyChanged("Cnt");
            }
        }

        private string _kouSei;

        public string KouSei
        {
            get { return _kouSei; }
            set
            {
                _kouSei = value;
                OnPropertyChanged("KouSei");
            }
        }

        public DateTime _dtPro;

        public DateTime DtPro
        {
            get { return _dtPro; }
            set
            {
                _dtPro = value;
                OnPropertyChanged("DtPro");
            }
        }

        // INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]string propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
    }

    /// <summary>
    /// ステージ選択_ラジオボタン
    /// </summary>
    [ValueConversion(typeof(Enum), typeof(bool))]
    public class RadioButtonConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null) return false;
            return value.ToString() == parameter.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null) return Binding.DoNothing;
            if ((bool)value)
            {
                return Enum.Parse(targetType, parameter.ToString());
            }
            return Binding.DoNothing;
        }
    }
}