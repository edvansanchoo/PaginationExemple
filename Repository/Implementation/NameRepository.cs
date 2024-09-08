using PaginationTest.Repository.Interface;

namespace PaginationTest.Repository.Implementation
{
    public class NameRepository : INameRepository
    {
        private readonly List<string> _names;

        public NameRepository()
        {
            _names = new List<string>();
            for (int i = 0; i < 1000; i++)
            {
                _names.Add($"Nome {i.ToString()}");
            }
        }

        public IEnumerable<string> GetNames(int page, int pageSize)
        {
            return _names
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public IEnumerable<string> SearchNames(string searchTerm, int page, int pageSize)
        {
            return _names
                .Where(n => n.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public IEnumerable<string> SearchAllNames(string searchTerm)
        {
            return _names
                .Where(n => n.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public int Count()
        {
            return _names.Count();
        }
    }
}
