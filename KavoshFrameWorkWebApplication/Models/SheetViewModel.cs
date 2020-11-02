using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KavoshFrameWorkWebApplication.Models
{
    public class SheetViewModel
    {
        public SheetViewModel()
        {
            Rows = new List<SheetRowViewModel>();
        }
        public List<SheetRowViewModel> Rows { get; set; }
        public string FileName { get; set; }
 
    }
    public class SheetRowViewModel
    {
        public SheetRowViewModel()
        {
            Cells = new List<SheetCellViewModel>();
        }
        public List<SheetCellViewModel> Cells { get; set; }
    }
    public class SheetCellViewModel
    {
        public string Value { get; set; }
        public int ColSpan { get; set; } = 1;
        public int RowSpan { get; set; } = 1;

    }
}
