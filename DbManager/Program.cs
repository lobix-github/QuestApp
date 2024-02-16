using QuestApp.DbContexts;
using System;
using System.Linq;

namespace DbManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Starting db manager...");
            
            var serverType = args.Any() ? args[0] : DB.Constants.SERVER_TYPE;
            Console.WriteLine($"Using server type: {serverType}, connecting...");

            try
            {
                if (serverType != "Local")
                {
                    Console.WriteLine("Error: can not drop/create db for non-Local server type.");
                    Console.ReadKey();
                    return;
                }

                using (var db = (QuestDbContext)Activator.CreateInstance(Type.GetType($"DbManager.{serverType}QuestDbContext")))
                {
                    db.DropCreateDatabase();
                    db.Seed();
                }

                Console.WriteLine($"Finished - no erros (press any key).");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e}");
            }

            Console.ReadKey();
        }
    }
}
