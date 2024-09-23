using DAL.DTO.Req;
using DAL.Models;
using DAL.Repositories.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Services
{

    public class UserServices : IUserServices
    {

        private readonly PeerlendingContext _context;


        public UserServices(PeerlendingContext context)
        {
            _context = context;
        }


        public async Task<string> Register(ReqRegisterUserDto register)
        {
            var isAnyEmail = await _context.MstUsers.SingleOrDefaultAsync(e => e.Email == register.Email);

            if (isAnyEmail != null)
            {
                throw new Exception("Email already used");
            }

            var newUser = new MstUser
            {
                Name = register.Name,
                Email = register.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(register.Password),
                Role = register.Role,
                Balance = register.Balance

            };

            await _context.MstUsers.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return newUser.Name;
        }
    }
}
