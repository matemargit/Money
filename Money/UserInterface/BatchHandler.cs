using System.Diagnostics;

namespace Money.UserInterface;

internal sealed class BatchHandler<TResult>
{
    private readonly string _explanationText;
    private readonly Func<string[], TResult> _parserDelegate;

    public BatchHandler(string explanationText,
                       Func<string[], TResult> parserDelegate)
    {
        _explanationText = explanationText;
        _parserDelegate = parserDelegate;
    }

    public IReadOnlyList<TResult> DoBatchInput()
    {
        string fileName = CreateTempTxtFile();
        WriteExplanationText(fileName);
        RunEditorAndWaitForExit(fileName);

        string[] lines = File.ReadAllLines(fileName);
        List<TResult> results = new(lines.Length);

        int errorLines = 0;
        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line) || line.StartsWith('#'))
                continue;

            string[] parts = line.Split(';', StringSplitOptions.RemoveEmptyEntries);
            try
            {
                TResult parsed = _parserDelegate.Invoke(parts);
                results.Add(parsed);
            }
            catch (FormatException)
            {
                ++errorLines;
            }
        }
        if (errorLines > 0)
        {
            Ui.Warning("{0} rows were skipped due to format issues", errorLines);
        }

        DeleteTempFile(fileName);

        return results;
    }

    private static string CreateTempTxtFile()
    {
        string tempName = Path.GetTempFileName();
        return Path.ChangeExtension(tempName, ".txt");
    }

    private void WriteExplanationText(string fileName)
    {
        using (StreamWriter writer = File.CreateText(fileName))
        {
            writer.Write(_explanationText);
        }
    }

    private static void RunEditorAndWaitForExit(string fileName)
    {
        using (Process process = new Process())
        {
            process.StartInfo.FileName = fileName;
            process.StartInfo.UseShellExecute = true;
            process.Start();
            process.WaitForExit();
        }
    }

    private static void DeleteTempFile(string fileName)
    {
        if (File.Exists(fileName))
        {
            File.Delete(fileName);
        }
    }

}
