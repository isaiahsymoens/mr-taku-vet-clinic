using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace MrTakuVetClinic.Services
{
    public class BreedService
    {
        private readonly IBreedRepository _breedRepository;
        public BreedService(IBreedRepository breedRepository)
        {
            _breedRepository = breedRepository;
        }

        public async Task<IEnumerable<Breed>> GetAllBreedsAsync()
        {
            return await _breedRepository.GetAllAsync();
        }

        public async Task<Breed> GetBreedByIdAsync(int id)
        {
            var breed = await _breedRepository.GetByIdAsync(id);
            if (breed == null)
            {
                throw new ArgumentException("Breed does not exist.");
            }
            return breed;
        }

        public async Task PostBreedAsync(Breed breed)
        {
            await _breedRepository.AddAsync(breed);
        }
    }
}
