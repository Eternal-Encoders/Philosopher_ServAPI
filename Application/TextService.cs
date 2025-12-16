using CommonMark;
using System.Text.RegularExpressions;

namespace Philosopher_ServAPI.Application
{
    public class TextService
    {
        public async Task<string> GetHtmlText()
        {
            return File.ReadAllText("wwwroot/study_fies_html.txt");
        }

        public async Task CreateHtmlText()
        {
            var settings = CommonMarkSettings.Default.Clone();
            Regex regexExclude = new(@"![[]][(].*[)]");
            string text = File.ReadAllText("wwwroot/study_fies.md");

            settings.OutputFormat = OutputFormat.Html;
            //return CommonMarkConverter.Convert(text, settings);
            //Без изображений
            var htmlText =  CommonMarkConverter.Convert(regexExclude.Replace(text, ""), settings);

            File.WriteAllText("wwwroot/study_fies_html.txt", htmlText);
        }
    }
}
