using System.Collections.Generic;

namespace QuestApp.Shared
{
    public class UserNoneAnyGraphBase : UserGraphBase
    {
        protected override Dictionary<int, double> GetValues => NoneAnyValues;
    }
}
