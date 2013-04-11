﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xenon.Syntax;
using Xenon.Middle;

namespace Xenon.MiddleImpl
{

    /// <summary>
    /// Aa_Tool.xml/＜editor＞要素。
    /// </summary>
    public class MemoryAatoolxml_EditorImpl : MemorySetverContainerImpl ,MemoryAatoolxml_Editor
    {



        #region 生成と破棄
        //────────────────────────────────────────

        /// <summary>
        /// コンストラクター。
        /// </summary>
        /// <param name="parent_Cf">親設定。</param>
        public MemoryAatoolxml_EditorImpl(Configuration_Node parent_Cf)
            : base(parent_Cf)
        {
            this.name = "";
        }

        //────────────────────────────────────────

        /// <summary>
        /// クリアー。
        /// </summary>
        public override void Clear()
        {
            this.parent = null;

            this.name = "";
            this.dictionary_Fsetvar_Configurationtree = new Dictionary_Fsetvar_ConfigurationtreeImpl();
        }

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        private string name;

        /// <summary>
        /// エディター名
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        //────────────────────────────────────────
        #endregion



    }


}
