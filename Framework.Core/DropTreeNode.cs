using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core
{
    public class DropTreeNode
    {
        public string RootValue { get; set; }
        public string TreeNodeID { get; set; }
        public string Text { get; set; }
        public string Value { get; set; }
        public string ParentTreeNodeID { get; set; }

        public string Css { get; set; }
    }
}
