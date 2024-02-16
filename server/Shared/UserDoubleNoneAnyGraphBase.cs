using System.Collections.Generic;

namespace QuestApp.Shared
{
    public class UserDoubleNoneAnyGraphBase : UserGraphBase
    {
        protected override Dictionary<int, double> GetValues => DoubleNoneAnyValues;
    }
}
