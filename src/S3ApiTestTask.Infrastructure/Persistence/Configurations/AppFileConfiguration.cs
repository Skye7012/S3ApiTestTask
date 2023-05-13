using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using S3ApiTestTask.Domain.Entities;
using S3ApiTestTask.Infrastructure.Persistence.Common;

namespace S3ApiTestTask.Infrastructure.Persistence.Configurations;

/// <summary>
/// Конфигурация для <see cref="AppFile"/>
/// </summary>
public class AppFileConfiguration : EntityBaseConfiguration<AppFile>
{
	/// <inheritdoc/>
	public override void ConfigureChild(EntityTypeBuilder<AppFile> builder)
	{
		builder.HasComment("Файлы");

		builder.Property(e => e.Name);
		builder.Property(e => e.DeletedOn);

		builder.HasIndex(e => e.Name)
			.IsUnique();
	}
}
