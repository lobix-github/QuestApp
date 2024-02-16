using Microsoft.EntityFrameworkCore;
using QuestApp.DbContexts;

namespace DbManager
{
    class LocalQuestDbContext : QuestDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=Quest;Trusted_Connection=true;ConnectRetryCount=0");
        }
    }

    class AzureQuestDbContext : QuestDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("Server=tcp:questapp.database.windows.net,1433;Initial Catalog=Quest;Persist Security Info=False;User ID=questappadmin;Password=QuestApp!234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }
    }
}
