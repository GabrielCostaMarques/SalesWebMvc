using SalesWebMvc.Data;
using SalesWebMvc.Models;

namespace SalesWebMvc.Services
{
    public class SallerService
    {
        //as classes de serviços se mexem com o banco precisam de uma dependencia do DbContext do Projeto, sempre criando o construtor da injeção da dependencia para a dependencia ser criada
        private readonly SalesWebMvcContext _context;

        public SallerService(SalesWebMvcContext context)
        {
            _context = context;
        }



        public List<Saller> FindAll()
        {
            //acessando a tabela saller do context e convertendo para uma lista
            return _context.Saller.ToList();
        }

        public void Insert(Saller obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }

        public Saller FindById(int id)
        {
            return _context.Saller.FirstOrDefault(obj => obj.Id == id);
        }

        public void Remove(int id)
        {
            var obj = _context.Saller.Find(id);
            _context.Saller.Remove(obj);
            _context.SaveChanges();
        }
    }
}
