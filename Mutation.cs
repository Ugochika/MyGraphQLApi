using FluentValidation;
using HotChocolate;

public class Mutation
{
    public Book AddBook(
        BookInput input,
        [Service] IBookRepository repository,
        [Service] IValidator<BookInput> validator) // Inject validator
    {
        // Validate input
        var validationResult = validator.Validate(input);
        if (!validationResult.IsValid)
        {
            throw new GraphQLException(validationResult.Errors
                .Select(e => ErrorBuilder.New()
                    .SetMessage(e.ErrorMessage)
                    .SetCode("VALIDATION_ERROR")
                    .Build()));
        }
        
        var book = new Book
        {
            Title = input.Title,
            Author = input.Author,
            Genre = input.Genre ?? "Uncategorized",
            PublishedYear = input.PublishedYear
        };
        
        return repository.AddBook(book);
    }

    public Book? UpdateBook(
        int id,
        BookInput input,
        [Service] IBookRepository repository,
        [Service] IValidator<BookInput> validator) // Inject validator
    {
        // Validate input
        var validationResult = validator.Validate(input);
        if (!validationResult.IsValid)
        {
            throw new GraphQLException(validationResult.Errors
                .Select(e => ErrorBuilder.New()
                    .SetMessage(e.ErrorMessage)
                    .SetCode("VALIDATION_ERROR")
                    .Build()));
        }
        
        var book = repository.GetBookById(id);
        if (book == null) return null;
        
        book.Title = input.Title;
        book.Author = input.Author;
        book.Genre = input.Genre ?? book.Genre;
        book.PublishedYear = input.PublishedYear;
        
        return repository.UpdateBook(book);
    }

    public bool DeleteBook(
        int id,
        [Service] IBookRepository repository) => 
        repository.DeleteBook(id);
}