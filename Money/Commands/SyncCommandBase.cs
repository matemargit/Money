﻿using System.Diagnostics;
using System.Globalization;

namespace Money.Commands;
internal abstract class SyncCommandBase : AsyncCommand
{
    private static Task<int> RunGitAsync(string gitCommand, string directory)
    {
        var tcs = new TaskCompletionSource<int>();

        var process = new Process
        {
            StartInfo = 
            { 
                FileName = "git.exe",
                WorkingDirectory = directory,
                Arguments = gitCommand,
            },
            EnableRaisingEvents = true
        };

        process.Exited += (sender, args) =>
        {
            tcs.SetResult(process.ExitCode);
            process.Dispose();
        };

        process.Start();

        return tcs.Task;
    }

    protected Task<int> Pull(string directory)
    {
        return RunGitAsync("pull", directory);
    }

    protected async Task<int> Push(DateTime last, string directory)
    {
        string timeStamp = last.ToString(CultureInfo.InvariantCulture);

        File.WriteAllText(Path.Combine(directory, "last.sync"), timeStamp);
        int addCode = await RunGitAsync("add .", directory);
        int commitCode = await RunGitAsync($"commit -m \"sync {timeStamp}\"", directory);
        int pushCode = await RunGitAsync("push", directory);

        return addCode + pushCode + commitCode;
    }

    protected async Task<DateTime> GetLastInsertDateFile(string directory)
    {
        var file = Path.Combine(directory, "last.sync");
        if (!File.Exists(file))
        {
            return DateTime.MinValue;
        }

        var text = await File.ReadAllTextAsync(file);

        return DateTime.Parse(text, CultureInfo.InvariantCulture);
    }
}