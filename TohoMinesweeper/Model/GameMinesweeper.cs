using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace TohoMinesweeper.Model
{
    public class TimerSecondChangedEventArgs : EventArgs
    {
        public uint TimeLeft;
    }

    class GameMinesweeper
    {
        private uint _numMines;
        private uint _numVerticalBlocks;
        private uint _numHorizontalBlocks;
        private uint _timeLimit;
        private MineBlock[,] _mineBlocks;

        private GameState _currentGameState = GameState.Finished; //ゲーム開始前
        public GameState CurrentGameState
        {
            get{ return this._currentGameState; }
        }

        public delegate void TimerSecondChangedEventHandler(object sender, TimerSecondChangedEventArgs e);
        public event TimerSecondChangedEventHandler TimerSecondChanged;
        private uint _currentTime;
        private BackgroundWorker TimerWorker = null;

        public enum GameState : int
        {
            Starting = 0,
            Started,
            Finishing,
            Finished,
        }

        public GameMinesweeper(uint mines, uint horizontalBlocks, uint verticalBlocks, uint timeLimit)
        {
            this._numMines = mines;
            this._numHorizontalBlocks = horizontalBlocks;
            this._numVerticalBlocks = verticalBlocks;
            this._timeLimit = timeLimit;
            this._mineBlocks = new MineBlock[verticalBlocks, horizontalBlocks];
        }

        public bool StartGame()
        {
            System.Diagnostics.Debug.WriteLine("StartGame()");

            _currentGameState = GameState.Starting; //ゲーム開始処理中

            if (TimerWorker != null)
            {
                if (TimerWorker.IsBusy)
                {
                    TimerWorker.CancelAsync();
                }
            }

            var numBlocks = _numVerticalBlocks * _numHorizontalBlocks; //ブロックの総数
            if (numBlocks < _numMines)
            {
                return false;
            }

            //ブロックの生成、地雷の設置
            bool[] isMines = new bool[numBlocks];
            for (int i = 0; i < numBlocks; i++)
            {
                isMines[i] = (i < _numMines) ? true : false;
            }
            bool[] isMinesRandom = isMines.OrderBy(i => Guid.NewGuid()).ToArray();
            int index = 0;
            for (int row = 0; row < _numVerticalBlocks; row++)
            {
                for (int column = 0; column < _numHorizontalBlocks; column++)
                {
                    if (isMinesRandom[index])
                    {
                        _mineBlocks[row, column] = new MineBlock(true); //地雷あり
                    }
                    else
                    {
                        _mineBlocks[row, column] = new MineBlock(false); //地雷なし
                    }
                    index++;
                }
            }

            for (int row = 0; row < _numVerticalBlocks; row++)
            {
                for (int column = 0; column < _numHorizontalBlocks; column++)
                {
                    if (_mineBlocks[row, column].IsMine)
                    {
                        //地雷の場合
                        for (int arroundRow = (row - 1); arroundRow <= (row + 1); arroundRow++)
                        {
                            if (arroundRow < 0 || arroundRow >= _numVerticalBlocks)
                                continue;

                            for (int arroundColumn = (column - 1); arroundColumn <= (column + 1); arroundColumn++)
                            {
                                if (arroundColumn < 0 || arroundColumn >= _numHorizontalBlocks)
                                    continue;

                                if (arroundRow == row && arroundColumn == column)
                                    continue;

                                //周囲のブロックに1足す
                                _mineBlocks[arroundRow, arroundColumn].NumMines++;
                            }
                        }
                    }
                }
            }

            //for (int row = 0; row < _numVerticalBlocks; row++)
            //{
            //    for (int column = 0; column < _numHorizontalBlocks; column++)
            //    {
            //        System.Diagnostics.Debug.WriteLine("MineBlocks[{0},{1}]: IsMine={2}  mineNum={3}",
            //            row, column, _mineBlocks[row, column].IsMine, _mineBlocks[row, column].NumMines);
            //    }
            //}

            //タイマーの設置
            TimerWorker = new BackgroundWorker();
            TimerWorker.WorkerSupportsCancellation = true;
            TimerWorker.DoWork += (s, evt) =>
                {
                    System.Diagnostics.Debug.WriteLine("TimerWorker.DoWork()");

                    BackgroundWorker worker = s as BackgroundWorker;
                    _currentTime = _timeLimit;
                    var startTime = DateTime.Now;
                    while (!worker.CancellationPending)
                    {
                        System.Threading.Thread.Sleep(1);

                        TimeSpan tp = DateTime.Now.Subtract(startTime);
                        if (tp.TotalMilliseconds >= 1000)
                        {
                            _currentTime--;
                            if (TimerSecondChanged != null)
                            {
                                var e = new TimerSecondChangedEventArgs();
                                e.TimeLeft = _currentTime;
                                TimerSecondChanged(this, e); //イベント通知
                            }
                            if (_currentTime <= 0)
                                break;

                            startTime = DateTime.Now;
                        }
                    }
                };
            TimerWorker.RunWorkerCompleted += (s, evt) =>
                {
                    if (TimerWorker != null)
                    {
                        TimerWorker.Dispose();
                        TimerWorker = null;
                    }
                    FinishGame(true);
                };
            TimerWorker.RunWorkerAsync();
            
            _currentGameState = GameState.Started; //ゲーム開始

            return true;
        }

        public void FinishGame(bool isTimeUp)
        {
            System.Diagnostics.Debug.WriteLine("FinishGame()");

            _currentGameState = GameState.Finishing;

            //ゲームの終了

            _currentGameState = GameState.Finished;
        }
    }
}
