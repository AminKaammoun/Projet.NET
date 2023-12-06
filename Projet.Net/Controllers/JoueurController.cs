using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projet.Net.Models;

namespace Projet.Net.Controllers
{
    public class JoueurController : Controller
    {
        private readonly IitgamingContext _context;

        public JoueurController(IitgamingContext context)
        {
            _context = context;
        }

        // GET: Joueur
        public async Task<IActionResult> Index()
        {
            var iitgamingContext = _context.Joueurs.Include(j => j.Equipe).Include(j => j.User);
            return View(await iitgamingContext.ToListAsync());
        }

        // GET: Joueur/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Joueurs == null)
            {
                return NotFound();
            }

            var joueur = await _context.Joueurs
                .Include(j => j.Equipe)
                .Include(j => j.User)
                .FirstOrDefaultAsync(m => m.JoueurId == id);
            if (joueur == null)
            {
                return NotFound();
            }

            return View(joueur);
        }

        // GET: Joueur/Create
        public IActionResult Create()
        {
            ViewData["EquipeId"] = new SelectList(_context.Equipes, "EquipeId", "EquipeId");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");
            return View();
        }

        // POST: Joueur/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JoueurId,Pseudonyme,DateNaissance,EquipeId,UserId")] Joueur joueur)
        {
            if (ModelState.IsValid)
            {
                _context.Add(joueur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EquipeId"] = new SelectList(_context.Equipes, "EquipeId", "EquipeId", joueur.EquipeId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", joueur.UserId);
            return View(joueur);
        }

        // GET: Joueur/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Joueurs == null)
            {
                return NotFound();
            }

            var joueur = await _context.Joueurs.FindAsync(id);
            if (joueur == null)
            {
                return NotFound();
            }
            ViewData["EquipeId"] = new SelectList(_context.Equipes, "EquipeId", "EquipeId", joueur.EquipeId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", joueur.UserId);
            return View(joueur);
        }

        // POST: Joueur/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JoueurId,Pseudonyme,DateNaissance,EquipeId,UserId")] Joueur joueur)
        {
            if (id != joueur.JoueurId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(joueur);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JoueurExists(joueur.JoueurId))
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
            ViewData["EquipeId"] = new SelectList(_context.Equipes, "EquipeId", "EquipeId", joueur.EquipeId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", joueur.UserId);
            return View(joueur);
        }

        // GET: Joueur/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Joueurs == null)
            {
                return NotFound();
            }

            var joueur = await _context.Joueurs
                .Include(j => j.Equipe)
                .Include(j => j.User)
                .FirstOrDefaultAsync(m => m.JoueurId == id);
            if (joueur == null)
            {
                return NotFound();
            }

            return View(joueur);
        }

        // POST: Joueur/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Joueurs == null)
            {
                return Problem("Entity set 'IitgamingContext.Joueurs'  is null.");
            }
            var joueur = await _context.Joueurs.FindAsync(id);
            if (joueur != null)
            {
                _context.Joueurs.Remove(joueur);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JoueurExists(int id)
        {
          return (_context.Joueurs?.Any(e => e.JoueurId == id)).GetValueOrDefault();
        }
    }
}
