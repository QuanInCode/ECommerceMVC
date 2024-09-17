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
    [Route("/KhachHang")]
    public class KhachHangsController : Controller
    {
        private readonly Hshop2023Context _context;

        public KhachHangsController(Hshop2023Context context)
        {
            _context = context;
        }
        [Route("")]
        [Route("index")]
        // GET: Admin/KhachHangs
        public async Task<IActionResult> Index()
        {
            return View(await _context.KhachHangs.ToListAsync());
        }
        [HttpGet("Details/{id}")]
        // GET: Admin/KhachHangs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khachHang = await _context.KhachHangs
                .FirstOrDefaultAsync(m => m.MaKh == id);
            if (khachHang == null)
            {
                return NotFound();
            }

            return View(khachHang);
        }
        [HttpGet("Create")]
        // GET: Admin/KhachHangs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/KhachHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaKh,MatKhau,HoTen,GioiTinh,NgaySinh,DiaChi,DienThoai,Email,Hinh,HieuLuc,VaiTro,RandomKey")] KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(khachHang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(khachHang);
        }
        [HttpGet("Edit/{id}")]
        // GET: Admin/KhachHangs/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khachHang = await _context.KhachHangs.FindAsync(id);
            if (khachHang == null)
            {
                return NotFound();
            }
            return View(khachHang);
        }

        // POST: Admin/KhachHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaKh,MatKhau,HoTen,GioiTinh,NgaySinh,DiaChi,DienThoai,Email,Hinh,HieuLuc,VaiTro,RandomKey")] KhachHang khachHang)
        {
            if (id != khachHang.MaKh)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(khachHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KhachHangExists(khachHang.MaKh))
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
            return View(khachHang);
        }
        [HttpGet("Delete/{id}")]
        // GET: Admin/KhachHangs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khachHang = await _context.KhachHangs
                .FirstOrDefaultAsync(m => m.MaKh == id);
            if (khachHang == null)
            {
                return NotFound();
            }

            return View(khachHang);
        }

        // POST: Admin/KhachHangs/Delete/5
        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var khachHang = await _context.KhachHangs.FindAsync(id);
            if (khachHang != null)
            {
                _context.KhachHangs.Remove(khachHang);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KhachHangExists(string id)
        {
            return _context.KhachHangs.Any(e => e.MaKh == id);
        }
    }
}
