using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
    public class TransactionController : ControllerBase
    {
        private readonly MoneyMinderContext _context;

        public TransactionController(MoneyMinderContext context)
        {
            _context = context;
        }

        // GET: api/Transaction
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
        {
            return await _context.Transactions.ToListAsync();
        }

        // GET: api/Transaction/User/Id
        [HttpGet("User/{userid}")]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions(int userid)
        {
            return await _context.Transactions.Where(t=>t.UserID==userid).ToListAsync();
        }

        // GET: api/Transaction/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetTransaction(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);

            if (transaction == null)
            {
                return NotFound();
            }

            return transaction;
        }

        // PUT: api/Transaction/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransaction(int id, Transaction transaction)
        {
            if (id != transaction.TransactionID)
            {
                return BadRequest();
            }

            _context.Entry(transaction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(id))
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

        // POST: api/Transaction
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Transaction>> PostTransaction(Transaction transaction)
        {
            if (transaction != null)
            { 
            if (transaction?.TransactionDate == null || transaction?.TransactionDate.ToString() == "")
            {
                transaction.TransactionDate = DateTime.Today;
            }
            if (transaction?.TransactionDate != null)
            {
                _context.Transactions.Add(transaction);
            }
            await _context.SaveChangesAsync();

                return CreatedAtAction("GetTransaction", new { id = transaction.TransactionID }, transaction);
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE: api/Transaction/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("user")]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetUserTransactions()
        {
            // Retrieve user ID from token
            var userId = GetUserIdFromToken();

            if (userId == -1)
            {
                return Unauthorized();
            }

            // Fetch transactions for the logged-in user
            var transactions = await _context.Transactions
                .Where(t => t.UserID == userId)
                .ToListAsync();

            return Ok(transactions);
        }

        private int GetUserIdFromToken()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }
            else
            {
                return -1;
            }
        }

        private bool TransactionExists(int id)
        {
            return _context.Transactions.Any(e => e.TransactionID == id);
        }
    }
}
