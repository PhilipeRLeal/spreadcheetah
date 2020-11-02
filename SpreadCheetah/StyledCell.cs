using SpreadCheetah.Styling;
using System;
using System.Collections.Generic;

namespace SpreadCheetah
{
    public readonly struct StyledCell : IEquatable<StyledCell>
    {
        public DataCell DataCell { get; }
        public StyleId? StyleId { get; }

        public StyledCell(string? value, StyleId? styleId)
        {
            DataCell = new DataCell(value);
            StyleId = styleId;
        }

        public StyledCell(int value, StyleId? styleId)
        {
            DataCell = new DataCell(value);
            StyleId = styleId;
        }

        public StyledCell(int? value, StyleId? styleId)
        {
            DataCell = new DataCell(value);
            StyleId = styleId;
        }

        public StyledCell(long value, StyleId? styleId)
        {
            DataCell = new DataCell(value);
            StyleId = styleId;
        }

        public StyledCell(long? value, StyleId? styleId)
        {
            DataCell = new DataCell(value);
            StyleId = styleId;
        }

        public StyledCell(float value, StyleId? styleId)
        {
            DataCell = new DataCell(value);
            StyleId = styleId;
        }

        public StyledCell(float? value, StyleId? styleId)
        {
            DataCell = new DataCell(value);
            StyleId = styleId;
        }

        public StyledCell(double value, StyleId? styleId)
        {
            DataCell = new DataCell(value);
            StyleId = styleId;
        }

        public StyledCell(double? value, StyleId? styleId)
        {
            DataCell = new DataCell(value);
            StyleId = styleId;
        }

        public StyledCell(decimal value, StyleId? styleId)
        {
            DataCell = new DataCell(value);
            StyleId = styleId;
        }

        public StyledCell(decimal? value, StyleId? styleId)
        {
            DataCell = new DataCell(value);
            StyleId = styleId;
        }

        public StyledCell(bool value, StyleId? styleId)
        {
            DataCell = new DataCell(value);
            StyleId = styleId;
        }

        public StyledCell(bool? value, StyleId? styleId)
        {
            DataCell = new DataCell(value);
            StyleId = styleId;
        }

        public bool Equals(StyledCell other)
        {
            return DataCell.Equals(other.DataCell) && EqualityComparer<StyleId?>.Default.Equals(StyleId, other.StyleId);
        }

        public override bool Equals(object? obj) => obj is StyledCell other && Equals(other);
        public override int GetHashCode() => HashCode.Combine(DataCell, StyleId);
        public static bool operator ==(StyledCell left, StyledCell right) => left.Equals(right);
        public static bool operator !=(StyledCell left, StyledCell right) => !left.Equals(right);
    }
}
