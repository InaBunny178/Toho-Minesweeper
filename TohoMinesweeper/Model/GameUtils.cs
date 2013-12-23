using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TohoMinesweeper.Model
{
    public class GameUtils
    {
        //最終的には設定で変更
        public static uint NumMines = 40;
        public static uint NumVerticalBlocks = 16;
        public static uint NumHorizontalBlocks = 16;
        public static uint TimeLimit = 987;

        //ボタン
        public static readonly int ButtonWidth = 27;
        public static readonly int ButtonHeight = 31;
        //public static readonly int ButtonWidth = 36;
        //public static readonly int ButtonHeight = 42;
    }
}
