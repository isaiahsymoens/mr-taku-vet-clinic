using Microsoft.EntityFrameworkCore;
using MrTakuVetClinic.Data;
using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Interfaces.Repositories;
using System.Threading.Tasks;

namespace MrTakuVetClinic.Repositories
{
    public class UserTypeRepository : Repository<UserType>, IUserTypeRepository
    {
        public UserTypeRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> IsTypeNameExits(string typeName)
        {
            return await _context.UserTypes.AnyAsync(u => u.TypeName.ToLower().Trim() == typeName.ToLower().Trim());
        }
    }
}
