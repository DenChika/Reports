using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Reports.DAL.Conditions;
using Reports.DAL.Entities;
using Reports.DAL.TasksChange;
using Reports.Server.Database;
using Task = Reports.DAL.Entities.Task;

namespace Reports.Server.Services
{
    public class ReportService : IReportService
    {
        private readonly ReportsDatabaseContext _context;

        public ReportService(ReportsDatabaseContext context)
        {
            _context = context;
        }

        public async Task<Report> Create(Guid employeeId, int days)
        {
            var report = new Report(employeeId, days, Guid.NewGuid());
            var reportFromDb = await _context.Reports.AddAsync(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<List<Report>> GetAll()
        {
            return await _context.Reports.ToListAsync();
        }

        public List<Task> GetWeekTasks(DateTime reportDate)
        {
            return (from taskChange in _context.TasksChanges let updateTime = taskChange.ChangeTime 
                let task = _context.Tasks.Find(taskChange.TaskId) 
                where DateTime.Compare(updateTime, reportDate) > 0 && task.Condition != Condition.Resolved select task).ToList();
        }

        public List<Report> GetBySubordinates(Guid employeeId, DateTime reportDate, int days)
        {
            var subordinates = (from employee in _context.Employees 
                where employee.BossId.Equals(employeeId) select employee.Id).ToList();

            var reports = new List<Report>();
            foreach (Report report in _context.Reports)
            {
                if (DateTime.Compare(report.ResolvedDay, reportDate.AddDays(-days)) > 0) 
                    reports.AddRange(from subId in subordinates where report.EmployeeId.Equals(subId) select report);
            }

            return reports;
        }

        public void AddTask(Guid taskId, Guid reportId)
        {
            Task task = _context.Tasks.Find(taskId);
            Report report = _context.Reports.Find(reportId);
            foreach (TaskChange taskChange in _context.TasksChanges)
            {
                DateTime updateTime = taskChange.ChangeTime;
                if (DateTime.Compare(updateTime, report.ResolvedDay.AddDays(-report.Days)) > 0)
                    report.AddTaskChange(task.Id, report.EmployeeId, updateTime);
            }
        }

        public void UpdateRedactorAccess(bool redactorAccess, Guid reportId)
        {
            foreach (Report report in _context.Reports)
            {
                if (report.Id.Equals(reportId))
                {
                    report.RedactorAccess = redactorAccess;
                }
            }
        }
    }
} 