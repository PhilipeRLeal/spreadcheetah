using System.Diagnostics.CodeAnalysis;

namespace SpreadCheetah.Helpers;

internal static class ThrowHelper
{
    [DoesNotReturn]
    public static void CellReferenceInvalid(string? paramName) => throw new ArgumentException("Invalid reference for a cell or a range of cells.", paramName);

    [DoesNotReturn]
    public static void StartWorksheetNotAllowedAfterFinish() => throw new SpreadCheetahException("Can't start another worksheet after " + nameof(Spreadsheet.FinishAsync) + " has been called.");

    [DoesNotReturn]
    public static void WorksheetNameAlreadyExists(string? paramName) => throw new ArgumentException("A worksheet with the given name already exists.", paramName);

    [DoesNotReturn]
    public static void WorksheetNameEmptyOrWhiteSpace(string? paramName) => throw new ArgumentException("The name can not be empty or consist only of whitespace.", paramName);

    [DoesNotReturn]
    public static void WorksheetNameInvalidCharacters(string? paramName, string invalidChars) => throw new ArgumentException("The name can not contain any of the following characters: " + invalidChars, paramName);

    [DoesNotReturn]
    public static void WorksheetNameStartsOrEndsWithSingleQuote(string? paramName) => throw new ArgumentException("The name can not start or end with a single quote.", paramName);

    [DoesNotReturn]
    public static void WorksheetNameTooLong(string? paramName) => throw new ArgumentException("The name can not be more than 31 characters.", paramName);
}