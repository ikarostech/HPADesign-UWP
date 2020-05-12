using HPADesign.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace HPADesign.IO
{
    public interface IDXFWriter
    {
        //List<Coordinate> Coordinates { get; set; }
        StorageFile File { get; set; }
    }
    public interface IPrintableElement
    {
        string Print();
    }
    public interface IPrintable
    {
        List<IPrintableElement> Shapes { get; set; }
    }
    public class DXFWriter : IDXFWriter
    {
        public StorageFile File { get; set; }
        
        public DXFWriter(StorageFile file)
        {
            this.File = file;
        }
        public void Write(IPrintable printable)
        {
            printable.Shapes.Select(async (x) => await FileIO.WriteTextAsync(File,x.Print()));
        }
    }
}
