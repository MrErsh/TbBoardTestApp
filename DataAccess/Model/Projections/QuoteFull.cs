using System;

namespace DataAccess.Model.Projections
{
    public class QuoteFull
    {
        public int RowId { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
