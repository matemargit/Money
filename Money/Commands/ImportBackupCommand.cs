﻿using System.Diagnostics.CodeAnalysis;
using System.IO.Compression;

using Money.CommandsSettings;
using Money.Data;
using Money.Data.Dto;
using Money.Extensions;

using Spectre.Console.Cli;

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
                        (int createdCategory, int createdEntry) = _writeOnlyData.Import(data);
                        Ui.Success($"Imported {createdCategory} categories and {createdEntry} entries");
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
/*
                 using (var stream = File.Create(settings.FileName))
                {
                    using (var compressed = new GZipStream(stream, CompressionLevel.SmallestSize, true))
                    {
                        compressed.WriteJson(data);
                    }
                }
 */