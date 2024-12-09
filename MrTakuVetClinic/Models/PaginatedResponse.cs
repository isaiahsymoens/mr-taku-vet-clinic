using System;
using System.Collections.Generic;

namespace MrTakuVetClinic.Models
{
    public class PaginatedResponse<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int PageNumber {  get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
        public int? FirstPage => TotalPages > 0 ? 1 : (int?)null;
        public int? LastPage => TotalPages > 0 ? TotalPages : (int?)null;
        public int? NextPage => PageNumber < TotalPages ? PageNumber + 1 : (int?)null;
        public int? PreviousPage => PageNumber > 1 ? PageNumber - 1 : (int?) null;
     
        public PaginatedResponse(IEnumerable<T> data, int pageNumber, int pageSize, int totalItems)
        {
            Data = data;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalItems = totalItems;
        }
    }
}
