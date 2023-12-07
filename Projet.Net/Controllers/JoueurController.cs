﻿using System;
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
            var iitgamingContext = _context.Joueurs.Include(j => j.Equipe);
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
            // Populate EquipeId with the Equipe names
            ViewData["EquipeId"] = new SelectList(_context.Equipes, "EquipeId", "NomEquipe");
            return View();
        }

        // POST: Joueur/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Pseudonyme,DateNaissance,EquipeId")] Joueur joueur)
        {
            if (ModelState.IsValid)
            {
                // Check if a player with the same pseudonyme already exists
                if (_context.Joueurs.Any(e => e.Pseudonyme == joueur.Pseudonyme))
                {
                    ModelState.AddModelError("Pseudonyme", "A player with this pseudonyme already exists.");
                    ViewData["EquipeId"] = new SelectList(_context.Equipes, "EquipeId", "NomEquipe", joueur.EquipeId);
                    return View(joueur);
                }

                // Remove the manual assignment of EquipeId
                _context.Add(joueur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Use EquipeName instead of EquipeId for SelectList
            ViewData["EquipeId"] = new SelectList(_context.Equipes, "EquipeId", "NomEquipe", joueur.EquipeId);
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

            // Use EquipeName instead of EquipeId for SelectList
            ViewData["EquipeId"] = new SelectList(_context.Equipes, "EquipeId", "NomEquipe", joueur.EquipeId);
            return View(joueur);
        }

        // POST: Joueur/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JoueurId,Pseudonyme,DateNaissance")] Joueur joueur)
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

            // Use EquipeName instead of EquipeId for SelectList
            ViewData["EquipeId"] = new SelectList(_context.Equipes, "EquipeId", "NomEquipe", joueur.EquipeId);
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
