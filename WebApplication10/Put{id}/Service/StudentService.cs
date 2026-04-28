using Microsoft.AspNetCore.Connections;
using Microsoft.Data.SqlClient;
using WebApplication10.Delete_id_.Exception;
using WebApplication10.Put_id_.DTOs;

namespace WebApplication10.Put_id_.Service;

public class StudentService(IConfiguration configuration) : IStudentService
{
    public async Task UpdateAsync(int id, UpdateStudentDto dto)
    {
        await using var connection = new SqlConnection(configuration.GetConnectionString("Default"));
        await connection.OpenAsync();

        await using var transaction = (SqlTransaction)await connection.BeginTransactionAsync();
        await using var command = new SqlCommand();

        command.Connection = connection;
        command.Transaction = transaction;

        try
        {
            command.CommandText = """
                                  SELECT 1
                                  FROM Student
                                  WHERE id = @id
                                  """;

            command.Parameters.AddWithValue("@id", id);

            var exists = await command.ExecuteScalarAsync();

            if (exists is null)
            {
                throw new NotFoundException($"Student with id {id} not found");
            }

            command.Parameters.Clear();

            command.CommandText = """
                                  UPDATE Student
                                  SET firstname = @firstName,
                                      lastname = @lastName,
                                      pesel = @pesel,
                                      phone = @phone,
                                      email = @email
                                  WHERE id = @id
                                  """;

            command.Parameters.AddWithValue("@firstName", dto.FirstName);
            command.Parameters.AddWithValue("@lastName", dto.LastName);
            command.Parameters.AddWithValue("@pesel", dto.Pesel);
            command.Parameters.AddWithValue("@phone", dto.Phone);
            command.Parameters.AddWithValue("@email", dto.Email);
            command.Parameters.AddWithValue("@id", id);

            await command.ExecuteNonQueryAsync();

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}