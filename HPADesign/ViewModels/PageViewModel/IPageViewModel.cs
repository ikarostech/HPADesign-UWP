using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPADesign.Models;
namespace HPADesign.ViewModels
{
    interface IPageViewModel
    {
        Project project { get; }
    }
}
