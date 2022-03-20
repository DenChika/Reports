using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reports.DAL.Entities;
using Task = Reports.DAL.Entities.Task;

namespace Reports.Server.Services
{
    public interface IReportService
    {
        public Task<Report> Create(Guid employeeId, int days);

        public Task<List<Report>> GetAll();
        public List<Task> GetWeekTasks(DateTime reportDate);

        public List<Report> GetBySubordinates(Guid employeeId, DateTime reportDate, int days);

        public void AddTask(Guid taskId, Guid reportId);

        public void UpdateRedactorAccess(bool redactorAccess, Guid reportId);
    }
}