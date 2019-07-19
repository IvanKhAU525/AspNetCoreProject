using System;
using AspNetCoreProject.Infrastructure.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;

namespace AspNetCoreProject.Infrastructure.Data.Context
{

    public partial class InquirerContext : DbContext
    {
        public InquirerContext(DbContextOptions<InquirerContext> options)
            : base(options) {
        }

        public virtual DbSet<ActionPlanDb> Actionplans { get; set; }
        public virtual DbSet<ActionDb> Actions { get; set; }
        public virtual DbSet<AnswerDb> Answers { get; set; }
        public virtual DbSet<HintDb> Hints { get; set; }
        public virtual DbSet<InteractionDb> Interactions { get; set; }
        public virtual DbSet<LearnDb> Learns { get; set; }
        public virtual DbSet<QuestionnaireDb> Questionnaires { get; set; }
        public virtual DbSet<QuestionDb> Questions { get; set; }
        public virtual DbSet<SessionDb> Sessions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<ActionPlanDb>(entity =>
            {
                entity.HasKey(e => e.ActionPlanId);

                entity.ToTable("actionplans", "inquirer");

                entity.Property(e => e.ActionPlanId).HasColumnType("int(11)");

                entity.Property(e => e.LinkToWebsite).IsUnicode(false);

                entity.Property(e => e.TextDisplay).IsUnicode(false);
            });

            modelBuilder.Entity<ActionDb>(entity =>
            {
                entity.HasKey(e => e.ActionId);

                entity.ToTable("actions", "inquirer");

                entity.HasIndex(e => e.ActionPlanId)
                    .HasName("actions_actionplans_fk");

                entity.Property(e => e.ActionId).HasColumnType("int(11)");

                entity.Property(e => e.ActionPlanId).HasColumnType("int(11)");

                entity.Property(e => e.TextDisplay).HasColumnType("mediumtext");

                entity.HasOne(d => d.ActionPlan)
                    .WithMany(p => p.Actions)
                    .HasForeignKey(d => d.ActionPlanId)
                    .HasConstraintName("actions_actionplans_fk");
            });

            modelBuilder.Entity<AnswerDb>(entity => {
                
                entity.HasKey(e => e.AnswerId);
                
                entity.ToTable("answers", "inquirer");

                entity.HasIndex(e => e.ActionId)
                    .HasName("answers_actions_fk");

                entity.HasIndex(e => e.LearnId)
                    .HasName("answers_learns_fk");

                entity.HasIndex(e => e.NextQuestionId)
                    .HasName("answers_nextquestions_fk");

                entity.HasIndex(e => e.QuestionId)
                    .HasName("answers_questions_fk");

                entity.Property(e => e.AnswerId).HasColumnType("int(11)");

                entity.Property(e => e.ActionId).HasColumnType("int(11)");

                entity.Property(e => e.LearnId).HasColumnType("int(11)");

                entity.Property(e => e.NextQuestionId).HasColumnType("int(11)");

                entity.Property(e => e.QuestionId).HasColumnType("int(11)");

                entity.Property(e => e.TextDisplay).IsUnicode(false);

                entity.HasOne(d => d.Action)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.ActionId)
                    .HasConstraintName("answers_actions_fk");

                entity.HasOne(d => d.Learn)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.LearnId)
                    .HasConstraintName("answers_learns_fk");

                entity.HasOne(d => d.NextQuestion)
                    .WithMany(p => p.AnswersNextQuestion)
                    .HasForeignKey(d => d.NextQuestionId)
                    .HasConstraintName("answers_nextquestions_fk");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.AnswersQuestion)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("answers_questions_fk");
            });

            modelBuilder.Entity<HintDb>(entity =>
            {
                entity.HasKey(e => e.HintId);

                entity.ToTable("hints", "inquirer");

                entity.Property(e => e.HintId).HasColumnType("int(11)");

                entity.Property(e => e.TextDisplay).IsUnicode(false);
            });

            modelBuilder.Entity<InteractionDb>(entity =>
            {
                entity.HasKey(e => e.InteractionId);

                entity.ToTable("interactions", "inquirer");

                entity.HasIndex(e => e.SessionId)
                    .HasName("interactions_sessions_fk");

                entity.Property(e => e.InteractionId).HasColumnType("int(11)");

                entity.Property(e => e.AnswerId).HasColumnType("int(11)");

                entity.Property(e => e.AnswerTextDisplay).IsUnicode(false);

                entity.Property(e => e.QuestionId).HasColumnType("int(11)");

                entity.Property(e => e.QuestionTextDisplay).IsUnicode(false);

                entity.Property(e => e.SessionId).HasColumnType("int(11)");

                entity.HasOne(d => d.Session)
                    .WithMany(p => p.Interactions)
                    .HasForeignKey(d => d.SessionId)
                    .HasConstraintName("interactions_sessions_fk");
            });

            modelBuilder.Entity<LearnDb>(entity =>
            {
                entity.HasKey(e => e.LearnId);

                entity.ToTable("learns", "inquirer");

                entity.Property(e => e.LearnId).HasColumnType("int(11)");

                entity.Property(e => e.TextDisplay).IsUnicode(false);
            });

            modelBuilder.Entity<QuestionnaireDb>(entity =>
            {
                entity.HasKey(e => e.QuestionnaireId);

                entity.ToTable("questionnaires", "inquirer");

                entity.Property(e => e.QuestionnaireId).HasColumnType("int(11)");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Name).HasColumnType("tinytext");
            });

            modelBuilder.Entity<QuestionDb>(entity =>
            {
                entity.HasKey(e => e.QuestionId);

                entity.ToTable("questions", "inquirer");

                entity.HasIndex(e => e.HintId)
                    .HasName("questions_hints_fk");

                entity.HasIndex(e => e.QuestionnaireId)
                    .HasName("questions_questionnaires_fk");

                entity.Property(e => e.QuestionId).HasColumnType("int(11)");

                entity.Property(e => e.HintId).HasColumnType("int(11)");

                entity.Property(e => e.QuestionnaireId).HasColumnType("int(11)");

                entity.Property(e => e.TextDisplay).IsUnicode(false);

                entity.HasOne(d => d.Hint)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.HintId)
                    .HasConstraintName("questions_hints_fk");

                entity.HasOne(d => d.Questionnaire)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.QuestionnaireId)
                    .HasConstraintName("questions_questionnaires_fk");
            });

            modelBuilder.Entity<SessionDb>(entity =>
            {
                entity.HasKey(e => e.SessionId);

                entity.ToTable("sessions", "inquirer");

                entity.Property(e => e.SessionId).HasColumnType("int(11)");
            });
        }
    }
}
