using AutoMapper;
using HF6Svende.Application.DTO.Color;
using HF6Svende.Application.DTO.Country;
using HF6Svende.Application.Service_Interfaces;
using HF6Svende.Core.Repository_Interfaces;
using HF6Svende.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HF6Svende.Application.Services
{
    public class ColorService : IColorService
    {
        private readonly IColorRepository _colorRepository;
        private readonly IMapper _mapper;


        public ColorService(IColorRepository colorRepository, IMapper mapper)
        {
            _colorRepository = colorRepository;
            _mapper = mapper;
        }
        public async Task<List<ColorDTO>> GetAllColorsAsync()
        {
            try
            {
                // Get all colors
                var colors = await _colorRepository.GetAllColorsAsync();

                // Mapping back to dto
                return _mapper.Map<List<ColorDTO>>(colors);

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the colors.", ex);
            }
        }
    }
}
