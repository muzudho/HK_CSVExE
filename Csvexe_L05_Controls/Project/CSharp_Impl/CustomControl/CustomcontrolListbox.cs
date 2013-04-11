﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Xenon.Syntax;//Log_TextIndentedImpl
using Xenon.Middle;//HValidator
using Xenon.Table; //FldDefinition,IntCellData,DefaultTable


namespace Xenon.Controls
{
    /// <summary>
    /// リストボックスのカスタム・コントロール。
    /// </summary>
    public partial class CustomcontrolListbox : ListBox, Customcontrol
    {



        #region 生成と破棄
        //────────────────────────────────────────

        public CustomcontrolListbox()
        {
            this.controlCommon__ = new ControlCommonImpl();
            this.sItemDisplayFormat = "";
            this.sListValueField = "";

            this.nIndex_PreselectedItem = -1;

            this.DataSourceChanged += new System.EventHandler(this.ccListbox_DataSourceChanged);

            this.list_Expressionv_FListboxValidation = new List<Expressionv_3FListboxValidation>();

            this.list_SText_Display = new List<string>();
            this.list_ForeBrush = new List<Brush>();
        }

        //────────────────────────────────────────

        public void Clear()
        {
            this.Items.Clear();
        }

        /// <summary>
        /// イベントハンドラーを全て除去します。
        /// </summary>
        public void ClearAllEventhandlers(Log_Reports log_Reports)
        {
            Remover_AllEventhandlers remover = new Remover_AllEventhandlersImpl(
                this,
                this.ControlCommon.Owner_MemoryApplication,
                log_Reports
                );

            remover.Suppress(
                this.ControlCommon.Owner_MemoryApplication,
                log_Reports
                );

            //            remover.Resume(log_Reports);
        }

        //────────────────────────────────────────

        public void Destruct(
            Log_Reports log_Reports
            )
        {
            Log_Method pg_Method = new Log_MethodImpl();
            pg_Method.BeginMethod(Info_Controls.Name_Library, this, "Destruct(10)",log_Reports);
            //
            //

            this.ClearAllEventhandlers(log_Reports);

            //
            // 破棄フラグを立てます。
            //
            this.ControlCommon.BDestructed = true;

            this.Clear();

            //
            //
            //
            //
            pg_Method.EndMethod(log_Reports);
        }

        //────────────────────────────────────────
        #endregion



        #region アクション
        //────────────────────────────────────────

        public void SelectItem(
            Expression_Node_String ec_KeyFieldName,
            Expression_Node_String ec_ExpectedValue,
            Log_Reports log_Reports
            )
        {
            Log_Method pg_Method = new Log_MethodImpl();
            pg_Method.BeginMethod(Info_Controls.Name_Library, this, "SelectItem",log_Reports);
            //


            string sName_KeyFld = ec_KeyFieldName.Execute4_OnExpressionString(EnumHitcount.Unconstraint, log_Reports);

            bool bMatched = false;

            //
            // リストボックスは、現在作成中かも知れない。
            // 項目を選択するタイミングが合わないかも知れない。
            //
            // ※TODO: リストボックスの構築が終わるまで待ちたい。
            //
            string err_STableName;
            for (int nRow = 0; nRow < this.Items.Count; nRow++)
            {
                //.WriteLine(this.GetType().Name + "#SelectItem:(" + Info_Forms .LibraryName+ ") rowIndex=[" + rowIndex + "]");

                DataRowView dataRowView = (DataRowView)this.Items[nRow];

                Cell o_FldValue = null;//フィールド
                {
                    // todo: フィールド値
                    if (dataRowView.DataView.Table.Columns.Contains(sName_KeyFld))
                    {
                        object fldValue = dataRowView.Row[sName_KeyFld];

                        bool bParsedSuccessful = Utility_Cell.TryParse(
                            fldValue,
                            out o_FldValue,
                            true,
                            log_Reports
                            );
                        if (!log_Reports.Successful)
                        {
                            goto gt_EndMethod;
                        }
                    }
                    else
                    {
                        // エラー
                        err_STableName = dataRowView.DataView.Table.TableName;
                        goto gt_Error_NotFoundField;
                    }
                }

                Cell o_ExpectedValue = null;
                {
                    //
                    // oFldValue と同じ型の CellData を作成。
                    //
                    o_ExpectedValue = Utility_Cell.NewInstance(
                        o_FldValue,
                        true,
                        pg_Method.Fullname,
                        log_Reports
                        );
                    if (!log_Reports.Successful)
                    {
                        goto gt_EndMethod;
                    }

                    o_ExpectedValue.Text = ec_ExpectedValue.Execute4_OnExpressionString(EnumHitcount.Unconstraint, log_Reports);
                }

                if (log_Reports.Successful)
                {
                    if (o_ExpectedValue.Equals(o_FldValue))
                    {
                        //
                        // 一致したら
                        //

                        bMatched = true;
                        //
                        // その列を選択。
                        //
                        this.ControlCommon.BAutomaticinputting = true;
                        this.SelectedIndex = nRow;
                        {
                            // リストボックスの縦幅
                            int nH = this.Height;
                            // 項目の高さ
                            int nIH = this.ItemHeight;
                            // 表示項目数
                            float nVi = (float)nH / (float)nIH;
                            // 半分の位置
                            int nHalf = (int)Math.Floor(nVi / 2.0f);

                            int nTI = nRow - nHalf;
                            if (nTI < 0)
                            {
                                nTI = 0;
                            }
                            this.TopIndex = nTI;//選択した項目が中央に表示されるようにスクロール。
                        }
                        this.ControlCommon.BAutomaticinputting = false;

                        break;
                    }
                }
            }


            // 一致していなければ、選択を解除。
            if (!bMatched)
            {
                //
                // まず、選択解除。
                //
                this.ControlCommon.BAutomaticinputting = true;
                this.ClearSelected();//.SelectedIndex = -1;
                this.ControlCommon.BAutomaticinputting = false;
            }

            goto gt_EndMethod;
        //
        //
            #region 異常系
        //────────────────────────────────────────
        gt_Error_NotFoundField:
            {
                Builder_TexttemplateP1p tmpl = new Builder_TexttemplateP1pImpl();
                tmpl.SetParameter(1, sName_KeyFld,log_Reports);//キー・フィールド名
                tmpl.SetParameter(2, err_STableName, log_Reports);//テーブル名
                tmpl.SetParameter(3, Log_RecordReportsImpl.ToText_Configuration(ec_KeyFieldName.Cur_Configuration), log_Reports);//キー・フィールド名の設定位置パンくずリスト
                tmpl.SetParameter(4, Log_RecordReportsImpl.ToText_Configuration(ec_ExpectedValue.Cur_Configuration), log_Reports);//テーブル名の設定位置パンくずリスト

                this.ControlCommon.Owner_MemoryApplication.CreateErrorReport("Er:505;", tmpl, log_Reports);
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
            #endregion
        //
        //
        gt_EndMethod:
            pg_Method.EndMethod(log_Reports);
        }

        //────────────────────────────────────────

        /// <summary>
        /// 再描画のタイミングで、データの再読込をさせる指定をします。
        /// </summary>
        /// <param name="log_Reports"></param>
        public void Dirty(
            Log_Reports log_Reports
            )
        {
            // 表示テキストを空っぽにします。
            this.List_SText_Display.Clear();
            this.List_ForeBrush.Clear();
        }

        //────────────────────────────────────────

        /// <summary>
        /// データソースから値を取得し、コントロールに取り込みます。
        /// 
        /// データソースが設定されていない場合は、フォームのクリアーになります。
        /// </summary>
        public void RefreshData(
            Log_Reports log_Reports
            )
        {
            List<Expression_Node_String> ecList_Data = this.ControlCommon.Expression_Control.SelectDirectchildByNodename(NamesNode.S_DATA, false, EnumHitcount.Unconstraint, log_Reports);
            List<Expression_Node_String> ecList_DataSource = Utility_Expression_NodeImpl.SelectItemsByPmAsCsv(ecList_Data, PmNames.S_ACCESS.Name_Pm, ValuesAttr.S_FROM, false, EnumHitcount.First_Exist, log_Reports);
            if (!log_Reports.Successful)
            {
                goto gt_EndMethod;
            }
            Expression_Node_String ec_DataSource = ecList_DataSource[0];


            if (null == ec_DataSource)
            {
                // データソースが未設定のとき

                this.Items.Clear();
            }
            else
            {
                // データソースが設定されているとき。

                // リフレッシュして、データを読み取り直します。
                this.Refresh();
            }

            goto gt_EndMethod;
        //
        //
        //
        //
        gt_EndMethod:
            ;
        }

        //────────────────────────────────────────

        public void UsercontrolToMemory(
            Log_Reports log_Reports
            )
        {
            Log_Method pg_Method = new Log_MethodImpl();
            pg_Method.BeginMethod(Info_Controls.Name_Library, this, "UsercontrolToMemory",log_Reports);
            //
            //

            if (null == this.ControlCommon.Expression_Control)
            {
                // このコントロールに対応づくテーブル等の設定がなく、ただの空箱の場合。
                // Visual Studio のビジュアルエディターで直接置いただけの時は、ここに来ます。

                // 何もせず終了。
                goto gt_EndMethod;
            }


            List<Expression_Node_String> listExpr_Data = this.ControlCommon.Expression_Control.SelectDirectchildByNodename(NamesNode.S_DATA, false, EnumHitcount.Unconstraint, log_Reports);
            List<Expression_Node_String> listExpr_DataTarget = Utility_Expression_NodeImpl.SelectItemsByPmAsCsv(listExpr_Data, PmNames.S_ACCESS.Name_Pm, ValuesAttr.S_TO, false, EnumHitcount.First_Exist, log_Reports);
            if (!log_Reports.Successful)
            {
                goto gt_EndMethod;
            }
            Expression_Node_String ec_DataTarget = listExpr_DataTarget[0];


            if (null == ec_DataTarget)
            {
                // エラー：     データターゲットが未設定のとき
                goto gt_Error_Datatarget;
            }
            else
            {
                // データターゲットが設定されているとき

                //#未実装 TODO: 実装すること。
                if (log_Reports.CanCreateReport)
                {
                    Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                    r.SetTitle("▲エラー474！", pg_Method);

                    StringBuilder t = new StringBuilder();

                    t.Append("コントロール=[");
                    t.Append(this.Name);
                    t.Append("]のUpdateDataをしようとしましたが、" + pg_Method.Fullname + " は　未実装でした。");
                    t.Append(Environment.NewLine);
                    t.Append("プログラミングのミスの可能性があります。");
                    t.Append(Environment.NewLine);
                    t.Append(Environment.NewLine);

                    // ヒント

                    r.Message = t.ToString();
                    log_Reports.EndCreateReport();
                }
            }

            goto gt_EndMethod;
        //
        //
            #region 異常系
        //────────────────────────────────────────
        gt_Error_Datatarget:
            {
                Builder_TexttemplateP1p tmpl = new Builder_TexttemplateP1pImpl();
                tmpl.SetParameter(1, this.Name, log_Reports);//コントロール名

                this.ControlCommon.Owner_MemoryApplication.CreateErrorReport("Er:507;", tmpl, log_Reports);
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
            #endregion
        //
        //
        gt_EndMethod:
            pg_Method.EndMethod(log_Reports);
        }

        //────────────────────────────────────────

        public void AddValidator(
            Expressionv_Validator_Old ecv_Validator,
            Log_Reports log_Reports
            )
        {
            // 未実装 TODO 実装すること。
        }

        //────────────────────────────────────────

        public void AddValidator_FListboxForItems(
            Expressionv_3FListboxValidation ecv_Validator,
            Log_Reports log_Reports
            )
        {
            this.list_Expressionv_FListboxValidation.Add(ecv_Validator);
        }

        //────────────────────────────────────────
        #endregion



        #region 判定
        //────────────────────────────────────────

        /// <summary>
        /// 妥当性を判定します。
        /// </summary>
        public void JudgeValidity(
            Log_Reports log_Reports
            )
        {
            // 未実装 TODO 実装すること。

            //
            // リストボックスの表示は、
            // ここではなく、リスト項目描画クラスで行っている。
        }

        //────────────────────────────────────────
        #endregion



        #region イベントハンドラー
        //────────────────────────────────────────

        /// <summary>
        /// </summary>
        /// <param nFcName="sender"></param>
        /// <param nFcName="e"></param>
        private void ccListbox_DataSourceChanged(object sender, EventArgs e)
        {
            Log_Method pg_Method = new Log_MethodImpl(0);
            Log_Reports log_Reports_ThisMethod = new Log_ReportsImpl(pg_Method);
            pg_Method.BeginMethod(Info_Controls.Name_Library, this, "ccListbox_DataSourceChanged",log_Reports_ThisMethod);
            //
            //
            string sName_Usercontrol = this.ControlCommon.Expression_Name_Control.Execute4_OnExpressionString(EnumHitcount.Unconstraint, log_Reports_ThisMethod);

            log_Reports_ThisMethod.Comment_EventCreationMe  ="[" + sName_Usercontrol + "]コントロール（リストボックス）のデータソースが変更されました。";

            ////
            ////
            ////
            //// 項目選択
            ////
            ////
            ////
            //if (log_Reports.Successful)
            //{
            //    // ローカル変数に落とす。
            //    Request_DefaultListItemImpl receipt_DefaultListItemImpl2 = this.Receipt_DefaultListItemImpl;
            //    if (null != receipt_DefaultListItemImpl2)
            //    {
            ////essageBox.Show("[" + this.ControlCommon.NFcName.Value + "]コントロール（リストボックス）のデータソースが変更されました。\nデフォルト値の要求設定がありました。\n nKeyColumnName=[" + receipt_DefaultListItemImpl2.NKeyFldName.GetString(VolumeConstraintEnu.Unconstraint, log_Reports, "*" + this.GetType().NFcName + "#") + "]\n nExpectedValue=[" + receipt_DefaultListItemImpl2.NExpectedValue.GetString(VolumeConstraintEnu.Unconstraint, log_Reports, "*" + this.GetType().NFcName + "#") + "]", this.GetType().NFcName + "#:(" + Info_Forms .LibraryName+ ") デバッグ");


            //        this.SelectItem(
            //            receipt_DefaultListItemImpl2.NKeyFldName,
            //            receipt_DefaultListItemImpl2.NExpectedValue,
            //            log_Reports,
            //            "*" + this.GetType().NFcName + "#"
            //            );
            //    }
            //}

            //
            //
            pg_Method.EndMethod(log_Reports_ThisMethod);
            log_Reports_ThisMethod.EndLogging(pg_Method);
        }

        //────────────────────────────────────────
        #endregion



        #region プロパティー
        //────────────────────────────────────────

        private ControlCommon controlCommon__;

        /// <summary>
        /// コントロールの共通プロパティー、およびロジックが含まれているクラスです。
        /// 
        /// C#では多重継承ができないので、共通のプロパティー、ロジックがあれば、ここに含めます。
        /// </summary>
        public ControlCommon ControlCommon
        {
            get
            {
                return controlCommon__;
            }
        }

        //────────────────────────────────────────

        private List<Expressionv_3FListboxValidation> list_Expressionv_FListboxValidation;

        /// <summary>
        /// 妥当性判定項目のリスト。
        /// </summary>
        public List<Expressionv_3FListboxValidation> List_Expressionv_FListboxValidation
        {
            get
            {
                return list_Expressionv_FListboxValidation;
            }
        }

        //────────────────────────────────────────

        private int nIndex_PreselectedItem;

        /// <summary>
        /// リストボックスで、前回選択されていた項目インデックス。
        /// 
        /// 自動化されていませんので、前回選択されていた項目は、使う側のソースでセットしてください。
        /// 
        /// リストボックスのselectedIndex値の初期値は 0 なので、このプロパティの初期値は -1 にしておきます。
        /// TODO リストボックスの SelectedIndexChanged の誤発動をなくしたい。
        /// </summary>
        public int NIndex_PreselectedItem
        {
            set
            {
                nIndex_PreselectedItem = value;
            }
            get
            {
                return nIndex_PreselectedItem;
            }
        }

        //────────────────────────────────────────

        private Table_Humaninput tableHumaninput_Datasource;

        /// <summary>
        /// このリストボックスのデータソースを、データテーブルから取っている場合、
        /// もし「テーブル情報」があれば、セットしておくことができます。
        /// 
        /// Xn_L11_NorenImpl:NorenListboxUtil.cs:NorenListboxUtil.BindTableToDataSourceでsetを使用。
        /// </summary>
        public Table_Humaninput Table_Humaninput_Datasource
        {
            set
            {
                tableHumaninput_Datasource = value;
            }
            get
            {
                return tableHumaninput_Datasource;
            }
        }

        //────────────────────────────────────────

        private string sItemDisplayFormat;

        /// <summary>
        /// 項目の表示書式を指定した文字列です。
        /// 「%1%:%2%|ID|NAME」のような値です。
        /// </summary>
        public string SItemDisplayFormat
        {
            get
            {
                return sItemDisplayFormat;
            }
            set
            {
                sItemDisplayFormat = value;
            }
        }

        //────────────────────────────────────────

        private string sListValueField;

        /// <summary>
        /// リストボックスの値が入っている、レコードのフィールド名。
        /// 
        /// TODO: 項目の表示書式を指定した文字列です。「%1%:%2%|ID|NAME」のような値です。
        /// ※現状、フィールド名が入っている？
        /// </summary>
        public string SListValueField
        {
            get
            {
                return sListValueField;
            }
            set
            {
                sListValueField = value;
            }
        }

        //────────────────────────────────────────

        private List<string> list_SText_Display;

        /// <summary>
        /// 項目テキストのリスト。
        /// </summary>
        public List<string> List_SText_Display
        {
            get
            {
                return list_SText_Display;
            }
            set
            {
                list_SText_Display = value;
            }
        }

        //────────────────────────────────────────

        private List<Brush> list_ForeBrush;

        /// <summary>
        /// 項目のブラシのリスト。TODO:フォームが持ったほうがいい？
        /// </summary>
        public List<Brush> List_ForeBrush
        {
            get
            {
                return list_ForeBrush;
            }
            set
            {
                list_ForeBrush = value;
            }
        }

        //────────────────────────────────────────
        #endregion



    }
}
