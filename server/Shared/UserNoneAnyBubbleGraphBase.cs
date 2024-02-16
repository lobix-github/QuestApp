using System.Collections.Generic;

namespace QuestApp.Shared
{
    public class UserNoneAnyBubbleGraphBase : UserBubbleGraphBase
    {
        protected override Dictionary<int, double> GetValues => NoneAnyValues;
    }
}
