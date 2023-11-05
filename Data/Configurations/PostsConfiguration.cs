using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace DataAccess.Configurations
{
    public class PostsConfiguration : IEntityTypeConfiguration<Posts>
    {
        public void Configure(EntityTypeBuilder<Posts> builder)
        {
            builder.ToTable("Posts"); // Đặt tên bảng là "Posts"
            builder.HasKey(p => p.PostID); // Xác định trường "PostID" là khóa chính

            builder.Property(p => p.PostID)
                .IsRequired();

            builder.Property(p => p.UserID)
                .IsRequired();

            builder.Property(p => p.Content)
                .IsRequired();

            builder.Property(p => p.TimePost)
                .IsRequired();

            // Cấu hình liên kết đến người dùng (FromUser)
            builder.HasOne(p => p.FromUser)
                .WithMany(u => u.Posts) // Tập hợp Posts trong ManagerUser
                .HasForeignKey(p => p.UserID)
            .OnDelete(DeleteBehavior.Restrict);
            // Cấu hình liên kết đến các comment (Comments)
            builder.HasMany(p => p.Comments)
                .WithOne(c => c.Post)
                .HasForeignKey(c => c.PostID)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
