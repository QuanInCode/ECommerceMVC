﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ECommerceMVC.Data;

namespace ECommerceMVC.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("/loai")]
    public class LoaisController : Controller
    {
        private readonly Hshop2023Context _context;

        public LoaisController(Hshop2023Context context)
        {
            _context = context;
        }

        // GET: Admin/Loais
        [Route("")]
        [Route("index")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Loais.ToListAsync());
        }

        // GET: Admin/Loais/Details/5
        [HttpGet("details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loai = await _context.Loais
                .FirstOrDefaultAsync(m => m.MaLoai == id);
            if (loai == null)
            {
                return NotFound();
            }

            return View(loai);
        }

        // GET: Admin/Loais/Create
        [HttpGet("create")]

        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Loais/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaLoai,TenLoai,TenLoaiAlias,MoTa,Hinh")] Loai loai)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loai);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(loai);
        }

        // GET: Admin/Loais/Edit/5
        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loai = await _context.Loais.FindAsync(id);
            if (loai == null)
            {
                return NotFound();
            }
            return View(loai);
        }

        // POST: Admin/Loais/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaLoai,TenLoai,TenLoaiAlias,MoTa,Hinh")] Loai loai)
        {
            if (id != loai.MaLoai)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loai);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoaiExists(loai.MaLoai))
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
            return View(loai);
        }

        // GET: Admin/Loais/Delete/5
        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loai = await _context.Loais
                .FirstOrDefaultAsync(m => m.MaLoai == id);
            if (loai == null)
            {
                return NotFound();
            }

            return View(loai);
        }

        // POST: Admin/Loais/Delete/5
        [HttpPost("delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loai = await _context.Loais.FindAsync(id);
            if (loai != null)
            {
                _context.Loais.Remove(loai);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoaiExists(int id)
        {
            return _context.Loais.Any(e => e.MaLoai == id);
        }
    }
}
