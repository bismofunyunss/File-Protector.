using System.Globalization;
using System.Text;

public static class DataConversionHelpers
{
    public static string? ByteArrayToHexString(byte[] Buffer)
    {
        if (Buffer == null)
            return null;
        return Convert.ToHexString(Buffer, 0, Buffer.Length).ToLower(CultureInfo.CurrentCulture);
    }
    public static byte[]? HexStringToByteArray(string Input)
    {
        if (string.IsNullOrWhiteSpace(Input))
            return null;
        return Encoding.UTF8.GetBytes(Input);
    }
    public static string? StringToHex(byte[] Buffer)
    {
        if (Buffer == null)
            return null;
        return Convert.ToHexString(Buffer, 0, Buffer.Length).ToLower(CultureInfo.CurrentCulture);
    }
    public static string? ByteArrayToString(byte[] Buffer)
    {
        if (Buffer == null)
            return null;
        return Encoding.UTF8.GetString(Buffer);
    }
    public static byte[]? StringToByteArray(string Input)
    {
        if (string.IsNullOrWhiteSpace(Input))
            return null;
        return Encoding.UTF8.GetBytes(Input);
    }
    public static string? ByteArrayToBase64String(byte[] Buffer)
    {
        if (Buffer == null)
            return null;
        return Convert.ToBase64String(Buffer, 0, Buffer.Length);
    }
    public static byte[]? Base64StringToByteArray(string Input)
    {
        if (string.IsNullOrWhiteSpace(Input))
            return null;
        return Convert.FromBase64String(Input);
    }
}
