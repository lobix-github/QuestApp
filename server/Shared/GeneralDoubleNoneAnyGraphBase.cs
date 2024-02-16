using System.Collections.Generic;

namespace QuestApp.Shared
{
    public class GeneralDoubleNoneAnyGraphBase : GeneralGraphBase
    {
        protected override Dictionary<int, double> GetValues => DoubleNoneAnyValues;
    }
}
