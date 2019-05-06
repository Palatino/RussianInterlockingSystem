using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TetrisComponents
{
    public abstract class Piece
    {
        public TetrisBoard board { get; }
        public int colorIndex { get; }
        public List<PieceCell> cells;
        public int centroidRow;
        public int centroidColumn;
        public int configuration;
        public List<List<int>> configurations;

        public Piece(TetrisBoard board_, int colorIndex_)
        {
            board = board_;
            colorIndex = colorIndex_;
            configuration = 0;
        }

        public void MoveRight()
        {
            //Check pice is not already on the edge
            int rightMost = 0;
            foreach(PieceCell cell in cells)
            {
                if (cell.column > rightMost) rightMost = cell.column;
            }

            if (rightMost >= board.columns - 1) return;


            //Check the right cells are free

            foreach(PieceCell cell in cells)
            {
                if (!board.cells[cell.row][cell.column + 1].isEmpty)
                {
                    return;
                }

            }

            foreach(PieceCell cell in cells)
            {
                cell.column += 1;  
            }
            centroidColumn += 1;

        }

        public void MoveLeft()
        {
            //Check piece is not already on the edge
            int LeftMost = board.columns;
            foreach (PieceCell cell in cells)
            {
                if (cell.column < LeftMost) LeftMost = cell.column;
            }

            if (LeftMost == 0) return;

            //Check the left cells are free

            foreach (PieceCell cell in cells)
            {
                if (!board.cells[cell.row][cell.column - 1].isEmpty)
                {
                    return;
                }

            }

            //Else move the piece

            foreach (PieceCell cell in cells)
            {
                cell.column -= 1; 
            }
            centroidColumn -= 1;

        }

        public void MoveDown()
        {
            //Check piece is not already on the bottom
            int maxY = 0;
            foreach(PieceCell cell in cells)
            {
                if (cell.row > maxY) maxY = cell.row;
            }

            if(maxY >= board.rows-1)
            {
                board.KillActivePiece();
                return;
            }

            //Check bottom cell is free
            foreach (PieceCell cell in cells)
            {
                if (!board.cells[cell.row + 1][cell.column].isEmpty)
                {
                    board.KillActivePiece();
                    return;
                }
            }

            //If everything is ok move piece down

            foreach (PieceCell cell in cells)
            {
                cell.row += 1;
            }
            centroidRow += 1;

        }

        public void Rotate()
        {
            configuration += 1;
            configuration = configuration % 4;
            List<int> coordinates = configurations[configuration];

            List<PieceCell> tempLocation = new List<PieceCell>
            {
                new PieceCell(this, centroidRow +coordinates[0], centroidColumn+coordinates[1]),
                new PieceCell(this, centroidRow +coordinates[2], centroidColumn+coordinates[3]),
                new PieceCell(this, centroidRow +coordinates[4], centroidColumn+coordinates[5]),
                new PieceCell(this, centroidRow +coordinates[6], centroidColumn+coordinates[7])
            };

            bool commit = true;

            foreach (PieceCell cell in tempLocation)
            {
                if (cell.row < 0 || cell.row > board.rows-1)
                {
                    commit = false;
                    break;
                }
                if (cell.column < 0 || cell.column > board.columns-1)
                {
                    commit = false;
                    break;
                } 
                if (!board.cells[cell.row][cell.column].isEmpty)
                {
                    commit = false;
                    break;
                } 

            }

            if (commit) this.cells = tempLocation;
        }

        public void Draw()
        {
            foreach(PieceCell cell in cells)
            {
                cell.Draw();
            }

        }

    }
}
