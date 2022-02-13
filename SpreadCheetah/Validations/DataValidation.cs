using SpreadCheetah.Helpers;

namespace SpreadCheetah.Validations;

public sealed class DataValidation
{
    private const string MinGreaterThanMaxMessage = "The min value must be less than or equal to the max value.";

    internal ValidationType Type { get; }
    internal ValidationOperator Operator { get; }
    internal string? Value1 { get; }
    internal string? Value2 { get; }

    private DataValidation(
        ValidationType type,
        ValidationOperator op,
        string? value1,
        string? value2 = null)
    {
        Type = type;
        Operator = op;
        Value1 = value1;
        Value2 = value2;
    }

    public bool IgnoreBlank { get; set; } = true;
    public bool ShowErrorAlert { get; set; } = true;
    public bool ShowInputMessage { get; set; } = true;
    public string? ErrorTitle { get; set; } // TODO: Validate length
    public string? ErrorMessage { get; set; } // TODO: Validate length
    public string? InputTitle { get; set; } // TODO: Validate length
    public string? InputMessage { get; set; } // TODO: Validate length

    public ValidationErrorType ErrorType
    {
        get => _errorType;
        set => _errorType = EnumHelper.IsDefined(value)
            ? value
            : throw new ArgumentOutOfRangeException(nameof(value), value, null);
    }

    private ValidationErrorType _errorType;

    private static DataValidation Decimal(ValidationOperator op, double min, double max) => max < min
        ? throw new ArgumentException(MinGreaterThanMaxMessage, nameof(min))
        : new DataValidation(ValidationType.Decimal, op, min.ToStringInvariant(), max.ToStringInvariant());

    private static DataValidation Decimal(ValidationOperator op, double value) => new(ValidationType.Decimal, op, value.ToStringInvariant());
    public static DataValidation DecimalBetween(double min, double max) => Decimal(ValidationOperator.Between, min, max);
    public static DataValidation DecimalNotBetween(double min, double max) => Decimal(ValidationOperator.NotBetween, min, max);
    public static DataValidation DecimalEqualTo(double value) => Decimal(ValidationOperator.EqualTo, value);
    public static DataValidation DecimalNotEqualTo(double value) => Decimal(ValidationOperator.NotEqualTo, value);
    public static DataValidation DecimalGreaterThan(double value) => Decimal(ValidationOperator.GreaterThan, value);
    public static DataValidation DecimalGreaterThanOrEqualTo(double value) => Decimal(ValidationOperator.GreaterThanOrEqualTo, value);
    public static DataValidation DecimalLessThan(double value) => Decimal(ValidationOperator.LessThan, value);
    public static DataValidation DecimalLessThanOrEqualTo(double value) => Decimal(ValidationOperator.LessThanOrEqualTo, value);

    private static DataValidation Integer(ValidationOperator op, int min, int max) => max < min
        ? throw new ArgumentException(MinGreaterThanMaxMessage, nameof(min))
        : new DataValidation(ValidationType.Integer, op, min.ToStringInvariant(), max.ToStringInvariant());

    private static DataValidation Integer(ValidationOperator op, int value) => new(ValidationType.Integer, op, value.ToStringInvariant());
    public static DataValidation IntegerBetween(int min, int max) => Integer(ValidationOperator.Between, min, max);
    public static DataValidation IntegerNotBetween(int min, int max) => Integer(ValidationOperator.NotBetween, min, max);
    public static DataValidation IntegerEqualTo(int value) => Integer(ValidationOperator.EqualTo, value);
    public static DataValidation IntegerNotEqualTo(int value) => Integer(ValidationOperator.NotEqualTo, value);
    public static DataValidation IntegerGreaterThan(int value) => Integer(ValidationOperator.GreaterThan, value);
    public static DataValidation IntegerGreaterThanOrEqualTo(int value) => Integer(ValidationOperator.GreaterThanOrEqualTo, value);
    public static DataValidation IntegerLessThan(int value) => Integer(ValidationOperator.LessThan, value);
    public static DataValidation IntegerLessThanOrEqualTo(int value) => Integer(ValidationOperator.LessThanOrEqualTo, value);

    private static DataValidation TextLength(ValidationOperator op, int min, int max) => max < min
        ? throw new ArgumentException(MinGreaterThanMaxMessage, nameof(min))
        : new DataValidation(ValidationType.TextLength, op, min.ToStringInvariant(), max.ToStringInvariant());

    private static DataValidation TextLength(ValidationOperator op, int value) => new(ValidationType.TextLength, op, value.ToStringInvariant());
    public static DataValidation TextLengthBetween(int min, int max) => TextLength(ValidationOperator.Between, min, max);
    public static DataValidation TextLengthNotBetween(int min, int max) => TextLength(ValidationOperator.NotBetween, min, max);
    public static DataValidation TextLengthEqualTo(int value) => TextLength(ValidationOperator.EqualTo, value);
    public static DataValidation TextLengthNotEqualTo(int value) => TextLength(ValidationOperator.NotEqualTo, value);
    public static DataValidation TextLengthGreaterThan(int value) => TextLength(ValidationOperator.GreaterThan, value);
    public static DataValidation TextLengthGreaterThanOrEqualTo(int value) => TextLength(ValidationOperator.GreaterThanOrEqualTo, value);
    public static DataValidation TextLengthLessThan(int value) => TextLength(ValidationOperator.LessThan, value);
    public static DataValidation TextLengthLessThanOrEqualTo(int value) => TextLength(ValidationOperator.LessThanOrEqualTo, value);
}
