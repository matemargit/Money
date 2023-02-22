namespace Money.Commands;

internal sealed class CategoryAddCommand : AsyncCommand<CategoryAddSettings>
{
    private readonly IWriteOnlyData _writeOnlyData;

    public CategoryAddCommand(IWriteOnlyData writeOnlyData)
    {
        _writeOnlyData = writeOnlyData;
    }

    public override Task<int> ExecuteAsync(CommandContext context,
                                           CategoryAddSettings settings)
    {
        return settings.BatchMode
            ? BatchMode(settings)
            : SingleMode(settings);
    }

    private async Task<int> BatchMode(CategoryAddSettings settings)
    {
        BatchHandler<string> batchHandler = new(Resources.BatchCategoryText, parts => parts[0]);
        IReadOnlyList<string> batchInputs = batchHandler.DoBatchInput();
        foreach (string input in batchInputs)
        {
            (bool success, ulong id) = await _writeOnlyData.CreateCategoryAsync(input);

            if (!success)
                Ui.Error(Resources.ErrorCategoryAlreadyExists, settings.CategoryName);
            else
                Ui.Success(id);
        }

        return Constants.Success;
    }

    private async Task<int> SingleMode(CategoryAddSettings settings)
    {
        (bool success, ulong id) = await _writeOnlyData.CreateCategoryAsync(settings.CategoryName);

        if (!success)
            return Ui.Error(Resources.ErrorCategoryAlreadyExists, settings.CategoryName);

        Ui.Success(id);

        return Constants.Success;
    }
}
