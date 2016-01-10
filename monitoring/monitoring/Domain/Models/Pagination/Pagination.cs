namespace monitoring.Domain.Models.Pagination
{
    public class Pagination
    {
        public int PageSize { get; private set; }
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public long TotalRegisters { get; private set; }

        public Pagination SetRequestData(int pageSize, int currentPage)
        {
            this.PageSize = pageSize;
            this.CurrentPage = currentPage;

            return this;
        }

        public Pagination SetResponseData(long totalRegisters)
        {
            this.TotalRegisters = totalRegisters;

            decimal totalPages = ((decimal)totalRegisters) / this.PageSize;

            if (totalPages > ((int)totalPages))
                totalPages = ((int)totalPages) + 1;

            this.TotalPages = (int)totalPages;

            return this;
        }
    }
}
