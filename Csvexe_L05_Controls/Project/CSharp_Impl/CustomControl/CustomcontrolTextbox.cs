﻿using System;
using System.Collections.Generic;
using System.Drawing;//Color
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Xenon.Syntax;
using Xenon.Middle;//HValidator

namespace Xenon.Controls
{
    /// <summary>
    /// テキストボックスのカスタム・コントロールです。
    /// 
    /// 手による直接入力と、自動設定による入力を区別するテキストボックスです。
    /// </summary>
    public class CustomcontrolTextbox : TextBox, Customcontrol
    {



        #region 生成と破棄
        //────────────────────────────────────────

        public CustomcontrolTextbox()
        {
            this.controlCommon__ = new ControlCommonImpl();

            this.list_Expressionv_Validator = new List<Expressionv_Validator_Old>();
            this.sNewline = "";
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

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);

        }

        //────────────────────────────────────────
        #endregion



        #region アクション
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
            Log_Method pg_Method = new Log_MethodImpl();
            pg_Method.BeginMethod(Info_Controls.Name_Library, this, "RefreshData",log_Reports);
            //
            //

            List<Expression_Node_String> ecList_Data = this.ControlCommon.Expression_Control.SelectDirectchildByNodename(NamesNode.S_DATA, false, EnumHitcount.Unconstraint, log_Reports);
            List<Expression_Node_String> ecList_DataSource = Utility_Expression_NodeImpl.SelectItemsByPmAsCsv(ecList_Data, PmNames.S_ACCESS.Name_Pm, ValuesAttr.S_FROM, false, EnumHitcount.First_Exist, log_Reports);
            if (!log_Reports.Successful)
            {
                goto gt_EndMethod;
            }
            Expression_Node_String ec_DataSource = ecList_DataSource[0];


            if (null == ec_DataSource)
            {
                // データソースが設定されていないとき
                //.WriteLine(this.GetType().Name + "#RefreshData: データソースが設定されていません。");

                this.Clear();
            }
            else
            {

                if (log_Reports.Successful)
                {

                    this.ControlCommon.BAutomaticinputting = true;

                    //
                    // 最初の１件。なければ空文字列。
                    //
                    Expression_Node_String ec_Str = ec_DataSource;
                    string sText = ec_Str.Execute4_OnExpressionString(EnumHitcount.First_Exist_Or_Zero, log_Reports);
                    //pg_Method.WriteDebug_ToConsole("ec_Str.Execute4=[" + sText + "]");

                    //
                    // 改行文字を、改行に変換。
                    //
                    if ("" != this.SNewline)
                    {
                        sText = sText.Replace(this.SNewline, Environment.NewLine);
                    }

                    this.Text = sText;

                    this.ControlCommon.BAutomaticinputting = false;
                }

            }

            goto gt_EndMethod;
        //
        //
        //
        //
        gt_EndMethod:
            pg_Method.EndMethod(log_Reports);
        }

        //────────────────────────────────────────

        /// <summary>
        /// データ・ターゲットへの出力を行います。
        /// </summary>
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


            List<Expression_Node_String> ecList_Data = this.ControlCommon.Expression_Control.SelectDirectchildByNodename(NamesNode.S_DATA, false, EnumHitcount.Unconstraint, log_Reports);
            List<Expression_Node_String> ecList_DataTarget = Utility_Expression_NodeImpl.SelectItemsByPmAsCsv(ecList_Data, PmNames.S_ACCESS.Name_Pm, ValuesAttr.S_TO, false, EnumHitcount.First_Exist, log_Reports);
            if (!log_Reports.Successful)
            {
                goto gt_EndMethod;
            }
            Expression_Node_String ec_DataTarget = ecList_DataTarget[0];


            if (null == ec_DataTarget)
            {
                // エラー：     データターゲットが未設定のとき
                goto gt_Error_NullDatatarget;
            }


            if (log_Reports.Successful)
            {

                //this……イベントハンドラーのsender引数と一致すること。

                // TODO 数値型テキストボックスで空白を出力しようとしたときにエラーになるのはバグなので修正したい。


                // 特にトリムは行いません。
                string sText = this.Text;
                //
                // 改行文字を、改行に変換。
                //
                if ("" != this.SNewline)
                {
                    sText = sText.Replace(Environment.NewLine, this.SNewline);
                }


                //
                // テーブルにデータを書き出す方法。
                {
                    ToMemory_Performer toM = new ExpressionDataTargetUpdaterImpl();
                    toM.ToMemory(
                        sText,
                        this.ControlCommon.Expression_Control,
                        this.ControlCommon.Owner_MemoryApplication,
                        log_Reports
                        );
                }


                if (log_Reports.Successful)
                {
                    // 成功時
                    this.BackColor = System.Drawing.SystemColors.Window;
                }
                else
                {
                    // 設定失敗時。
                    this.BackColor = Color.Yellow;
                }
            }

            goto gt_EndMethod;
        //
        //
            #region 異常系
        //────────────────────────────────────────
        gt_Error_NullDatatarget:
            {
                Builder_TexttemplateP1p tmpl = new Builder_TexttemplateP1pImpl();
                tmpl.SetParameter(1, this.Name, log_Reports);//コントロール名

                this.ControlCommon.Owner_MemoryApplication.CreateErrorReport("Er:513;", tmpl, log_Reports);
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
        /// 妥当性判定ロジックを追加します。
        /// </summary>
        /// <param nFcName="nValidator"></param>
        public void AddValidator(
            Expressionv_Validator_Old ecv_Validator,
            Log_Reports log_Reports
            )
        {
            if (ecv_Validator is Expressionv_TextValidator_Old)
            {
                this.list_Expressionv_Validator.Add((Expressionv_TextValidator_Old)ecv_Validator);
            }
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
            foreach (Expressionv_TextValidator_Old ecv_Validator in this.list_Expressionv_Validator)
            {
                EnumValidation_Old enumValidation = ecv_Validator.JudgeValidity(this.Text);

                switch (enumValidation)
                {
                    case EnumValidation_Old.Ok:
                        // OK
                        if (this.ReadOnly || !this.Enabled)
                        {
                            this.BackColor = SystemColors.Control;
                        }
                        else
                        {
                            this.BackColor = SystemColors.Window;
                        }
                        goto gt_EndMethod;// 関数から抜けます。

                    case EnumValidation_Old.Ng:
                        // NG
                        this.BackColor = Color.Yellow;
                        goto gt_EndMethod;// 関数から抜けます。

                    default:
                        // 続行
                        break;
                }
            }

            // OK
            if (this.ReadOnly || !this.Enabled)
            {
                this.BackColor = SystemColors.Control;
            }
            else
            {
                this.BackColor = SystemColors.Window;
            }

            //
        //
        //
        //
        gt_EndMethod:
            return;
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

        /// <summary>
        /// 妥当性判定項目のリスト。
        /// </summary>
        private List<Expressionv_Validator_Old> list_Expressionv_Validator;

        /// <summary>
        /// 妥当性判定項目のリスト。
        /// </summary>
        public List<Expressionv_Validator_Old> List_Expressionv_Validator
        {
            get
            {
                return list_Expressionv_Validator;
            }
        }

        //────────────────────────────────────────

        /// <summary>
        /// 改行文字列
        /// </summary>
        private string sNewline;

        //────────────────────────────────────────

        public string SNewline
        {
            get
            {
                return sNewline;
            }
            set
            {
                sNewline = value;
            }
        }

        //────────────────────────────────────────
        #endregion



    }
}
