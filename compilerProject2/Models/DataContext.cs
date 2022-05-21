using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace compilerProject2.Models
{
    public partial class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CharsId> CharsIds { get; set; } = null!;
        public virtual DbSet<KeyWord> KeyWords { get; set; } = null!;
        public virtual DbSet<SymbolsHasSameId> SymbolsHasSameIds { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=YASMEEN\\SQLEXPRESS;Initial Catalog=compilerProject;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CharsId>(entity =>
            {
                entity.HasKey(e => e.CharId)
                    .HasName("PK__CharsId__82E7F8DF2AB9E0C9");

                entity.ToTable("CharsId");

                entity.Property(e => e.CharId).HasColumnName("charID");

                entity.Property(e => e.Char)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("char");
            });

            modelBuilder.Entity<KeyWord>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.KeyWord1)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("keyWord");

                entity.Property(e => e.ReturnToken)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SymbolsHasSameId>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("symbolsHasSameId");

                entity.Property(e => e.Symbol)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("symbol");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
