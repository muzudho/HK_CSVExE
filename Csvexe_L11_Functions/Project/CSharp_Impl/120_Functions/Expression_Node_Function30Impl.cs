﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Data;//DataRowView
using Xenon.Syntax;
using Xenon.Controls;
using Xenon.Middle;//FormObjectProperties,Usercontrol
using Xenon.Table;//DefaultTable
using Xenon.Expr;


namespace Xenon.Functions
{

    /// <summary>
    /// ウィンドウ表示。
    /// </summary>
    public class Expression_Node_Function30Impl : Expression_Node_FunctionAbstract
    {



        #region 用意
        //────────────────────────────────────────
        //
        // 関数名
        //

        public static readonly string NAME_FUNCTION = "Sf:ウィンドウ表示;";

        //────────────────────────────────────────
        //
        // 引数名
        //

        /// <summary>
        /// ロード完了時に実行する、トゥゲザーの名前。
        /// 
        /// TODO:使ってる？？
        /// </summary>
        public static string PM_NAME_TOGETHER = PmNames.S_NAME_TOGETHER.Name_Pm;

        /// <summary>
        /// フォーム・グループ名。未設定ならヌル。
        /// </summary>
        public static string PM_NAME_FORM = PmNames.S_NAME_FORM.Name_Pm;

        //────────────────────────────────────────
        #endregion

        

        #region 生成と破棄
        //────────────────────────────────────────

        public Expression_Node_Function30Impl(EnumEventhandler enumEventhandler, List<string> listS_ArgName, ConfigurationtreeToFunction_Item functiontranslatoritem)
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

            Expression_Node_Function f0 = new Expression_Node_Function30Impl(this.EnumEventhandler,this.List_NameArgumentInitializer,this.Functiontranslatoritem);
            f0.Parent_Expression = parent_Expression;
            f0.Cur_Configuration = cur_Conf;
            ((Expression_Node_FunctionAbstract)f0).Owner_MemoryApplication = (MemoryApplication)owner_MemoryApplication;
            //関数名初期化
            f0.SetAttribute(PmNames.S_NAME.Name_Pm, new Expression_Leaf_StringImpl(NAME_FUNCTION, null, cur_Conf), log_Reports);

            ((Expression_Node_Function30Impl)f0).In_Subroutine_Function30_1 = null;
            ((Expression_Node_Function30Impl)f0).In_Subroutine_Function30_2 = null;
            f0.SetAttribute(Expression_Node_Function30Impl.PM_NAME_TOGETHER, new Expression_Leaf_StringImpl("", null, cur_Conf), log_Reports);
            f0.SetAttribute(Expression_Node_Function30Impl.PM_NAME_FORM, new Expression_Node_StringImpl(this, cur_Conf), log_Reports);

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
            //
            //
            //
            //（）メソッドの開始。
            //
            //
            //
            Log_Method log_Method = new Log_MethodImpl(0, Log_ReportsImpl.BDebugmode_Static);
            log_Method.BeginMethod(Info_Functions.Name_Library, this, "Execute5_Main",log_Reports);


            if (this.EnumEventhandler == EnumEventhandler.O_Lr)
            {
                this.Execute6_Sub(
                    this.Functionparameterset.Sender,
                    log_Reports
                    );

                //
                //
            }
            else if (this.EnumEventhandler == EnumEventhandler.O_Ea)
            {
                Configurationtree_Node conf_ThisMethod = new Configurationtree_NodeImpl(log_Method.Fullname, null);


                Configuration_Node cf_Event = this.Cur_Configuration.GetParentByNodename(
                    NamesNode.S_EVENT, EnumConfiguration.Unknown, false, log_Reports);
                this.Execute6_Sub(
                    this.Functionparameterset.Sender,
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
            object sender,
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
            Log_Method log_Method = new Log_MethodImpl(1, Log_ReportsImpl.BDebugmode_Static);
            log_Method.BeginMethod(Info_Functions.Name_Library, this, "Execute6_Sub", log_Reports);

            //
            //
            //
            //
            //
            //
            //
            if (log_Method.CanDebug(1))
            {
                log_Method.WriteDebug_ToConsole("関数３０「新規ウィンドウを開く」を実行します。");
            }


            string sName_Fnc;
            this.TrySelectAttribute(out sName_Fnc, PmNames.S_NAME.Name_Pm, EnumHitcount.One_Or_Zero, log_Reports);

            if (log_Reports.CanStopwatch)
            {
                log_Method.Log_Stopwatch.Message = "Nアクション[" + sName_Fnc + "]実行";
                log_Method.Log_Stopwatch.Begin();
            }


            //
            //
            //
            //（）タスク・デスクリプション
            //
            //
            //
            {
                if (sender is Customcontrol)
                {
                    Customcontrol cct = (Customcontrol)sender;

                    string sName_Control;
                    if (null == cct.ControlCommon.Expression_Name_Control)
                    {
                        sName_Control = "＜エラー：名無し＞";
                    }
                    else
                    {
                        sName_Control = cct.ControlCommon.Expression_Name_Control.Execute4_OnExpressionString(
                            EnumHitcount.Unconstraint,
                            log_Reports
                            );
                    }

                    log_Reports.Comment_EventCreationMe = "／追記：[" + sName_Control + "]コントロールが、[" + sName_Fnc + "]アクションを実行。";
                }
                else
                {
                    log_Reports.Comment_EventCreationMe = "／追記：[" + sName_Fnc + "]アクションを実行。";
                }
            }


            // ┳━┳━┳━┳━┳━┳━┳━┳━┳━┳━┳━┳━┳━┳━┳━
            // ┻━┻━┻━┻━┻━┻━┻━┻━┻━┻━┻━┻━┻━┻━┻━
            // 開始
            // ┳━┳━┳━┳━┳━┳━┳━┳━┳━┳━┳━┳━┳━┳━┳━
            // ┻━┻━┻━┻━┻━┻━┻━┻━┻━┻━┻━┻━┻━┻━┻━

            Configurationtree_NodeImpl cf_ThisMethod = new Configurationtree_NodeImpl("!ハードコーディング_NAction30#Execute6_Sub", null);

            if (log_Reports.Successful)
            {
                // 正常時

                Expression_Node_String ec_ArgFormgroup;
                this.TrySelectAttribute(out ec_ArgFormgroup, Expression_Node_Function30Impl.PM_NAME_FORM, EnumHitcount.One_Or_Zero, log_Reports);

                if (null == ec_ArgFormgroup)
                {
                    //
                    // エラー
                    goto gt_Error_NoForm;
                }
            }



            //
            //
            //
            //（３）レイアウト_テーブル読取
            //
            //
            //
            List<Table_Humaninput> oList_Table_Form;//（フォームのセットアップに使う）
            //
            // 「フォーム名（レイアウト_ターゲット名）」を指定。
            if (log_Reports.Successful)
            {
                // 正常時

                // テーブル名から、レイアウト・ファイルパスの取得。
                Expression_Node_String ec_ArgFormgroup;
                this.TrySelectAttribute(out ec_ArgFormgroup, Expression_Node_Function30Impl.PM_NAME_FORM, EnumHitcount.One_Or_Zero, log_Reports);

                oList_Table_Form = this.Owner_MemoryApplication.MemoryTables.GetTable_HumaninputByFormgroup(ec_ArgFormgroup, true, log_Reports);
            }
            else
            {
                oList_Table_Form = new List<Table_Humaninput>();
            }


            //
            //
            //
            //（４）formsフォルダーパス取得
            //
            //
            //
            Expression_Node_Filepath ec_Fopath_Forms;
            if (log_Reports.Successful)
            {
                // 正常時

                XenonName o_Name_Variable = new XenonNameImpl(NamesVar.S_SP_FORMS, this.Cur_Configuration);

                // 変数名。
                Expression_Leaf_StringImpl ec_Atom = new Expression_Leaf_StringImpl(this, o_Name_Variable.Cur_Configuration);
                ec_Atom.SetString(o_Name_Variable.SValue, log_Reports);

                // ファイルパス。
                log_Reports.Log_Callstack.Push(log_Method, "⑤");
                ec_Fopath_Forms = this.Owner_MemoryApplication.MemoryVariables.GetExpressionfilepathByVariablename(ec_Atom, true, log_Reports);
                log_Reports.Log_Callstack.Pop(log_Method, "⑤");
            }
            else
            {
                ec_Fopath_Forms = null;
            }


            //
            //
            //
            //（５）フォームをセットアップ。
            //
            //
            //
            if (log_Reports.Successful)
            {
                // 正常時

                this.Owner_MemoryApplication.MemoryForms.SetupFormAndLoadUsercontrolconfigs(
                    oList_Table_Form,
                    ec_Fopath_Forms,
                    this.Owner_MemoryApplication.MemoryForms.Mainwnd_FormWrapping.Form,
                    log_Reports
                    );
            }


            //
            //
            //
            //（６）『トゥゲザー設定ファイル』読取。
            //
            //
            //
            if (log_Reports.Successful)
            {
                // タイプデータ値「ＳｃｒｉｐｔＴｏｇｅｔｈｅｒｓ」。
                Expression_Leaf_StringImpl ec_NameVariable = new Expression_Leaf_StringImpl(this, cf_ThisMethod);
                ec_NameVariable.SetString(ValuesTypeData.S_CODE_TOGETHERS, log_Reports);

                List<MemoryCodefileinfo> listInfo=null;
                if (log_Reports.Successful)
                {
                    listInfo = this.Owner_MemoryApplication.MemoryCodefiles.GetCodefileinfoByTypedata(ec_NameVariable, true, log_Reports);
                }

                if (log_Reports.Successful)
                {
                    foreach (MemoryCodefileinfo scriptfile in listInfo)
                    {
                        if (log_Reports.Successful)
                        {
                            this.Owner_MemoryApplication.MemoryTogethers.LoadFile(scriptfile.Expression_Filepath, log_Reports);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }


            //
            //
            //
            //（７）「プロジェクトが選択（切替）されたとき」
            //
            //
            //
            if (null != this.In_Subroutine_Function30_2)
            {
                this.In_Subroutine_Function30_2.Perform(
                    oList_Table_Form,
                    ec_Fopath_Forms,
                    this.Cur_Configuration,
                    this.Owner_MemoryApplication.MemoryForms.Mainwnd_FormWrapping.Form,
                    sender,
                    log_Reports
                    );
            }


            //
            //
            //
            //（８）独自実装のコントロールのプロパティー編集。主に、フォームの活性化をしているだけ。
            //
            //
            //
            if (null != this.In_Subroutine_Function30_1)
            {
                this.In_Subroutine_Function30_1.Perform(this.Owner_MemoryApplication, log_Reports);
            }

            // （Ｘ５）コントロールに、妥当性判定を設定します。
            if (log_Reports.Successful)
            {
                // タイプデータ値。
                Expression_Leaf_StringImpl ec_Name_Variable = new Expression_Leaf_StringImpl(this, cf_ThisMethod);
                ec_Name_Variable.SetString(ValuesTypeData.S_CODE_VALIDATORS, log_Reports);

                List<MemoryCodefileinfo> list_Info = null;
                if (log_Reports.Successful)
                {
                    list_Info = this.Owner_MemoryApplication.MemoryCodefiles.GetCodefileinfoByTypedata(ec_Name_Variable, true, log_Reports);
                }

                if (log_Reports.Successful)
                {
                    foreach (MemoryCodefileinfo moScriptfile in list_Info)
                    {
                        if (log_Reports.Successful)
                        {
                            this.Owner_MemoryApplication.MemoryValidators.LoadFile(
                                moScriptfile.Expression_Filepath.Execute4_OnExpressionString(EnumHitcount.Unconstraint,log_Reports),
                                log_Reports);
                        }
                        else
                        {
                            break;
                        }
                    }
                }

            }


            //
            //
            //
            //（９）「レイアウトテーブル」に書かれているコントロール名だけの一覧を作成。
            //
            //
            //
            List<string> sList_Name_Control = new List<string>();
            foreach (Table_Humaninput tableH_Form in oList_Table_Form)
            {
                if (tableH_Form.DataTable.Columns.Contains(NamesFld.S_NAME))
                {
                    // 「NAME」フィールドのあるテーブルが本表。無いのは参照表。

                    foreach (DataRow row in tableH_Form.DataTable.Rows)
                    {
                        string name_Control = ((Cell)row[NamesFld.S_NAME]).Text;
                        sList_Name_Control.Add(name_Control.Trim());
                    }
                }
            }


            //
            //
            //
            //（１０）指定レイアウト内の全てのコントロールの、"Se:読取時;" イベントを実行します。
            //
            //
            //
            if (log_Reports.Successful)
            {
                Executer1_UsercontrolAndEvent exe1 = new Executer1_UsercontrolAndEventImpl();
                exe1.Execute1_AllUsercontrols(
                    sList_Name_Control,
                    sender,
                    new XenonNameImpl(NamesSe.S_LOAD, cf_ThisMethod),
                    this.Owner_MemoryApplication,
                    log_Reports
                    );
            }

            //
            //
            //
            // ロード完了
            //
            //
            //

            goto gt_EndMethod;
        //
        //
            #region 異常系
        //────────────────────────────────────────
        gt_Error_NoForm:
            {
                Builder_TexttemplateP1p tmpl = new Builder_TexttemplateP1pImpl();
                tmpl.SetParameter(1, Log_RecordReportsImpl.ToText_Configuration(cf_ThisMethod), log_Reports);//設定位置パンくずリスト

                this.Owner_MemoryApplication.CreateErrorReport("Er:110015;", tmpl, log_Reports);
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

        private Subroutine_Function30_1 in_Subroutine_Function30_1;

        /// <summary>
        /// ウィンドウを開くアクションの内部処理。
        /// 無ければヌル。
        /// </summary>
        public Subroutine_Function30_1 In_Subroutine_Function30_1
        {
            get
            {
                // 暫定追加
                return in_Subroutine_Function30_1;
            }
            set
            {
                in_Subroutine_Function30_1 = value;
            }
        }

        //────────────────────────────────────────

        private Subroutine_Function30_2 in_Subroutine_Function30_2;

        /// <summary>
        /// ウィンドウを開くアクションの内部処理。
        /// 無ければヌル。
        /// </summary>
        public Subroutine_Function30_2 In_Subroutine_Function30_2
        {
            get
            {
                // 暫定追加。
                return in_Subroutine_Function30_2;
            }
            set
            {
                in_Subroutine_Function30_2 = value;
            }
        }

        //────────────────────────────────────────
        #endregion



    }
}
