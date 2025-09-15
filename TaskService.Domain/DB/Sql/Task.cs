namespace TaskService.Domain.DB.Sql
{
    public class Task
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public Guid UserId { get; set; }

    }
}
