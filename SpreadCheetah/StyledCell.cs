using SpreadCheetah.Styling;
using System;
using System.Collections.Generic;

namespace SpreadCheetah
{
    public readonly struct StyledCell : IEquatable<StyledCell>
    {
        public Cell Cell { get; }
        public StyleId? StyleId { get; }

        public StyledCell(string? value, StyleId? styleId)
        {
            Cell = new Cell(value);
            StyleId = styleId;
        }

        public StyledCell(int value, StyleId? styleId)
        {
            Cell = new Cell(value);
            StyleId = styleId;
        }

        public StyledCell(int? value, StyleId? styleId)
        {
            Cell = new Cell(value);
            StyleId = styleId;
        }

        public StyledCell(float value, StyleId? styleId)
        {
            Cell = new Cell(value);
            StyleId = styleId;
        }

        public StyledCell(float? value, StyleId? styleId)
        {
            Cell = new Cell(value);
            StyleId = styleId;
        }

        public StyledCell(double value, StyleId? styleId)
        {
            Cell = new Cell(value);
            StyleId = styleId;
        }

        public StyledCell(double? value, StyleId? styleId)
        {
            Cell = new Cell(value);
            StyleId = styleId;
        }

        public StyledCell(decimal value, StyleId? styleId)
            : this(decimal.ToDouble(value), styleId)
        {
        }

        public StyledCell(decimal? value, StyleId? styleId)
            : this(value != null ? decimal.ToDouble(value.Value) : null as double?, styleId)
        {
        }

        public StyledCell(bool value, StyleId? styleId)
        {
            Cell = new Cell(value);
            StyleId = styleId;
        }

        public StyledCell(bool? value, StyleId? styleId)
        {
            Cell = new Cell(value);
            StyleId = styleId;
        }

        public bool Equals(StyledCell other)
        {
            return Cell.Equals(other.Cell) && EqualityComparer<StyleId?>.Default.Equals(StyleId, other.StyleId);
        }

        public override bool Equals(object? obj)
        {
            return obj is StyledCell other && Equals(other);
        }

        public override int GetHashCode()
        {
            var hashCode = -1472682488;
            hashCode = hashCode * -1521134295 + Cell.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<StyleId?>.Default.GetHashCode(StyleId);
            return hashCode;
        }

        public static bool operator ==(StyledCell left, StyledCell right) => left.Equals(right);
        public static bool operator !=(StyledCell left, StyledCell right) => !left.Equals(right);
    }
}
