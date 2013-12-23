using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TohoMinesweeper.Model
{
    class MineBlock
    {
        private bool _isMine = false;
        public bool IsMine
        {
            get { return this._isMine; }
            set { this._isMine = value; }
        }

        private int _numMines = 0;
        public int NumMines
        {
            get { return this._numMines; }
            set { this._numMines = value; }
        }

        private BlockState _currentBlockState = BlockState.None;
        public BlockState CurrentBlockState
        {
            get { return this._currentBlockState; }
            set { this._currentBlockState = value; }
        }

        public enum BlockState : int
        {
            None = 0,
            Clicked,
            Check,
        }

        /// <summary>
        /// 地雷かどうか
        /// </summary>
        /// <param name="isMine"></param>
        public MineBlock(bool isMine)
        {
            this._isMine = isMine;
        }

    }
}
