
public class BookBatchDataLoader : BatchDataLoader<int, Book>
{
    private readonly IBookRepository _repository;

    public BookBatchDataLoader(
        IBatchScheduler scheduler,
        IBookRepository repository)
        : base(scheduler)
    {
        _repository = repository;
    }

    protected override async Task<IReadOnlyDictionary<int, Book>> LoadBatchAsync(
        IReadOnlyList<int> keys, 
        CancellationToken ct)
    {
        var books = await _repository.GetBooksByIds(keys);
        return books.ToDictionary(b => b.Id);
    }
}

