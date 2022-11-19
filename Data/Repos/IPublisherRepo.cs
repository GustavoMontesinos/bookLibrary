using Library.Models;

namespace Library.Data.Repos;

public interface IPublisherRepo
{
    bool SaveChanges();
    bool Exists(Guid id);
    void DeletePublisher(Guid id);
    void UpdatePublisher(Publisher publisher);
    void CreatePublisher(Publisher publisher);
    Publisher GetPublisherById(Guid id);
    IEnumerable<Publisher> GetPublishers();
    IEnumerable<Publisher> GetPublisherByName(string name);
    ICollection<Book> GetBooksOfPublisher(Guid id);
}