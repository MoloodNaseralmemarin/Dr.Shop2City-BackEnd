using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DrShop2City.DataLayer.Entities.Site;

namespace DrShop2City.Infrastructure.Services.Interfaces
{
    public interface ISliderService : IDisposable
    {
        Task<List<Slider>> GetAllSliders();
        Task<List<Slider>> GetActiveSliders();
        Task AddSlider(Slider slider);
        Task UpdateSlider(Slider slider);
        Task<Slider> GetSliderById(long sliderId);
    }
}