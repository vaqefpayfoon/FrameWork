using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Linq.Expressions;

namespace KavoshFrameWorkCore.Extensions
{
    public static class CollationExtentions
    {
        public static void Collation<T>(
         this EntityTypeBuilder<T> entityTypeBuilder, Expression<Func<T, object>> col, string collation)
             where T : class
        {
            string maxLength = entityTypeBuilder.Property(col).Metadata.GetMaxLength()?.ToString() ?? "MAX";

            string columnType = entityTypeBuilder.Property(col).Metadata.GetConfiguredColumnType() ?? $"NVARCHAR({ maxLength})";

            string columnCollation = $"{ columnType} COLLATE {collation}";

            entityTypeBuilder.Property(col).HasColumnType(columnCollation);
        }
    }
}
