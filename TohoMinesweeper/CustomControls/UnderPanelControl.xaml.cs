using System;
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
using TohoMinesweeper.Model;
using System.IO;

namespace TohoMinesweeper
{
    /// <summary>
    /// 左にタイマー（画像入り）
    /// 右に地雷の数
    /// </summary>
    public partial class UnderPanelControl : UserControl
    {
        public enum PictureType : int
        {
            Timer = 0,
            Mines,
        }

        string[] ImageUris = new string[]
        {
            "../Resources/timers/timer0.png",
            "../Resources/timers/timer1.png",
            "../Resources/timers/timer2.png",
            "../Resources/timers/timer3.png",
            "../Resources/timers/timer4.png",
            "../Resources/timers/timer5.png",
            "../Resources/timers/timer6.png",
            "../Resources/timers/timer7.png",
            "../Resources/timers/timer8.png",
            "../Resources/timers/timer9.png",
        };

        public UnderPanelControl()
        {
            InitializeComponent();
        }

        public void SetNumberPicture(PictureType type, uint number)
        {
            if (number >= 1000)
                return;

            Image image0, image1, image2;
            uint digit0, digit1, digit2;
            if (type == PictureType.Timer)
            {
                image0 = TimerImage0;
                image1 = TimerImage1;
                image2 = TimerImage2;
            }
            else
            {
                image0 = MinesImage0;
                image1 = MinesImage1;
                image2 = MinesImage2;
            }
            digit0 = (uint)((double)number / 100);
            uint rest = number % 100;
            digit1 = (uint)((double)rest / 10);
            digit2 = rest % 10;

            //100の位
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(ImageUris[digit0], UriKind.RelativeOrAbsolute);
            bitmap.EndInit();
            image0.Source = bitmap;

            //10の位
            bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(ImageUris[digit1], UriKind.RelativeOrAbsolute);
            bitmap.EndInit();
            image1.Source = bitmap;

            //1の位
            bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(ImageUris[digit2], UriKind.RelativeOrAbsolute);
            bitmap.EndInit();
            image2.Source = bitmap;

            //var bitmap = TohoMinesweeper.Properties.Resources.timer4;
            //using (MemoryStream memory = new MemoryStream())
            //{
            //    bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
            //    memory.Position = 0;
            //    BitmapImage bitmapImage = new BitmapImage();
            //    bitmapImage.BeginInit();
            //    bitmapImage.StreamSource = memory;
            //    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            //    bitmapImage.EndInit();
            //    TimerImage0.Source = bitmapImage;
            //}
        }

    }
}
