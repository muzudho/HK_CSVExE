﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xenon.Middle
{



    /// <summary>
    /// データベースのフィールドの型。
    /// </summary>
    public enum EnumTypedb
    {



        #region 用意
        //────────────────────────────────────────

        Int,

        String,

        /// <summary>
        /// 型指定は string。インスタンスは Configurationtree_NodeFilepath。
        /// </summary>
        ConfFilepath,

        Bool,

        End,

        Another

        //────────────────────────────────────────
        #endregion



    }



}
