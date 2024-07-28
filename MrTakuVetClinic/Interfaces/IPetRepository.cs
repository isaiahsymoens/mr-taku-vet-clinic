using MrTakuVetClinic.Entities;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Interfaces
{
    public interface IPetRepository : IRepository<Pet>
    {
        Task<Pet> GetPetByIdAsync(int id);
    }
}
