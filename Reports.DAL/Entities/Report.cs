using System;
using System.Collections.Generic;
using Reports.DAL.TasksChange;

namespace Reports.DAL.Entities
{
    public class Report
    {
        private readonly List<TaskChange> _tasksChanges;

        private Report()
        {
        }
        public Report(Guid employeeId, int days, Guid id)
        {
            EmployeeId = employeeId;
            Days = days;
            Id = id;
            RedactorAccess = true;
            _tasksChanges = new List<TaskChange>();
        }

        public Guid Id { get; private set; }
        public int Days { get; private set; }

        public IReadOnlyList<TaskChange> TasksChanges => _tasksChanges;

        public DateTime ResolvedDay { get; set; }
        public Guid EmployeeId { get; private set; }
        public bool RedactorAccess { get; set; }

        public void AddTaskChange(Guid taskId, Guid employeeId, DateTime date)
        {
            _tasksChanges.Add(new TaskChange(taskId, employeeId, date));
        }
    }
}