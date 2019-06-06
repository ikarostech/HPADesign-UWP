using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPADesign.Models;
using HPADesign.Helpers;

namespace HPADesign.ViewModels
{
    public class PageViewModel : Observable
    {
        protected Project project { get; }
        public PageViewModel(Project project)
        {
            this.project = project;
        }
    }
}
