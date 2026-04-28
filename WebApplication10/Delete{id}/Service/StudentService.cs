using Microsoft.Data.SqlClient;
using WebApplication10.Delete_id_.Exception;
using NotFoundException = WebApplication10.getByIdNstd.Exception.NotFoundException;

namespace WebApplication10.Delete_id_.Service;

public class StudentService(IConfiguration configuration) : IStundentService
{
    public async Task RemoveAsync(int id)
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
                                  DELETE FROM StudentCourse
                                  WHERE StudentId = @id
                                  """;

            command.Parameters.AddWithValue("@id", id);

            await command.ExecuteNonQueryAsync();

            command.Parameters.Clear();

            command.CommandText = """
                                  DELETE FROM Student
                                  WHERE id = @id
                                  """;

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