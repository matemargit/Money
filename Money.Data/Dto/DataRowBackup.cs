namespace Money.Data.Dto;

public sealed record class DataRowBackup : IDataRowBase
{
    public required DateOnly Date { get; init; }
    public required DateTime AddedOn { get; init; }
    public required string Description { get; init; }
    public required double Amount { get; init; }
    public required string CategoryName { get; init; }
}
