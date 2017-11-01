using System;

namespace DataAccess.Model
{
    public class Quote
    {
        public int RowId { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public Guid CategoryId { get; set; }
    }
}