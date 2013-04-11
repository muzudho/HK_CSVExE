﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xenon.Syntax;

namespace Xenon.Middle
{
    public interface MemoryTogethers
    {



        #region 生成と破棄
        //────────────────────────────────────────

        /// <summary>
        /// new した直後の内容に戻します。
        /// </summary>
        void Clear(MemoryApplication owner_MemoryApplication);

        //────────────────────────────────────────
        #endregion



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// Rfr 設定ファイル読取。
        /// </summary>
        /// <param name="n_FilePath_Rfr"></param>
        /// <param name="log_Reports"></param>
        void LoadFile(
            Expression_Node_Filepath ec_FilePath_Rfr,
            Log_Reports log_Reports
            );

        //────────────────────────────────────────

        /// <summary>
        /// コントロールに、最新のデータを表示します。
        /// </summary>
        /// <param name="together_Conf">トゥゲザー要素の名前です。</param>
        /// <param name="log_Reports"></param>
        void RefreshDataByTogether(
            Configurationtree_Node together_Conf,
            Log_Reports log_Reports
            );

        /// <summary>
        /// フォームのデータの再読み込みを行います。
        /// 
        /// どのフォームを再読み込みするかは、コントロール・リフレッシュ設定ファイルで
        /// 設定しているトゥゲザー要素の名前を指定します。
        /// </summary>
        /// <param name="o_Name_Together"></param>
        void RefreshDataRange(
            XenonName o_Name_Together,
            Log_Reports log_Reports
            );

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        /// <summary>
        /// トゥゲザー設定ファイル。
        /// </summary>
        Configurationtree_Node Configurationtree_Togetherconfig
        {
            set;
            get;
        }

        //────────────────────────────────────────
        #endregion



    }
}
