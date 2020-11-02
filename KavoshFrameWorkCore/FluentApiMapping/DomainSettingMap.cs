using Microsoft.EntityFrameworkCore.Metadata.Builders;
using KavoshFrameWorkCore.Models;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;

namespace KavoshFrameWorkCore.FluentApiMapping
{
    public class DomainSettingMap : KavoshFrameWorkCommon.Extensions.DbEntityConfiguration<DomainSetting>
    {
        public override void Configure(EntityTypeBuilder<DomainSetting> t)
        {
            t.ToTable("DomainSetting");
            //t.HasKey(x => x.DomainID);
            t.HasKey(x => x.Id);
            t.Property(x => x.Status);
            t.Property(x => x.Type);
            t.Property(x => x.Server);
            t.Property(x => x.UserName);
            t.Property(x => x.Password);
            t.Property(x => x.Description);
            t.Property(x => x.Title);
            t.Property(x => x.Type);
            t.Property(x => x.TypeName);
            t.Property(x => x.IsActive);

        }
    }

}