﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;//DataRowView
using System.Windows.Forms;//Application
using Xenon.Controls;
using Xenon.Syntax;
using Xenon.Middle;//MoOpyopyo,FormObjectProperties,Usercontrol
using Xenon.Table;//DefaultTable
using Xenon.Expr;

namespace Xenon.Functions
{



    /// <summary>
    /// 指定した変数に、フィールド値を格納します。
    /// </summary>
    public class Expression_Node_Function25Impl : Expression_Node_FunctionAbstract
    {



        #region 用意
        //────────────────────────────────────────
        //
        // 関数名
        //

        public static readonly string NAME_FUNCTION = "Sf:変数設定_選択行の列指定;";

        //────────────────────────────────────────
        //
        // 引数名
        //

        /// <summary>
        /// フィールド名。
        /// 
        /// 元は名無し。
        /// </summary>
        public static readonly string PM_NAME_FIELD = PmNames.S_NAME_FIELD.Name_Pm;

        /// <summary>
        /// 値格納先変数名。
        /// </summary>
        public static readonly string PM_NAME_VAR_DESTINATION = PmNames.S_NAME_VAR_DESTINATION.Name_Pm;

        //────────────────────────────────────────
        #endregion



        #region 生成と破棄
        //────────────────────────────────────────

        public Expression_Node_Function25Impl(EnumEventhandler enumEventhandler, List<string> listS_ArgName, ConfigurationtreeToFunction_Item functiontranslatoritem)
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

            Expression_Node_Function f0 = new Expression_Node_Function25Impl(this.EnumEventhandler,this.List_NameArgumentInitializer,this.Functiontranslatoritem);
            f0.Parent_Expression = parent_Expression;
            f0.Cur_Configuration = cur_Conf;
            ((Expression_Node_FunctionAbstract)f0).Owner_MemoryApplication = (MemoryApplication)owner_MemoryApplication;
            //関数名初期化
            f0.SetAttribute(PmNames.S_NAME.Name_Pm, new Expression_Leaf_StringImpl(NAME_FUNCTION, null, cur_Conf), log_Reports);

            f0.SetAttribute(Expression_Node_Function25Impl.PM_NAME_FIELD, new Expression_Leaf_StringImpl("", null, cur_Conf), log_Reports);
            f0.SetAttribute(Expression_Node_Function25Impl.PM_NAME_VAR_DESTINATION, new Expression_Leaf_StringImpl("", null, cur_Conf), log_Reports);

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
        public override string Execute5_Main(Log_Reports log_Reports)// EventArgs e
        {
            //
            //
            //
            //（）メソッド開始
            //
            //
            //
            Log_Method log_Method = new Log_MethodImpl(0, Log_ReportsImpl.BDebugmode_Static);
            log_Method.BeginMethod(Info_Functions.Name_Library, this, "Execute5_Main",log_Reports);

            string sFncName0;
            this.TrySelectAttribute(out sFncName0, PmNames.S_NAME.Name_Pm, EnumHitcount.One_Or_Zero, log_Reports);

            if (log_Reports.CanStopwatch)
            {
                log_Method.Log_Stopwatch.Message = "Nアクション[" + sFncName0 + "]実行";
                log_Method.Log_Stopwatch.Begin();
            }

            if (this.EnumEventhandler == EnumEventhandler.O_Ea)
            {
                if (this.Functionparameterset.Sender is Customcontrol)
                {
                    Customcontrol fcCc = (Customcontrol)this.Functionparameterset.Sender;

                    string sName_Usercontrol = fcCc.ControlCommon.Expression_Name_Control.Execute4_OnExpressionString(
                        EnumHitcount.Unconstraint,
                        log_Reports
                        );

                    log_Reports.Comment_EventCreationMe = "／追記：[" + sName_Usercontrol + "]コントロールが、[" + sFncName0 + "]アクションを実行。";
                }
                else
                {
                    log_Reports.Comment_EventCreationMe = "／追記：[" + sFncName0 + "]アクションを実行。";
                }


                ListBox pcLst = (ListBox)this.Functionparameterset.Sender;
                this.Execute6_Sub(
                    pcLst,
                    log_Reports
                    );
            }

            log_Method.EndMethod(log_Reports);
            return "";
        }

        //────────────────────────────────────────

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pcLst"></param>
        /// <param name="log_Reports"></param>
        protected void Execute6_Sub(
            ListBox pcLst,
            Log_Reports log_Reports
            )
        {
            //
            //
            //
            //（）メソッド開始
            //
            //
            //
            Log_Method log_Method = new Log_MethodImpl(0, Log_ReportsImpl.BDebugmode_Static);
            log_Method.BeginMethod(Info_Functions.Name_Library, this, "Execute6_Sub", log_Reports);


            //
            //このイベントが起こったリストボックスの名前。
            //
            string sName_Control;
            if (pcLst is CustomcontrolListbox)
            {
                CustomcontrolListbox cclst = (CustomcontrolListbox)pcLst;
                sName_Control = cclst.ControlCommon.Expression_Name_Control.Execute4_OnExpressionString(EnumHitcount.Unconstraint, log_Reports);
                log_Method.WriteDebug_ToConsole(sName_Control);
            }
            else
            {
                sName_Control = "";
            }


            //
            //
            //
            //現在、選択している項目。
            //
            //
            //
            DataRowView selectedDataRow = (DataRowView)pcLst.SelectedItem;
            if (null == selectedDataRow)
            {
                // 選択している行がなければ。

                // エラー。
                goto gt_Error_NoSelectedField;
            }


            //
            //
            //
            // 現在選択しているレコードの 指定フィールドの値を取得します。
            //
            //
            //
            {
                //指定されているフィールド名。
                string sName_Field;
                this.TrySelectAttribute(out sName_Field, Expression_Node_Function25Impl.PM_NAME_FIELD, EnumHitcount.One_Or_Zero, log_Reports);

                //そのフィールドの値。
                IntCellImpl cellData = (IntCellImpl)selectedDataRow[sName_Field];
                string sValue_Field = cellData.Text.Trim();
                //.WriteLine(this.GetType().Name + "#: ◆　fieldValue=[" + fieldValue + "]");

                //変数名。
                Expression_Node_String ec_Name_ArgDestinationVariable;
                this.TrySelectAttribute(out ec_Name_ArgDestinationVariable, Expression_Node_Function25Impl.PM_NAME_VAR_DESTINATION, EnumHitcount.One_Or_Zero, log_Reports);

                //指定した変数に、フィールド値を格納します。
                this.Owner_MemoryApplication.MemoryVariables.SetStringValue(
                    new XenonNameImpl(
                        ec_Name_ArgDestinationVariable.Execute4_OnExpressionString(EnumHitcount.Unconstraint, log_Reports),
                        ec_Name_ArgDestinationVariable.Cur_Configuration
                        ),
                    sValue_Field,
                    true,
                    log_Reports
                    );
            }

            goto gt_EndMethod;
        //
        //
            #region 異常系
        //────────────────────────────────────────
        gt_Error_NoSelectedField:
            {
                Builder_TexttemplateP1p tmpl = new Builder_TexttemplateP1pImpl();
                tmpl.SetParameter(1, sName_Control, log_Reports);//コントロール名
                tmpl.SetParameter(2, pcLst.SelectedIndex.ToString(), log_Reports);//リストボックスの選択しているindex

                this.Owner_MemoryApplication.CreateErrorReport("Er:110010;", tmpl, log_Reports);
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
