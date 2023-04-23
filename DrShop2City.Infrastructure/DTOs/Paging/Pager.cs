namespace DrShop2City.Infrastructure.DTOs.Paging
{
    public class Pager
    {
        //متد
                                                       //active page
        public static BasePaging Build(int pageCount, int pageNumber, int take)
        {
            if (pageNumber <= 1) pageNumber = 1;

            return new BasePaging
            {
                ActivePage = pageNumber,
                PageCount = pageCount,
                PageId = pageNumber,
                TakeEntity = take,
                SkipEntity = (pageNumber - 1) * take,
                //اینجا میخوام سه تا صفحه قبل نشون بده سه تا بعد
                StartPage = pageNumber - 3 <= 0 ? 1 : pageNumber - 3,
                EndPage = pageNumber + 3 > pageCount ? pageCount : pageNumber + 3
            };
        }
    }
}