namespace Drink_Finder_WebApp.Models
{
    public class Page<T>
    {
        public List<T>? items { get; set; }
        public int totalItems { get; set; }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public int totalPages { get; set; }
    }
}
