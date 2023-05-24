using System.IO;
using System.Windows.Media.Imaging;

namespace DemoExam.Wpf.Utils;

public static class BytesToBitmapConverter
{
    public static BitmapImage Convert(byte[] byteArray)
    {
        var image = new BitmapImage();
        using (var ms = new MemoryStream(byteArray))
        {
            ms.Position = 0;
            image.BeginInit();
            image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.StreamSource = ms;
            image.EndInit();
        }
        image.Freeze();

        return image;
    }
}