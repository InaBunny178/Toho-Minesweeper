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
    /// 地雷ボタンを指定されたマスだけ作成
    /// ボタンの制御
    /// </summary>
    public partial class MineBlockControl : UserControl
    {
        private Button[,] MineButtons;

        public MineBlockControl()
        {
            InitializeComponent();
            InitGrid();
        }

        private void InitGrid()
        {
            for (int i = 0; i < GameUtils.NumVerticalBlocks; i++)
            {
                GameGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) });
            }
            for (int i = 0; i < GameUtils.NumHorizontalBlocks; i++)
            {
                GameGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Auto) });
            }

            MineButtons = new Button[GameUtils.NumVerticalBlocks, GameUtils.NumHorizontalBlocks];

            for (int i = 0; i < GameUtils.NumVerticalBlocks; i++)
            {
                for (int j = 0; j < GameUtils.NumHorizontalBlocks; j++)
                {
                    MineButtons[i, j] = new Button() { Width = GameUtils.ButtonWidth, Height = GameUtils.ButtonHeight };
                    MineButtons[i, j].SetValue(Grid.RowProperty, i);
                    MineButtons[i, j].SetValue(Grid.ColumnProperty, j);
                    GameGrid.Children.Add(MineButtons[i, j]);
                }
            }
        }
    }
}
