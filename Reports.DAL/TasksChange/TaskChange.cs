using System;

namespace Reports.DAL.TasksChange
{
    public class TaskChange
    {
        public TaskChange(Guid taskId, Guid employeeId, DateTime changeTime)
        {
            Id = Guid.NewGuid();
            TaskId = taskId;
            EmployeeId = employeeId;
            ChangeTime = changeTime;
        }

        public Guid Id { get; set; }
        public Guid TaskId { get; set; }

        public Guid EmployeeId { get; set; }

        public DateTime ChangeTime { get; set; }
    }
}