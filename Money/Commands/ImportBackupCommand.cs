﻿using System.Diagnostics.CodeAnalysis;
using System.IO.Compression;

using Money.Data.Dto;

namespace Money.Commands
{
    internal sealed class ImportBackupCommand : Command<ImportSetting>
    {
        private readonly IWriteOnlyData _writeOnlyData;

        public ImportBackupCommand(IWriteOnlyData writeOnlyData)
        {
            _writeOnlyData = writeOnlyData;
        }


        public override int Execute([NotNull] CommandContext context,
                                    [NotNull] ImportSetting settings)
        {
            try
            {
                using (FileStream srtream = File.OpenRead(settings.FileName))
                {
                    using (GZipStream compressed = new GZipStream(srtream, CompressionMode.Decompress))
                    {
                        List<ExportRow> data = compressed.ReadJson<List<ExportRow>>();
                        (int createdCategory, int createdEntry) = _writeOnlyData.ImportAsync(data);
                        Ui.Success(Resources.SuccesImport, createdCategory, createdEntry);
                        return Constants.Success;
                    }
                }
            }
            catch (Exception ex)
            {
                Ui.PrintException(ex);
                return Constants.IoError;
            }
        }
    }
}
