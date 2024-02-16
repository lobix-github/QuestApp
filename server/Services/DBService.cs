using DB.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using QuestApp.DbContexts;
using QuestApp.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestApp.Services
{
    public class DBService
    {
        private readonly QuestDbContext db;
        private readonly IConfiguration configuration;
        private readonly AuthenticationStateProvider authenticationStateProvider;
        private readonly IStringLocalizer<Pages.ResultsPage> L;

        public DBService(QuestDbContext _db, IConfiguration _configuration, AuthenticationStateProvider _authenticationStateProvider, IStringLocalizer<Pages.ResultsPage> l)
        {
            db = _db;
            configuration = _configuration;
            authenticationStateProvider = _authenticationStateProvider;
            L = l;
        }

        private Task<QuestionnaireDTO> questionnaireTask;
        public Task<QuestionnaireDTO> GetQuestionnaire()
        {
            if (questionnaireTask == null)
            {
                lock(this)
                {
                    if (questionnaireTask == null)
                    {
                        questionnaireTask = Task.Run(() =>
                        {
                            var builder = new DbContextOptionsBuilder<QuestDbContext>().UseSqlServer(configuration.GetConnectionString(DB.Constants.SERVER_TYPE));
                            using (var db = new QuestDbContext(builder.Options))
                            {
                                var quest = db.Questionnaires.AsNoTrackingWithIdentityResolution()
                                                .Include(x => x.Sections)
                                                .ThenInclude(x => x.Categories)
                                                .ThenInclude(x => x.Questions)
                                                .ThenInclude(x => x.Answers)
                                                .First();

                                return quest.Map<QuestionnaireDTO>();
                            }
                        });
                    }
                }
            }

            return questionnaireTask;
        }

        private SectionDTO[] sections;
        public SectionDTO[] GetSections()
        {
            if (sections == null)
            {
                lock (this)
                {
                    if (sections == null)
                    {
                        var builder = new DbContextOptionsBuilder<QuestDbContext>().UseSqlServer(configuration.GetConnectionString(DB.Constants.SERVER_TYPE));
                        using (var db = new QuestDbContext(builder.Options))
                        {
                            var dbSections = db.Sections.AsNoTrackingWithIdentityResolution()
                                            .Include(x => x.Categories)
                                            .ThenInclude(x => x.Questions)
                                            .ThenInclude(x => x.Answers)
                                            .ToArray();

                            sections = dbSections.Select(x => x.Map<SectionDTO>()).ToArray();
                        }
                    }
                }
            }

            return sections;
        }

        private List<Submit> allSubmits;
        public List<Submit> GetAllSubmits()
        {
            if (allSubmits == null)
            {
                lock (this)
                {
                    if (allSubmits == null)
                    {
                        var builder = new DbContextOptionsBuilder<QuestDbContext>().UseSqlServer(configuration.GetConnectionString(DB.Constants.SERVER_TYPE));
                        using (var db = new QuestDbContext(builder.Options))
                        {
                            allSubmits = db.Submits.AsNoTrackingWithIdentityResolution().Include(x => x.Answers).ToList();
                        }
                    }
                }
            }

            return allSubmits;
        }

        private List<SubmitDTO> userSubmits;
        private Task<List<SubmitDTO>> userSubmitsTask;
        public async Task<List<SubmitDTO>> GetUserSubmitsAsync()
        {
            if (userSubmitsTask == null)
            {
                lock (this)
                {
                    if (userSubmitsTask == null)
                    {
                        userSubmitsTask = Task.Run(async () =>
                        {
                            var builder = new DbContextOptionsBuilder<QuestDbContext>().UseSqlServer(configuration.GetConnectionString(DB.Constants.SERVER_TYPE));
                            using (var db = new QuestDbContext(builder.Options))
                            {
                                var user = await authenticationStateProvider.GetUserAsync(db);
                                var submits = await db.Submits.AsNoTrackingWithIdentityResolution()
                                                            .Where(x => x.UserId == user.Id)
                                                            .OrderBy(x => x.Created)
                                                            .Include(x => x.Questionnaire)
                                                            .Include(x => x.Answers)
                                                            .Include(x => x.User)
                                                            .ToArrayAsync();

                                return userSubmits = submits.Select((x, idx) => { var res = x.Map<SubmitDTO>(); res.Index = idx; res.L = L; return res; }).ToList();
                            }
                        });
                    }
                }
            }

            return await userSubmitsTask;
        }

        public async Task<int> SubmitAsync(Submit submit)
        {
            await db.Submits.AddAsync(submit);
            await db.SaveChangesAsync();

            var submitDTO = submit.Map<SubmitDTO>(); 
            submitDTO.Index = userSubmits.Count; 
            submitDTO.L = L;
            userSubmits.Add(submitDTO);

            allSubmits.Add(submit);
            userSubmitsTask = null;

            return submit.Id;
        }

        public async Task SubmitUserAsync(UserDTO userDTO)
        {
            var user = db.Users.Single(x => x.Email == userDTO.Email);
            _ = userDTO.Map(user);
            await db.SaveChangesAsync();
        }

        public async Task<StatsDTO> GetStatsAsync()
        {
            var stats = await db.Stats.AsNoTrackingWithIdentityResolution().FirstAsync();
            var usersCount = await db.Users.CountAsync(x => x.Role.Contains(DB.Role.normal.ToString()));
            var submitsCount = await db.Submits.CountAsync();
            
            var statsDTO = stats.Map<StatsDTO>();
            statsDTO.UsersCount = usersCount;
            statsDTO.SubmitsCount = submitsCount;
            return statsDTO;
        }

        public void IncreaseGeneratedReportsStats()
        {
            var stats = db.Stats.First();
            stats.GeneratedReportsCount++;
            db.SaveChanges();
        }
    }
}
