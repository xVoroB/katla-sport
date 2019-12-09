﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KatlaSport.DataAccess;
using KatlaSport.DataAccess.EmployeeInfo;
using KatlaSport.Services.Interfaces;
using DbCV = KatlaSport.DataAccess.EmployeeInfo.EmployeeCV;

namespace KatlaSport.Services.EmployeeManagement
{
    public class EmployeeCVRepositoryService : IRepository<EmployeeCV>
    {
        private IEmployeeContext _context;
        private IUserContext _userContext;

        public EmployeeCVRepositoryService(IEmployeeContext context, IUserContext userContext)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
        }

        public async Task AddEntityAsync(EmployeeCV entity)
        {
            var updateCV = Mapper.Map<EmployeeCV, UpdateEmployeeCVRequest>(entity);
            var dbCV = Mapper.Map<UpdateEmployeeCVRequest, DbCV>(updateCV);

            _context.EmployeeCVs.Add(dbCV);

            await _context.SaveChangesAsync();
        }

        public async Task<List<EmployeeCV>> GetAllEntitiesAsync()
        {
            var dbCVs = await _context.EmployeeCVs.OrderBy(p => p.Id).ToArrayAsync();

            return dbCVs.Select(p => Mapper.Map<EmployeeCV>(p)).ToList();
        }

        public async Task<EmployeeCV> GetEntityAsync(int id)
        {
            var dbCVs = await _context.EmployeeCVs.Where(p => p.Id == id).ToArrayAsync();

            if (dbCVs.Length == 0)
            {
                throw new RequestedResourceNotFoundException();
            }

            return Mapper.Map<DbCV, EmployeeCV>(dbCVs[0]);
        }

        public async Task RemoveEntityAsync(EmployeeCV entity)
        {
            var dbCVs = await _context.EmployeeCVs.Where(p => p.Id == entity.Id).ToArrayAsync();
            if (dbCVs.Length == 0)
            {
                throw new RequestedResourceHasConflictException();
            }

            var dbCV = dbCVs[0];
            _context.EmployeeCVs.Remove(dbCV);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEntityAsync(EmployeeCV entity)
        {
            var dbCVs = await _context.EmployeeCVs.Where(p => p.Id == entity.Id).ToArrayAsync();
            if (dbCVs.Length == 0)
            {
                throw new RequestedResourceHasConflictException();
            }

            var updateCV = Mapper.Map<EmployeeCV, UpdateEmployeeCVRequest>(entity);
            var dbCV = dbCVs[0];
            Mapper.Map(updateCV, dbCV);

            await _context.SaveChangesAsync();
        }
    }
}
