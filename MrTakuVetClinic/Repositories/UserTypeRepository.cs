﻿using MrTakuVetClinic.Data;
using MrTakuVetClinic.Entities;
using MrTakuVetClinic.Interfaces;

namespace MrTakuVetClinic.Repositories
{
    public class UserTypeRepository : Repository<UserType>, IUserTypeRepository
    {
        public UserTypeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
