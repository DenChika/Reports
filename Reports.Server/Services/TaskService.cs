using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using Reports.DAL.Conditions;
using Reports.DAL.Entities;
using Reports.DAL.TasksChange;
using Reports.Server.Database;
using Task = Reports.DAL.Entities.Task;

namespace Reports.Server.Services
{
    public class TaskService : ITaskService
    {
        private readonly ReportsDatabaseContext _context;

        public TaskService(ReportsDatabaseContext context) 
        {
            _context = context;
        }

        public void Changing(Guid taskId, Guid employeeId, DateTime time)
        {
            _context.TasksChanges.Add(new TaskChange(taskId, employeeId, time));
        }
        public async Task<List<Task>> GetAll()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<Task> FindById(Guid id)
        {
            return await _context.Tasks.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public Task FindByCreateDate(DateTime createDate)
        {
            Guid taskId = _context.TasksChanges.FirstOrDefault(x => x.ChangeTime == createDate).TaskId;
            return _context.Tasks.Find(taskId);
        }

        public Task FindByLastUpdateDate(DateTime updateDate)
        {
            Guid taskId = _context.TasksChanges.LastOrDefault(x => x.ChangeTime == updateDate).TaskId;
            return _context.Tasks.Find(taskId);
        }

        public List<Task> FindByExecutor(Guid executorId)
        {
            return _context.Tasks.Where(task => task.ExecutorId.Equals(executorId)).ToList();
        }

        public List<Task> FindUpdateByEmployee(Guid id)
        {
            return (from taskChange in _context.TasksChanges where taskChange.EmployeeId.Equals(id) 
                select _context.Tasks.FirstOrDefault(x => x.Id == id)).ToList();
        }

        public List<Task> FindBySubordinates(Guid id)
        {
            var subordinates = _context.Employees.Where(employee => employee.BossId.Equals(id)).ToList();
            var tasks = new List<Task>();
            return subordinates.Aggregate(tasks, (current, subordinate) => 
                (List<Task>) current.Union(FindByExecutor(subordinate.Id)));
        }

        public async Task<Task> Create(Guid executorId)
        {
            var task = new Task(Guid.NewGuid(), executorId);
            Changing(task.Id, executorId, DateTime.Now);
            EntityEntry<Task> taskFromDb = await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async void ChangeCondition(Guid taskId, Guid employeeId, Condition condition)
        {
            Task task = await FindById(taskId);
            task.Condition = condition;
            Changing(taskId, employeeId, DateTime.Now);
        }

        public void AddComment(Guid taskId, Guid employeeId, string comment)
        {
            Task task = _context.Tasks.Find(taskId);
            if (task == null) return;
            task.AddComment(comment);
            Changing(taskId, employeeId, DateTime.Now);
        }
    }
} 