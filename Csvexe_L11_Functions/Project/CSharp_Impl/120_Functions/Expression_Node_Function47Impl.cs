﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Windows.Forms;//Application
using Xenon.Syntax;
using Xenon.Middle;//MoOpyopyo,FormObjectProperties,Usercontrol
using Xenon.Functions;

namespace Xenon.Functions
{


    /// <summary>
    /// [使い方]
    /// フォルダーの中のファイル、フォルダーを一覧したCSVファイルを作成します。
    /// </summary>
    public class Expression_Node_Function47Impl : Expression_Node_FunctionAbstract
    {



        #region 用意
        //────────────────────────────────────────
        //
        // 関数名
        //

        public static readonly string NAME_FUNCTION = "Sf:CSV書出し_ファイルリスト;";

        //────────────────────────────────────────
        //
        // 引数名
        //

        /// <summary>
        /// 検索されるフォルダー。
        /// </summary>
        public const string PM_FOLDER_SOURCE = "Pm:folder-source;";

        /// <summary>
        /// 出力先ファイル。
        /// </summary>
        public const string PM_FILE_EXPORT = "Pm:file-export;";

        /// <summary>
        /// 出力先フィールド。
        /// </summary>
        public const string PM_FIELD_EXPORT = "Pm:field-export;";

        /// <summary>
        /// 「File」「Folder」「Both」のいずれか。無指定なら「Both」扱い。
        /// </summary>
        public const string PM_FILTER = "Pm:filter;";

        /// <summary>
        /// 「Yes」「No」「Ignore（または無指定）」のいずれか。サブフォルダーも検索する場合「Yes」。
        /// </summary>
        public const string PM_IS_SEARCH_SUBFOLDER = "Pm:is-search-subfolder;";

        /// <summary>
        /// ポップアップの有無。「block」なら出ない。
        /// </summary>
        public const string PM_POPUP = "Pm:popup;";

        public const string S_BLOCK = "block";

        //────────────────────────────────────────
        #endregion

        

        #region 生成と破棄
        //────────────────────────────────────────

        public Expression_Node_Function47Impl(EnumEventhandler enumEventhandler, List<string> listS_ArgName, ConfigurationtreeToFunction_Item functiontranslatoritem)
            :base(enumEventhandler, listS_ArgName, functiontranslatoritem)
        {
        }

        public override Expression_Node_Function NewInstance(
            Expression_Node_String parent_Expression, Configuration_Node cur_Conf,
            object/*MemoryApplication*/ owner_MemoryApplication, Log_Reports log_Reports)
        {
            Log_Method log_Method = new Log_MethodImpl(0);
            log_Method.BeginMethod(Info_Functions.Name_Library, this, "NewInstance",log_Reports);
            //

            Expression_Node_Function f0 = new Expression_Node_Function47Impl(this.EnumEventhandler, this.List_NameArgumentInitializer, this.Functiontranslatoritem);
            f0.Parent_Expression = parent_Expression;
            f0.Cur_Configuration = cur_Conf;
            ((Expression_Node_FunctionAbstract)f0).Owner_MemoryApplication = (MemoryApplication)owner_MemoryApplication;
            //関数名初期化
            f0.SetAttribute(PmNames.S_NAME.Name_Pm, new Expression_Leaf_StringImpl(NAME_FUNCTION, null, cur_Conf), log_Reports);

            f0.SetAttribute(Expression_Node_Function47Impl.PM_FOLDER_SOURCE, new Expression_Node_StringImpl(this, cur_Conf), log_Reports);

            //
            log_Method.EndMethod(log_Reports);
            return f0;
        }

        //────────────────────────────────────────
        #endregion



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// アクション実行。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override string Execute5_Main(Log_Reports log_Reports)
        {
            Log_Method log_Method = new Log_MethodImpl(0, Log_ReportsImpl.BDebugmode_Static);
            log_Method.BeginMethod(Info_Functions.Name_Library, this, "Execute5_Main", log_Reports);

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

            string sName_Fnc;
            this.TrySelectAttribute(out sName_Fnc, PmNames.S_NAME.Name_Pm, EnumHitcount.One_Or_Zero, log_Reports);

            if (log_Reports.CanStopwatch)
            {
                log_Method.Log_Stopwatch.Message = "Nアクション[" + sName_Fnc + "]実行";
                log_Method.Log_Stopwatch.Begin();
            }


            //ScriptVariableフォルダー
            string pm_Folder_Source;
            this.TrySelectAttribute(out pm_Folder_Source, Expression_Node_Function47Impl.PM_FOLDER_SOURCE, EnumHitcount.One_Or_Zero, log_Reports);

            //書出し先ファイル
            Expression_Node_String fileExport_Expr;
            this.TrySelectAttribute(out fileExport_Expr, Expression_Node_Function47Impl.PM_FILE_EXPORT, EnumHitcount.One_Or_Zero, log_Reports);

            //書出し先フィールド
            Expression_Node_String fieldExport_Expr;
            this.TrySelectAttribute(out fieldExport_Expr, Expression_Node_Function47Impl.PM_FIELD_EXPORT, EnumHitcount.One, log_Reports);

            //フィルター指定
            string pm_Filter;
            this.TrySelectAttribute(out pm_Filter, Expression_Node_Function47Impl.PM_FILTER, EnumHitcount.One_Or_Zero, log_Reports);
            pm_Filter = pm_Filter.Trim();

            //サブフォルダー検索
            string pm_Is_Search_Subfolder;
            this.TrySelectAttribute(out pm_Is_Search_Subfolder, Expression_Node_Function47Impl.PM_IS_SEARCH_SUBFOLDER, EnumHitcount.One_Or_Zero, log_Reports);

            //ポップアップ指定
            string str_Popup;
            this.TrySelectAttribute(out str_Popup, Expression_Node_Function47Impl.PM_POPUP, EnumHitcount.One_Or_Zero, log_Reports);
            str_Popup = str_Popup.Trim();


            //ディレクトリー階層を走査したい。
            Filesystemreport filesystemreporter = new FilesystemreportImpl();
            if(log_Reports.Successful)
            {
                Filesystemrunner runner = new FilesystemrunnerImpl();
                //log_Method.WriteDebug_ToConsole("初回 pm_Folder_Source=[" + pm_Folder_Source + "] pm_Filter=[" + pm_Filter + "] pm_Is_Search_Subfolder=[" + pm_Is_Search_Subfolder + "]");
                runner.Run(
                    filesystemreporter,
                    pm_Folder_Source,
                    pm_Filter,
                    pm_Is_Search_Subfolder,
                    log_Reports
                    );
            }

            // 検索結果をCSVテーブルの形にして出力。
            if (log_Reports.Successful)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("NO,");
                sb.Append(fieldExport_Expr.Execute4_OnExpressionString(EnumHitcount.Unconstraint, log_Reports));// "FILE"
                sb.Append(",END");

                sb.Append(Environment.NewLine);
                sb.Append("int,string,END");
                sb.Append(Environment.NewLine);
                sb.Append("-1,ファイルパス,END");
                sb.Append(Environment.NewLine);

                int field_No = 0;
                filesystemreporter.ForEach(delegate(string filesystementry, ref bool isBreak2, Log_Reports log_Reports2)
                {
                    //連番
                    sb.Append(field_No);
                    sb.Append(",");
                    sb.Append(filesystementry);
                    sb.Append(",END");
                    sb.Append(Environment.NewLine);
                    field_No++;
                }, log_Reports);
                sb.Append("EOF,,");
                sb.Append(Environment.NewLine);


                try
                {
                    string sFile_Export2 = fileExport_Expr.Execute4_OnExpressionString(EnumHitcount.Unconstraint, log_Reports);
                    System.IO.File.WriteAllText(
                        sFile_Export2,
                        sb.ToString(),
                        Global.ENCODING_CSV
                        );

                    if (str_Popup != S_BLOCK)
                    {
                        Log_TextIndented s = new Log_TextIndentedImpl();

                        s.Append("ファイルに書き込みました。");
                        s.Newline();
                        s.Append("[");
                        s.Append(sFile_Export2);
                        s.Append("]");
                        s.Newline();
                        s.Newline();

                        s.Append("検索した場所：");
                        s.Newline();
                        s.Append("[");
                        s.Append(pm_Folder_Source);
                        s.Append("]");
                        s.Newline();
                        s.Newline();

                        s.Append("検索オプション（Pm:filter;）：");
                        s.Newline();
                        s.Append("[");
                        s.Append(pm_Filter);
                        s.Append("]");
                        s.Newline();

                        MessageBox.Show(s.ToString(), "▲実行結果！（L02）");
                    }
                }
                catch (Exception ex)
                {
                    // 異常時は必ずポップアップが出る。
                    MessageBox.Show(
                        ex.Message,
                        "▲エラー201！(" + log_Method.Fullname + ")#Write"
                        );
                }
            }

            log_Method.EndMethod(log_Reports);
        }

        //────────────────────────────────────────
        #endregion



    }
}
