namespace WebApplication10.Services;

public class Przyklad
{
    SomeDto? dto = null;

    await using var connection = new SqlConnection(configuration.GetConnectionString("Default"));
    await using var command = new SqlCommand();

    await connection.OpenAsync();

    command.Connection = connection;
    command.CommandText = """
                          SELECT ...
                          FROM ...
                          WHERE id = @id
                          """;

    command.Parameters.AddWithValue("@id", id);

    await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
    {
        dto ??= new SomeDto
        {
            // fields
        };
    }

if (dto is null)
{
    throw new NotFoundExcpetion($"Object with id {id} not found");
}

return dto;
}