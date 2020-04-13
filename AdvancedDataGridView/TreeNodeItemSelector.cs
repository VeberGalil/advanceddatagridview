﻿#region License
// Advanced DataGridView
//
// Copyright (c), 2020 Vladimir Bershadsky <vladimir@galileng.com>
// Copyright (c), 2014 Davide Gironi <davide.gironi@gmail.com>
// Original work Copyright (c), 2013 Zuby <zuby@me.com>
//
// Please refer to LICENSE file for licensing information.
#endregion

using System;
using System.Windows.Forms;

namespace Zuby.ADGV
{
    [System.ComponentModel.DesignerCategory("")]
    internal class TreeNodeItemSelector : TreeNode
    {

        #region public enum

        public enum CustomNodeType : byte
        {
            Default,
            SelectAll,
            SelectEmpty,
            DateTimeNode
        }

        #endregion


        #region class properties

        private CheckState _checkState = CheckState.Unchecked;

        #endregion


        #region constructor

        /// <summary>
        /// TreeNodeItemSelector constructor
        /// </summary>
        /// <param name="text"></param>
        /// <param name="value"></param>
        /// <param name="state"></param>
        /// <param name="nodeType"></param>
        private TreeNodeItemSelector(String text, object value, CheckState state, CustomNodeType nodeType)
            : base(text)
        {
            CheckState = state;
            NodeType = nodeType;
            Value = value;
        }

        #endregion


        #region public clone method

        /// <summary>
        /// Clone a Node
        /// </summary>
        /// <returns></returns>
        public new TreeNodeItemSelector Clone()
        {
            TreeNodeItemSelector n = new TreeNodeItemSelector(Text, Value, _checkState, NodeType)
            {
                NodeFont = NodeFont
            };

            if (GetNodeCount(false) > 0)
            {
                foreach (TreeNodeItemSelector child in Nodes)
                    n.AddChild(child.Clone());
            }

            return n;
        }

        #endregion


        #region public getters / setters

        /// <summary>
        /// Get Node NodeType
        /// </summary>
        public CustomNodeType NodeType { get; private set; }

        /// <summary>
        /// Get Node value
        /// </summary>
        public object Value { get; private set; }

        /// <summary>
        /// Get Node parent
        /// </summary>
        new public TreeNodeItemSelector Parent { get; set; }

        /// <summary>
        /// Node is Checked
        /// </summary>
        new public bool Checked
        {
            get => (CheckState == CheckState.Checked);
            set => CheckState = (value ? CheckState.Checked : CheckState.Unchecked);
        }

        /// <summary>
        /// Get or Set the current Node CheckState
        /// </summary>
        public CheckState CheckState
        {
            get => _checkState;
            set
            {
                _checkState = value;
                //switch (_checkState)
                //{
                //    case CheckState.Checked:
                //        this.StateImageIndex = 1;
                //        break;
                //    case CheckState.Indeterminate:
                //        this.StateImageIndex = 2;
                //        break;
                //    default:
                //        this.StateImageIndex = 0 ;
                //        break;
                //}
                switch (_checkState)
                {
                    case CheckState.Checked:
//                        this.StateImageKey = (this.TreeView?.RightToLeft ?? RightToLeft.No) == RightToLeft.Yes ? TreeNodeStateImages.KeyCheckedRtl : TreeNodeStateImages.KeyChecked;
                        this.StateImageKey = TreeNodeStateImages.KeyChecked;
                        break;
                    case CheckState.Indeterminate:
                        this.StateImageKey = TreeNodeStateImages.KeyIndeterminate;
                        break;
                    default:
                        this.StateImageKey = TreeNodeStateImages.KeyUnchecked;
                        break;
                }
            }
        }

        #endregion


        #region public create nodes methods

        /// <summary>
        /// Create a Node
        /// </summary>
        /// <param name="text"></param>
        /// <param name="value"></param>
        /// <param name="state"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static TreeNodeItemSelector CreateNode(string text, object value, CheckState state, CustomNodeType type)
        {
            return new TreeNodeItemSelector(text, value, state, type);
        }

        /// <summary>
        /// Create a child Node
        /// </summary>
        /// <param name="text"></param>
        /// <param name="value"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public TreeNodeItemSelector CreateChildNode(string text, object value, CheckState state)
        {
            TreeNodeItemSelector n = null;

            //specific method for datetimenode
            if (NodeType == CustomNodeType.DateTimeNode)
            {
                n = new TreeNodeItemSelector(text, value, state, CustomNodeType.DateTimeNode);
            }

            if (n != null)
                AddChild(n);

            return n;
        }
        public TreeNodeItemSelector CreateChildNode(string text, object value)
        {
            return CreateChildNode(text, value, _checkState);
        }

        /// <summary>
        /// Add a child Node to this Node
        /// </summary>
        /// <param name="child"></param>
        protected void AddChild(TreeNodeItemSelector child)
        {
            child.Parent = this;
            Nodes.Add(child);
        }

        #endregion

    }
}