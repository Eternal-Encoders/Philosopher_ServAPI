using Philosopher_ServAPI.Core.Models.Entities.Book;
using Philosopher_ServAPI.Core.Repositories;
using Philosopher_ServAPI.Core.Shared.Database;
using Philosopher_ServAPI.Helpers.Exceptions;
using System.Text.RegularExpressions;

namespace Philosopher_ServAPI.Application
{
    public class TextSectionService
    {
        private readonly ITextSectionRepository _textSectionRep;

        public TextSectionService(ITextSectionRepository textSectionRep)
        {
            _textSectionRep = textSectionRep;
        }

        public async Task CreateTextSections()
        {
            string text = File.ReadAllText("wwwroot/study_fies.md");
            List<TextSection> list = [];

            MatchCollection matches = Regex.Matches(Regex.Replace(text, @"![[]][(].*[)]", ""), @"[#]+\s.+\n");

            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                {
                    list.Add(new TextSection
                    {
                        Title = match.Value
                    });
                }
            }

            if (list.Count > 0)
            {
                await _textSectionRep.AddRangeAsync(list);
                await _textSectionRep.SaveChanges();
            }
        }

        //public async Task CreateTextSectionsFromText(IFormFile file)
        //{

        //}

        public async Task<TextSection> GetTextSectionById(Guid id)
        {
            var section = await _textSectionRep.FirstOrDefaultAsync(c => c.Id == id) ??
                throw new NotFoundException($"Text section with id {id} is not found");

            return section;
        }

        public async Task<IReadOnlyList<TextSection>> GetAllTextSections()
        {
            var sections = await _textSectionRep.ListAsync();

            return sections ?? [];
        }

        public async Task DeleteTextSectionById(Guid id)
        {
            await _textSectionRep.RemoveAsync(c => c.Id == id);
            await _textSectionRep.SaveChanges();
        }
    }
}
