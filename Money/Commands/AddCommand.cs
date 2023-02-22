using System.Globalization;

using Money.Data.Dto;

namespace Money.Commands;

internal sealed class AddCommand : AsyncCommand<AddSettings>
{
    private readonly IWriteOnlyData _writeOnlyData;

    public AddCommand(IWriteOnlyData writeOnlyData)
    {
        _writeOnlyData = writeOnlyData;
    }

    public override Task<int> ExecuteAsync(CommandContext context, AddSettings settings)
    {
        return settings.BatchMode
            ? BatchMode(settings)
            : SingleMode(settings);
    }

    private async Task<int> BatchMode(AddSettings settings)
    {
        BatchHandler<DataRowUi> batchHandler = new(Resources.BatchSpendingsText, DtoAdapter.DataRowUiFromParts);
        IReadOnlyList<DataRowUi> batchInputs = batchHandler.DoBatchInput();

        foreach (DataRowUi input in batchInputs)
        {
            (bool success, ulong id) = await _writeOnlyData.InsertAsync(input.Amount,
                                                                        input.Description,
                                                                        input.Date,
                                                                        input.CategoryName);
            if (!success)
                Ui.Error(Resources.ErrorCategoryDoesntExist, settings.Category);
            else
                Ui.Success(id);
        }

        return Constants.Success;
    }

    private async Task<int> SingleMode(AddSettings settings)
    {
        (bool success, ulong id) = await _writeOnlyData.InsertAsync(settings.Amount,
                                                                    settings.Text,
                                                                    settings.Date,
                                                                    settings.Category);

        if (!success)
        {
            return Ui.Error(Resources.ErrorCategoryDoesntExist, settings.Category);
        }

        Ui.Success(id);
        return Constants.Success;
    }
}
