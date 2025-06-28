using FluentValidation;
using HotChocolate.AspNetCore;
//using HotChocolate.AspNetCore.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddDataLoader<BookBatchDataLoader>()
    .AddFiltering()
    .AddSorting()
   // .AddFluentValidation() // Enable automatic validation
    .AddErrorFilter(error => 
    {
        if (error.Exception is ValidationException)
        {
            return error.WithMessage("Validation error")
                         .WithCode("VALIDATION_FAILED")
                         .RemoveException();
        }
        return error;
    });

// Register repository
builder.Services.AddSingleton<IBookRepository, BookRepository>();

// Register validators
builder.Services.AddScoped<IValidator<BookInput>, BookInputValidator>();

var app = builder.Build();

// Configure middleware pipeline
app.UseRouting();

// Add root endpoint
app.MapGet("/", () => Results.Redirect("/graphql-ui"));

// Handle all unmatched routes
app.MapFallback(() => "Book API is running. Use /graphql for GraphQL endpoint");

// Enable GraphQL Studio
if (app.Environment.IsDevelopment())
{
    // Simplified version
    app.MapBananaCakePop("/graphql-ui");
}

// GraphQL endpoint
app.MapGraphQL();

app.Run();