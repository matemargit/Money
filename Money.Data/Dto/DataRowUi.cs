namespace Money.Data.Dto;

public sealed record class DataRowUi : IDataRowBase
{
    public required DateOnly Date { get; init; }
    public string Description { get; init; }
    public double Amount { get; init; }
    public string CategoryName { get; init; }

    public DataRowUi()
    {
        Description = string.Empty;
        CategoryName = string.Empty;
    }
}
