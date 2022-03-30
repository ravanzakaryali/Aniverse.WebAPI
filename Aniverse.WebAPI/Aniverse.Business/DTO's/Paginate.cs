using System;
using System.Collections.Generic;
using System.Text;

namespace Aniverse.Business.DTO_s
{
    public class Paginate<T>
    {
        public Paginate(List<T> data, int currentPage, int pageCount)
        {
            Data = data;
            CurrentPage = currentPage;
            PageCount = pageCount;
        }
        public List<T> Data { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }
}
