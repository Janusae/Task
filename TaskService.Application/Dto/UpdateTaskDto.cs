using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskService.Application.Dto
{
    public class UpdateTaskDto : CreateTaskDto
    {
        public Guid Id { get; set; }
    }
}
