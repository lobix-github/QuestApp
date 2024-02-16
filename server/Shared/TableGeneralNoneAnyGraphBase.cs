using System.Collections.Generic;

namespace QuestApp.Shared
{
    public class TableGeneralNoneAnyGraphBase : TableGeneralGraphBase
    {
        protected override Dictionary<int, double> GetValues => NoneAnyValues;
    }
}
