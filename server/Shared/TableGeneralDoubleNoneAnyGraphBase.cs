using System.Collections.Generic;

namespace QuestApp.Shared
{
    public class TableGeneralDoubleNoneAnyGraphBase : TableGeneralGraphBase
    {
        protected override Dictionary<int, double> GetValues => DoubleNoneAnyValues;
    }
}
