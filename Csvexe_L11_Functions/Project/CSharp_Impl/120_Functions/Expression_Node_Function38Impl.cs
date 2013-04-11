﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using Xenon.Syntax;
using Xenon.Controls;
using Xenon.Middle;//MoOpyopyo,FormObjectProperties,Usercontrol

namespace Xenon.Functions
{
    public class Expression_Node_Function38Impl : Expression_Node_FunctionAbstract
    {



        #region 用意
        //────────────────────────────────────────
        //
        // 関数名
        //

        public static readonly string NAME_FUNCTION = "Sf:値Toセル;";

        //────────────────────────────────────────
        //
        // 引数名
        //

        /// <summary>
        /// セットしたい値。
        /// </summary>
        public static readonly string PM_FROM = PmNames.S_FROM.Name_Pm;

        /// <summary>
        /// セット先。＜fnc name="Sf:cell;"＞を子として持つもの。
        /// </summary>
        public static readonly string PM_TO = PmNames.S_TO.Name_Pm;

        //────────────────────────────────────────
        #endregion


        
        #region 生成と破棄
        //────────────────────────────────────────

        public Expression_Node_Function38Impl(EnumEventhandler enumEventhandler, List<string> listS_ArgName, ConfigurationtreeToFunction_Item functiontranslatoritem)
            :base(enumEventhandler,listS_ArgName,functiontranslatoritem)
        {
        }

        public override Expression_Node_Function NewInstance(
            Expression_Node_String parent_Expression, Configuration_Node cur_Conf,
            object/*MemoryApplication*/ owner_MemoryApplication, Log_Reports log_Reports)
        {
            Log_Method log_Method = new Log_MethodImpl(0);
            log_Method.BeginMethod(Info_Functions.Name_Library, this, "NewInstance",log_Reports);
            //

            Expression_Node_Function f0 = new Expression_Node_Function38Impl(this.EnumEventhandler,this.List_NameArgumentInitializer,this.Functiontranslatoritem);
            f0.Parent_Expression = parent_Expression;
            f0.Cur_Configuration = cur_Conf;
            ((Expression_Node_FunctionAbstract)f0).Owner_MemoryApplication = (MemoryApplication)owner_MemoryApplication;
            //関数名初期化
            f0.SetAttribute(PmNames.S_NAME.Name_Pm, new Expression_Leaf_StringImpl(NAME_FUNCTION, null, cur_Conf), log_Reports);

            f0.SetAttribute(Expression_Node_Function38Impl.PM_FROM, new Expression_Node_StringImpl(this, cur_Conf), log_Reports);
            f0.SetAttribute(Expression_Node_Function38Impl.PM_TO, new Expression_Node_StringImpl(this, cur_Conf), log_Reports);

            //
            log_Method.EndMethod(log_Reports);
            return f0;
        }

        //────────────────────────────────────────
        #endregion




        #region アクション

        /// <summary>
        /// アクション実行。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override string Execute5_Main(Log_Reports log_Reports)
        {
            Log_Method log_Method = new Log_MethodImpl(0, Log_ReportsImpl.BDebugmode_Static);
            log_Method.BeginMethod(Info_Functions.Name_Library, this, "Execute5_Main",log_Reports);
            //
            //

            if (this.EnumEventhandler == EnumEventhandler.O_Lr)
            {
                this.Execute6_Sub(
                    log_Reports
                    );
            }
            else if (this.EnumEventhandler == EnumEventhandler.O_Ea)
            {
                this.Execute6_Sub(
                    log_Reports
                    );
            }

            //
            //
            log_Method.EndMethod(log_Reports);
            return "";
        }

        //────────────────────────────────────────

        protected void Execute6_Sub(
            Log_Reports log_Reports
            )
        {
            Log_Method log_Method = new Log_MethodImpl(0, Log_ReportsImpl.BDebugmode_Static);
            log_Method.BeginMethod(Info_Functions.Name_Library, this, "Execute6_Sub", log_Reports);

            if (log_Reports.CanStopwatch)
            {
                string sFncName;
                this.TrySelectAttribute(out sFncName, PmNames.S_NAME.Name_Pm, EnumHitcount.One_Or_Zero, log_Reports);
                log_Method.Log_Stopwatch.Message = "Nアクション[" + sFncName + "]実行";
                log_Method.Log_Stopwatch.Begin();
            }
            //
            //

            //
            // テーブルにデータを書き出す方法。
            string err_sNodeName;
            //string err_sFncName;
            {
                ToMemory_Performer toM = new ExpressionDataTargetUpdaterImpl();

                // ID？ 『f-var value="Us:クリップmr_SK10;"』のように記述されているので、変数展開して "6001"等 を取得する。
                string sFrom;
                this.TrySelectAttribute(out sFrom, Expression_Node_Function38Impl.PM_FROM, EnumHitcount.One_Or_Zero, log_Reports);
                //ystem.Console.WriteLine(this.GetType().Name + "#: ”ｆｒｏｍ”の型＝[" + this.In_nFrom.GetType().Name + "]　”ｆｒｏｍ”の子要素数＝[" + this.In_nFrom.ChildNList.Count + "] sFrom＝[" + sFrom + "]");

                // 『Sf:cell;』で、セルが指定されているはず。
                Expression_Node_String ec_ArgTo;
                this.TrySelectAttribute(out ec_ArgTo, Expression_Node_Function38Impl.PM_TO, EnumHitcount.One_Or_Zero, log_Reports);

                {
                    string sNodeName;
                    sNodeName = ec_ArgTo.Cur_Configuration.Name;

                    // ａｒｇ３はバグで、ｎａｍｅ属性は取得できない。
                    //string sFncName;
                    //e_ArgTo.TrySelectAttribute(out sFncName, PmNames.NAME.SAttrName, true, EnumHitcount.Unconstraint, log_Reports);

                    if (!(NamesNode.S_ARG == sNodeName))// && E_SysFnc38Impl.S_ARG_TO == sFncName
                    {
                        // エラー
                        err_sNodeName = sNodeName;
                        //err_sFncName = sFncName;
                        goto gt_Error_NotTo;
                    }
                }

                if (log_Reports.Successful)
                {
                    toM.ToMemory_ParentFcells(
                        sFrom,
                        ec_ArgTo,// Ｓｆ：ｃｅｌｌ；の親を指定すること。
                        this.Owner_MemoryApplication,
                        log_Reports
                        );
                }

            }

            goto gt_EndMethod;
        //
        //
            #region 異常系
        //────────────────────────────────────────
        gt_Error_NotTo:
            {
                Builder_TexttemplateP1p tmpl = new Builder_TexttemplateP1pImpl();
                tmpl.SetParameter(1, Expression_Node_Function38Impl.PM_TO, log_Reports);//引数名
                tmpl.SetParameter(2, err_sNodeName, log_Reports);//ノード名

                this.Owner_MemoryApplication.CreateErrorReport("Er:110025;", tmpl, log_Reports);
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
            #endregion
        //
        //
        gt_EndMethod:
            log_Method.EndMethod(log_Reports);
        }

        //────────────────────────────────────────
        #endregion



    }
}
