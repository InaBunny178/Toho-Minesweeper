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

namespace TohoMinesweeper
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameMinesweeper CurrentGame;

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// メニュー：ゲームスタート
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameStartExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("GameStartExecuted");

            //ゲーム初期化、スタート
            CurrentGame = new GameMinesweeper(
                GameUtils.NumMines,
                GameUtils.NumHorizontalBlocks,
                GameUtils.NumVerticalBlocks,
                GameUtils.TimeLimit);
            CurrentGame.TimerSecondChanged += TimerSecondChangedEvent;
            CurrentGame.StartGame();

            //UI初期化
            underPanelControl.SetNumberPicture(UnderPanelControl.PictureType.Timer, GameUtils.TimeLimit);
            underPanelControl.SetNumberPicture(UnderPanelControl.PictureType.Mines, GameUtils.NumMines);

        }

        /// <summary>
        /// タイマーの残り時間変更イベントの追加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerSecondChangedEvent(object sender, TimerSecondChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("time left: {0} sec", e.TimeLeft);
            this.Dispatcher.BeginInvoke(
                new Action(() =>
                {
                    underPanelControl.SetNumberPicture(UnderPanelControl.PictureType.Timer, e.TimeLeft);
                }));
        }

        /// <summary>
        /// メニュー：オプション設定画面起動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameOptionExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("GameOptionExecuted");
        }

        /// <summary>
        /// メニュー：ゲーム終了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameEndExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("GameEndExecuted");
            Environment.Exit(0);
        }

        /// <summary>
        /// メニュー：ヘルプ表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HelpDisplayExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("HelpDisplayExecuted");
        }
    }
}
