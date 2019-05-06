using System;
using System.Drawing;
using Grasshopper.Kernel;

namespace RussianInterlockingSystem
{
    public class RussianInterlockingSystemInfo : GH_AssemblyInfo
    {
        public override string Name
        {
            get
            {
                return "RussianInterlockingSystem";
            }
        }
        public override Bitmap Icon
        {
            get
            {
                //Return a 24x24 pixel bitmap to represent this GHA library.
                return null;
            }
        }
        public override string Description
        {
            get
            {
                //Return a short string describing the purpose of this GHA library.
                return "";
            }
        }
        public override Guid Id
        {
            get
            {
                return new Guid("cee16642-352d-440a-acaa-87f14c653d0c");
            }
        }

        public override string AuthorName
        {
            get
            {
                //Return a string identifying you or your company.
                return "Pablo Alvarez";
            }
        }
        public override string AuthorContact
        {
            get
            {
                //Return a string representing your preferred contact details.
                return "www.linkedin.com/in/palvarezrio";
            }
        }
    }
}
