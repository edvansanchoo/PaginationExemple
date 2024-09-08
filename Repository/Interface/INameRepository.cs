namespace PaginationTest.Repository.Interface
{
    public interface INameRepository
    {
        IEnumerable<string> GetNames(int page, int pageSize);
        IEnumerable<string> SearchNames(string searchTerm, int page, int pageSize);
        IEnumerable<string> SearchAllNames(string searchTerm);
        int Count();
    }
}
