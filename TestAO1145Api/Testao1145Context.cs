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

    public virtual DbSet<Aswer> Aswers { get; set; }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Mark> Marks { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Studentanswer> Studentanswers { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    public virtual DbSet<Teachercrosssubject> Teachercrosssubjects { get; set; }

    public virtual DbSet<Test> Tests { get; set; }

    public virtual DbSet<Testcrossquestion> Testcrossquestions { get; set; }

    public virtual DbSet<Testcrossteacher> Testcrossteachers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=127.0.0.1;user=root;database=testao1145", Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.7.33-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_unicode_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Aswer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("aswer");

            entity.HasIndex(e => e.IdQuestion, "FK_aswer_question_Id");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("int(11)");
            entity.Property(e => e.IdQuestion)
                .HasColumnType("int(11)")
                .HasColumnName("Id_Question");
            entity.Property(e => e.Text)
                .HasMaxLength(255)
                .HasColumnName("text");

            entity.HasOne(d => d.IdQuestionNavigation).WithMany(p => p.Aswers)
                .HasForeignKey(d => d.IdQuestion)
                .HasConstraintName("FK_aswer_question_Id");
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("class");

            entity.HasIndex(e => e.IdStudent, "FK_class_student_ID");

            entity.HasIndex(e => e.IdTeacher, "FK_class_teacher_Id");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("int(11)");
            entity.Property(e => e.IdStudent)
                .HasColumnType("int(11)")
                .HasColumnName("Id_Student");
            entity.Property(e => e.IdTeacher)
                .HasColumnType("int(11)")
                .HasColumnName("Id_Teacher");
            entity.Property(e => e.Number).HasColumnType("int(11)");

            entity.HasOne(d => d.IdStudentNavigation).WithMany(p => p.Classes)
                .HasForeignKey(d => d.IdStudent)
                .HasConstraintName("FK_class_student_ID");

            entity.HasOne(d => d.IdTeacherNavigation).WithMany(p => p.Classes)
                .HasForeignKey(d => d.IdTeacher)
                .HasConstraintName("FK_class_teacher_Id");
        });

        modelBuilder.Entity<Mark>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("mark");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("int(11)");
            entity.Property(e => e.Number).HasColumnType("int(11)");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("question");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("int(11)");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("student");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
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
        });

        modelBuilder.Entity<Studentanswer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("studentanswer");

            entity.HasIndex(e => e.IdStudent, "FK_studentanswer_student_ID");

            entity.HasIndex(e => e.IdTest, "FK_studentanswer_test_Id");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("int(11)");
            entity.Property(e => e.DateTime).HasColumnType("datetime");
            entity.Property(e => e.IdStudent)
                .HasColumnType("int(11)")
                .HasColumnName("Id_Student");
            entity.Property(e => e.IdTest)
                .HasColumnType("int(11)")
                .HasColumnName("Id_Test");

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

            entity.ToTable("subject");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("int(11)");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("teacher");

            entity.HasIndex(e => e.IdClass, "FK_teacher_class_Id");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("int(11)");
            entity.Property(e => e.FirstName).HasMaxLength(255);
            entity.Property(e => e.IdClass)
                .HasColumnType("int(11)")
                .HasColumnName("Id_Class");
            entity.Property(e => e.LastName).HasMaxLength(255);
            entity.Property(e => e.Login).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);

            entity.HasOne(d => d.IdClassNavigation).WithMany(p => p.Teachers)
                .HasForeignKey(d => d.IdClass)
                .HasConstraintName("FK_teacher_class_Id");
        });

        modelBuilder.Entity<Teachercrosssubject>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("teachercrosssubject");

            entity.HasIndex(e => e.IdSubject, "FK_teachercrosssubject_subject_Id");

            entity.HasIndex(e => e.IdTeacher, "FK_teachercrosssubject_teacher_Id");

            entity.Property(e => e.IdSubject)
                .HasColumnType("int(11)")
                .HasColumnName("Id_Subject");
            entity.Property(e => e.IdTeacher)
                .HasColumnType("int(11)")
                .HasColumnName("Id_Teacher");

            entity.HasOne(d => d.IdSubjectNavigation).WithMany()
                .HasForeignKey(d => d.IdSubject)
                .HasConstraintName("FK_teachercrosssubject_subject_Id");

            entity.HasOne(d => d.IdTeacherNavigation).WithMany()
                .HasForeignKey(d => d.IdTeacher)
                .HasConstraintName("FK_teachercrosssubject_teacher_Id");
        });

        modelBuilder.Entity<Test>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("test");

            entity.HasIndex(e => e.IdQuestion, "FK_test_aswer_Id");

            entity.HasIndex(e => e.IdMark, "FK_test_mark_Id");

            entity.HasIndex(e => e.IdSubject, "FK_test_subject_Id");

            entity.HasIndex(e => e.IdTeacher, "FK_test_teacher_Id");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("int(11)");
            entity.Property(e => e.IdMark)
                .HasColumnType("int(11)")
                .HasColumnName("Id_Mark");
            entity.Property(e => e.IdQuestion)
                .HasColumnType("int(11)")
                .HasColumnName("Id_Question");
            entity.Property(e => e.IdSubject)
                .HasColumnType("int(11)")
                .HasColumnName("Id_Subject");
            entity.Property(e => e.IdTeacher)
                .HasColumnType("int(11)")
                .HasColumnName("Id_Teacher");
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.IdMarkNavigation).WithMany(p => p.Tests)
                .HasForeignKey(d => d.IdMark)
                .HasConstraintName("FK_test_mark_Id");

            entity.HasOne(d => d.IdQuestionNavigation).WithMany(p => p.Tests)
                .HasForeignKey(d => d.IdQuestion)
                .HasConstraintName("FK_test_question_Id");

            entity.HasOne(d => d.IdSubjectNavigation).WithMany(p => p.Tests)
                .HasForeignKey(d => d.IdSubject)
                .HasConstraintName("FK_test_subject_Id");

            entity.HasOne(d => d.IdTeacherNavigation).WithMany(p => p.Tests)
                .HasForeignKey(d => d.IdTeacher)
                .HasConstraintName("FK_test_teacher_Id");
        });

        modelBuilder.Entity<Testcrossquestion>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("testcrossquestion");

            entity.HasIndex(e => e.IdQuestion, "FK_testcrossquestion_question_Id");

            entity.HasIndex(e => e.IdTest, "FK_testcrossquestion_test_Id");

            entity.Property(e => e.IdQuestion)
                .HasColumnType("int(11)")
                .HasColumnName("Id_Question");
            entity.Property(e => e.IdTest)
                .HasColumnType("int(11)")
                .HasColumnName("Id_Test");
        });

        modelBuilder.Entity<Testcrossteacher>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("testcrossteacher");

            entity.HasIndex(e => e.IdStudent, "FK_testcrossteacher_student_ID");

            entity.HasIndex(e => e.IdTest, "FK_testcrossteacher_test_Id");

            entity.Property(e => e.IdStudent)
                .HasColumnType("int(11)")
                .HasColumnName("Id_Student");
            entity.Property(e => e.IdTest)
                .HasColumnType("int(11)")
                .HasColumnName("Id_Test");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
