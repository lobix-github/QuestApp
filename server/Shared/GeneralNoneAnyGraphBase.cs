using System.Collections.Generic;

namespace QuestApp.Shared
{
    public class GeneralNoneAnyGraphBase : GeneralGraphBase
    {
        protected override Dictionary<int, double> GetValues => NoneAnyValues;
    }
}
