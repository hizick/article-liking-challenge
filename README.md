# article-liking-challenge
code challenge for rock content

In this code, 

### How the client-side, server-side + database are structured

The client side is structured in a way that when user lands on the page, they see the like count and when they click the like button, it prompts them to login (if they are not).

When they do, they can now like/unlike as they wish. Optimistic updates was used on the frontend, so that liking process can appear faster.

Server side also restricts unidentfied users to use the button.

For database, three tables were created using code first approach. 
Artice, User, Likes. One to many relationship was created between Artcile and Likes, and User and Likes.

### Restricting abuse of the button.

1. Users can only like article once, increasing the like count and toggling the button to unlike. This goes vice versa for when they unlike

2. The Database and table are structured in a way that users can only like once. There can never be more than one like for a user.

3. Only existing and logged in users can like article.

### Scaling to millions of users and perform without issues

1. The above listed can help with the application scalablity.

2. Asynchronous programming was used throughout the application, so there is highly reduced thread blocks, which could slow general processes down.

3. Optimistic updates was used on the frontend, so that liking process can appear faster.

4. When users unlike, their likes are deleted from the database so as to leave less data on the table for faster query.

## All above work for:
### A million concurrent users clicking the button at the same time
### A million concurrent users requesting the article's like count at the same time
