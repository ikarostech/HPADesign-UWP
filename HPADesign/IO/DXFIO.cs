using HPADesign.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using HPADesign.Helpers;
using HPADesign.IO.Components;

namespace HPADesign.IO
{
    public interface IDXFWriter
    {
        //List<Coordinate> Coordinates { get; set; }
        //StorageFile File { get; set; }
        void Write(IPrintable printable);
    }
    public interface IPrintableElement
    {
        string Print();
    }
    
    public class DXFWriter : IDXFWriter
    {
        private string path { get; }
        
        public DXFWriter(StorageFile file)
        {
            path = file.Path;
        }
        
        public void Write(IPrintable printable)
        {
            StreamWriter sw = new StreamWriter(path);
            //sw.Write(DXF.Content(printable));
            sw.Close();
        }
    }
    
}
