﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using Xenon.Syntax;
using Xenon.Controls;
using Xenon.Middle;

namespace Xenon.XmlToConf
{
    /// <summary>
    /// 
    /// </summary>
    class XmlToConfigurationtree_V_3FListboxValidationImpl_ : XmlToConfigurationtree_C_Parser15Impl
    {



        #region 生成と破棄
        //────────────────────────────────────────

        protected override Configurationtree_Node CreateMyself(
            XmlElement cur_X, Configurationtree_Node parent_Cf, MemoryApplication memoryApplication, Log_Reports log_Reports)
        {
            Configurationtree_Node cur_Cf;
            cur_Cf = new Configurationtree_NodeImpl(NamesNode.S_F_LISTBOX_VALIDATION, parent_Cf);

            return cur_Cf;
        }

        //────────────────────────────────────────
        #endregion



        #region アクション
        //────────────────────────────────────────

        protected override void Parse_SAttribute(
            XmlElement cur_X,
            Configurationtree_Node cur_Cf,
            MemoryApplication memoryApplication,
            Log_Reports log_Reports
            )
        {
            Log_Method log_Method = new Log_MethodImpl(0);
            log_Method.BeginMethod(Info_XmlToConf.Name_Library, this, "Parse_SAttr",log_Reports);
            //
            //

            //string sIvtv = x_Cur.Attributes.GetNamedItem(AttrNames.S_ITEM_VALUE_TO_VARIABLE).Value;
            //string sIvtvTrim = "";
            //if (null == sIvtv)
            //{
            //    sIvtvTrim = "";
            //}
            //else
            //{
            //    sIvtvTrim = sIvtv.Trim();
            //}
            cur_Cf.Dictionary_Attribute.Set(PmNames.S_NAME_VAR.Name_Pm, "", log_Reports);// PmNames.Z_ITEM_VALUE_TO_VARIABLE sIvtv;

            goto gt_EndMethod;
        //
        //
        gt_EndMethod:
            log_Method.EndMethod(log_Reports);
        }

        //────────────────────────────────────────

        protected override void Parse_ChildNodes(
            XmlElement cur_X,
            Configurationtree_Node cur_Cf,
            MemoryApplication memoryApplication,
            Log_Reports log_Reports
            )
        {
            Log_Method log_Method = new Log_MethodImpl(0, Log_ReportsImpl.BDebugmode_Static);
            log_Method.BeginMethod(Info_XmlToConf.Name_Library, this, "Parse_ChildNodes",log_Reports);
            //

            XmlElement err_XADisplay = null;

            Usercontrol uct = null;
            if (log_Reports.Successful)
            {
                Configuration_Node cf_Control = cur_Cf.GetParentByNodename(
                    NamesNode.S_CONTROL1, EnumConfiguration.Tree, true, log_Reports);

                if (log_Reports.Successful)
                {
                    uct = Utility_XmlToConfigurationtree_NodeImpl.GetUsercontrol(
                        (Configurationtree_Node)cf_Control, memoryApplication, log_Reports);
                }
            }

            if (log_Reports.Successful)
            {
                if (uct is UsercontrolListbox)
                {
                    //
                    // リストボックスなら。
                    UsercontrolListbox uctLst = (UsercontrolListbox)uct;

                    //
                    // ＜a-select-record＞、＜ａ－ｄｉｓｐｌａｙ＞要素
                    //
                    XmlNodeList child_XNl = cur_X.ChildNodes;

                    foreach (XmlNode x_childNode in child_XNl)
                    {
                        if (XmlNodeType.Element == x_childNode.NodeType)
                        {
                            XmlElement xChild = (XmlElement)x_childNode;
                            err_XADisplay = xChild;

                            string child_SName_Fnc = xChild.GetAttribute(PmNames.S_NAME.Name_Attribute);

                            //
                            //
                            if (NamesFnc.S_VLD_DISPLAY == child_SName_Fnc)//【変更 2012-07-19】
                            {

                                XmlToConfigurationtree_C15_Elm to = XmlToConfigurationtree_Collection.GetTranslatorByFncName(child_SName_Fnc, log_Reports);
                                to.XmlToConfigurationtree(
                                    xChild,
                                    cur_Cf,
                                    memoryApplication,
                                    log_Reports
                                    );

                            }
                            else if (NamesFnc.S_VLD_SELECT_RECORD == child_SName_Fnc)
                            {
                                // Sf:Vld-SelectRecord;

                                XmlToConfigurationtree_C15_Elm to = XmlToConfigurationtree_Collection.GetTranslatorByFncName(child_SName_Fnc, log_Reports);
                                to.XmlToConfigurationtree(
                                    xChild,
                                    cur_Cf,
                                    memoryApplication,
                                    log_Reports
                                    );

                            }
                            else
                            {
                                //
                                // エラー。
                                goto gt_Error_UndefinedChild11;
                            }

                        }
                    }
                }
            }

            goto gt_EndMethod;
        //
            #region 異常系
        //────────────────────────────────────────
        gt_Error_UndefinedChild11:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー385！", log_Method);

                StringBuilder s = new StringBuilder();
                s.Append("＜ｆ－ｌｉｓｔ－ｂｏｘ－ｖａｌｉｄａｔｉｏｎ＞要素に、＜ａ－ｄｉｓｐｌａｙ＞＜a-select-record＞要素以外の要素");
                s.Append(Environment.NewLine);
                s.Append("[");
                s.Append(err_XADisplay.Name);
                s.Append("]が含まれていました。");
                s.Append(Environment.NewLine);
                s.Append(Environment.NewLine);

                // ヒント

                r.Message = s.ToString();
                log_Reports.EndCreateReport();
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
        gt_Error_UndefinedClass:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー386！", log_Method);

                StringBuilder s = new StringBuilder();
                s.Append("なんらかのエラー。");
                s.Append(Environment.NewLine);

                // ヒント

                r.Message = s.ToString();
                log_Reports.EndCreateReport();
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
            #endregion
        //
        gt_EndMethod:
            log_Method.EndMethod(log_Reports);
        }

        //────────────────────────────────────────

        protected override void LinkToParent(
            Configurationtree_Node cur_Cf, Configurationtree_Node parent_Cf, MemoryApplication memoryApplication, Log_Reports log_Reports)
        {
            Log_Method log_Method = new Log_MethodImpl(0, Log_ReportsImpl.BDebugmode_Static);
            log_Method.BeginMethod(Info_XmlToConf.Name_Library, this, "LinkToParent", log_Reports);

            Usercontrol uct = null;
            if (log_Reports.Successful)
            {
                Configuration_Node cf_Control = cur_Cf.GetParentByNodename(
                    NamesNode.S_CONTROL1, EnumConfiguration.Tree, true, log_Reports);

                if (log_Reports.Successful)
                {
                    uct = Utility_XmlToConfigurationtree_NodeImpl.GetUsercontrol(
                        (Configurationtree_Node)cf_Control, memoryApplication, log_Reports);
                }
            }

            uct.ControlCommon.Configurationtree_Control.List_Child.Add(cur_Cf, log_Reports);
            goto gt_EndMethod;
        //
            #region 異常系
        //────────────────────────────────────────
        gt_Error_UndefinedClass:
            if (log_Reports.CanCreateReport)
            {
                Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                r.SetTitle("▲エラー386！", log_Method);

                StringBuilder s = new StringBuilder();
                s.Append("なんらかのエラー。");
                s.Append(Environment.NewLine);

                // ヒント

                r.Message = s.ToString();
                log_Reports.EndCreateReport();
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
            #endregion
        //
        gt_EndMethod:
            log_Method.EndMethod(log_Reports);
        }

        //────────────────────────────────────────
        #endregion



    }
}
