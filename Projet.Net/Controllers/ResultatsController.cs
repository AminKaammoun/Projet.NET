﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projet.Net.Models;

namespace Projet.Net.Controllers
{
    public class ResultatsController : Controller
    {
        private readonly IitgamingContext _context;

        public ResultatsController(IitgamingContext context)
        {
            _context = context;
        }

        // GET: Resultats
        public async Task<IActionResult> Index()
        {
            var iitgamingContext = _context.Resultats.Include(r => r.EquipeGagnante).Include(r => r.EquipePerdante).Include(r => r.Tournoi);
            return View(await iitgamingContext.ToListAsync());
        }

        // GET: Resultats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Resultats == null)
            {
                return NotFound();
            }

            var resultat = await _context.Resultats
                .Include(r => r.EquipeGagnante)
                .Include(r => r.EquipePerdante)
                .Include(r => r.Tournoi)
                .FirstOrDefaultAsync(m => m.ResultatId == id);
            if (resultat == null)
            {
                return NotFound();
            }

            return View(resultat);
        }

        // GET: Resultats/Create
        public IActionResult Create()
        {
            ViewData["EquipeGagnanteId"] = new SelectList(_context.Equipes, "EquipeId", "NomEquipe");
            ViewData["EquipePerdanteId"] = new SelectList(_context.Equipes, "EquipeId", "NomEquipe");
            ViewData["TournoiId"] = new SelectList(_context.Tournois, "TournoiId", "Nom");
            return View();
        }

        // POST: Resultats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TournoiId,EquipeGagnanteId,EquipePerdanteId,ScoreGagnant,ScorePerdant,DateMatch")] Resultat resultat)
        {
            if (ModelState.IsValid)
            {
                _context.Add(resultat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EquipeGagnanteId"] = new SelectList(_context.Equipes, "EquipeId", "NomEquipe", resultat.EquipeGagnanteId);
            ViewData["EquipePerdanteId"] = new SelectList(_context.Equipes, "EquipeId", "NomEquipe", resultat.EquipePerdanteId);
            ViewData["TournoiId"] = new SelectList(_context.Tournois, "TournoiId", "Nom", resultat.TournoiId);
            return View(resultat);
        }

        // GET: Resultats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Resultats == null)
            {
                return NotFound();
            }

            var resultat = await _context.Resultats.FindAsync(id);
            if (resultat == null)
            {
                return NotFound();
            }
            ViewData["EquipeGagnanteId"] = new SelectList(_context.Equipes, "EquipeId", "NomEquipe", resultat.EquipeGagnanteId);
            ViewData["EquipePerdanteId"] = new SelectList(_context.Equipes, "EquipeId", "NomEquipe", resultat.EquipePerdanteId);
            ViewData["TournoiId"] = new SelectList(_context.Tournois, "TournoiId", "Nom", resultat.TournoiId);
            return View(resultat);
        }

        // POST: Resultats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TournoiId,EquipeGagnanteId,EquipePerdanteId,ScoreGagnant,ScorePerdant,DateMatch")] Resultat resultat)
        {
            if (id != resultat.ResultatId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resultat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResultatExists(resultat.ResultatId))
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
            ViewData["EquipeGagnanteId"] = new SelectList(_context.Equipes, "EquipeId", "NomEquipe", resultat.EquipeGagnanteId);
            ViewData["EquipePerdanteId"] = new SelectList(_context.Equipes, "EquipeId", "NomEquipe", resultat.EquipePerdanteId);
            ViewData["TournoiId"] = new SelectList(_context.Tournois, "TournoiId", "Nom", resultat.TournoiId);
            return View(resultat);
        }

        // GET: Resultats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Resultats == null)
            {
                return NotFound();
            }

            var resultat = await _context.Resultats
                .Include(r => r.EquipeGagnante)
                .Include(r => r.EquipePerdante)
                .Include(r => r.Tournoi)
                .FirstOrDefaultAsync(m => m.ResultatId == id);
            if (resultat == null)
            {
                return NotFound();
            }

            return View(resultat);
        }

        // POST: Resultats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Resultats == null)
            {
                return Problem("Entity set 'IitgamingContext.Resultats'  is null.");
            }
            var resultat = await _context.Resultats.FindAsync(id);
            if (resultat != null)
            {
                _context.Resultats.Remove(resultat);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResultatExists(int id)
        {
          return (_context.Resultats?.Any(e => e.ResultatId == id)).GetValueOrDefault();
        }
    }
}
