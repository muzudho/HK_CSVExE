﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;//XmlNode

using System.Windows.Forms;
using Xenon.Syntax;
using Xenon.Middle;


namespace Xenon.XmlToConf
{

    /// <summary>
    /// ＜ｆｎｃ＞の子要素を解析。
    /// </summary>
    public class XmlToConfigurationtree_C14_HubImpl : XmlToConfigurationtree_C14_Hub
    {



        #region アクション
        //────────────────────────────────────────

        public void XmlToConfigurationtree(
            XmlElement cur_X,
            Configurationtree_Node cur_Cf,
            MemoryApplication memoryApplication,
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
            log_Method.BeginMethod(Info_XmlToConf.Name_Library, this, "XmlToConfigurationtree", log_Reports);


            //
            //
            //
            //（）子要素
            //
            //
            //
            XmlNodeList child_XNl = cur_X.ChildNodes;

            string err_SName_CurNode = "";
            XmlElement err_XElm = null;
            Exception err_Excp = null;

            foreach (XmlNode child_XNode in child_XNl)
            {
                if (XmlNodeType.Element == child_XNode.NodeType)
                {
                    XmlElement xChild = (XmlElement)child_XNode;

                    string sName_Node = xChild.Name;


                    XmlToConfigurationtree_C15_Elm to;

                    bool bHit = this.Dictionary_XmlToConfigurationtree_ElmP.ContainsKey(sName_Node);
                    if (!bHit)
                    {
                        // 未該当＝エラー
                        err_SName_CurNode = cur_X.Name;
                        err_XElm = xChild;
                        goto gt_Error_UndefinedElement;
                    }

                    to = this.Dictionary_XmlToConfigurationtree_ElmP[sName_Node];

                    if (log_Reports.Successful)
                    {
                        try
                        {
                            to.XmlToConfigurationtree(xChild, cur_Cf, memoryApplication, log_Reports);
                        }
                        catch (Exception ex)
                        {
                            //
                            // エラー。
                            err_XElm = xChild;
                            err_Excp = ex;
                            goto gt_Error_Exception;
                        }
                    }

                }

            }

            goto gt_EndMethod;
        //
        //
            #region 異常系
        //────────────────────────────────────────
        gt_Error_UndefinedElement:
            {
                Builder_TexttemplateP1p tmpl = new Builder_TexttemplateP1pImpl();
                tmpl.SetParameter(1, err_XElm.Name, log_Reports);//子要素名
                tmpl.SetParameter(2, err_SName_CurNode, log_Reports);//自要素名

                StringBuilder s1 = new StringBuilder();
                foreach (string key in this.Dictionary_XmlToConfigurationtree_ElmP.Keys)
                {
                    s1.Append("　＜");
                    s1.Append(key);
                    s1.Append("＞");
                    s1.Append(Environment.NewLine);
                }
                tmpl.SetParameter(3, s1.ToString(), log_Reports);//要素リスト

                tmpl.SetParameter(4, Log_RecordReportsImpl.ToText_Configuration(cur_Cf), log_Reports);//設定位置パンくずリスト

                memoryApplication.CreateErrorReport("Er:8033;", tmpl, log_Reports);
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
        gt_Error_Exception:
            {
                Builder_TexttemplateP1p tmpl = new Builder_TexttemplateP1pImpl();
                tmpl.SetParameter(1, err_XElm.Name, log_Reports);//要素名
                tmpl.SetParameter(2, Log_RecordReportsImpl.ToText_Configuration(cur_Cf), log_Reports);//設定位置パンくずリスト
                tmpl.SetParameter(3, Log_RecordReportsImpl.ToText_Exception(err_Excp), log_Reports);//例外メッセージ

                memoryApplication.CreateErrorReport("Er:8034;", tmpl, log_Reports);
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


        
        #region プロパティー
        //────────────────────────────────────────

        private static Dictionary<string, XmlToConfigurationtree_C15_Elm> dictionary_XmlToConfigurationtree_ElmP;

        private Dictionary<string, XmlToConfigurationtree_C15_Elm> Dictionary_XmlToConfigurationtree_ElmP
        {
            get
            {
                //
                //
                //
                //（）メソッド開始
                //
                //
                //
                Log_Method log_Method = new Log_MethodImpl(0);
                Log_Reports d_Logging_ThisMethod = new Log_ReportsImpl(log_Method);
                log_Method.BeginMethod(Info_XmlToConf.Name_Library, this, "Dictionary_XmlToConfigurationtree_ElmP set", d_Logging_ThisMethod);


                if (null == dictionary_XmlToConfigurationtree_ElmP)
                {
                    dictionary_XmlToConfigurationtree_ElmP = new Dictionary<string, XmlToConfigurationtree_C15_Elm>();

                    //
                    // TODO: 間違った入れ子関係も　読み取りしてしまうので、そこらへんのチェックも入れたい。
                    //

                    //
                    //
                    //
                    //子要素パーサー一覧
                    //
                    //
                    //

                    //
                    //＜ｆ－ｓｔｒ＞
                    //
                    {
                        XmlToConfigurationtree_C15_Elm to = XmlToConfigurationtree_Collection.GetTranslatorByNodeName(NamesNode.S_F_STR, d_Logging_ThisMethod);
                        dictionary_XmlToConfigurationtree_ElmP.Add(NamesNode.S_F_STR, to);
                    }

                    //
                    //＜ｆ－ｖａｒ＞
                    //
                    {
                        XmlToConfigurationtree_C15_Elm to = XmlToConfigurationtree_Collection.GetTranslatorByNodeName(NamesNode.S_F_VAR, d_Logging_ThisMethod);
                        dictionary_XmlToConfigurationtree_ElmP.Add(NamesNode.S_F_VAR, to);
                    }

                    //
                    //＜ｆ－ｐａｒａｍ＞
                    //
                    {
                        XmlToConfigurationtree_C15_Elm to = XmlToConfigurationtree_Collection.GetTranslatorByNodeName(NamesNode.S_F_PARAM, d_Logging_ThisMethod);
                        dictionary_XmlToConfigurationtree_ElmP.Add(NamesNode.S_F_PARAM, to);
                    }

                    //
                    //＜ｆｎｃ＞
                    //
                    {
                        XmlToConfigurationtree_C15_Elm to = XmlToConfigurationtree_Collection.GetTranslatorByNodeName(NamesNode.S_FNC, d_Logging_ThisMethod);
                        dictionary_XmlToConfigurationtree_ElmP.Add(NamesNode.S_FNC, to);
                    }

                    //
                    //＜ｄｅｆ－ｐａｒａｍ＞
                    //
                    {
                        XmlToConfigurationtree_C15_Elm to = XmlToConfigurationtree_Collection.GetTranslatorByNodeName(NamesNode.S_DEF_PARAM, d_Logging_ThisMethod);
                        dictionary_XmlToConfigurationtree_ElmP.Add(NamesNode.S_DEF_PARAM, to);
                    }

                    //
                    //＜ａｒｇ＞　※＜ｆｎｃ＞＜ａｃｔｉｏｎ＞専用の子要素。
                    //
                    {
                        XmlToConfigurationtree_C15_Elm to = XmlToConfigurationtree_Collection.GetTranslatorByNodeName(NamesNode.S_ARG, d_Logging_ThisMethod);
                        dictionary_XmlToConfigurationtree_ElmP.Add(NamesNode.S_ARG, to);
                    }
                }

                //
                //
                log_Method.EndMethod(d_Logging_ThisMethod);
                d_Logging_ThisMethod.EndLogging(log_Method);

                return dictionary_XmlToConfigurationtree_ElmP;
            }
        }

        //────────────────────────────────────────
        #endregion



    }
}
