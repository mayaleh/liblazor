# Server side:
1. New Controllers respect Core.Identity

    - BookController Save data - Recieve Book containig Author, then: If Author does not exist, create new author. If Book does not exist, create new Book with reference to Auhtor. Then, create reference between User and Book (User has Book == Book has User) If reference does not exist.
    - AuthorController: new controller with actions Public: (GetOne, GetAll...). Protected: (Adding). How about User <=> Author ?
    - Registration for new users later...

2. New Models

    - Complete BookModel (save, delete)
    - New AuthorModel. CRU actions

3. Service for converting/translate/transforming server data for client (for client use Shared to public Entity - like DB View)

# Client side:

1. 