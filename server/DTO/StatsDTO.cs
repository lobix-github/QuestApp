namespace QuestApp.DTO
{
    public class StatsDTO
    {
        public int StartUsersCount { get; set; }
        public int GeneratedReportsCount { get; set; }
        
        public int UsersCount { get; set; }
        public int SubmitsCount { get; set; }

        public int AllUsersCount => StartUsersCount + UsersCount;
        public int AllSubmitsCount => SubmitsCount;
        public int AllGeneratedReportsCount => (int)(1.06 * StartUsersCount + GeneratedReportsCount);
    }
}
