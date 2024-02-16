using DB.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace QuestApp.DbContexts
{
    public class QuestDbContext: DbContext
    {
        public DbSet<Submit> Submits { get; }
        public DbSet<User> Users { get; }
        public DbSet<Questionnaire> Questionnaires { get; }
        public DbSet<Section> Sections { get; }
        public DbSet<Category> Categories { get; }
        public DbSet<Question> Questions { get; }
        public DbSet<Answer> Answers { get; }

        public DbSet<Stats> Stats { get; }

        public QuestDbContext(DbContextOptions<QuestDbContext> options) : base(options)
        {
            Submits = Set<Submit>();
            Users = Set<User>();
            Questionnaires = Set<Questionnaire>();
            Sections = Set<Section>();
            Categories = Set<Category>();
            Questions = Set<Question>();
            Answers = Set<Answer>();

            Stats = Set<Stats>();
        }

        public QuestDbContext() : this(new DbContextOptions<QuestDbContext>())
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var submitConf = modelBuilder.Entity<Submit>();
            submitConf.ToTable("Submits");
            submitConf.HasKey(x => x.Id);
            submitConf.Property(x => x.Created).HasDefaultValueSql("GetUtcDate()");
            submitConf.HasOne(x => x.User).WithMany(x => x.Submits).HasForeignKey(x => x.UserId);
            submitConf.HasOne(x => x.Questionnaire).WithMany(x => x.Submits).HasForeignKey(x => x.QuestionnaireId);
            submitConf.HasMany(x => x.Answers).WithMany(x => x.Submits)
                .UsingEntity<Dictionary<string, object>>(
                    "SubmitAnswer",
                    j => j.HasOne<Answer>().WithMany().OnDelete(DeleteBehavior.Cascade),
                    j => j.HasOne<Submit>().WithMany().OnDelete(DeleteBehavior.ClientCascade));

            var userConf = modelBuilder.Entity<User>();
            userConf.ToTable("Users");
            userConf.HasKey(x => x.Id);
            userConf.Property(x => x.Email).IsRequired();
            userConf.Property(x => x.Role).HasMaxLength(string.Concat(Enum.GetNames(typeof(DB.Role)), ", ").Length).IsRequired();
            userConf.Property(x => x.Password).IsRequired();
            userConf.Property(x => x.Name).IsRequired();
            userConf.Property(x => x.CountryId).IsRequired();
            userConf.Property(x => x.CitySizeId).IsRequired();
            userConf.Property(x => x.NumberOfEmployeesId).IsRequired();
            userConf.Property(x => x.SectorId).IsRequired();
            userConf.Property(x => x.ServiceAreaId).IsRequired();
            userConf.Property(x => x.OperationTimeId).IsRequired();
            userConf.Property(x => x.TurnoverId).IsRequired();
            userConf.Property(x => x.EnterpriseRoleId).IsRequired();
            userConf.Property(x => x.Created).HasDefaultValueSql("GetUtcDate()");
            userConf.HasMany(x => x.Questionnaires).WithOne(x => x.Author).HasForeignKey(x => x.AuthorId).OnDelete(DeleteBehavior.NoAction);
            userConf.HasMany(x => x.EditableQuestionnaires).WithMany(x => x.Editors);

            var questConf = modelBuilder.Entity<Questionnaire>();
            questConf.ToTable("Questionnaires");
            questConf.HasKey(x => x.Id);
            questConf.Property(x => x.Created).HasDefaultValueSql("GetUtcDate()");
            questConf.HasMany(x => x.Editors).WithMany(x => x.EditableQuestionnaires);
            questConf.HasMany(x => x.Sections).WithOne(x => x.Questionnaire).HasForeignKey(x => x.QuestionnaireId);
            questConf.HasQueryFilter(x => !x.IsDeleted);

            var sectionsConf = modelBuilder.Entity<Section>();
            sectionsConf.ToTable("Sections");
            sectionsConf.HasKey(x => x.Id);
            sectionsConf.Property(x => x.Index).IsRequired();
            sectionsConf.HasMany(x => x.Categories).WithOne(x => x.Section).HasForeignKey(x => x.SectionId);

            var categotriesConf = modelBuilder.Entity<Category>();
            categotriesConf.ToTable("Categories");
            categotriesConf.HasKey(x => x.Id);
            categotriesConf.Property(x => x.Index).IsRequired();
            categotriesConf.HasMany(x => x.Questions).WithOne(x => x.Category).HasForeignKey(x => x.CategoryId);

            var questionConf = modelBuilder.Entity<Question>();
            questionConf.ToTable("Questions");
            questionConf.HasKey(x => x.Id);
            questionConf.Property(x => x.Wage).IsRequired();
            questionConf.Property(x => x.Index).IsRequired();
            questionConf.HasMany(x => x.Answers).WithOne(x => x.Question).HasForeignKey(x => x.QuestionId);

            var answerConf = modelBuilder.Entity<Answer>();
            answerConf.ToTable("Answers");
            answerConf.HasKey(x => x.Id);
            answerConf.Property(x => x.Index).IsRequired();

            var statsConf = modelBuilder.Entity<Stats>();
            statsConf.ToTable("Stats");
            statsConf.HasKey(x => x.Id);
        }
    }
}
