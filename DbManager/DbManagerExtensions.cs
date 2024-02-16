using AutoMapper;
using DB;
using DB.Models;
using DbManager.DTO;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QuestApp.DbContexts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;

namespace DbManager
{
    public static class DbManagerExtensions
    {
        public static void DropCreateDatabase(this QuestDbContext db)
        {
            Console.WriteLine($"Deleting db...");
            db.Database.EnsureDeleted();
            
            Console.WriteLine($"Creating db...");
            db.Database.EnsureCreated();

        }

        public static void Seed(this QuestDbContext db)
        {
            Console.WriteLine($"Seeding db - users...");

            // users
            var systemUser = new User();
            systemUser.Email = "system@questapp.com";
            systemUser.Role = Role.normal.ToString();
            systemUser.Password = Crypto.GetDbPassword(systemUser.Email, "questappadmin");
            systemUser.Name = "QuestApp system";
            systemUser.CountryId = 1;
            systemUser.CitySizeId = 1;
            systemUser.NumberOfEmployeesId = 1;
            systemUser.SectorId = 3;
            systemUser.ServiceAreaId = 22;
            systemUser.OperationTimeId = 1;
            systemUser.TurnoverId = 1;
            systemUser.EnterpriseRoleId = 1;
            db.Users.Add(systemUser);
            db.SaveChanges();

            var user1 = new User();
            user1.Email = "luk81luk81@gmail.com";
            user1.Role = $"{Role.normal}";
            user1.Password = Crypto.GetDbPassword(user1.Email, "asda");
            user1.Name = "LobiXoft i syn";
            user1.CountryId = 1;
            user1.CitySizeId = 2;
            user1.NumberOfEmployeesId = 3;
            user1.SectorId = 3;
            user1.ServiceAreaId = 22;
            user1.OperationTimeId = 3;
            user1.TurnoverId = 2;
            user1.EnterpriseRoleId = 3;
            db.Users.Add(user1);
            db.SaveChanges();

            var userTranslate = new User();
            userTranslate.Email = "translate@questapp.com";
            userTranslate.Role = Role.translate.ToString();
            userTranslate.Password = Crypto.GetDbPassword(userTranslate.Email, "questapptranslate");
            userTranslate.Name = "QuestApp translation";
            userTranslate.CountryId = 1;
            userTranslate.CitySizeId = 1;
            userTranslate.NumberOfEmployeesId = 1;
            userTranslate.SectorId = 3;
            userTranslate.ServiceAreaId = 22;
            userTranslate.OperationTimeId = 1;
            userTranslate.TurnoverId = 1;
            userTranslate.EnterpriseRoleId = 1;
            db.Users.Add(userTranslate);
            db.SaveChanges();

            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<DbManagerMapperProfile>()).CreateMapper();
            var questDTO = JsonConvert.DeserializeObject<QuestionnaireDTO>(File.ReadAllText(@"Data\questionnaire.txt"));

            var quest = mapper.Map<Questionnaire>(questDTO);
            quest.Author = user1;
            quest.Editors.Add(user1);
            db.Questionnaires.Add(quest);
            db.SaveChanges();

            int counter = 0;
            questDTO = mapper.Map<QuestionnaireDTO>(quest);
            var submits = CreateSystemQuestionnaires(db, questDTO).ToArray();
            foreach (var submit in submits)
            {
                submit.User = systemUser;
                submit.Questionnaire = quest;

                db.Submits.Add(submit);
                if (counter++ % 100 == 0)
                {
                    Console.WriteLine($"Seeding db - surveys ({counter}/{submits.Length})...");
                    db.SaveChanges();
                }
            }
            db.SaveChanges();

            var newSubmit = new Submit();
            newSubmit.CountryId = user1.CountryId;
            newSubmit.CitySizeId = user1.CitySizeId;
            newSubmit.NumberOfEmployeesId = user1.NumberOfEmployeesId;
            newSubmit.SectorId = user1.SectorId;
            newSubmit.ServiceAreaId = user1.ServiceAreaId;
            newSubmit.OperationTimeId = user1.OperationTimeId;
            newSubmit.TurnoverId = user1.TurnoverId;
            newSubmit.EnterpriseRoleId = user1.EnterpriseRoleId;
            newSubmit.User = user1;
            newSubmit.Questionnaire = quest;
            newSubmit.Answers = submits.Last().Answers;
            db.Submits.Add(newSubmit);
            db.SaveChanges();

            Console.WriteLine($"Initializing stats...");
            var stats = new Stats();
            stats.StartUsersCount = submits.Length - 1; // "system@questapp.com" is already counted in submits.Length
            db.Stats.Add(stats);
            db.SaveChanges();
        }

        private static IEnumerable<Submit> CreateSystemQuestionnaires(QuestDbContext db, QuestionnaireDTO questDTO)
        {
            var connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}; Extended Properties=Excel 12.0;", @"Data\input.xlsx");

            var adapter = new OleDbDataAdapter("SELECT * FROM [data$]", connectionString);
            var ds = new DataSet();
            adapter.Fill(ds);
            var data = ds.Tables[0].AsEnumerable().ToArray();

            int counter = 0;
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<DbManagerMapperProfile>()).CreateMapper();
            foreach (var row in data)
            {
                if (counter++ % 100 == 0)
                {
                    Console.WriteLine($"Creating questionnaire {counter}/{data.Length}...");
                }
                var newQuestDTO = questDTO.Clone();

                for (int sec = 0; sec < newQuestDTO.Sections.OrderBy(x => x.Index).Count(); sec++)
                {
                    for (int cat = 0; cat < newQuestDTO.Sections[sec].Categories.OrderBy(x => x.Index).Count(); cat++)
                    {
                        for (int q = 0; q < newQuestDTO.Sections[sec].Categories[cat].Questions.OrderBy(x => x.Index).Count(); q++)
                        {
                            var question = newQuestDTO.Sections[sec].Categories[cat].Questions[q];
                            var a_index = int.Parse(row[$"a_{sec + 1}_{cat + 1}_{q + 1}"].ToString());
                            question.SelectedAnswer = question.Answers.Single(x => x.Index == a_index).Id;
                        }
                    }
                }

                var answerIds = newQuestDTO.Sections.SelectMany(section => section.Categories.SelectMany(cat => cat.Questions.Select(q => q.SelectedAnswer)));
                var quest = mapper.Map<Questionnaire>(newQuestDTO);
                var answers = db.Answers.Where(x => answerIds.Contains(x.Id)).ToList();
                var submit = new Submit
                {
                    Answers = answers,
                    CountryId = int.Parse(row["CountryId"].ToString()),
                    CitySizeId = int.Parse(row["CitySizeId"].ToString()),
                    NumberOfEmployeesId = int.Parse(row["NumberOfEmployeesId"].ToString()),
                    SectorId = int.Parse(row["SectorId"].ToString()),
                    ServiceAreaId = int.Parse(row["ServiceAreaId"].ToString()),
                    OperationTimeId = int.Parse(row["OperationTimeId"].ToString()),
                    TurnoverId = int.Parse(row["TurnoverId"].ToString()),
                    EnterpriseRoleId = int.Parse(row["EnterpriseRoleId"].ToString()),
                };

                yield return submit;
            }
        }
    }
}
