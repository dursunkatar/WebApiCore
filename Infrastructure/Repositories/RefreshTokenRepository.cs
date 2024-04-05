using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class RefreshTokenRepository : Repository<RefreshToken, ApplicationDbContext>, IRefreshRepository
    {
        public RefreshTokenRepository(ApplicationDbContext context) : base(context)
        {
        }
    }

}
