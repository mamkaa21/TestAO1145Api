using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace TestAO1145Api;

public partial class Testao1145Context : DbContext
{
    public Testao1145Context()
    {
    }

    public Testao1145Context(DbContextOptions<Testao1145Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Answer> Answers { get; set; }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Mark> Marks { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Studentanswer> Studentanswers { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    public virtual DbSet<Test> Tests { get; set; }

    public virtual DbSet<Testcrossquestion> Testcrossquestions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=192.168.200.13;user=student;password=student;database=testao1145", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.3.39-mariadb")); //"server=192.168.200.13;user=student;password=student;database=testao1145"

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Answer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("answer")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.IdQuestion, "FK_answer_question_Id");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.IdQuestion)
                .HasColumnType("int(11)")
                .HasColumnName("Id_Question");
            entity.Property(e => e.Text)
                .HasMaxLength(255)
                .HasColumnName("text");

            entity.HasOne(d => d.IdQuestionNavigation).WithMany(p => p.Answers)
                .HasForeignKey(d => d.IdQuestion)
                .HasConstraintName("FK_answer_question_Id");
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("class")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.IdTeacher, "FK_class_teacher_Id");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.IdTeacher)
                .HasColumnType("int(11)")
                .HasColumnName("Id_Teacher");
            entity.Property(e => e.Number).HasColumnType("int(11)");

            entity.HasOne(d => d.IdTeacherNavigation).WithMany(p => p.Classes)
                .HasForeignKey(d => d.IdTeacher)
                .HasConstraintName("FK_class_teacher_Id");
        });

        modelBuilder.Entity<Mark>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("mark")
                .UseCollation("utf8mb4_unicode_ci");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.CountQ).HasColumnType("int(11)");
            entity.Property(e => e.Number).HasColumnType("int(11)");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("question")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.IdTest, "FK_question_test_Id");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.IdTest)
                .HasColumnType("int(11)")
                .HasColumnName("Id_Test");
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.IdTestNavigation).WithMany(p => p.Questions)
                .HasForeignKey(d => d.IdTest)
                .HasConstraintName("FK_question_test_Id");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("student")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.IdClass, "FK_student_class_Id");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("ID");
            entity.Property(e => e.Age).HasMaxLength(255);
            entity.Property(e => e.FirstName).HasMaxLength(255);
            entity.Property(e => e.IdClass)
                .HasColumnType("int(11)")
                .HasColumnName("Id_Class");
            entity.Property(e => e.LastName).HasMaxLength(255);
            entity.Property(e => e.Login).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);

            entity.HasOne(d => d.IdClassNavigation).WithMany(p => p.Students)
                .HasForeignKey(d => d.IdClass)
                .HasConstraintName("FK_student_class_Id");
        });

        modelBuilder.Entity<Studentanswer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("studentanswer")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.IdMark, "FK_studentanswer_mark_Id");

            entity.HasIndex(e => e.IdStudent, "FK_studentanswer_student_ID");

            entity.HasIndex(e => e.IdTest, "FK_studentanswer_test_Id");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.DateTime).HasColumnType("datetime");
            entity.Property(e => e.IdMark)
                .HasColumnType("int(11)")
                .HasColumnName("Id_Mark");
            entity.Property(e => e.IdStudent)
                .HasColumnType("int(11)")
                .HasColumnName("Id_Student");
            entity.Property(e => e.IdTest)
                .HasColumnType("int(11)")
                .HasColumnName("Id_Test");

            entity.HasOne(d => d.IdMarkNavigation).WithMany(p => p.Studentanswers)
                .HasForeignKey(d => d.IdMark)
                .HasConstraintName("FK_studentanswer_mark_Id");

            entity.HasOne(d => d.IdStudentNavigation).WithMany(p => p.Studentanswers)
                .HasForeignKey(d => d.IdStudent)
                .HasConstraintName("FK_studentanswer_student_ID");

            entity.HasOne(d => d.IdTestNavigation).WithMany(p => p.Studentanswers)
                .HasForeignKey(d => d.IdTest)
                .HasConstraintName("FK_studentanswer_test_Id");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("subject")
                .UseCollation("utf8mb4_unicode_ci");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("teacher")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.Id, "UK_teacher_Id").IsUnique();

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.FirstName).HasMaxLength(255);
            entity.Property(e => e.LastName).HasMaxLength(255);
            entity.Property(e => e.Login).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);

            entity.HasMany(d => d.IdSubjects).WithMany(p => p.IdTeachers)
                .UsingEntity<Dictionary<string, object>>(
                    "Teachercrosssubject",
                    r => r.HasOne<Subject>().WithMany()
                        .HasForeignKey("IdSubject")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_teachercrosssubject_subject_Id"),
                    l => l.HasOne<Teacher>().WithMany()
                        .HasForeignKey("IdTeacher")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_teachercrosssubject_teacher_Id"),
                    j =>
                    {
                        j.HasKey("IdTeacher", "IdSubject")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j
                            .ToTable("teachercrosssubject")
                            .UseCollation("utf8mb4_unicode_ci");
                        j.HasIndex(new[] { "IdSubject" }, "FK_teachercrosssubject_subject_Id");
                        j.HasIndex(new[] { "IdTeacher" }, "FK_teachercrosssubject_teacher_Id");
                        j.IndexerProperty<int>("IdTeacher")
                            .HasColumnType("int(11)")
                            .HasColumnName("Id_Teacher");
                        j.IndexerProperty<int>("IdSubject")
                            .HasColumnType("int(11)")
                            .HasColumnName("Id_Subject");
                    });
        });

        modelBuilder.Entity<Test>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("test")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.IdSubject, "FK_test_subject_Id");

            entity.HasIndex(e => e.IdTeacher, "FK_test_teacher_Id");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.CountQuestionTest).HasColumnType("int(11)");
            entity.Property(e => e.IdSubject)
                .HasColumnType("int(11)")
                .HasColumnName("Id_Subject");
            entity.Property(e => e.IdTeacher)
                .HasColumnType("int(11)")
                .HasColumnName("Id_Teacher");
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.IdSubjectNavigation).WithMany(p => p.Tests)
                .HasForeignKey(d => d.IdSubject)
                .HasConstraintName("FK_test_subject_Id");

            entity.HasOne(d => d.IdTeacherNavigation).WithMany(p => p.Tests)
                .HasForeignKey(d => d.IdTeacher)
                .HasConstraintName("FK_test_teacher_Id");
        });

        modelBuilder.Entity<Testcrossquestion>(entity =>
        {
            entity.HasKey(e => new { e.IdStudent, e.IdQuestion, e.IdAnswer })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

            entity
                .ToTable("testcrossquestion")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.IdAnswer, "FK_testcrossquestion_answer_Id");

            entity.HasIndex(e => e.IdQuestion, "FK_testcrossquestion_question_Id");

            entity.Property(e => e.IdStudent)
                .ValueGeneratedOnAdd()
                .HasColumnType("int(11)")
                .HasColumnName("Id_Student");
            entity.Property(e => e.IdQuestion)
                .HasColumnType("int(11)")
                .HasColumnName("Id_Question");
            entity.Property(e => e.IdAnswer)
                .HasColumnType("int(11)")
                .HasColumnName("Id_Answer");

            entity.HasOne(d => d.IdAnswerNavigation).WithMany(p => p.Testcrossquestions)
                .HasForeignKey(d => d.IdAnswer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_testcrossquestion_answer_Id");

            entity.HasOne(d => d.IdQuestionNavigation).WithMany(p => p.Testcrossquestions)
                .HasForeignKey(d => d.IdQuestion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_testcrossquestion_question_Id");

            entity.HasOne(d => d.IdStudentNavigation).WithMany(p => p.Testcrossquestions)
                .HasForeignKey(d => d.IdStudent)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_testcrossquestion_studentanswer_Id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
