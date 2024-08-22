using HF6Svende.Application.DTO.Color;
using HF6Svende.Application.DTO.Country;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HF6Svende.Application.Service_Interfaces
{
    public interface IColorService
    {
        Task<List<ColorDTO>> GetAllColorsAsync();
    }
}
