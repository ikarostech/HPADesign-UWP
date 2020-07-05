using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using HPADesign.Models.Components;
using HPADesign.IO;
using HPADesign.Helpers;

namespace HPADesign.IO.Components
{
    public interface IPrintable
    {
        List<IPrintableElement> Shapes { get; }
    }
    public interface IComponentPrinter : IPrintable
    {
        string LocalPrint();
        string GrobalPrint();
    }

    public abstract class ComponentPrinter : IComponentPrinter
    {
        public virtual List<IPrintableElement> Shapes { get; set; }

        public string LocalPrint()
        {
            return DXF.Content(this);
        }
        /// <summary>
        /// TODO
        /// </summary>
        /// <returns></returns>
        public string GrobalPrint()
        {
            return null;
        }
    }

}
