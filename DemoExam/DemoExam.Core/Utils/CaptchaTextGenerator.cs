using System.Text;

namespace DemoExam.Core.Utils;

public class CaptchaTextGenerator
{
    private static List<char> _characters = new List<char>() { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

    static CaptchaTextGenerator()
    {
        const int start = (int)'a';
        const int stop = (int)'z';
        for (var i = start; i <= stop; i++)
        {
            var character = (char)i;
            _characters.Add(character);
            _characters.Add(character.ToString().ToUpper()[0]);
        }
    }

    public static string GenerateCaptchaText()
    {
        var stringBuilder = new StringBuilder();
        var random = new Random();
        for (int i = 0; i < 4; i++)
        {
            stringBuilder.Append(_characters[random.Next(0, _characters.Count)]);
        }

        return stringBuilder.ToString();
    }
}