namespace Money.Data.Dto;

public interface IDataRowBase
{
    string Description { get; init; }
    double Amount { get; init; }
    string CategoryName { get; init; }
}
