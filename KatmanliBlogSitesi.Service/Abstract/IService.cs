using KatmanliBlogSitesi.Data.Abstract;
using KatmanliBlogSitesi.Entites;

namespace KatmanliBlogSitesi.Service.Abstract
{
    public interface IService<T> : IRepository<T> where T : class, IEntity, new() // "T" dedik burayı generic olarak oluşturduk yani her yerden ulaşılabilsin yani Product, Brand gibi yerlerden de görülsün.
    {
    }
}
