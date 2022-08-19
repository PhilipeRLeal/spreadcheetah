using SpreadCheetah.CellValueWriters.Number;
using SpreadCheetah.Helpers;
using SpreadCheetah.Styling;
using System.Buffers.Text;

namespace SpreadCheetah.CellValueWriters.Time;

internal class DateTimeCellValueWriter : NumberCellValueWriterBase
{
    protected override int DataCellElementLength =>
        BeginDataCell().Length +
        DataCellHelper.EndDefaultCell.Length;

    protected override int GetStyleId(StyleId styleId)
    {
        throw new NotImplementedException();
    }

    // <c s="1"><v>
    // NOTE: Assumes the default style for DateTime has index 1 in styles.xml.
    protected override ReadOnlySpan<byte> BeginDataCell() => new[]
    {
        (byte)'<', (byte)'c', (byte)' ', (byte)'s', (byte)'=', (byte)'"',
        (byte)'1', (byte)'"', (byte)'>', (byte)'<', (byte)'v', (byte)'>'
    };

    // <c s="1"><f>
    // NOTE: Assumes the default style for DateTime has index 1 in styles.xml.
    protected override ReadOnlySpan<byte> BeginFormulaCell() => new[]
    {
        (byte)'<', (byte)'c', (byte)' ', (byte)'s', (byte)'=', (byte)'"',
        (byte)'1', (byte)'"', (byte)'>', (byte)'<', (byte)'f', (byte)'>'
    };

    // TODO: Possibly less characters needed?
    protected override int MaxNumberLength => ValueConstants.DoubleValueMaxCharacters;

    protected override int GetValueBytes(in DataCell cell, Span<byte> destination)
    {
        Utf8Formatter.TryFormat(cell.NumberValue.DoubleValue, destination, out var bytesWritten);
        return bytesWritten;
    }

    public override bool Equals(in CellValue value, in CellValue other) => value.DoubleValue == other.DoubleValue;
    public override int GetHashCodeFor(in CellValue value) => value.DoubleValue.GetHashCode();
}