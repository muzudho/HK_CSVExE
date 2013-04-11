﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using Xenon.Syntax;
using Xenon.Functions;
using Xenon.Layout;//FcCreator
using Xenon.Middle;

namespace Xenon.Csvexe
{
    public class Expression_Node_Function_BootCsvEditorExImpl : Expression_Node_Function_BootCsvEditorImpl
    {


        #region 用意
        //────────────────────────────────────────
        //
        // 関数名
        //

        /// <summary>
        /// 関数名。
        /// </summary>
        public new const string NAME_FUNCTION = "Sf:Frame02Ex;";

        //────────────────────────────────────────
        #endregion



        #region 生成と破棄
        //────────────────────────────────────────

        public Expression_Node_Function_BootCsvEditorExImpl(EnumEventhandler enumEventhandler, List<string> listS_ArgName, ConfigurationtreeToFunction_Item functiontranslatoritem)
            : base(enumEventhandler, listS_ArgName,functiontranslatoritem)
        {
        }

        public override Expression_Node_Function NewInstance(
            Expression_Node_String parent_Expression, Configuration_Node cur_Conf,
            object owner_MemoryApplication, Log_Reports pg_Logging)
        {
            Log_Method pg_Method = new Log_MethodImpl(0);
            pg_Method.BeginMethod(Info_CSVEditorImpl.Name_Library, this, "NewInstance",pg_Logging);
            //

            Expression_Node_Function f0 = new Expression_Node_Function_BootCsvEditorExImpl(this.EnumEventhandler,this.List_NameArgumentInitializer,this.Functiontranslatoritem);
            f0.Parent_Expression = parent_Expression;
            f0.Cur_Configuration = cur_Conf;
            ((Expression_Node_FunctionAbstract)f0).Owner_MemoryApplication = (MemoryApplication)owner_MemoryApplication;
            //関数名初期化
            f0.Dictionary_Expression_Attribute.Set(PmNames.S_NAME.Name_Pm, new Expression_Leaf_StringImpl(NAME_FUNCTION, null, cur_Conf), pg_Logging);

            //「プロジェクト選択時」のイベントハンドラーを上書き要求。
            {
                Expression_Node_Function expr_Func2 = Collection_Function.NewFunction2(
                        Expression_Node_Function_OnEditorSelected_Impl.NAME_FUNCTION,
                        f0,
                        cur_Conf,
                        //EnumEventhandler.Tp_B_Wr_Rhn,
                        owner_MemoryApplication,
                        pg_Logging);

                ((Expression_Node_Function_BootCsvEditorExImpl)f0).Functionitem_OnProjectSelected = expr_Func2;
            }

            //
            pg_Method.EndMethod(pg_Logging);
            return f0;
        }

        //────────────────────────────────────────
        #endregion



        #region イベントハンドラー
        //────────────────────────────────────────

        /// <summary>
        /// 独自実装のモデルをセットアップするタイミング。
        /// </summary>
        protected override void On_P2_NewModelSetup(Log_Reports pg_Logging)
        {
            // このエディター用に、ノレンをカスタマイズ。

            // 上書き

            this.Owner_MemoryApplication.MemoryForms.UsercontrolCreator1.Dictionary_UsercontrolCreator[NamesF.S_LST] = new UsercontrolCreator2_Lst03Impl();
        }

        //────────────────────────────────────────
        #endregion



    }
}
