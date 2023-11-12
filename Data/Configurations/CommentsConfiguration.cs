using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Data.Configurations
{
    public class CommentsConfiguration : IEntityTypeConfiguration<Comments>
    {
        public void Configure(EntityTypeBuilder<Comments> builder)
        {
            builder.ToTable("Comments"); // Đặt tên bảng là "Comments"
            builder.HasKey(c => c.CommentID); // Xác định trường "CommentID" là khóa chính

            builder.Property(c => c.CommentID)
                .IsRequired();

            builder.Property(c => c.PostID)
                .IsRequired();

            builder.Property(c => c.Content)
                .IsRequired()
                .HasMaxLength(500); // Độ dài tối đa cho nội dung

            builder.Property(c => c.TimeComment)
                .IsRequired();

            // Cấu hình liên kết đến bài viết (Post)
            builder.HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostID);

            // Cấu hình liên kết đến người dùng (ManagerUser)
            builder.HasOne(c => c.UserComment)
                .WithMany()
                .HasForeignKey(c => c.UserID);
        }
    }
}
