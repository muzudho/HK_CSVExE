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
    /// ボタンのカスタム・コントロールです。
    /// 
    /// 手による直接入力と、自動設定による入力を区別するテキストボックスです。
    /// 
    /// (Human Button Custom Control)
    /// </summary>
    public class CustomcontrolButton : Button, Customcontrol
    {



        #region 生成と破棄
        //────────────────────────────────────────

        public CustomcontrolButton()
        {
            this.controlCommon__ = new ControlCommonImpl();
            this.list_Expressionv_Validator = new List<Expressionv_Validator_Old>();
            this.Padding = new Padding(0);
            this.Margin = new Padding(0);
        }

        //────────────────────────────────────────

        public void Clear()
        {
            // ボタンなので、何もしません。
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
                log_Reports);
            //            remover.Resume(log_Reports);
        }

        //────────────────────────────────────────

        public void Destruct(
            Log_Reports log_Reports
            )
        {
            Log_Method pg_Method = new Log_MethodImpl(0, Log_ReportsImpl.BDebugmode_Static);
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
            pg_Method.EndMethod(log_Reports);
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
            //
            // ボタンなので何もしません。
            //
        }

        //────────────────────────────────────────

        /// <summary>
        /// データ・ターゲットへの出力を行います。
        /// 
        /// イベント・ハンドラー以外でも、直接、データターゲットへの出力を行うことができます。
        /// </summary>
        public void UsercontrolToMemory(
            Log_Reports log_Reports
            )
        {
            //
            // ボタン → エラー。

            Log_Method pg_Method = new Log_MethodImpl(0, Log_ReportsImpl.BDebugmode_Static);
            pg_Method.BeginMethod(Info_Controls.Name_Library, this, "UsercontrolToMemory",log_Reports);
            //
            //

            //#このルートはエラー
            {
                Builder_TexttemplateP1p tmpl = new Builder_TexttemplateP1pImpl();
                //%N%なし

                this.ControlCommon.Owner_MemoryApplication.CreateErrorReport("Er:538;", tmpl, log_Reports);
            }

            //
            //
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
                        if (!this.Enabled)
                        {
                            this.BackColor = SystemColors.Control;
                        }
                        else
                        {
                            this.BackColor = SystemColors.Window;
                        }
                        return;// 関数から抜けます。

                    case EnumValidation_Old.Ng:
                        // NG
                        this.BackColor = Color.Yellow;
                        return;// 関数から抜けます。

                    default:
                        // 続行
                        break;
                }
            }

            // OK
            if (!this.Enabled)
            {
                this.BackColor = SystemColors.Control;
            }
            else
            {
                this.BackColor = SystemColors.Window;
            }
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
        #endregion



    }
}
