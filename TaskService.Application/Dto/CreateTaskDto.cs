namespace TaskService.Application.Dto
{
    public class CreateTaskDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public Guid UserId { get; set; }
    }
}
