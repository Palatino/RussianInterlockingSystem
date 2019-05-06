using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Input;
using System.Timers;
using System.Threading;


using Grasshopper;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace TetrisComponents
{
    public class RussianInterlocking : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the RussianInterlocking class.
        /// </summary>
        /// 
        public TetrisBoard board = null;
        public RussianInterlocking()
          : base("Russian Interlocking", "Tetris",
              "Tetris Game",
              "Useless Components", "Components")
        {
            board = new TetrisBoard(Grasshopper.Instances.ActiveCanvas, 20, 10, 35);
                        board.activepiece = new BarPiece(board);

            //Change the panning function froms the arrows to WASD keys so arrows can be used to play
            Grasshopper.GUI.Canvas.GH_Canvas.NavigationPanRight = System.Windows.Forms.Keys.D;
            Grasshopper.GUI.Canvas.GH_Canvas.NavigationPanLeft = System.Windows.Forms.Keys.A;
            Grasshopper.GUI.Canvas.GH_Canvas.NavigationPanDown = System.Windows.Forms.Keys.S;
            Grasshopper.GUI.Canvas.GH_Canvas.NavigationPanUp = System.Windows.Forms.Keys.W;

        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
           
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            board.insertionPoint = new PointF(this.Attributes.Pivot.X - (float)((board.columns/2.0)*board.cellSize), this.Attributes.Pivot.Y +20);

            
            Grasshopper.Instances.ActiveCanvas.CanvasPaintBackground -= canvasPaintHandler;
            Grasshopper.Instances.ActiveCanvas.CanvasPaintBackground += canvasPaintHandler;

            if (Keyboard.IsKeyDown(Key.Right))
            {
                Thread.Sleep(50);
                board.activepiece.MoveRight();
            }

            if (Keyboard.IsKeyDown(Key.Left))
            {
                Thread.Sleep(50);
                board.activepiece.MoveLeft();
            }

            if (Keyboard.IsKeyDown(Key.Down))
            {
                Thread.Sleep(50);
                board.activepiece.MoveDown();
            }

            if (Keyboard.IsKeyDown(Key.Up))
            {
                Thread.Sleep(50);
                board.activepiece.Rotate();
            }

            board.CheckForFullRows();

            ExpireSolution(true);
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return null ;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("2ddf346b-be21-4b15-993a-ed9a94e69ed8"); }
        }

        public void canvasPaintHandler(Grasshopper.GUI.Canvas.GH_Canvas canvas)
        {
            board.Draw();
        }

        public override void RemovedFromDocument(GH_Document document)
        {
            base.RemovedFromDocument(document);

            //Unsuscruibe to clean screen
            Grasshopper.Instances.ActiveCanvas.CanvasPaintBackground -= canvasPaintHandler;

            //Return arrwo functions

            Grasshopper.GUI.Canvas.GH_Canvas.NavigationPanRight = System.Windows.Forms.Keys.Right;
            Grasshopper.GUI.Canvas.GH_Canvas.NavigationPanLeft = System.Windows.Forms.Keys.Left;
            Grasshopper.GUI.Canvas.GH_Canvas.NavigationPanDown = System.Windows.Forms.Keys.Down;
            Grasshopper.GUI.Canvas.GH_Canvas.NavigationPanUp = System.Windows.Forms.Keys.Up;

        }


    }
}