namespace DemoExam.Core.Utils;

public static class FileToByteArrayConverter
{
    public static byte[] Convert(string fileName)
    {
        using var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
        var byteArray = new byte[fs.Length];
        fs.Read(byteArray, 0, byteArray.Length);
        return byteArray;
    }
}