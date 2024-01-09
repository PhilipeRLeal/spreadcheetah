using System.Text;

namespace SpreadCheetah.Helpers;

internal static class Utf8EncodingExtensions
{
    public static bool TryGetBytesInternal(this UTF8Encoding encoding, ReadOnlySpan<char> chars, Span<byte> bytes, out int bytesWritten)
    {
        // Try first with an approximate value length, then try with a more accurate value length
        if (Utf8Helper.DestinationCanFitTranscodedString(chars, bytes))
        {
            bytesWritten = encoding.GetBytes(chars, bytes);
            return true;
        }

        bytesWritten = 0;
        return false;
    }
}
