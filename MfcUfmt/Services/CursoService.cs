using MfcUfmt.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MfcUfmt.Data;
using MfcUfmt.Models.Enuns;

namespace MfcUfmt.Services
{
    public class CursoService
    {
        private readonly MfcUfmtContext _context;

        public CursoService(MfcUfmtContext context)
        {
            _context = context;
        }

        public async Task InsertAsync(Curso obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Curso> FindByIdAsync(int id)
        {
            return await _context.Curso
                .Include(obj => obj.Trabalho)
                .FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task<List<Curso>> FindAllAsync()
        {

            var obj = await _context.Curso
                .OrderBy(x => x.NomeCurso)
                .ToListAsync();
            return obj;
        }

        public async Task<List<CursoCidade>>  FindByTrabalhoId(int id)
        {
            //CursoCidade e;
            var cursos = await InsereLocalCurso();
            var obj = cursos.Where(cidade => cidade.TrabalhoId == id).ToList();
            return obj; 
        }

        public async Task<List<Curso>> GetCursosPesquisa(string searchString)
        {
            try
            {
                var obj = await _context.Curso.Where(s => s.Descricao.Contains(searchString)).ToListAsync();
                return obj;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<List<CursoCidade>> InsereLocalCurso()
        {
            try
            {
                var listCursos = new List<CursoCidade>();
                var cursos = await _context.Curso
                    .OrderBy(x => x.NomeCurso)
                    .ToListAsync();
                foreach (var VARIABLE in cursos)
                {
                    string descricao = VARIABLE.Descricao;

                    if (descricao.Contains("Cuiabá"))
                    {
                        listCursos.Add(new CursoCidade(VARIABLE.Id, VARIABLE.NomeCurso, VARIABLE.Descricao, VARIABLE.Trabalho, VARIABLE.TrabalhoId, "Cuiabá"));
                    }
                    else if (descricao.Contains("Rondonópolis"))
                    {
                        listCursos.Add(new CursoCidade(VARIABLE.Id, VARIABLE.NomeCurso, VARIABLE.Descricao, VARIABLE.Trabalho, VARIABLE.TrabalhoId, "Rondonópolis"));
                    }
                    else if (descricao.Contains("Pontal do Araguaia"))
                    {
                        listCursos.Add(new CursoCidade(VARIABLE.Id, VARIABLE.NomeCurso, VARIABLE.Descricao, VARIABLE.Trabalho, VARIABLE.TrabalhoId, "Pontal do Araguaia"));
                    }
                    else if (descricao.Contains("Barra do Garças"))
                    {
                        listCursos.Add(new CursoCidade(VARIABLE.Id, VARIABLE.NomeCurso, VARIABLE.Descricao, VARIABLE.Trabalho, VARIABLE.TrabalhoId, "Barra do Garças"));

                    }
                    else if (descricao.Contains("Sinop"))
                    {
                        listCursos.Add(new CursoCidade(VARIABLE.Id, VARIABLE.NomeCurso, VARIABLE.Descricao,
                            VARIABLE.Trabalho, VARIABLE.TrabalhoId, "Sinop"));
                    }
                    else
                    {
                        listCursos.Add(new CursoCidade(VARIABLE.Id, VARIABLE.NomeCurso, VARIABLE.Descricao, VARIABLE.Trabalho, VARIABLE.TrabalhoId, "Outras cidades"));
                    }


                }

                return listCursos;

            }
            catch (Exception e)
            {
                System.Console.WriteLine("Erro : " + e);
                throw;
            }
        }
        /*
public async Task RemoveAsync(int id)
{
   try
   {
       var obj = await _context.Curso.FindAsync(id);
       _context.Curso.Remove(obj);
       await _context.SaveChangesAsync();
   }
   catch (DbUpdateException e)
   {
       throw new IntegrityException("Can't delete seller because he/she has sales");
   }
}

public async Task UpdateAsync(Curso obj)
{
   bool hasAny = await _context.Curso.AnyAsync(x => x.Id == obj.Id);
   if (!hasAny)
   {
       throw new NotFoundException("Id not found");
   }
   try
   {
       _context.Update(obj);
       await _context.SaveChangesAsync();
   }
   catch (DbUpdateConcurrencyException e)
   {
       throw new DbConcurrencyException(e.Message);
   }
}
*/


    }
}