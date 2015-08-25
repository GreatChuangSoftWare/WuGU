using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core
{
    /// <summary>
    /// 树形下拉框
    /// </summary>
    public class SelectTreeList
    {
        #region 字段

        List<DropTreeNode> treeNodes = new List<DropTreeNode>();
        string rootValue = null;
        string valueField = null;
        string textField = null;
        string nodeField = null;
        string parentField = null;

        string css = null;

        string selectValue = "";

        bool isFinalLevel = false;

        public bool IsFinalLevel
        {
            get { return isFinalLevel; }
            set { isFinalLevel = value; }
        }

        public string SelectValue
        {
            get { return selectValue; }
            set { selectValue = value; }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="items">项集合</param>
        /// <param name="rootValue">根元素字段</param>
        /// <param name="textField">显示文本字段</param>
        /// <param name="nodeField">节点字段</param>
        /// <param name="parentField">父节点字段</param>
        public SelectTreeList(IEnumerable items, string rootValue, string textField, string nodeField, string parentField)
        {
            this.rootValue = rootValue;
            this.valueField = nodeField;
            this.textField = textField;
            this.nodeField = nodeField;
            this.parentField = parentField;
            Validate();
            Init(items);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="items">项集合</param>
        /// <param name="rootValue">根元素字段</param>
        /// <param name="valueField">获取值字段</param>
        /// <param name="textField">显示文本字段</param>
        /// <param name="nodeField">节点字段</param>
        /// <param name="parentField">父节点字段</param>
        public SelectTreeList(IEnumerable items, string rootValue, string textField, string nodeField, string parentField, string valueField, string selectValue, bool isFinalLevel = false, string Css = "")
        {
            this.rootValue = rootValue;
            this.valueField = valueField;
            this.textField = textField;
            this.nodeField = nodeField;
            this.parentField = parentField;
            this.selectValue = selectValue;
            this.isFinalLevel = isFinalLevel;
            this.css = Css;

            Validate();
            Init(items);
        }

        #endregion

        /// <summary>
        /// 验证各个参数的有效性
        /// </summary>
        public void Validate()
        {
            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrEmpty(valueField))
                sb.AppendLine("获取值字段不能为空");
            if (string.IsNullOrEmpty(textField))
                sb.AppendLine("显示文本字段不能为空");
            if (string.IsNullOrEmpty(nodeField))
                sb.AppendLine("节点字段不能为空");
            if (string.IsNullOrEmpty(parentField))
                sb.AppendLine("父节点字段字段不能为空");
            if (sb.Length > 0)
                throw new Exception("验证错误：" + sb.ToString());
        }

        /// <summary>
        /// 初始化TreeNodes集合
        /// </summary>
        /// <param name="items"></param>
        public void Init(IEnumerable items)
        {
            foreach (var item in items)
            {
                DropTreeNode treeNode = new DropTreeNode();
                Type type = item.GetType();

                PropertyInfo property = type.GetProperty(this.valueField);
                object o = property.GetValue(item, null);
                treeNode.Value = o.ToString();

                property = type.GetProperty(this.textField);
                o = property.GetValue(item, null);
                treeNode.Text = o.ToString();

                property = type.GetProperty(this.nodeField);
                o = property.GetValue(item, null);
                treeNode.TreeNodeID = o.ToString();

                property = type.GetProperty(this.parentField);
                o = property.GetValue(item, null);

                treeNode.ParentTreeNodeID = o.ToString();

                if (!string.IsNullOrEmpty(this.css))
                {
                    property = type.GetProperty(this.css);
                    o = property.GetValue(item, null);
                    treeNode.Css = o.ToString();
                }
                else
                    treeNode.Css = "";

                treeNodes.Add(treeNode);
            }
        }

        #region 字段属性

        public List<DropTreeNode> TreeNodes
        {
            get { return treeNodes; }
            set { treeNodes = value; }
        }

        public string RootValue
        {
            get { return rootValue; }
            set { rootValue = value; }
        }

        public string ValueField
        {
            get { return valueField; }
            set { valueField = value; }
        }

        public string TextField
        {
            get { return textField; }
            set { textField = value; }
        }

        public string NodeField
        {
            get { return nodeField; }
            set { nodeField = value; }
        }

        public string ParentField
        {
            get { return parentField; }
            set { parentField = value; }
        }

        #endregion
    }

}
