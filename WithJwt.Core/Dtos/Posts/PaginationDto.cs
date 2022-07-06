using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithJwt.Core.Dtos.Posts
{
    public class PaginationDto
    {
        public int CurrentPage { get; set; } //başlangıç sayfası(seçili sayfa)

        public int PageSize { get; set; } //bir sayfada kaç değer tutulacağı 

        public int TotalCount { get; set; } //toplam değer(post) sayısı

        public int TotalPages => (int)Math.Ceiling(decimal.Divide(TotalCount, PageSize));

        public bool ShowPrevious => CurrentPage > 1;// önceki sayfa  var mı

        public bool ShowNext => CurrentPage < TotalPages; //sonraki sayfa var mı
    }
}
