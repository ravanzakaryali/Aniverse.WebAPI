using Aniverse.Core.Entites;
using Aniverse.Core.Entities;
using Aniverse.Data.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Aniverse.Data.DAL
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Animal> Animal { get; set; }
        public DbSet<AnimalCategory> AnimalCategory { get; set; }
        public DbSet<AnimalGroup> AnimalGroups { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Product> PostProducts { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Story> Story { get; set; }
        public DbSet<UserFriend> UserFriends { get; set; }
        public DbSet<UserSM> UserSM { get; set; }
        public DbSet<AnimalFollow> AnimalFollows { get; set; }
        public DbSet<SavePost> SavePosts { get; set; }
        public DbSet<PageFollow> PageFollows { get; set; }
        public DbSet<Page> Page { get; set; } 
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AnimalCategoryConfiguration());
            builder.ApplyConfiguration(new AnimalConfiguration());
            builder.ApplyConfiguration(new AppUserConfiguration());
            builder.ApplyConfiguration(new PictureConfiguration());
            builder.ApplyConfiguration(new StoryConfigration());
            builder.ApplyConfiguration(new PostConfiguration());
            builder.ApplyConfiguration(new CommentConfiguration());
            builder.ApplyConfiguration(new AnimalFollowConfiguration());
            builder.ApplyConfiguration(new SavePostConfiguration());
            builder.ApplyConfiguration(new UserFriendConfiguration());
            builder.ApplyConfiguration(new PageConfiguration());
            builder.ApplyConfiguration(new PageFollowConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
