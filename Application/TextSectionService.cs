using Philosopher_ServAPI.Core.Models.Entities.Book;
using Philosopher_ServAPI.Core.Repositories;
using Philosopher_ServAPI.Core.Shared.Database;
using Philosopher_ServAPI.Helpers.Exceptions;
using System.Text.RegularExpressions;

namespace Philosopher_ServAPI.Application
{
    public class TextSectionService
    {
        private readonly ITextSectionRepository _repository;

        public TextSectionService(ITextSectionRepository repository)
        {
            _repository = repository;
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
                await Task.Run(() => _repository.AddRangeAsync(list))
                .ContinueWith(t => _repository.SaveChanges());
            }
        }

        //public async Task CreateTextSectionsFromText(IFormFile file)
        //{

        //}

        public async Task<TextSection> GetTextSectionById(Guid id)
        {
            var section = await _repository.FirstOrDefaultAsync(c => c.Id == id) ??
                throw new NotFoundException($"Text section with id {id} is not found");

            return section;
        }

        public async Task<IReadOnlyList<TextSection>> GetAllTextSections()
        {
            var sections = await _repository.ListAsync();

            return sections ?? [];
        }
    }
}
