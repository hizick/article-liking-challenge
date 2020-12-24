# article-liking-challenge
code challenge for rock content

In this code, 

### How the client-side, server-side + database are structured

The client side is structured in a way that when user lands on the page, they see the like count and when they click the like button, it prompts them to login (if they are not).

When they do, they can now like/unlike as they wish. Optimistic updates was used on the frontend, so that liking process can appear faster.
This was done with ajax so the page wouldn't reload/refresh when user likes and/or when the page calls the server.

Server side also restricts unidentfied users to use the button.

For database, three tables were created using code first approach. 
Artice, User, Likes. One to many relationship was created between Artcile and Likes, and User and Likes.

```C#
[Table("Article")] //all entity configurations are done with ef core fluent api in the persitence folder
    public class Article
    {
        public int ArticleId { get; set; } 
        public string Title { get; set; }
        public string ArticleText { get; set; }
        public List<Like> Likes { get; set; }
        public Article()
        {
            Likes = new List<Like>();
        }
    }
    
  [Table("User")] //all entity configurations are done with fluent api in the persitence folder
  public class User
  {
      public string UserId { get; set; }
      public List<Like> Likes { get; set; }

      //public string Password { get; set; } //this is not important for this system right now
  }
  
  [Table("Like")] //all entity configurations are done with fluent api in the persitence folder
    public class Like
    {
        public int ArticleId { get; set; }
        public string UserId { get; set; }
        public bool Liked { get; set; }
        
    }
```
```C# //fLUENT API
public class LikeDbContext : DbContext
    {
        public LikeDbContext(DbContextOptions<LikeDbContext> options)
            : base(options){}
        public DbSet<Like> Like { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Article> Article { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Like>(entity =>
            {
                entity.HasKey(e => new { e.ArticleId, e.UserId });
                //this prevents insertion of duplicate records
            });

            modelBuilder.Entity<Article>(entity =>
            {
                entity.HasKey(e => new { e.ArticleId });
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => new {e.UserId });
            });
        }
    }
 ```

The userId and ArticleId which are foreign keys because of the table relationship are also primary keys on the Like table.
This is done with EFCore fluentAPI to prevent duplicate data (i.e. one user liking twice) on the table.

### Restricting abuse of the button.

1. Users can only like article once, increasing the like count and toggling the button to unlike. This goes vice versa for when they unlike

2. The Database and table are structured in a way that users can only like once. There can never be more than one like for a user.

3. Only existing and logged in users can like article.

### Scaling to millions of users and perform without issues

1. The above listed can help with the application scalablity.

2. Asynchronous programming was used throughout the application, so there is highly reduced thread blocks, which could slow general processes down.

3. Optimistic updates was used on the frontend, so that liking process can appear faster.

4. When users unlike, their likes are deleted from the database so as to leave less data on the table for faster query.

## All above can also work for:
##### A million concurrent users clicking the button at the same time
##### A million concurrent users requesting the article's like count at the same time
