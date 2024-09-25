﻿using DAL.DTO.Req;
using DAL.DTO.Res;
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
    public class LoanServices : ILoanServices
    {

        private readonly PeerlendingContext _peerlendingContext;

        public LoanServices(PeerlendingContext peerlendingContext)
        {
            _peerlendingContext = peerlendingContext;
        }
        public async Task<string> CreateLoan(ReqLoanDto loan)
        {
            var newLoan = new MstLoans
            {
                BorrowerId = loan.BorrowerId,
                Amount = loan.Amount,
                InterestRate = loan.InterestRate,
                Duration = loan.Duration,

            };

            await _peerlendingContext.MstLoans.AddAsync(newLoan);
            await _peerlendingContext.SaveChangesAsync();


            return newLoan.BorrowerId;
        }

        public async Task<List<ResListLoanDto>> LoanList(string status)
        {
           
            var loans = await _peerlendingContext.MstLoans.
                Include(l => l.User).
                Select(x => new ResListLoanDto
            {
                LoanId = x.Id,
                BorrowerName = x.User.Name,
                Amount = x.Amount,
                InterestRate = x.InterestRate,
                Duration = x.Duration,
                Status = x.Status,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt
            }).OrderBy(x => x.CreatedAt)
            .Where(x => string.IsNullOrEmpty(status) || x.Status == status)
            .ToListAsync();

        
            return loans;


        }

        public async Task<string> UpdateStatusLoan(ReqLoanStatusDto loan, string id)
        {
            var borrower = _peerlendingContext.MstLoans.FirstOrDefault(x => x.Id == id);

            if (borrower == null)
            {
                throw new Exception("Loan id not found!");
            }

            borrower.Status = loan.Status;
            borrower.UpdatedAt = DateTime.UtcNow;

            _peerlendingContext.MstLoans.Update(borrower);
            _peerlendingContext.SaveChanges();

            return id;

        }
    }
}
