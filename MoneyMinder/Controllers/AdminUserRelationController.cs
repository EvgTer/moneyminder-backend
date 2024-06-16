using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoneyMinder.Helpers.DbClasses;
using MoneyMinder.Models;

namespace MoneyMinder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminUserRelationController : ControllerBase
    {
        private readonly MoneyMinderContext _context;

        public AdminUserRelationController(MoneyMinderContext context)
        {
            _context = context;
        }

        // GET: api/AdminUserRelation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdminUserRelation>>> GetAdminUserRelations()
        {
            return await _context.AdminUserRelations.ToListAsync();
        }

        // GET: api/AdminUserRelation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdminUserRelation>> GetAdminUserRelation(int id)
        {
            var adminUserRelation = await _context.AdminUserRelations.FindAsync(id);

            if (adminUserRelation == null)
            {
                return NotFound();
            }

            return adminUserRelation;
        }

        // PUT: api/AdminUserRelation/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdminUserRelation(int id, AdminUserRelation adminUserRelation)
        {
            if (id != adminUserRelation.AdminID)
            {
                return BadRequest();
            }

            _context.Entry(adminUserRelation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminUserRelationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/AdminUserRelation
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AdminUserRelation>> PostAdminUserRelation(AdminUserRelation adminUserRelation)
        {
            _context.AdminUserRelations.Add(adminUserRelation);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AdminUserRelationExists(adminUserRelation.AdminID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAdminUserRelation", new { id = adminUserRelation.AdminID }, adminUserRelation);
        }

        // DELETE: api/AdminUserRelation/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdminUserRelation(int id)
        {
            var adminUserRelation = await _context.AdminUserRelations.FindAsync(id);
            if (adminUserRelation == null)
            {
                return NotFound();
            }

            _context.AdminUserRelations.Remove(adminUserRelation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdminUserRelationExists(int id)
        {
            return _context.AdminUserRelations.Any(e => e.AdminID == id);
        }
    }
}
