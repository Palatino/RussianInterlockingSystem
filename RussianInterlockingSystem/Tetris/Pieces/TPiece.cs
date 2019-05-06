using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TetrisComponents.Tetris.Pieces
{
    class TPiece : Piece
    {
        public TPiece(TetrisBoard board_) :
            base(board_, 8)
        {
            centroidColumn = (int)((board_.columns - 1) / 2.0);
            centroidRow = 0;
            configurations = new List<List<int>> {
                new List<int>() { 0, -1, 0, 0, 0, 1, 1, 0 },
                new List<int>() { -1, 0, 0, 0, 1, 0, 0, 1},
                new List<int>() { 0, -1, 0, 0, 0, 1, -1, 0 },
                new List<int>() { -1, 0, 0, 0, 1, 0, 0, -1 },
            };

            this.cells = new List<PieceCell>
            {

                new PieceCell(this, centroidRow +configurations[0][0], centroidColumn+configurations[0][1]),
                new PieceCell(this, centroidRow +configurations[0][2], centroidColumn+configurations[0][3]),
                new PieceCell(this, centroidRow +configurations[0][4], centroidColumn+configurations[0][5]),
                new PieceCell(this, centroidRow +configurations[0][6], centroidColumn+configurations[0][7])
            };
        }
    }
}
