using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MfcUfmt.Data;
using MfcUfmt.Models;
using Microsoft.EntityFrameworkCore;

namespace MfcUfmt.Services
{
    public class TrabalhoService
    {
        private readonly MfcUfmtContext _context;

        public TrabalhoService(MfcUfmtContext context)
        {
            _context = context;
        }

        public async Task<List<Trabalho>> FindAllAsync()
        {
            return await _context.Trabalho.OrderBy(x => x.NomeTrabalho).ToListAsync();
        }


    }

}
