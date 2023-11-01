using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.Domain.Helpers
{
    public class PaginatedList<T> where T : class
    {
        public PaginatedList() =>
            (Items, CurrentPage, TotalPages) = (new(), 1, 1);
        public PaginatedList(List<T> items, int currentPage, int totalPages) =>
            (Items, CurrentPage, TotalPages) = (items, currentPage, totalPages);

        public List<T> Items { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
