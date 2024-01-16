﻿using BE.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BE.ViewCommponents
{
    public class FooterViewCommponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public FooterViewCommponent(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            Dictionary<string, string> settings = await _context.Settings.ToDictionaryAsync(x => x.Key, y => y.Value);
            return View(settings);
        }
    }
}
