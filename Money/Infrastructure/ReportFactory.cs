using System.Globalization;

using Money.Data.Dto;

namespace Money.Infrastructure;
internal static class ReportFactory
{
    private static string HeaderStatistics(IEnumerable<DataRowUi> items, double total)
    {
        double sum = 0;
        double min = double.MaxValue;
        double max = double.MinValue;
        int count = 0;

        foreach (DataRowUi item in items)
        {
            ++count;
            sum += item.Amount;
            if (item.Amount < min)
                min = item.Amount;
            if (item.Amount > max)
                max = item.Amount;
        }

        double avg = sum / count;
        double percent = sum / total;


        return $"<pre>" +
            $"<p>Sum: {sum:C0}" +
            $"<br>Min: {min:C0}" +
            $"<br>Max: {max:C0}" +
            $"<br>Avg: {avg:C0}" +
            $"<br>%: {percent:P0}" +
            $"</p></pre>";
    }

    public static string CreateReport(IEnumerable<DataRowUi> data,
                                      Statistics statistics,
                                      DateOnly startDate,
                                      DateOnly endDate)
    {
        HtmlBuilder html = new("Report", CultureInfo.CurrentUICulture);

        IEnumerable<IGrouping<DateOnly, DataRowUi>> dates = data.GroupBy(x => x.Date);

        html.Heading(1, string.Format(Resources.StatFromToHeader, startDate, endDate));

        Dictionary<string, string> statTable = new()
        {
            { Resources.StatSpendingCount, statistics.Count.ToString("N0") },
            { Resources.StatSpendingWithDays, statistics.Days.ToString("N0") },
            { Resources.StatTotalSpending, statistics.Sum.ToString("C0") },
            { Resources.StatAveragePerCount, statistics.AvgPerCount.ToString("C0") },
            { Resources.StatAveragePerDay, statistics.AvgPerDay.ToString("C0") },
        };

        foreach (KeyValuePair<string, double> item in statistics.SumPerCategory)
        {
            statTable.Add(item.Key, item.Value.ToString("C0"));
        }

        html.Table(statTable);

        html.Heading(1, "Details");

        foreach (IGrouping<DateOnly, DataRowUi> date in dates)
        {
            html.Details($"{date.Key} {HeaderStatistics(date, statistics.Sum)}",
                         (content) => content.Table(date));
        }

        return html.GetHtml();
    }
}
