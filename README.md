# BookStore Web API
Solution is based on Onion Architecture with layers:
* [BookStore.Domain](https://github.com/PavloPustelnyk/BookStoreApi/tree/master/BookStore.Domain) - It is the center part of the architecture. It holds all application domain models.
* [BookStore.Infrastructure.ApiContext](https://github.com/PavloPustelnyk/BookStoreApi/tree/master/BookStore.Infrastructure.ApiContext) - Project with BookStoreDbContext class and database migrations.
* [BookStore.Infrastructure.Services](https://github.com/PavloPustelnyk/BookStoreApi/tree/master/BookStore.Infrastructure.Services) - The layer holds interfaces and services for outer API layer.
* [BookStore.API](https://github.com/PavloPustelnyk/BookStoreApi/tree/master/BookStore) - Outer API layer with controllers.
