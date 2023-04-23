using DrShop2City.Infrastructure.Services.Interfaces;
using DrShop2City.Infrastructure.Utilities.Common;
using Microsoft.AspNetCore.Mvc;

namespace DrShop2City.WebApi.Controllers
{
    public class SliderController : SiteBaseController
    {
        #region constructor

        private ISliderService sliderService;

        public SliderController(ISliderService sliderService)
        {
            this.sliderService = sliderService;
        }

        #endregion

        #region all active sliders
        [HttpGet("GetActiveSliders")]
        public async Task<IActionResult> GetActiveSliders()
        {
            Thread.Sleep(4000);
            var sliders = await sliderService.GetActiveSliders();

            return JsonResponseStatus.Success(sliders);
        }

        #endregion
    }
}