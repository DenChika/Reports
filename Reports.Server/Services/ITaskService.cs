using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reports.DAL.Conditions;
using Task = Reports.DAL.Entities.Task;

namespace Reports.Server.Services
{
    public interface ITaskService
    {
        public void Changing(Guid taskId, Guid employeeId, DateTime time);
        public Task<List<Task>> GetAll();

        public Task<Task> FindById(Guid id);

        public Task FindByCreateDate(DateTime createDate);

        public Task FindByLastUpdateDate(DateTime updateDate);

        public List<Task> FindByExecutor(Guid executorId);

        public List<Task> FindUpdateByEmployee(Guid id);

        public List<Task> FindBySubordinates(Guid id);

        public Task<Task> Create(Guid executorId);

        public void ChangeCondition(Guid taskId, Guid employeeId, Condition condition);

        public void AddComment(Guid taskId, Guid employeeId, string comment);
    }
}