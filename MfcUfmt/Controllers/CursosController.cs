using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MfcUfmt.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MfcUfmt.Models;
using MfcUfmt.Services;
using MfcUfmt.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace MfcUfmt.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CursosController : Controller
    {
        private readonly MfcUfmtContext _context;

        private readonly TrabalhoService _trabalhoService;

        private readonly CursoService _cursoService;


        public CursosController(MfcUfmtContext context, CursoService cursoService, TrabalhoService trabalhoService)
        {
            _context = context;
            _cursoService = cursoService;
            _trabalhoService = trabalhoService;
        }

        // GET: Cursoes
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var list = await _cursoService.FindAllAsync();
            var trabalho = await _trabalhoService.FindAllAsync();
            return View(list);
        }

        // GET: Cursoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não foi fornecido" });
            }

            var curso = await _context.Curso
                .Include(c => c.Trabalho)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (curso == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Curso não foi encontrado" });
            }

            return View(curso);
        }

        // GET: Cursoes/Create
        public async Task<IActionResult> Create()
        {
            var trabalhos = await _trabalhoService.FindAllAsync();
            var viewModel = new CursoViewModel { Trabalhos = trabalhos };
            return View(viewModel);
        }

        // POST: Cursoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomeCurso,Descricao,TrabalhoId")] Curso curso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(curso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TrabalhoId"] = new SelectList(_context.Set<Trabalho>(), "Id", "Id", curso.TrabalhoId);
            return View(curso);
        }

        // GET: Cursoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não foi fornecido" });
            }

            var curso = await _context.Curso.FindAsync(id);
            if (curso == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Curso não foi encontrado" });
            }
            List<Trabalho> trabalhos = await _trabalhoService.FindAllAsync();
            CursoViewModel viewModel = new CursoViewModel { Curso = curso, Trabalhos = trabalhos };
            return View(viewModel);
        }

        // POST: Cursoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomeCurso,Descricao,TrabalhoId")] Curso curso)
        {
            if (id != curso.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Ids não correspondem" });
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(curso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException e)
                {
                    if (!CursoExists(curso.Id))
                    {
                        return RedirectToAction(nameof(Error), new { message = e.Message });
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["TrabalhoId"] = new SelectList(_context.Set<Trabalho>(), "Id", "Id", curso.TrabalhoId);
            return View(curso);
        }

        // GET: Cursoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não foi encontrado" });
            }

            var curso = await _context.Curso
                .Include(c => c.Trabalho)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (curso == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Curso não foi encontrado" });
            }

            return View(curso);
        }

        // POST: Cursoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var curso = await _context.Curso.FindAsync(id);
            _context.Curso.Remove(curso);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CursoExists(int id)
        {
            return _context.Curso.Any(e => e.Id == id);
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier

            };
            return View(viewModel);
        }
    }
}
