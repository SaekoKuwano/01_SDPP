using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace WpfCtr_CreateLabel.Models
{
    public class ColorItem : INotifyPropertyChanged
    {
        //private SolidColorBrush _borderColor;

        //public SolidColorBrush BorderColor
        //{
        //    get { return _borderColor; }
        //    set
        //    {
        //        _borderColor = value;
        //        OnPropertyChanged("BorderColor");
        //    }
        //}

        public SolidColorBrush BorderColor { get; private set; }

        // コンストラクター
        public ColorItem()
        {
            //BorderColor = new SolidColorBrush(Colors.Red);
            UpdateBrush();
        }

        // INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]string propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        private void UpdateBrush()
        {
            //var color = BorderColor.Color;
            //BorderColor = new SolidColorBrush(color);

            BorderColor = new SolidColorBrush(Colors.Red);

            OnPropertyChanged("BorderColor");
        }
    }
}