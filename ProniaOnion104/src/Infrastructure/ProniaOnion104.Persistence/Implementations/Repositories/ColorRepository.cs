using ProniaOnion104.Application.Abstractions.Repositories;
using ProniaOnion104.Domain.Entities;
using ProniaOnion104.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion104.Persistence.Implementations.Repositories
{
    public class ColorRepository:Repository<Color>,IColorRepository
    {
        public ColorRepository(AppDbContext context) : base(context) { }
      
    }
}
