﻿using System;
using System.Collections.Generic;
using System.Data;//DataRow
using System.Linq;
using System.Text;

using Xenon.Syntax;
using Xenon.Table;//DefaultTable

namespace Xenon.Operating
{

    /// <summary>
    /// </summary>
    public class MemoryToMemory_Stylesheet
    {



        #region アクション
        //────────────────────────────────────────

        /// <summary>
        /// スタイルシート・テーブルは、最低限「NAME」「STYLE」の2つで構成されたテーブルです。
        /// </summary>
        /// <param name="oStyleSheetTable"></param>
        /// <returns></returns>
        public MemoryStyles Translate(
            Table_Humaninput xenonTable_Stylesheet,
            Log_Reports log_Reports
            )
        {
            Log_Method pg_Method = new Log_MethodImpl();
            pg_Method.BeginMethod(Info_Operating.Name_Library, this, "MToO",log_Reports);

            //
            //
            //
            //

            if (null == xenonTable_Stylesheet)
            {
                goto gt_Error_NullTable;
            }

            MemoryStyles oStyleAttrList = new MemoryStylesImpl();

            int nIndex = 0;
            foreach (DataRow dataRow in xenonTable_Stylesheet.DataTable.Rows)
            {
                string sId;
                if (log_Reports.Successful)
                {
                    // 正常時

                    Cell valueH;
                    if (log_Reports.Successful)
                    {
                        // 正常時

                        valueH = Utility_Row.GetFieldvalue(
                            "NAME",
                            dataRow,
                            true,
                            log_Reports,
                            "スタイルシートテーブル（NAME検索時）"
                            );
                        if (!log_Reports.Successful)
                        {
                            // 既エラー。
                            goto gt_EndMethod;
                        }

                    }
                    else
                    {
                        valueH = null;
                    }

                    if (log_Reports.Successful)
                    {
                        // 正常時

                        sId = ((Cell)valueH).Text;//"スタイルシートテーブルパーサーのID"
                    }
                    else
                    {
                        sId = "";
                    }
                }
                else
                {
                    sId = "";
                }

                string sStyle;
                if (log_Reports.Successful)
                {
                    // 正常時

                    Cell valueH = Utility_Row.GetFieldvalue(
                        "STYLE",
                        dataRow,
                        true,
                        log_Reports,
                        "スタイルシートテーブル（STYLE検索時）"
                        );
                    if (!log_Reports.Successful)
                    {
                        // 既エラー。
                        goto gt_EndMethod;
                    }

                    sStyle = ((Cell)valueH).Text;//"スタイルシートテーブルパーサーのSTYLE"
                }
                else
                {
                    sStyle = "";
                }

                RecordXenonStyle item = new RecordXenonStyleImpl();
                item.Id = sId;
                item.Style = sStyle;
                oStyleAttrList.Dictionary_RecordStyle.Add(sId, item);

                nIndex++;
            }

            goto gt_EndMethod;
        //
        //
            #region 異常系
        //────────────────────────────────────────
        gt_Error_NullTable:
            {
                oStyleAttrList = null;

                if (log_Reports.CanCreateReport)
                {
                    Log_RecordReports r = log_Reports.BeginCreateReport(EnumReport.Error);
                    r.SetTitle("▲エラー99999！", pg_Method);

                    StringBuilder t = new StringBuilder();
                    t.Append("テーブルがヌルでした。");
                    t.Append(Environment.NewLine);
                    t.Append(Environment.NewLine);

                    // ヒント

                    r.Message = t.ToString();
                    log_Reports.EndCreateReport();
                }
            }
            goto gt_EndMethod;
        //────────────────────────────────────────
            #endregion
        //
        //
        gt_EndMethod:
            pg_Method.EndMethod(log_Reports);
            return oStyleAttrList;
        }

        //────────────────────────────────────────
        #endregion



    }
}
