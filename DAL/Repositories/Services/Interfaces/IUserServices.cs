using DAL.DTO.Req;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Services.Interfaces
{
    public interface IUserServices
    {
        Task<string> Register(ReqRegisterUserDto register);
    }
}
