# article-liking-challenge
code challenge for rock content

In this code, 

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
