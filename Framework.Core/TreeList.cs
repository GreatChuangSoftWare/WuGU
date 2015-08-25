using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core
{
    public class TreeNode
    {
        public string RootValue { get; set; }
        public string TreeNodeID { get; set; }
        public string Text { get; set; }
        public string Value { get; set; }
        public string ParentTreeNodeID { get; set; }

        public string ControllPath { get; set; }
        public string Other { get; set; }

        public string Css { get; set; }
        public string NodeUrl { get; set; }
    }

    /// <summary>
    /// 树形
    /// </summary>
    public class TreeList
    {
        #region 字段

        List<TreeNode> treeNodes = new List<TreeNode>();
        string rootValue = null;//根值
        string valueField = null;//
        string textField = null;//显示字段
        string nodeField = null;
        string parentField = null;//父节点字段
        string other = "";
        string cssField = "";
        string nodeUrl = "";

        public string NodeUrl
        {
            get { return nodeUrl; }
            set { nodeUrl = value; }
        }
        string controllerAction = "";
        string controllPath = "";

        public string CssField
        {
            get { return cssField; }
            set { cssField = value; }
        }

        public string ControllPath
        {
            get { return controllPath; }
            set { controllPath = value; }
        }

        string selectTreeNode = "";

        public string SelectTreeNode
        {
            get { return selectTreeNode; }
            set { selectTreeNode = value; }
        }

        #endregion

        #region 构造函数

        ///// <summary>
        ///// 构造函数
        ///// </summary>
        ///// <param name="items">项集合</param>
        ///// <param name="rootValue">根元素字段</param>
        ///// <param name="textField">显示文本字段</param>
        ///// <param name="nodeField">节点字段</param>
        ///// <param name="parentField">父节点字段</param>
        //public TreeList(IEnumerable items, string rootValue, string textField, string nodeField, string parentField, string controllerAction, string selectTreeNode = "",string Other="")
        //{
        //    this.rootValue = rootValue;
        //    this.valueField = nodeField;
        //    this.textField = textField;
        //    this.nodeField = nodeField;
        //    this.parentField = parentField;
        //    this.ControllerAction = controllerAction;
        //    this.other = other;
        //    Validate();
        //    Init(items);
        //}

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="items">数据集合</param>
        /// <param name="rootValue">根节点值</param>
        /// <param name="textField">显示文本字段名</param>
        /// <param name="nodeField">节点字段名</param>
        /// <param name="parentField">父节点字段名</param>
        /// <param name="controllerAction">控制器和Actioin:用于统一设置控制器和Actioin</param>
        /// <param name="selectTreeNode">选中树节点字符串：用于高亮显示</param>
        /// <param name="valueField">选中值字段</param>
        /// <param name="controllerActionField">控制器和Action字段</param>
        /// <param name="OtherField">其它信息</param>
        public TreeList(IEnumerable items, string rootValue, string textField, string nodeField,
            string parentField, string controllerAction, string selectTreeNode, string valueField,
            string controllerActionField, string OtherField = "", string CssField = "", string NodeUrl = "")
        {
            this.rootValue = rootValue;
            this.valueField = valueField;
            this.textField = textField;
            this.nodeField = nodeField;
            this.parentField = parentField;
            this.controllerAction = controllerAction;
            this.ControllPath = controllerActionField;
            this.other = OtherField;
            this.CssField = CssField;
            this.NodeUrl = NodeUrl;
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
                TreeNode treeNode = new TreeNode();
                Type type = item.GetType();

                PropertyInfo property = type.GetProperty(this.valueField);
                object o = property.GetValue(item, null);
                if (o != null)
                    treeNode.Value = o.ToString();

                property = type.GetProperty(this.textField);
                o = property.GetValue(item, null);
                if (o != null)
                    treeNode.Text = o.ToString();

                property = type.GetProperty(this.nodeField);
                o = property.GetValue(item, null);
                if (o != null)
                    treeNode.TreeNodeID = o.ToString();

                property = type.GetProperty(this.parentField);
                o = property.GetValue(item, null);
                if (o != null)
                    treeNode.ParentTreeNodeID = o.ToString();

                //Css样式
                if (CssField != "")
                {
                    property = type.GetProperty(this.CssField);
                    o = property.GetValue(item, null);
                    if (o != null)
                        treeNode.Css = o.ToString();
                }
                else
                    treeNode.Css = "";
                //其它

                //Controll字段
                if (ControllPath != "")
                {
                    property = type.GetProperty(this.ControllPath);
                    o = property.GetValue(item, null);
                    if (o != null)
                        treeNode.ControllPath = o.ToString();
                    //
                }

                if (NodeUrl != "")
                {
                    property = type.GetProperty(this.NodeUrl);
                    o = property.GetValue(item, null);
                    if (o != null)
                    {
                        //if (o.ToString() == "")
                        //    treeNode.NodeUrl = "javascript:void(null)";
                        //else
                        treeNode.NodeUrl = o.ToString();
                    }
                }

                if (other != "")
                {
                    property = type.GetProperty(this.other);
                    o = property.GetValue(item, null);
                    if (o != null)
                        treeNode.Other = o.ToString();
                }

                treeNodes.Add(treeNode);
            }
        }

        #region 字段属性

        public List<TreeNode> TreeNodes
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

        public string ControllerAction
        {
            get { return controllerAction; }
            set { controllerAction = value; }
        }

        #endregion
    }

}
