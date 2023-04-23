namespace DrShop2City.Infrastructure.DTOs.Paging
{
    public class BasePaging
    {
        public BasePaging()
        {
            //default
            PageId = 1;
            TakeEntity = 10;
        }

        //کدوم PageId
        public int PageId { get; set; }

        //تعداد صفحات
        public int PageCount { get; set; }

        //کدوم صفحه
        public int ActivePage { get; set; }

        //نقطه شروع
        public int StartPage { get; set; }

        //نقطه پایان
        public int EndPage { get; set; }

        //تعداد نمایش در صفحه
        public int TakeEntity { get; set; }
        //تعداد پرس در صفحه
        public int SkipEntity { get; set; }
    }
}
