﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xenon.Syntax;
using Xenon.Middle;

namespace Xenon.Functions
{



    /// <summary>
    /// (text format)
    /// </summary>
    public abstract class Utility_Textformat
    {



        #region アクション
        //────────────────────────────────────────

        public static string Format_StopwatchComment(
            Expression_Node_FunctionAbstract ec_Sa,
            Configuration_Node cf_ThisAction,
            Log_Reports log_Reports
            )
        {

            string sControl = "";
            {
                Configuration_Node cf_Event = cf_ThisAction.GetParentByNodename(
                    NamesNode.S_EVENT,　EnumConfiguration.Unknown, true, log_Reports);
                if (log_Reports.Successful)
                {
                    Configuration_Node owner_Configurationtree_Control = cf_Event.GetParentByNodename(
                        NamesNode.S_CONTROL1, EnumConfiguration.Tree, true, log_Reports);
                    ((Configurationtree_Node)owner_Configurationtree_Control).Dictionary_Attribute.TryGetValue(PmNames.S_NAME, out sControl, false, log_Reports);
                }
            }

            string sEventName = "";
            {
                Configuration_Node cf_Event = cf_ThisAction.GetParentByNodename(NamesNode.S_EVENT, EnumConfiguration.Tree, true, log_Reports);

                if (log_Reports.Successful)
                {
                    ((Configurationtree_Node)cf_Event).Dictionary_Attribute.TryGetValue(PmNames.S_NAME, out sEventName, false, log_Reports);
                }
            }

            string sActionType = "";
            {
                string sFncName0;
                ec_Sa.TrySelectAttribute(out sFncName0, PmNames.S_NAME.Name_Pm, EnumHitcount.One_Or_Zero, log_Reports);
                sActionType = sFncName0;
            }



            StringBuilder sb = new StringBuilder();

            {
                if ("" != sActionType)
                {
                    sb.Append("　Nアクション＝[");
                    sb.Append(sActionType);
                    sb.Append("]");
                }

                sb.Append("　FC[");
                sb.Append(sControl);
                sb.Append("].EV[");
                sb.Append(sEventName);
                sb.Append("]");
            }


            return sb.ToString();
        }

        //────────────────────────────────────────
        #endregion



    }
}
