namespace DAL.Interfaces
{
    public interface IRepository<T> where T: class
    {
        public T GetById(int id);
    }
}
