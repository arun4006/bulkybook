﻿using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class CoverTypeRepository : Repository<CoverType>, ICoverTypeRepository
    {
        private ApplicationDbContext _context;

        public CoverTypeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        //public void Save()
        //{
        //    _context.SaveChanges();
        //}

        public void Update(CoverType obj)
        {
            _context.CoverTypes.Update(obj);
        }
    }
}  
