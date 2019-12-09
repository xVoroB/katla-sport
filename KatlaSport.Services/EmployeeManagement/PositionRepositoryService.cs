using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KatlaSport.DataAccess;
using KatlaSport.DataAccess.EmployeeInfo;
using KatlaSport.Services.Interfaces;
using DbPosition = KatlaSport.DataAccess.EmployeeInfo.Position;

namespace KatlaSport.Services.EmployeeManagement
{
    public class PositionRepositoryService : IRepository<Position>
    {
        private IEmployeeContext _context;
        private IUserContext _userContext;

        public PositionRepositoryService(IEmployeeContext context, IUserContext userContext)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
        }

        public async Task<List<Position>> GetAllEntitiesAsync()
        {
            var dbPositions = await _context.Positions.OrderBy(e => e.Id).ToArrayAsync();
            return dbPositions.Select(p => Mapper.Map<Position>(p)).ToList();
        }

        public async Task<Position> GetEntityAsync(int id)
        {
            var dbPositions = await _context.Positions.Where(e => e.Id == id).ToArrayAsync();

            if (dbPositions.Length == 0)
            {
                throw new RequestedResourceNotFoundException();
            }

            return Mapper.Map<DbPosition, Position>(dbPositions[0]);
        }

        public async Task AddEntityAsync(Position entity)
        {
            var updatePosition = Mapper.Map<Position, UpdatePositionRequest>(entity);
            var dbPosition = Mapper.Map<UpdatePositionRequest, DbPosition>(updatePosition);
            _context.Positions.Add(dbPosition);

            await _context.SaveChangesAsync();
        }

        public async Task RemoveEntityAsync(Position entity)
        {
            var dbPositions = await _context.Positions.Where(p => p.Id == entity.Id).ToArrayAsync();
            if (dbPositions.Length == 0)
            {
                throw new RequestedResourceHasConflictException();
            }

            var dbPosition = dbPositions[0];
            _context.Positions.Remove(dbPosition);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEntityAsync(Position entity)
        {
            var dbPositions = await _context.Positions.Where(p => p.Id == entity.Id).ToArrayAsync();
            if (dbPositions.Length == 0)
            {
                throw new RequestedResourceHasConflictException();
            }

            var updatePosition = Mapper.Map<Position, UpdatePositionRequest>(entity);
            var dbPosition = dbPositions[0];
            Mapper.Map(updatePosition, dbPosition);

            await _context.SaveChangesAsync();
        }
    }
}
