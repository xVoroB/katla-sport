using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KatlaSport.DataAccess;
using KatlaSport.DataAccess.EmployeeInfo;
using KatlaSport.Services.Interfaces;
using DbEmployee = KatlaSport.DataAccess.EmployeeInfo.Employee;

namespace KatlaSport.Services.EmployeeManagement
{
    public class EmployeeRepositoryService : IRepository<Employee>
    {
        private IEmployeeContext _context;
        private IUserContext _userContext;

        public EmployeeRepositoryService(IEmployeeContext context, IUserContext userContext)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
        }

        public async Task<Employee> GetEntityAsync(int id)
        {
            var dbEmployee = await _context.Employees.Where(e => e.Id == id).ToArrayAsync();

            if (dbEmployee.Length == 0)
            {
                throw new RequestedResourceNotFoundException();
            }

            return Mapper.Map<DbEmployee, Employee>(dbEmployee[0]);
        }

        public async Task<List<Employee>> GetAllEntitiesAsync()
        {
            var dbEmployees = await _context.Employees.OrderBy(e => e.Id).ToArrayAsync();
            return dbEmployees.Select(h => Mapper.Map<Employee>(h)).ToList();
        }

        public async Task AddEntityAsync(Employee entity)
        {
            var dbEmployee = Mapper.Map<Employee, DbEmployee>(entity);

            _context.Employees.Add(dbEmployee);

            await _context.SaveChangesAsync();
        }

        public async Task RemoveEntityAsync(Employee entity)
        {
            var dbEmployees = await _context.Employees.Where(p => p.Id == entity.Id).ToArrayAsync();
            if (dbEmployees.Length == 0)
            {
                throw new RequestedResourceHasConflictException();
            }

            var dbEmployee = dbEmployees[0];
            _context.Employees.Remove(dbEmployee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEntityAsync(Employee entity)
        {
            var dbEmployees = await _context.Employees.Where(p => p.Id == entity.Id).ToArrayAsync();
            if (dbEmployees.Length == 0)
            {
                throw new RequestedResourceHasConflictException();
            }

            var updateEmployee = Mapper.Map<Employee, UpdateEmployeeRequest>(entity);
            var dbEmployee = dbEmployees[0];
            Mapper.Map(updateEmployee, dbEmployee);

            await _context.SaveChangesAsync();
        }
    }
}
