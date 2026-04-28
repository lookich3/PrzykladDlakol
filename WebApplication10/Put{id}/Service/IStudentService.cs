using WebApplication10.Put_id_.DTOs;

namespace WebApplication10.Put_id_.Service;

public interface IStudentService
{
    Task UpdateAsync(int id, UpdateStudentDto dto);
}