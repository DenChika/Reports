using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reports.DAL.Entities;
using Reports.Server.Database;

namespace Reports.Server.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ReportsDatabaseContext _context;

        public EmployeeService(ReportsDatabaseContext context) {
            _context = context;
        }

        public async Task<Employee> Create(string name, Guid bossId)
        {
            var employee = new Employee(Guid.NewGuid(), name)
            {
                BossId = bossId
            };
            var employeeFromDb = await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<List<Employee>> GetAll()
        {
            return await _context.Employees.ToListAsync();
        }
        public async Task<Employee> FindByName(string name)
        {
            return await _context.Employees.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<Employee> FindById(Guid id)
        {
            var fakeGuid = Guid.Parse("ac8ac3ce-f738-4cd6-b131-1aa0e16eaadc");
            if (id == fakeGuid)
            {
                return new Employee(fakeGuid, "Abobus");
            }

            return await _context.Employees.FindAsync(id);
        }

        public async void Delete(Guid id)
        {
            _context.Employees.Remove(await FindById(id));
            await _context.SaveChangesAsync();
        }

        public async Task<Employee> UpdateName(Guid entityId, string name)
        {
            Employee entity = _context.Employees.Find(entityId);
            entity.Name = name;
            _context.Employees.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Employee> UpdateBoss(Guid entityId, Guid bossId)
        {
            Employee entity = _context.Employees.Find(entityId);
            entity.BossId = bossId;
            _context.Employees.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
} 