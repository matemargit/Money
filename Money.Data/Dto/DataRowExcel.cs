namespace Money.Data.Dto;

public sealed record class DataRowExcel : IDataRowBase
{
    public DateTime Date { get; init; }
    public string Description { get; init; }
    public double Amount { get; init; }
    public string CategoryName { get; init; }

    public DataRowExcel()
    {
        Description = string.Empty;
        CategoryName = string.Empty;
    }
}
