
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace DataAccess.Configurations
{
    public class FriendshipsConfiguration : IEntityTypeConfiguration<Friendships>
    {
        public void Configure(EntityTypeBuilder<Friendships> builder)
        {
            builder.ToTable("Friendships"); // Đặt tên bảng là "Friendships"
            builder.HasKey(f => f.FriendshipID); // Xác định trường "FriendshipID" là khóa chính

            builder.Property(f => f.FriendshipID)
                .IsRequired();

            builder.Property(f => f.UserID1)
                .IsRequired();

            builder.Property(f => f.UserID2)
                .IsRequired();

            builder.Property(f => f.Status)
                .IsRequired();

            // Cấu hình liên kết đến người dùng User1 (ManagerUser)
            builder.HasOne(f => f.User)
       .WithMany()
       .HasForeignKey(f => f.UserID1)
       .OnDelete(DeleteBehavior.Restrict); // Cấu hình xóa dữ liệu liên quan khi xóa Friendship

            // Cấu hình liên kết đến người dùng User2 (ManagerUser)
            builder.HasOne(f => f.User)
      .WithMany()
      .HasForeignKey(f => f.UserID2)
      .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
