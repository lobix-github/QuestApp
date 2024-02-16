using System.Collections.Generic;

namespace QuestApp.Shared
{
    public class UserDoubleNoneAnyBubbleGraphBase : UserBubbleGraphBase
    {
        protected override Dictionary<int, double> GetValues => DoubleNoneAnyValues;
    }
}
