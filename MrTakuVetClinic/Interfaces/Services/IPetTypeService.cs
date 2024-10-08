﻿using MrTakuVetClinic.DTOs.PetType;
using MrTakuVetClinic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Interfaces.Services
{
    public interface IPetTypeService
    {
        Task<ApiResponse<IEnumerable<PetTypeDto>>> GetAllPetTypesAsync();
        Task<ApiResponse<PetTypeDto>> GetPetTypeByIdAsync(int id);
        Task<ApiResponse<PetTypeDto>> PostPetTypeAsync(PetTypePostDto petTypePostDto);
        Task<ApiResponse<PetTypeDto>> UpdatePetTypeAsync(int id, PetTypeUpdateDto petTypeUpdateDto);
        Task<ApiResponse<PetTypeDto>> DeletePetTypeAsync(int id);
    }
}
