using System.Globalization;

using Money.Data.Entities;

namespace Money.Data.Dto;

public static class DtoAdapter
{
    public static DataRowBackup FromCsvLine(string line)
    {
        string[] parts = line.Split(';', StringSplitOptions.RemoveEmptyEntries);
        return new DataRowBackup
        {
            Date = DateOnly.Parse(parts[0], CultureInfo.InvariantCulture),
            Description = parts[1],
            Amount = double.Parse(parts[2], CultureInfo.InvariantCulture),
            AddedOn = DateTime.Parse(parts[3], CultureInfo.InvariantCulture),
            CategoryName = parts[4]
        };
    }

    public static DataRowUi DataRowUiFromParts(string[] parts)
    {
        return new DataRowUi
        {
            Date = DateOnly.Parse(parts[0], CultureInfo.CurrentUICulture),
            Amount = double.Parse(parts[1], CultureInfo.CurrentUICulture),
            Description = parts[2],
            CategoryName = parts[3],
        };
    }

    public static string ToCsvLine(DataRowBackup backup)
    {
        CultureInfo c = CultureInfo.InvariantCulture;
        return $"{backup.Date.ToString(c)};{backup.Description};{backup.Amount.ToString(c)};{backup.AddedOn.ToString(c)};{backup.CategoryName}";
    }

    internal static DataRowExcel ToDataRowExcel(Spending spending)
    {
        return new DataRowExcel
        {
            Date = spending.Date.ToDateTime(TimeOnly.MinValue),
            Description = spending.Description,
            Amount = spending.Ammount,
            CategoryName = spending.Category.Description,
        };
    }

    internal static DataRowUi ToDataRowUi(Spending spending)
    {
        return new DataRowUi
        {
            Date = spending.Date,
            Description = spending.Description,
            Amount = spending.Ammount,
            CategoryName = spending.Category.Description
        };
    }

    internal static DataRowBackup ToDataRowBackup(Spending spending)
    {
        return new DataRowBackup
        {
            Date = spending.Date,
            Description = spending.Description,
            AddedOn = spending.AddedOn,
            Amount = spending.Ammount,
            CategoryName = spending.Category.Description,
        };
    }

    internal static Spending ToSpending(DataRowBackup dataRowBackup, Func<ulong> IdCreator, Category category)
    {
        return new Spending
        {
            Date = dataRowBackup.Date,
            Description = dataRowBackup.Description,
            AddedOn = dataRowBackup.AddedOn,
            Ammount = dataRowBackup.Amount,
            Category = category,
            Id = IdCreator.Invoke()
        };
    }


    internal static Spending ToSpending(DataRowExcel dataRowExcel, Func<ulong> IdCreator, Category category)
    {
        return new Spending
        {
            Date = new DateOnly(dataRowExcel.Date.Year, dataRowExcel.Date.Month, dataRowExcel.Date.Day),
            Description = dataRowExcel.Description,
            AddedOn = DateTime.Now,
            Ammount = dataRowExcel.Amount,
            Category = category,
            Id = IdCreator.Invoke()
        };
    }
}
