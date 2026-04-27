namespace WebApplication10.getByIdNstd.Service;

public class Przyklad
{
    MainDto? dto = null;
    Dictionary<int, ChildDto> children = [];

    await using var connection = new SqlConnection(configuration.GetConnectionString("Default"));
    await using var command = new SqlCommand();

    await connection.OpenAsync();

    command.Connection = connection;
    command.CommandText = """
                          SELECT ...
                          FROM MainTable m
                          LEFT JOIN ChildTable c ON ...
                          LEFT JOIN ...
                          WHERE m.id = @id
                          """;

    command.Parameters.AddWithValue("@id", id);

    await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
    {
        dto ??= new MainDto
        {
            // main fields
            Children = []
        };

        if (reader.IsDBNull(childIdIndex))
        {
            continue;
        }

        var childId = reader.GetInt32(childIdIndex);

        if (!children.ContainsKey(childId))
        {
            children.Add(childId, new ChildDto
            {
                // child fields
                Items = []
            });
        }

        var items = children[childId].Items.ToList();

        items.Add(new ItemDto
        {
            // item fields
        });

        children[childId].Items = items;
    }

if (dto is null)
{
    throw new NotFoundExcpetion($"Object with id {id} not found");
}

dto.Children = children.Values;

return dto;
}