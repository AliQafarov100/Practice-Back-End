using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _6hours_Back_End.DAL;
using _6hours_Back_End.Models;
using Microsoft.EntityFrameworkCore;

namespace _6hours_Back_End.Service
{
    public class LayoutService
    {
        private readonly AppDbContext _context;

        public LayoutService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<AnotherSetting>> GetDatas()
        {
            List<AnotherSetting> setting = await _context.AnotherSettings.ToListAsync();

            return setting;
        }


    }
}
