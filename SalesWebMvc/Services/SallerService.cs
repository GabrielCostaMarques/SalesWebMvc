using SalesWebMvc.Data;
using SalesWebMvc.Models;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Services.Exceptions;

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



        public async Task<List<Saller>> FindAllAsync()
        {
            //acessando a tabela saller do context e convertendo para uma lista
            return await _context.Saller.ToListAsync();
        }

        public async Task InsertAsync(Saller obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Saller> FindByIdAsync(int id)
        {
            //usando o include porque o FindById ele puxa o departamento apenas para retornar o saller, e nao o departamento
            return await _context.Saller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            var obj = await _context.Saller.FindAsync(id);
            _context.Saller.Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Saller obj)
        {
            bool hasAny =  await _context.Saller.AnyAsync(x => x.Id == obj.Id);

            if (!hasAny)
            {
                throw new NotFoundException("Id Not Found");
            }

            try
            {

                _context.Update(obj);
                await _context.SaveChangesAsync();

            }

            //o catch ele captura um exception da camada de acesso a dados, capturando essa exception criamos um exception a camada de serviço
            catch (DbConcurrencyException ex) {
                throw new DbConcurrencyException(ex.Message);
            }
        }
    }
}
