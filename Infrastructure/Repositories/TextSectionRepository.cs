using Philosopher_ServAPI.Core.Models.Entities.Book;
using Philosopher_ServAPI.Core.Repositories;

namespace Philosopher_ServAPI.Infrastructure.Repositories
{
    public class TextSectionRepository(PostgresDBContext dBContext) : PostgresRepository<TextSection>(dBContext), 
        ITextSectionRepository { }
}
