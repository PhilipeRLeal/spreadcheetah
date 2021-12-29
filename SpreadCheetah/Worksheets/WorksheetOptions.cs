using SpreadCheetah.Helpers;

namespace SpreadCheetah.Worksheets;

/// <summary>
/// Provides options to be used when starting a worksheet with <see cref="Spreadsheet"/>.
/// </summary>
public class WorksheetOptions
{
    /// <summary>
    /// The number of left-most columns that should be frozen.
    /// </summary>
    public int? FrozenColumns
    {
        get => _frozenColumns;
        set => _frozenColumns = value is < 1 or > SpreadsheetConstants.MaxNumberOfColumns
            ? throw new ArgumentOutOfRangeException(nameof(value), value, $"Number of frozen columns must be between 1 and {SpreadsheetConstants.MaxNumberOfColumns}")
            : value;
    }

    private int? _frozenColumns;

    /// <summary>
    /// The number of top-most rows that should be frozen.
    /// </summary>
    public int? FrozenRows
    {
        get => _frozenRows;
        set => _frozenRows = value is < 1 or > SpreadsheetConstants.MaxNumberOfRows
            ? throw new ArgumentOutOfRangeException(nameof(value), value, $"Number of frozen rows must be between 1 and {SpreadsheetConstants.MaxNumberOfRows}")
            : value;
    }

    private int? _frozenRows;

    internal SortedDictionary<int, ColumnOptions> ColumnOptions { get; } = new SortedDictionary<int, ColumnOptions>();

    /// <summary>
    /// Get options for a column in the worksheet. The first column has column number 1.
    /// </summary>
    /// <param name="columnNumber"></param>
    public ColumnOptions Column(int columnNumber)
    {
        if (columnNumber < 1) throw new ArgumentOutOfRangeException(nameof(columnNumber), "Column number can't be less than 1.");

        if (!ColumnOptions.TryGetValue(columnNumber, out var options))
        {
            options = new ColumnOptions();
            ColumnOptions.Add(columnNumber, options);
        }

        return options;
    }
}