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
            obj.Department=_context.Department.First();

            _context.Add(obj);
            _context.SaveChanges();
        }
    }
}
