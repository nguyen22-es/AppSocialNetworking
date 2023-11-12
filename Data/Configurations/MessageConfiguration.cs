using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Data.Configurations
{
    public class MessageConfiguration : IEntityTypeConfiguration<Messages>
    {
        public void Configure(EntityTypeBuilder<Messages> builder)
        {
            builder.ToTable("Messages"); // Đặt tên bảng là "Messages"
            builder.HasKey(s => s.MessageID); // Xác định trường "MessageID" là khóa chính

            builder.Property(s => s.MessageID)
                .IsRequired();

            builder.Property(s => s.SenderUserID)
                .IsRequired();

            builder.Property(s => s.ReceiverUserID)
                .IsRequired();

            builder.Property(s => s.Content)
                .IsRequired()
                .HasMaxLength(500);

            // Cấu hình liên kết đến người gửi (FromUser)
            builder.HasOne(s => s.FromUser)
                .WithMany()
                .HasForeignKey(s => s.SenderUserID)
                .OnDelete(DeleteBehavior.Restrict);

            // Cấu hình liên kết đến người nhận (ToUser)
            builder.HasOne(s => s.ToUser)
                .WithMany()
                .HasForeignKey(s => s.ReceiverUserID)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
