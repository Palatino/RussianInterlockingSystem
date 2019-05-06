using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Drawing;
using Grasshopper;
using TetrisComponents.Tetris.Pieces;

namespace TetrisComponents
{
    public class TetrisBoard
    {
        Random random = new Random();

        Timer timer;
        public int rows;
        public int columns;
        public int cellSize;
        public PointF insertionPoint;
        public List<List<BoardCell>> cells { get; set; }
        public Grasshopper.GUI.Canvas.GH_Canvas canvas;
        public Piece activepiece;
        public List<Color> colorPalette;
        public bool gameActive = true;
        public int score = 0;

        public TetrisBoard(Grasshopper.GUI.Canvas.GH_Canvas canvas_, int rows_, int columns_, int cellSize_ )
        {
            insertionPoint = new PointF(0, 0);
            canvas = canvas_;
            rows = rows_;
            columns = columns_;
            cellSize = cellSize_;

            //Set timer

            timer = new Timer(650);
            timer.AutoReset = true;
            timer.Elapsed += OnElapsed;
            timer.Enabled = true;

            //Create color pallete

            colorPalette = new List<Color>
            {
                Color.FromArgb(255,144,203,209),
                Color.FromArgb(255,97,180,188),
                Color.FromArgb(255,255,123,104),
                Color.FromArgb(255,255,85,59),
                Color.FromArgb(255,255,219,97),
                Color.FromArgb(255,255,203,31),
                Color.FromArgb(255,251,173,90),
                Color.FromArgb(255,250,150,39),
                Color.FromArgb(255,134,190,81),
                Color.FromArgb(255,100,171,33),
                Color.FromArgb(255,242,130,170),
                Color.FromArgb(255,240,97,151),
                Color.FromArgb(255,97,153,204),
                Color.FromArgb(255,50,124,187),
            };

            cells = new List<List<BoardCell>>();

            //Initialize board
            for (int i = 0; i<rows; i++)
            {
                List<BoardCell> row = new List<BoardCell>();

                for (int u=0; u<columns; u++)
                {
                    BoardCell cell = new BoardCell(this, i, u, cellSize);
                    row.Add(cell);
                }

                cells.Add(row);

            }
        }

        //Draw grid and call the draw methos of all the cells in the board
        public void Draw()
        {

            

            foreach (List<BoardCell> row in cells)
            {
                foreach (BoardCell cell in row)
                {
                    cell.Draw();
                }
            }

            activepiece.Draw();

            Pen pen = new Pen(Color.FromArgb(255,106,106,106));
            for(int i=0; i<rows+1; i++)
            {
                canvas.Graphics.DrawLine(pen, new Point((int)insertionPoint.X, (int)insertionPoint.Y+i * cellSize), new Point((int)insertionPoint.X + columns * cellSize, (int)insertionPoint.Y + i * cellSize));
            }

            for (int i = 0; i < columns+1; i++)
            {
                canvas.Graphics.DrawLine(pen, new Point((int)insertionPoint.X + i * cellSize, (int)insertionPoint.Y), new Point((int)insertionPoint.X + i * cellSize, (int)insertionPoint.Y + rows * cellSize));
            }

            Brush brush1 = new SolidBrush(Color.FromArgb(255, 56, 56, 56));
            if (gameActive)
            {
                canvas.Graphics.DrawString(string.Format("Score : {0}", score.ToString()), new Font("Impact", 24.0f), brush1, new PointF(insertionPoint.X - 100, insertionPoint.Y - 100));
            }

            else
            {
                canvas.Graphics.DrawString(string.Format("Score : {0}. You lost, press enter to reset", score.ToString()), new Font("Impact", 24.0f), brush1, new PointF(insertionPoint.X - 100, insertionPoint.Y - 100));
            }





        }

        public void KillActivePiece()
        {
            foreach(PieceCell cell in activepiece.cells)
            {
                cells[cell.row][cell.column].isEmpty = false;
                cells[cell.row][cell.column].colorIndex = activepiece.colorIndex;
            }

            activepiece = NewRandomPiece();
            if (!this.cells[0][(int)((this.columns - 1) / 2.0)].isEmpty) gameActive = false;
        }

        public void CheckForFullRows()
        {
            for(int i= rows-1; i>0; i--)
            {
                List<BoardCell> row = cells[i];
                bool full = true;
                foreach(BoardCell cell in row)
                {
                    if (cell.isEmpty) full = false;
                }

                if (full)
                {
                    for (int u = i; u >0; u--)
                    {
                        List<BoardCell> clearingRow = cells[u];
                        foreach (BoardCell cell in clearingRow)
                        {

                            cell.isEmpty = true;
                            if (!cells[u - 1][cell.column].isEmpty)
                            {
                                cell.colorIndex = cells[u - 1][cell.column].colorIndex;
                                cell.isEmpty = false;

                            }
                        }
                    }

                    //Update speed of timer according to score
                    if (score == 400) timer.Interval = 450;
                    if (score == 900) timer.Interval = 250;
                    if (score == 1900) timer.Interval = 175;

                    //Update score
                    score += 100;
                }
            }

        }

        public Piece NewRandomPiece()
        {
            int type = random.Next(7);
            Piece newPiece = null;
            switch (type)
            {
                case 0:
                    newPiece = new BarPiece(this);
                    break;

                case 1:
                    newPiece = new SquarePiece(this);
                    break;

                case 2:
                    newPiece = new TPiece(this);
                    break;

                case 3:
                    newPiece = new LRPiece(this);
                    break;

                case 4:
                    newPiece = new LLPiece(this);
                    break;

                case 5:
                    newPiece = new ZPiece(this);
                    break;

                case 6:
                    newPiece = new ZRPiece(this);
                    break;

            }
            return newPiece;
        }

        public void OnElapsed(Object sender, EventArgs e)
        {
            if (gameActive) this.activepiece.MoveDown();

        }
    }

}
