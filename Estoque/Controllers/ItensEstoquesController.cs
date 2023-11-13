using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Estoque.Data;
using Estoque.Models;

namespace Estoque.Controllers
{
    public class ItensEstoquesController : Controller
    {
        private readonly BancoContext _context;

        public ItensEstoquesController(BancoContext context)
        {
            _context = context;
        }

        // GET: ItensEstoques
        public async Task<IActionResult> Index()
        {
            return _context.Itens != null ?
                        View(await _context.Itens.ToListAsync()) :
                        Problem("Entity set 'BancoContext.Itens'  is null.");
        }

        // GET: ItensEstoques/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Itens == null)
            {
                return NotFound();
            }

            var itensEstoque = await _context.Itens
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itensEstoque == null)
            {
                return NotFound();
            }

            return View(itensEstoque);
        }

        // GET: ItensEstoques/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ItensEstoques/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Setor,Quantidade")] ItensEstoque itensEstoque)
        {
            if (ModelState.IsValid)
            {
                // Verificar se a quantidade é não negativa
                if (itensEstoque.Quantidade < 0)
                {
                    ModelState.AddModelError("Quantidade", "A quantidade não pode ser negativa.");
                    return View(itensEstoque);
                }

                _context.Add(itensEstoque);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(itensEstoque);
        }


        // GET: ItensEstoques/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Itens == null)
            {
                return NotFound();
            }

            var itensEstoque = await _context.Itens.FindAsync(id);
            if (itensEstoque == null)
            {
                return NotFound();
            }
            return View(itensEstoque);
        }

        // POST: ItensEstoques/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Setor,Quantidade")] ItensEstoque itensEstoque)
        {
            if (id != itensEstoque.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Verificar se a quantidade é não negativa
                if (itensEstoque.Quantidade < 0)
                {
                    ModelState.AddModelError("Quantidade", "A quantidade não pode ser negativa.");
                    return View(itensEstoque);
                }

                try
                {
                    _context.Update(itensEstoque);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItensEstoqueExists(itensEstoque.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(itensEstoque);
        }


        // GET: ItensEstoques/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Itens == null)
            {
                return NotFound();
            }

            var itensEstoque = await _context.Itens
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itensEstoque == null)
            {
                return NotFound();
            }

            return View(itensEstoque);
        }

        // POST: ItensEstoques/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Itens == null)
            {
                return Problem("Entity set 'BancoContext.Itens'  is null.");
            }
            var itensEstoque = await _context.Itens.FindAsync(id);
            if (itensEstoque != null)
            {
                _context.Itens.Remove(itensEstoque);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItensEstoqueExists(int id)
        {
            return (_context.Itens?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public IActionResult Listagem()
        {
            // Certifique-se de ter o DbSet definido no seu contexto (BancoContext)
            // DbSet<ItensEstoque> Itens { get; set; }

            // Buscar itens do banco de dados
            var listaItens = _context.Itens.ToList();

            return View(listaItens);
        }
    }
}
