using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.ViewModels
{
    public class PanelModel
    {
        public int Id { get; set; }
        public string Item { get; set; }
        public List<SubItem> SubItems { get; set; }
    }

    public class SubItem
    {
        public string Name { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
    }
}
