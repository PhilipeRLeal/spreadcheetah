//HintName: MyNamespace.MyGenRowContext.g.cs
// <auto-generated />
#nullable enable
using SpreadCheetah;
using SpreadCheetah.SourceGeneration;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MyNamespace
{
    public partial class MyGenRowContext
    {
        private static MyGenRowContext? _default;
        public static MyGenRowContext Default => _default ??= new MyGenRowContext();

        public MyGenRowContext()
        {
        }

        private WorksheetRowTypeInfo<SpreadCheetah.SourceGenerator.SnapshotTest.Models.RecordStructWithSingleProperty>? _RecordStructWithSingleProperty;
        public WorksheetRowTypeInfo<SpreadCheetah.SourceGenerator.SnapshotTest.Models.RecordStructWithSingleProperty> RecordStructWithSingleProperty => _RecordStructWithSingleProperty ??= WorksheetRowMetadataServices.CreateObjectInfo<SpreadCheetah.SourceGenerator.SnapshotTest.Models.RecordStructWithSingleProperty>(AddAsRowAsync, AddRangeAsRowsAsync);

        private static ValueTask AddAsRowAsync(SpreadCheetah.Spreadsheet spreadsheet, SpreadCheetah.SourceGenerator.SnapshotTest.Models.RecordStructWithSingleProperty obj, CancellationToken token)
        {
            if (spreadsheet is null)
                throw new ArgumentNullException(nameof(spreadsheet));
            return AddAsRowInternalAsync(spreadsheet, obj, token);
        }

        private static ValueTask AddRangeAsRowsAsync(SpreadCheetah.Spreadsheet spreadsheet, IEnumerable<SpreadCheetah.SourceGenerator.SnapshotTest.Models.RecordStructWithSingleProperty> objs, CancellationToken token)
        {
            if (spreadsheet is null)
                throw new ArgumentNullException(nameof(spreadsheet));
            if (objs is null)
                throw new ArgumentNullException(nameof(objs));
            return AddRangeAsRowsInternalAsync(spreadsheet, objs, token);
        }

        private static async ValueTask AddAsRowInternalAsync(SpreadCheetah.Spreadsheet spreadsheet, SpreadCheetah.SourceGenerator.SnapshotTest.Models.RecordStructWithSingleProperty obj, CancellationToken token)
        {
            var cells = ArrayPool<DataCell>.Shared.Rent(1);
            try
            {
                await AddCellsAsRowAsync(spreadsheet, obj, cells, token).ConfigureAwait(false);
            }
            finally
            {
                ArrayPool<DataCell>.Shared.Return(cells, true);
            }
        }

        private static async ValueTask AddRangeAsRowsInternalAsync(SpreadCheetah.Spreadsheet spreadsheet, IEnumerable<SpreadCheetah.SourceGenerator.SnapshotTest.Models.RecordStructWithSingleProperty> objs, CancellationToken token)
        {
            var cells = ArrayPool<DataCell>.Shared.Rent(1);
            try
            {
                await AddEnumerableAsRowsAsync(spreadsheet, objs, cells, token).ConfigureAwait(false);
            }
            finally
            {
                ArrayPool<DataCell>.Shared.Return(cells, true);
            }
        }

        private static async ValueTask AddEnumerableAsRowsAsync(SpreadCheetah.Spreadsheet spreadsheet, IEnumerable<SpreadCheetah.SourceGenerator.SnapshotTest.Models.RecordStructWithSingleProperty> objs, DataCell[] cells, CancellationToken token)
        {
            foreach (var obj in objs)
            {
                await AddCellsAsRowAsync(spreadsheet, obj, cells, token).ConfigureAwait(false);
            }
        }

        private static ValueTask AddCellsAsRowAsync(SpreadCheetah.Spreadsheet spreadsheet, SpreadCheetah.SourceGenerator.SnapshotTest.Models.RecordStructWithSingleProperty obj, DataCell[] cells, CancellationToken token)
        {
            cells[0] = new DataCell(obj.Value);
            return spreadsheet.AddRowAsync(cells.AsMemory(0, 1), token);
        }
    }
}