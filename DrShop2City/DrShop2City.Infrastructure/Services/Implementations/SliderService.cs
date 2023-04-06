using DrShop2City.Infrastructure.Services.Interfaces;
using DrShop2City.DataLayer.Entities.Site;
using DrShop2City.DataLayer.Repository;
using Microsoft.EntityFrameworkCore;

namespace DrShop2City.Infrastructure.Services.Implementations
{
    public class SliderService : ISliderService
    {
        #region constructor

        private readonly IGenericRepository<Slider> _sliderRepository;

        public SliderService(IGenericRepository<Slider> sliderRepository)
        {
            _sliderRepository = sliderRepository;
        }

        #endregion

        #region slider

        public async Task<List<Slider?>> GetAllSliders()
        {
            return await _sliderRepository.GetEntitiesQuery().ToListAsync();
        }

        public async Task<List<Slider>> GetActiveSliders()
        {
            return await _sliderRepository.GetEntitiesQuery().Where(s => s != null && !s.IsDelete).ToListAsync();
        }

        public async Task AddSlider(Slider? slider)
        {
            await _sliderRepository.AddEntity(slider);
            await _sliderRepository.SaveChanges();
        }

        public async Task UpdateSlider(Slider? slider)
        {
            _sliderRepository.UpdateEntity(slider);
            await _sliderRepository.SaveChanges();
        }

        public async Task<Slider?> GetSliderById(long sliderId)
        {
            return await _sliderRepository.GetEntityById(sliderId);
        }

        #endregion

        #region dispose

        public void Dispose()
        {
            _sliderRepository?.Dispose();
        }

        #endregion
    }
}
