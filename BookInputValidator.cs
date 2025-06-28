using FluentValidation;

public class BookInputValidator : AbstractValidator<BookInput>
{
    public BookInputValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(100).WithMessage("Title cannot exceed 100 characters");
            
        RuleFor(x => x.Author)
            .NotEmpty().WithMessage("Author is required")
            .MaximumLength(50).WithMessage("Author name cannot exceed 50 characters");
            
        RuleFor(x => x.PublishedYear)
            .InclusiveBetween(1800, DateTime.Now.Year)
            .WithMessage($"Published year must be between 1800 and {DateTime.Now.Year}");
            
        RuleFor(x => x.Genre)
            .MaximumLength(50).WithMessage("Genre cannot exceed 50 characters")
            .When(x => !string.IsNullOrEmpty(x.Genre));
    }
}