﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xenon.Syntax;
using Xenon.Controls;
using Xenon.Middle;
using Xenon.Expr;

namespace Xenon.ConfToExpr
{
    class ConfigurationtreeToExpression_V53_ADisplayImpl_ : ConfigurationtreeToExpression_AbstractImpl
    {



        #region アクション
        //────────────────────────────────────────

        public void Translate(
            Configurationtree_Node cur_Cf,
            Expressionv_3FListboxValidationImpl parent_Exprv,
            UsercontrolListbox uctLst,
            MemoryApplication memoryApplication,
            Log_TextIndented_ConfigurationtreeToExpression pg_ParsingLog,
            Log_Reports log_Reports
            )
        {
            Log_Method log_Method = new Log_MethodImpl(0, Log_ReportsImpl.BDebugmode_Static);
            log_Method.BeginMethod(Info_ConfigurationtreeToExpression.Name_Library, this, "SToE",log_Reports);

            if (log_Method.CanDebug(1))
            {
                pg_ParsingLog.Increment("(35)" + cur_Cf.Name);
            }

            //
            //

            string err_Child_SName_Node = "";
            string err_Parent_SName_Node = "";
            Configurationtree_Node err_Child_CfNode = null;



            //
            //
            //
            // 自
            //
            //
            //
            Expressionv_4ADisplayImpl cur_Exprv = new Expressionv_4ADisplayImpl(parent_Exprv, cur_Cf, memoryApplication);


            //
            //
            //
            // 属性
            //
            //
            //
            {
                {
                    PmName pmName = PmNames.S_TYPE;
                    string sValue;
                    bool bHit = cur_Cf.Dictionary_Attribute.TryGetValue(pmName, out sValue, false, log_Reports);
                    if (bHit)
                    {
                        cur_Exprv.Dictionary_SAttribute.Add(pmName.Name_Pm, sValue);
                    }
                }

                {
                    PmName pmName = PmNames.S_DESCRIPTION;
                    string sValue;
                    bool bHit = cur_Cf.Dictionary_Attribute.TryGetValue(pmName, out sValue, false, log_Reports);
                    if (bHit)
                    {
                        cur_Exprv.Dictionary_SAttribute.Add(pmName.Name_Pm, sValue);
                    }
                }
            }

            parent_Exprv.List_Expressionv_ADisplay.Add(cur_Exprv);
            uctLst.AddValidator_FListboxForItems(parent_Exprv, log_Reports);

            // #デバッグ中
            if (log_Method.CanDebug(1))
            {
                log_Method.WriteDebug_ToConsole(" 子＜f-●●＞数＝[" + cur_Cf.List_Child.Count + "]");
            }

            //
            //
            //
            // 子
            //
            //
            //
            cur_Cf.List_Child.ForEach(delegate(Configurationtree_Node child_Cf, ref bool bBreak)
            {
                if (child_Cf is Configurationtree_Node)
                {
                    Configurationtree_Node child_Configurationtree_Node = (Configurationtree_Node)child_Cf;

                    string sName_Fnc;
                    child_Configurationtree_Node.Dictionary_Attribute.TryGetValue(PmNames.S_NAME, out sName_Fnc, false, log_Reports);

                    if (NamesFnc.S_VLD_ALL_FIELDS_IS_EMPTY == sName_Fnc)
                    {
                        //
                        // ＜ｆ－ａｌｌ－ｆｉｅｌｄｓ－ｉｓ－ｅｍｐｔｙ＞要素
                        ConfigurationtreeToExpression_V54_FAllFieldsIsEmptyImpl_ to = new ConfigurationtreeToExpression_V54_FAllFieldsIsEmptyImpl_();
                        to.Translate(
                            child_Configurationtree_Node,
                            cur_Exprv,
                            memoryApplication,
                            pg_ParsingLog,
                            log_Reports
                            );
                    }
                    else if (NamesFnc.S_ALL_TRUE == sName_Fnc)
                    {
                        //
                        // ＜ｆ－ａｌｌ－ｔｒｕｅ＞要素
                        ConfigurationtreeToExpression_V54_FAllTrueImpl_ to = new ConfigurationtreeToExpression_V54_FAllTrueImpl_();
                        to.Translate(
                            child_Configurationtree_Node,
                            cur_Exprv,
                            memoryApplication,
                            pg_ParsingLog,
                            log_Reports
                            );

                    }
                    else
                    {
                        //
                        // エラー。
                        err_Child_SName_Node = child_Configurationtree_Node.Name;
                        err_Parent_SName_Node = cur_Cf.Name;
                        err_Child_CfNode = child_Configurationtree_Node;
                        bBreak = true;
                    }
                }
            });
            if (null != err_Child_SName_Node)
            {
                goto undefined_element;
            }

            goto gt_EndMethod;
        //
        //
            #region 異常系
        //────────────────────────────────────────
        undefined_element:
            {
                Builder_TexttemplateP1p tmpl = new Builder_TexttemplateP1pImpl();
                tmpl.SetParameter(1, err_Child_SName_Node, log_Reports);//子設定ノード名
                tmpl.SetParameter(2, err_Parent_SName_Node, log_Reports);//親設定ノード名
                tmpl.SetParameter(3, Log_RecordReportsImpl.ToText_Configuration(err_Child_CfNode), log_Reports);//設定位置パンくずリスト

                memoryApplication.CreateErrorReport("Er:7020;", tmpl, log_Reports);
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
            #endregion
        //
        //
        gt_EndMethod:
            if (Log_ReportsImpl.BDebugmode_Static)
            {
                pg_ParsingLog.Decrement(cur_Cf.Name);
            }
            log_Method.EndMethod(log_Reports);
        }

        //────────────────────────────────────────
        #endregion



    }
}
