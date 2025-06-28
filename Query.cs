public class Query
{
    // Get all books with filtering/sorting
    [UseFiltering]
    [UseSorting]
    public IEnumerable<Book> GetBooks([Service] IBookRepository repository) => 
        repository.GetAllBooks();

    // Get single book with DataLoader optimization
    public async Task<Book?> GetBookById(
        int id, 
        BookBatchDataLoader loader,
        CancellationToken ct) => 
        await loader.LoadAsync(id, ct);
}
