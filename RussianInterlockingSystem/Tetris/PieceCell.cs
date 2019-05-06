using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TetrisComponents
{
    public class PieceCell
    {
        private Piece piece;
        public int row;
        public int column;

        public PieceCell(Piece piece_, int row_, int column_)
        {
            piece = piece_;
            row = row_;
            column = column_;
        }

        public void Draw()
        {
            Graphics graphics = piece.board.canvas.Graphics;
            Brush brush1 = new SolidBrush(piece.board.colorPalette[piece.colorIndex]);
            Brush brush2 = new SolidBrush(piece.board.colorPalette[piece.colorIndex+1]);
            Point[] triangle1 = new Point[]
            {
                new Point((int)(piece.board.insertionPoint.X + this.column * piece.board.cellSize), (int)((piece.board.insertionPoint.Y)+ this.row * piece.board.cellSize)),
                new Point((int)(piece.board.cellSize+ piece.board.insertionPoint.X + column * piece.board.cellSize), (int)((piece.board.insertionPoint.Y)+ this.row * piece.board.cellSize)),
                new Point((int)(piece.board.insertionPoint.X + column * piece.board.cellSize), (int)(piece.board.cellSize + (piece.board.insertionPoint.Y)+ this.row * piece.board.cellSize))
            };

            Point[] triangle2 = new Point[]
            {

                new Point((int)(piece.board.cellSize+ piece.board.insertionPoint.X + column * piece.board.cellSize), (int)((piece.board.insertionPoint.Y)+ this.row * piece.board.cellSize)),
                new Point((int)(piece.board.insertionPoint.X + column * piece.board.cellSize), (int)(piece.board.cellSize + (piece.board.insertionPoint.Y)+ this.row * piece.board.cellSize)),
                new Point((int)(piece.board.cellSize + piece.board.insertionPoint.X + column * piece.board.cellSize), (int)(piece.board.cellSize + (piece.board.insertionPoint.Y)+ this.row * piece.board.cellSize))

            };

            graphics.FillPolygon(brush1, triangle1);
            graphics.FillPolygon(brush2, triangle2);
            brush1.Dispose();
            brush2.Dispose();
        }
    }
}
