using SpreadCheetah.CellReferences;
using SpreadCheetah.CellWriters;
using SpreadCheetah.Helpers;
using SpreadCheetah.Images.Internal;
using SpreadCheetah.MetadataXml;
using SpreadCheetah.Styling.Internal;
using SpreadCheetah.Validations;
using SpreadCheetah.Worksheets;

namespace SpreadCheetah;

internal sealed class Worksheet : IDisposable, IAsyncDisposable
{
    private readonly Stream _stream;
#pragma warning disable CA2213 // Disposed by Spreadsheet
    private readonly SpreadsheetBuffer _buffer;
#pragma warning restore CA2213 // Disposed by Spreadsheet
    private readonly BaseCellWriter<Cell> _cellWriter;
    private readonly BaseCellWriter<DataCell> _dataCellWriter;
    private readonly BaseCellWriter<StyledCell> _styledCellWriter;
    private readonly CellWriterState _state;
    private Dictionary<SingleCellOrCellRangeReference, DataValidation>? _validations;
    private HashSet<CellRangeRelativeReference>? _cellMerges;
    private string? _autoFilterRange;

    public Dictionary<SingleCellRelativeReference, string>? Notes { get; private set; }
    public List<WorksheetImage>? Images { get; private set; }

    public Worksheet(Stream stream, DefaultStyling? defaultStyling, SpreadsheetBuffer buffer, bool writeCellReferenceAttributes)
    {
        _stream = stream;
        _buffer = buffer;
        _state = new CellWriterState(buffer);

        if (writeCellReferenceAttributes)
        {
            _cellWriter = new CellWithReferenceWriter(_state, defaultStyling);
            _dataCellWriter = new DataCellWithReferenceWriter(_state, defaultStyling);
            _styledCellWriter = new StyledCellWithReferenceWriter(_state, defaultStyling);
        }
        else
        {
            _cellWriter = new CellWriter(_state, defaultStyling);
            _dataCellWriter = new DataCellWriter(_state, defaultStyling);
            _styledCellWriter = new StyledCellWriter(_state, defaultStyling);
        }
    }

    public int NextRowNumber => (int)_state.NextRowIndex;

    public async ValueTask WriteHeadAsync(WorksheetOptions? options, CancellationToken token)
    {
        await WorksheetStartXml.WriteAsync(options, _buffer, _stream, token).ConfigureAwait(false);

        if (options?.AutoFilter is not null)
        {
            _autoFilterRange = options.AutoFilter.CellRange.Reference;
        }
    }

    public bool TryAddRow(IList<Cell> cells)
        => _cellWriter.TryAddRow(cells, _state.NextRowIndex++);
    public bool TryAddRow(IList<DataCell> cells)
        => _dataCellWriter.TryAddRow(cells, _state.NextRowIndex++);
    public bool TryAddRow(IList<StyledCell> cells)
        => _styledCellWriter.TryAddRow(cells, _state.NextRowIndex++);
    public bool TryAddRow(IList<Cell> cells, RowOptions options)
        => _cellWriter.TryAddRow(cells, _state.NextRowIndex++, options);
    public bool TryAddRow(IList<DataCell> cells, RowOptions options)
        => _dataCellWriter.TryAddRow(cells, _state.NextRowIndex++, options);
    public bool TryAddRow(IList<StyledCell> cells, RowOptions options)
        => _styledCellWriter.TryAddRow(cells, _state.NextRowIndex++, options);
    public bool TryAddRow(ReadOnlySpan<Cell> cells)
        => _cellWriter.TryAddRow(cells, _state.NextRowIndex++);
    public bool TryAddRow(ReadOnlySpan<DataCell> cells)
        => _dataCellWriter.TryAddRow(cells, _state.NextRowIndex++);
    public bool TryAddRow(ReadOnlySpan<StyledCell> cells)
        => _styledCellWriter.TryAddRow(cells, _state.NextRowIndex++);
    public bool TryAddRow(ReadOnlySpan<Cell> cells, RowOptions options)
        => _cellWriter.TryAddRow(cells, _state.NextRowIndex++, options);
    public bool TryAddRow(ReadOnlySpan<DataCell> cells, RowOptions options)
        => _dataCellWriter.TryAddRow(cells, _state.NextRowIndex++, options);
    public bool TryAddRow(ReadOnlySpan<StyledCell> cells, RowOptions options)
        => _styledCellWriter.TryAddRow(cells, _state.NextRowIndex++, options);
    public ValueTask AddRowAsync(IList<Cell> cells, CancellationToken ct)
        => _cellWriter.AddRowAsync(cells, _state.NextRowIndex - 1, null, _stream, ct);
    public ValueTask AddRowAsync(IList<DataCell> cells, CancellationToken ct)
        => _dataCellWriter.AddRowAsync(cells, _state.NextRowIndex - 1, null, _stream, ct);
    public ValueTask AddRowAsync(IList<StyledCell> cells, CancellationToken ct)
        => _styledCellWriter.AddRowAsync(cells, _state.NextRowIndex - 1, null, _stream, ct);
    public ValueTask AddRowAsync(IList<Cell> cells, RowOptions options, CancellationToken ct)
        => _cellWriter.AddRowAsync(cells, _state.NextRowIndex - 1, options, _stream, ct);
    public ValueTask AddRowAsync(IList<DataCell> cells, RowOptions options, CancellationToken ct)
        => _dataCellWriter.AddRowAsync(cells, _state.NextRowIndex - 1, options, _stream, ct);
    public ValueTask AddRowAsync(IList<StyledCell> cells, RowOptions options, CancellationToken ct)
        => _styledCellWriter.AddRowAsync(cells, _state.NextRowIndex - 1, options, _stream, ct);
    public ValueTask AddRowAsync(ReadOnlyMemory<Cell> cells, CancellationToken ct)
        => _cellWriter.AddRowAsync(cells, _state.NextRowIndex - 1, null, _stream, ct);
    public ValueTask AddRowAsync(ReadOnlyMemory<DataCell> cells, CancellationToken ct)
        => _dataCellWriter.AddRowAsync(cells, _state.NextRowIndex - 1, null, _stream, ct);
    public ValueTask AddRowAsync(ReadOnlyMemory<StyledCell> cells, CancellationToken ct)
        => _styledCellWriter.AddRowAsync(cells, _state.NextRowIndex - 1, null, _stream, ct);
    public ValueTask AddRowAsync(ReadOnlyMemory<Cell> cells, RowOptions options, CancellationToken ct)
        => _cellWriter.AddRowAsync(cells, _state.NextRowIndex - 1, options, _stream, ct);
    public ValueTask AddRowAsync(ReadOnlyMemory<DataCell> cells, RowOptions options, CancellationToken ct)
        => _dataCellWriter.AddRowAsync(cells, _state.NextRowIndex - 1, options, _stream, ct);
    public ValueTask AddRowAsync(ReadOnlyMemory<StyledCell> cells, RowOptions options, CancellationToken ct)
        => _styledCellWriter.AddRowAsync(cells, _state.NextRowIndex - 1, options, _stream, ct);

    public bool TryAddDataValidation(string reference, DataValidation validation)
    {
        _validations ??= [];

        if (_validations.Count >= SpreadsheetConstants.MaxNumberOfDataValidations)
            return false;

        var cellReference = SingleCellOrCellRangeReference.Create(reference);
        _validations[cellReference] = validation;
        return true;
    }

    public void AddImage(in WorksheetImage image, out bool firstImage)
    {
        firstImage = false;

        if (Images is null)
        {
            firstImage = true;
            Images = [];
        }

        Images.Add(image);
    }

    public void AddNote(string cellReference, string noteText, out bool firstNote)
    {
        var reference = SingleCellRelativeReference.Create(cellReference);
        firstNote = false;

        if (Notes is null)
        {
            firstNote = true;
            Notes = [];
        }

        Notes[reference] = noteText;
    }

    public void MergeCells(CellRangeRelativeReference cellRange)
    {
        _cellMerges ??= [];
        _cellMerges.Add(cellRange);
    }

    public async ValueTask FinishAsync(CancellationToken token)
    {
        using var cellMergesPooledArray = _cellMerges?.ToPooledArray();
        using var validationsPooledArray = _validations?.ToPooledArray();

        await WorksheetEndXml.WriteAsync(
            cellMerges: cellMergesPooledArray?.Memory,
            validations: validationsPooledArray?.Memory,
            autoFilterRange: _autoFilterRange,
            hasNotes: Notes is not null,
            hasImages: Images is not null,
            buffer: _buffer,
            stream: _stream,
            token: token).ConfigureAwait(false);

        await _stream.FlushAsync(token).ConfigureAwait(false);
    }

    public void Dispose() => _stream.Dispose();
    public ValueTask DisposeAsync() => _stream.DisposeAsync();
}
