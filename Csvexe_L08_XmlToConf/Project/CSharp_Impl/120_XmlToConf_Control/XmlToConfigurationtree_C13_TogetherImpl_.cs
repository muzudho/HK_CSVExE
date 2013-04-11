﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;//XmlNode
using Xenon.Syntax;//Log_TextIndented
using Xenon.Table;
using Xenon.Middle;

namespace Xenon.XmlToConf
{

    /// <summary>
    /// (Stg) ＜ｔｏｇｅｔｈｅｒ＞
    /// 
    /// TODO:　フォーム設定ファイルの中に＜ｔｏｇｅｔｈｅｒ＞要素を書く形にしたい。
    /// </summary>
    class XmlToConfigurationtree_C13_TogetherImpl_ : XmlToConfigurationtree_C_Parser15Impl
    {



        #region 生成と破棄
        //────────────────────────────────────────

        protected override Configurationtree_Node CreateMyself(
            XmlElement cur_X, Configurationtree_Node parent_Cf, MemoryApplication memoryApplication, Log_Reports log_Reports)
        {
            Configurationtree_Node cur_Cf;

            if (NamesNode.S_CODEFILE_TOGETHERS == parent_Cf.Name)
            {
                cur_Cf = new Configurationtree_NodeImpl(NamesNode.S_TOGETHER, parent_Cf);
                cur_Cf.Dictionary_Attribute.Set(PmNames.S_IN.Name_Pm, "", log_Reports);
            }
            else
            {
                cur_Cf = new Configurationtree_NodeImpl(NamesNode.S_TOGETHER, parent_Cf);
            }

            return cur_Cf;
        }

        //────────────────────────────────────────
        #endregion



        #region アクション
        //────────────────────────────────────────

        public override void XmlToConfigurationtree(//override
            XmlElement cur_X,
            Configurationtree_Node parent_Cf,//トゥゲザー設定ファイル
            MemoryApplication memoryApplication,
            Log_Reports log_Reports
            )
        {
            Log_Method log_Method = new Log_MethodImpl(1);
            log_Method.BeginMethod(Info_XmlToConf.Name_Library, this, "XmlToConfigurationtree", log_Reports);
            //
            //



            //
            //
            //
            // 自
            //
            //
            //
            Configurationtree_Node cur_Cf = this.CreateMyself(cur_X, parent_Cf, memoryApplication, log_Reports);


            //
            //
            //
            //「トゥゲザー登録ファイル」に書かれているのか、
            //「コントロール設定ファイル」に書かれているのかで、処理を変えます。
            //
            //
            //
            bool bGlobalRfr;
            if (NamesNode.S_CODEFILE_TOGETHERS == parent_Cf.Name)
            {
                bGlobalRfr = true;

                //if (log_Method.CanDebug(1))
                //{
                //    log_Method.WriteDebug_ToConsole("親要素がトゥゲザーコンフィグってことは、グローバル・トゥゲザー？");
                //}
            }
            else
            {
                bGlobalRfr = false;

                //if (log_Method.CanDebug(1))
                //{
                //    log_Method.WriteDebug_ToConsole("トゥゲザーコンフィグじゃないって何？");
                //}
            }



            //
            //
            //
            // 属性
            //
            //
            //

            //ｎａｍｅ（未設定可）
            if (log_Reports.Successful)
            {
                XmlNode xNd = cur_X.Attributes.GetNamedItem(PmNames.S_NAME.Name_Attribute);
                if (null != xNd)
                {
                    cur_Cf.Dictionary_Attribute.Add(PmNames.S_NAME.Name_Pm, xNd.Value, cur_Cf, false, log_Reports);
                }
            }

            //ｉｎ（未設定可。コントロール設定ファイルには無い）
            if (log_Reports.Successful)
            {
                if (bGlobalRfr)
                {
                    XmlNode xNd = cur_X.Attributes.GetNamedItem(PmNames.S_IN.Name_Pm);
                    if (null != xNd)
                    {
                        cur_Cf.Dictionary_Attribute.Set(PmNames.S_IN.Name_Pm, xNd.Value, log_Reports);
                    }
                }
            }

            //ｏｎ（コントロール設定ファイルでは必須、グローバル・トゥゲザー登録ファイルには無い）
            if (log_Reports.Successful)
            {
                if (!bGlobalRfr)
                {
                    XmlNode xNd = cur_X.Attributes.GetNamedItem(PmNames.S_ON.Name_Attribute);
                    if (null != xNd)
                    {
                        cur_Cf.Dictionary_Attribute.Add(PmNames.S_ON.Name_Pm, xNd.Value, cur_Cf, false, log_Reports);
                    }
                    else
                    {
                        // エラー
                        goto gt_Error_NoOn;
                    }
                }
            }

            // ｔａｒｇｅｔ（コントロール設定ファイルでは必須、グローバル・トゥゲザー登録ファイルには無い）
            if (log_Reports.Successful)
            {
                if (!bGlobalRfr)
                {
                    XmlNode xNd = cur_X.Attributes.GetNamedItem(PmNames.S_TARGET1.Name_Attribute);
                    if (null != xNd)
                    {
                        cur_Cf.Dictionary_Attribute.Add(PmNames.S_TARGET1.Name_Pm, xNd.Value, cur_Cf, false, log_Reports);
                    }
                    else
                    {
                        // エラー
                        goto gt_Error_NoTarget;
                    }
                }


            }

            //ｄｅｓｃｒｉｐｔｉｏｎ（未設定可）
            if (log_Reports.Successful)
            {
                XmlNode xNd = cur_X.Attributes.GetNamedItem(PmNames.S_DESCRIPTION.Name_Attribute);
                if (null != xNd)
                {
                    cur_Cf.Dictionary_Attribute.Add(PmNames.S_DESCRIPTION.Name_Pm, xNd.Value, cur_Cf, true, log_Reports);
                }
            }



            //
            //
            //
            // 子
            //
            //
            //
            XmlElement err_Child_X;
            if (log_Reports.Successful)
            {
                if (bGlobalRfr)
                {
                    if (log_Reports.Successful)
                    {
                        //
                        // ｔａｒｇｅｔ要素
                        //
                        XmlNodeList child_XNl = cur_X.ChildNodes;

                        foreach (XmlNode child_XNode in child_XNl)
                        {
                            if (XmlNodeType.Element == child_XNode.NodeType)
                            {
                                XmlElement xChild = (XmlElement)child_XNode;

                                if (NamesNode.S_TARGET == xChild.Name)
                                {
                                    //
                                    // ｔａｒｇｅｔ要素
                                    //
                                    string sName_Target = xChild.Attributes.GetNamedItem(PmNames.S_NAME.Name_Attribute).Value;

                                    Configurationtree_Node cfRfr_Target = new Configurationtree_NodeImpl(NamesNode.S_TARGET, cur_Cf);
                                    cfRfr_Target.Dictionary_Attribute.Set(PmNames.S_NAME.Name_Pm, sName_Target, log_Reports);

                                    cur_Cf.List_Child.Add(cfRfr_Target, log_Reports);
                                }
                                else
                                {
                                    // エラー
                                    err_Child_X = xChild;
                                    goto gt_Error_Child;
                                }
                            }

                        }
                    }
                }
            }


            //
            //
            //
            // 親
            //
            //
            //
            string err_SIn;
            if (bGlobalRfr)
            {
                string sIn;
                if (log_Reports.Successful)
                {
                    // 重複チェック用。
                    List<string> sList_In = new List<string>();
                    List<string> sList_Name = new List<string>();


                    //
                    //
                    //
                    // （１）in属性が付いていれば　そちらへ、
                    // （２）nameが付いていれば　そちらへ。
                    // 重複名があれば発見したい。
                    //
                    //
                    //
                    cur_Cf.Dictionary_Attribute.TryGetValue(PmNames.S_IN, out sIn,
                        false,//空文字列でも構わない。
                        log_Reports);

                    string sName_Rfr;
                    cur_Cf.Dictionary_Attribute.TryGetValue(PmNames.S_NAME, out sName_Rfr, false, log_Reports);


                    if ("" != sIn)
                    {
                        // トゥゲザー登録ファイルに、in指定での＜ｔｏｇｅｔｈｅｒ＞要素を追加。

                        // 重複チェック。
                        if (!sList_In.Contains(sIn))
                        {
                            sList_In.Add(sIn);
                            parent_Cf.List_Child.Add(cur_Cf, log_Reports);
                        }
                        else
                        {
                            // エラー。
                            err_SIn = sIn;
                            goto gtj_Error_DuplicationIn;
                        }
                    }
                    else if ("" != sName_Rfr)
                    {
                        // トゥゲザー設定ファイルに、name指定での＜ｔｏｇｅｔｈｅｒ＞要素を追加。

                        // 重複チェック。
                        if (!sList_Name.Contains(sName_Rfr))
                        {
                            sList_Name.Add(sName_Rfr);
                            parent_Cf.List_Child.Add(cur_Cf, log_Reports);
                        }
                        else
                        {
                            // エラー
                            goto gt_Error_DuplicationTogether;
                        }
                    }
                    else
                    {
                        // エラー
                        goto gt_Error_Attr;
                    }
                }



                goto gt_EndMethod;
            }
            else
            {

                //
                //
                //
                // 親
                //
                //
                //
                if (log_Reports.Successful)
                {
                    string sOn;
                    cur_Cf.Dictionary_Attribute.TryGetValue(PmNames.S_ON, out sOn, false, log_Reports);

                    List<Configurationtree_Node> listCf_Together = parent_Cf.GetChildrenByNodename(NamesNode.S_TOGETHER, false, log_Reports);
                    foreach (Configurationtree_Node cf_Together in listCf_Together)
                    {
                        string sOn2;
                        cf_Together.Dictionary_Attribute.TryGetValue(PmNames.S_ON, out sOn2, false, log_Reports);

                        if (sOn == sOn2)
                        {
                            // エラー
                            goto gt_Error_DuplicationOn;
                        }
                    }

                    parent_Cf.List_Child.Add(cur_Cf, log_Reports);
                }
            }

            goto gt_EndMethod;
            //
        //
            #region 異常系
        //────────────────────────────────────────
        gtj_Error_DuplicationIn:
            {
                Builder_TexttemplateP1p tmpl = new Builder_TexttemplateP1pImpl();
                tmpl.SetParameter(1, NamesNode.S_TOGETHER, log_Reports);//ノード名
                tmpl.SetParameter(2, PmNames.S_IN.Name_Attribute, log_Reports);//引数名
                tmpl.SetParameter(3, err_SIn, log_Reports);//in属性値
                tmpl.SetParameter(4, Log_RecordReportsImpl.ToText_Configuration(cur_Cf), log_Reports);//設定位置パンくずリスト

                memoryApplication.CreateErrorReport("Er:8026;", tmpl, log_Reports);
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
        gt_Error_DuplicationTogether:
            {
                Builder_TexttemplateP1p tmpl = new Builder_TexttemplateP1pImpl();
                tmpl.SetParameter(1, NamesNode.S_TOGETHER, log_Reports);//ノード名

                string sName_Tg;
                cur_Cf.Dictionary_Attribute.TryGetValue(PmNames.S_NAME, out sName_Tg, false, log_Reports);
                tmpl.SetParameter(2, sName_Tg, log_Reports);//指定したtogether名

                tmpl.SetParameter(3, Log_RecordReportsImpl.ToText_Configuration(cur_Cf), log_Reports);//設定位置パンくずリスト

                memoryApplication.CreateErrorReport("Er:8027;", tmpl, log_Reports);
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
        gt_Error_DuplicationOn:
            {
                Builder_TexttemplateP1p tmpl = new Builder_TexttemplateP1pImpl();
                tmpl.SetParameter(1, PmNames.S_ON.Name_Attribute, log_Reports);//属性名on
                tmpl.SetParameter(2, NamesNode.S_TOGETHER, log_Reports);//ノード名トゥゲザー
                tmpl.SetParameter(3, Log_RecordReportsImpl.ToText_Configuration(parent_Cf), log_Reports);//設定位置パンくずリスト

                memoryApplication.CreateErrorReport("Er:8028;", tmpl, log_Reports);
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
        gt_Error_Attr:
            {
                Builder_TexttemplateP1p tmpl = new Builder_TexttemplateP1pImpl();
                tmpl.SetParameter(1, NamesNode.S_TOGETHER, log_Reports);//ノード名

                tmpl.SetParameter(2, "in,name", log_Reports);//属性名リスト

                tmpl.SetParameter(3, Log_RecordReportsImpl.ToText_Configuration(cur_Cf), log_Reports);//設定位置パンくずリスト

                memoryApplication.CreateErrorReport("Er:8029;", tmpl, log_Reports);
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
        gt_Error_Child:
            {
                Builder_TexttemplateP1p tmpl = new Builder_TexttemplateP1pImpl();
                tmpl.SetParameter(1, NamesNode.S_TOGETHER, log_Reports);//ノード名
                tmpl.SetParameter(2, NamesNode.S_TARGET, log_Reports);//期待する子ノード名
                tmpl.SetParameter(3, err_Child_X.Name, log_Reports);//実際の子ノード名
                tmpl.SetParameter(4, Log_RecordReportsImpl.ToText_Configuration(cur_Cf), log_Reports);//設定位置パンくずリスト

                memoryApplication.CreateErrorReport("Er:8030;", tmpl, log_Reports);
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
        gt_Error_NoTarget:
            {
                Builder_TexttemplateP1p tmpl = new Builder_TexttemplateP1pImpl();
                tmpl.SetParameter(1, NamesNode.S_TOGETHER, log_Reports);//ノード名
                tmpl.SetParameter(2, PmNames.S_TARGET1.Name_Attribute, log_Reports);//期待する属性名
                tmpl.SetParameter(3, Log_RecordReportsImpl.ToText_Configuration(parent_Cf), log_Reports);//設定位置パンくずリスト

                memoryApplication.CreateErrorReport("Er:8031;", tmpl, log_Reports);
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
        gt_Error_NoOn:
            {
                Builder_TexttemplateP1p tmpl = new Builder_TexttemplateP1pImpl();
                tmpl.SetParameter(1, NamesNode.S_TOGETHER, log_Reports);//ノード名
                tmpl.SetParameter(2, PmNames.S_ON.Name_Attribute, log_Reports);//期待する属性名
                tmpl.SetParameter(3, Log_RecordReportsImpl.ToText_Configuration(parent_Cf), log_Reports);//設定位置パンくずリスト

                memoryApplication.CreateErrorReport("Er:8032;", tmpl, log_Reports);
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
            #endregion
        //
        //
        gt_EndMethod:
            log_Method.EndMethod(log_Reports);
            return;
        }

        //────────────────────────────────────────

        protected override void Parse_SAttribute(
            XmlElement cur_X,
            Configurationtree_Node cur_Cf,
            MemoryApplication memoryApplication,
            Log_Reports log_Reports
            )
        {
        }

        //────────────────────────────────────────
        #endregion



    }
}
