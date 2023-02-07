﻿using Money.Data.Dto;

namespace Money.Data
{
    public interface IReadonlyData
    {
        int ChunkSize { get; }
        Task<List<string>> GetCategoriesAsync();
        Task<Statistics> GetStatisticsAsync(DateOnly start, DateOnly end);
        Task<List<DataRow>> ExportAsync(DateOnly? start = null, DateOnly? end = null);
        Task<List<DataRow>> ExportBackupAsync(int startOffset);
        Task<int> GetSpendingsCount();
        Task<List<UiDataRow>> Find(string what, string? category, DateOnly? startDate, DateOnly? endDate, bool isRegex);
    }
}
