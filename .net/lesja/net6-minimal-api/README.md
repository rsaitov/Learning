# .NET 6 Minimal API by Les Jackson

Building of a .NET 6 minimal API to contrast and compare it with a MVC API to further understand the differences and similarities between them.

[.net core 6] [web api] [mvc] [docker]

**Minimal APIs** are architected to create HTTP APIs with minimal dependencies. They are ideal for microservices and apps that want to include only the minimal files, features and dependencies in ASP.NET Core. 
(Microsoft Docs)

Minimal APIs don't have a controller (or use the MVC framework as a whole...)
- Don't support model validation.
- Don't support for JSONPatch.
- Don't support filters.
- Don't support custom model binding (support for IModelBinder).
**MVC**
- Software desing patter.
- Split program logic into three (interconnected) elements.
- Widely adopter in web development frameworks: Ruby on Rails, Spring (Java), Django (Python), ASP.NET MVC.

## Some dotnet cli commands
```
// create user secret UserId
dotnet user-secrets set "UserId" "sa"

// create user secret Password
dotnet user-secrets set "Password" "pwd"

// add initial migration
dotnet ef migrations add initialmigration

// update database
dotnet ef database update
```



## Links

YouTube - Full Course: https://www.youtube.com/watch?v=5YB49OEmbbE

The author's origin repository: https://github.com/binarythistle/05E03---Minimal-APIs

Minimal API Tutorial: https://docs.microsoft.com/en-us/aspnet/core/tutorials/min-web-api?view=aspnetcore-6.0&tabs=visual-studio