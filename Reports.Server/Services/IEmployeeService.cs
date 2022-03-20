using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reports.DAL.Entities;

namespace Reports.Server.Services
{
    public interface IEmployeeService
    {
        Task<Employee> Create(string name, Guid bossId);

        public Task<List<Employee>> GetAll();

        Task<Employee> FindByName(string name);

        Task<Employee> FindById(Guid id);

        void Delete(Guid id);

        Task<Employee> UpdateName(Guid entityId, string name);

        Task<Employee> UpdateBoss(Guid entityId, Guid bossId);

    }
}