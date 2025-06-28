using System.ComponentModel.DataAnnotations;
using HotChocolate.Types;
public class Book
{
    public int Id { get; set; }
    
    [GraphQLNonNullType]
    [Required]
    [StringLength(100)]
    public string Title { get; set; } = null!;
    
    [GraphQLNonNullType]
    [Required]
    [StringLength(50)]
    public string Author { get; set; } = null!;
    
    [StringLength(50)]
    public string? Genre { get; set; }
    
    [GraphQLNonNullType]
    [Range(1800, 2100)]
    public int PublishedYear { get; set; }
}

public class BookInput
{
    [Required(ErrorMessage = "Title is required")]
    [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
    public string Title { get; set; } = null!;

    [Required(ErrorMessage = "Author is required")]
    [StringLength(50, ErrorMessage = "Author name cannot exceed 50 characters")]
    public string Author { get; set; } = null!;

    [StringLength(50, ErrorMessage = "Genre cannot exceed 50 characters")]
    public string? Genre { get; set; }

    [Required(ErrorMessage = "Publication year is required")]
    [Range(1800, 2100, ErrorMessage = "Publication year must be between 1800 and 2100")]
    public int PublishedYear { get; set; }
}