using System;

namespace Reports.DAL.Comments
{
    public class Comment
    {
        private Comment()
        {
        }
        public Comment(string text)
        {
            Text = text;
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
        public string Text { get; }
    }
}