using System;
using System.Collections.Generic;
using System.IO;
using Reports.DAL.Comments;
using Reports.DAL.Conditions;

namespace Reports.DAL.Entities
{
    public class Task
    {
        private readonly List<Comment> _comments;

        private Task()
        {
        }
        public Task(Guid id, Guid executorId)
        {
            Id = id;
            Condition = Condition.Open;
            ExecutorId = executorId;
            _comments = new List<Comment>();
        }

        public Guid Id { get; private set; }

        public Condition Condition { get; set; }

        public Guid ExecutorId { get; set; }

        public IReadOnlyList<Comment> Comments => _comments;

        public void AddComment(string comment)
        {
            _comments.Add(new Comment(comment));
        }

    }
}