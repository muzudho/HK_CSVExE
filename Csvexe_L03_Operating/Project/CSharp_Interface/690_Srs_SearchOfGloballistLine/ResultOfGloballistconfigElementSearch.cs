﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xenon.Operating
{
    public interface ResultOfGloballistconfigElementSearch
    {



        #region プロパティー
        //────────────────────────────────────────

        /// <summary>
        /// 変数の型
        /// </summary>
        string Name_Type
        {
            set;
            get;
        }

        /// <summary>
        /// 変数番号の範囲
        /// </summary>
        string Text_NumberRange
        {
            set;
            get;
        }

        /// <summary>
        /// 優先度
        /// </summary>
        string Priority
        {
            set;
            get;
        }

        //────────────────────────────────────────
        #endregion



    }
}
