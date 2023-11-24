using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sefaz.ECPF.Core.Aggregates.Empresas;
using Microsoft.EntityFrameworkCore;
using Sefaz.ECPF.Infrasctruct.Mappings.Base;

namespace Sefaz.ECPF.Infrasctruct.Mappings
{
    internal class AtividadeMap : Mapping<Atividade>
    {
        public override void Configure(EntityTypeBuilder<Atividade> builder)
        {
            base.Configure(builder);
            builder.ToTable("TB_TASKS");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("CD_TASKS");

            builder.Property(a => a.Title)
                .HasColumnName("NM_TITLE");

            builder.Property(a => a.Description)
                .HasColumnName("DS_TASK");

            builder.Property(a => a.Completed)
                .HasColumnName("FL_TASK_COMPLETED");

            builder.Property(a => a.InsertionDate)
                .HasColumnName("DT_TASK_CREATION");

        }
    }
}
