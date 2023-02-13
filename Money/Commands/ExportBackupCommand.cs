﻿using System.IO.Compression;
using System.Text;

using Money.Data.Dto;

namespace Money.Commands;

internal sealed class ExportBackupCommand : AsyncCommand<ExportSetting>
{
    private readonly IReadonlyData _readonlyData;

    public ExportBackupCommand(IReadonlyData readonlyData)
    {
        _readonlyData = readonlyData;
    }

    public override async Task<int> ExecuteAsync(CommandContext context,
                                                 ExportSetting settings)
    {
        try
        {
            int recordCount = await _readonlyData.GetSpendingsCount();

            using (FileStream stream = File.Create(settings.FileName))
            {
                using (GZipStream compressed = new GZipStream(stream, CompressionLevel.SmallestSize, true))
                {
                    using (StreamWriter writer = new StreamWriter(compressed, Encoding.UTF8))
                    {
                        int pages = (recordCount / _readonlyData.ChunkSize) + 1;

                        int offset = 0;
                        for (int i = 0; i < pages; i++)
                        {
                            List<DataRowBackup> data = await _readonlyData.ExportBackupAsync(offset);
                            foreach (string? row in data.Select(DtoAdapter.ToCsvLine))
                            {
                                writer.Write(row);
                            }
                            offset += _readonlyData.ChunkSize;
                        }
                    }
                }
            }
            Ui.Success(Resources.SuccessExport, recordCount, settings.FileName);
            return Constants.Success;
        }
        catch (Exception ex)
        {
            Ui.PrintException(ex);
            return Constants.IoError;
        }
    }
}
