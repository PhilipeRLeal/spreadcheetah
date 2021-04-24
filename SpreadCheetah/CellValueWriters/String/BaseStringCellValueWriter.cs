using SpreadCheetah.Helpers;
using SpreadCheetah.Styling;

namespace SpreadCheetah.CellValueWriters.String
{
    internal abstract class BaseStringCellValueWriter : CellValueWriter
    {
        protected static readonly int DataCellElementLength =
            DataCellHelper.BeginStringCell.Length +
            DataCellHelper.EndStringCell.Length;

        protected static readonly int StyledCellElementLength =
            StyledCellHelper.BeginStyledStringCell.Length +
            SpreadsheetConstants.StyleIdMaxDigits +
            StyledCellHelper.EndStyleBeginInlineString.Length +
            DataCellHelper.EndStringCell.Length;

        protected static readonly int FormulaCellElementLength =
            StyledCellHelper.BeginStyledStringCell.Length +
            SpreadsheetConstants.StyleIdMaxDigits +
            FormulaCellHelper.EndStyleBeginFormula.Length +
            FormulaCellHelper.EndFormulaBeginCachedValue.Length +
            FormulaCellHelper.EndCachedValueEndCell.Length;

        public override bool TryWriteEndElement(in DataCell cell, SpreadsheetBuffer buffer)
        {
            var cellEnd = DataCellHelper.EndStringCell;
            var bytes = buffer.GetNextSpan();
            if (cellEnd.Length >= bytes.Length)
                return false;

            buffer.Index += SpanHelper.GetBytes(cellEnd, bytes);
            return true;
        }

        public override bool TryWriteEndElement(in Cell cell, SpreadsheetBuffer buffer)
        {
            if (cell.Formula is null)
                return TryWriteEndElement(cell.DataCell, buffer);

            var cellEnd = FormulaCellHelper.EndCachedValueEndCell;
            if (cellEnd.Length > buffer.GetRemainingBuffer())
                return false;

            buffer.Index += SpanHelper.GetBytes(cellEnd, buffer.GetNextSpan());
            return true;
        }

        public override bool WriteFormulaStartElement(StyleId? styleId, SpreadsheetBuffer buffer)
        {
            if (styleId is null)
            {
                buffer.Index += SpanHelper.GetBytes(FormulaCellHelper.BeginStringFormulaCell, buffer.GetNextSpan());
                return true;
            }

            var bytes = buffer.GetNextSpan();
            var bytesWritten = SpanHelper.GetBytes(StyledCellHelper.BeginStyledStringCell, bytes);
            bytesWritten += Utf8Helper.GetBytes(styleId.Id, bytes.Slice(bytesWritten));
            bytesWritten += SpanHelper.GetBytes(FormulaCellHelper.EndStyleBeginFormula, bytes.Slice(bytesWritten));
            buffer.Index += bytesWritten;
            return true;
        }

        public override bool WriteStartElement(in DataCell cell, SpreadsheetBuffer buffer)
        {
            buffer.Index += SpanHelper.GetBytes(DataCellHelper.BeginStringCell, buffer.GetNextSpan());
            return true;
        }

        public override bool WriteStartElement(in DataCell cell, StyleId styleId, SpreadsheetBuffer buffer)
        {
            var bytes = buffer.GetNextSpan();
            var bytesWritten = SpanHelper.GetBytes(StyledCellHelper.BeginStyledStringCell, bytes);
            bytesWritten += Utf8Helper.GetBytes(styleId.Id, bytes.Slice(bytesWritten));
            bytesWritten += SpanHelper.GetBytes(StyledCellHelper.EndStyleBeginInlineString, bytes.Slice(bytesWritten));
            buffer.Index += bytesWritten;
            return true;
        }
    }
}