using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TetrisComponents
{
    public class BoardCell
    {
        public TetrisBoard board;
        public int row { get; }
        public int column { get; }
        public int size { get; }
        public bool isEmpty { get; set; }
        public int colorIndex { get; set; }

        public BoardCell(TetrisBoard board_, int _row, int _column, int _size)
        {
            board = board_;
            row = _row;
            column = _column;
            size = _size;
            isEmpty = true;

        }

        public void Draw()
        {
            Graphics graphics = board.canvas.Graphics;

            if (!this.isEmpty)
            {
                
                Brush brush1 = new SolidBrush(board.colorPalette[colorIndex]);
                Brush brush2 = new SolidBrush(board.colorPalette[colorIndex + 1]);
                Point[] triangle1 = new Point[]
                {
                new Point((int)(board.insertionPoint.X + this.column * board.cellSize), (int)((board.insertionPoint.Y)+ this.row * board.cellSize)),
                new Point((int)(board.cellSize+ board.insertionPoint.X + column * board.cellSize), (int)((board.insertionPoint.Y)+ this.row * board.cellSize)),
                new Point((int)(board.insertionPoint.X + column * board.cellSize), (int)(board.cellSize + (board.insertionPoint.Y)+ this.row * board.cellSize))
                };

                Point[] triangle2 = new Point[]
                {

                new Point((int)(board.cellSize+ board.insertionPoint.X + column * board.cellSize), (int)((board.insertionPoint.Y)+ this.row * board.cellSize)),
                new Point((int)(board.insertionPoint.X + column * board.cellSize), (int)(board.cellSize + (board.insertionPoint.Y)+ this.row * board.cellSize)),
                new Point((int)(board.cellSize + board.insertionPoint.X + column * board.cellSize), (int)(board.cellSize + (board.insertionPoint.Y)+ this.row * board.cellSize))

                };

                graphics.FillPolygon(brush1, triangle1);
                graphics.FillPolygon(brush2, triangle2);
                brush1.Dispose();
                brush2.Dispose();
            }

            else
            {
                Brush brush = new SolidBrush(Color.FromArgb(255, 56, 56, 56));
                graphics.FillRectangle(brush, board.insertionPoint.X + column * size, board.insertionPoint.Y + row * size, size, size);
                brush.Dispose();
            }
        }


    }
}
