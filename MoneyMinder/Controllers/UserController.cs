using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoneyMinder.Helpers;
using MoneyMinder.Helpers.DbClasses;
using MoneyMinder.Models;

namespace MoneyMinder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly MoneyMinderContext _context;

        public UserController(MoneyMinderContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // GET: api/User/Transactions
        [HttpGet("Transactions")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersWithTransactions()
        {
            var users = await _context.Users.Include(u => u.Transactions).ToListAsync();
            return users;
        }

        // POST: api/User/login
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] UserLoginDto userLogin)
        {
            // Authenticate user
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userLogin.Email);
            if (user == null || !VerifyPassword(userLogin.Password, user.PasswordHash))
            {
                return BadRequest("Invalid email or password");
            }

            // Generate JWT token
            string secretKey = "ProjectMoneyMinder322";
            string issuer = "BCrypt";
            string audience = "MoneyMinder";

            var jwtService = new JwtService(secretKey, issuer, audience);
            var token = jwtService.GenerateToken(user.UserID);

            return Ok(new { token });
        }

        private bool VerifyPassword(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserID)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/User
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            try
            {
                // Ensure to set any necessary default values before saving
                user.CreateDate = DateTime.UtcNow; // Example: Setting a creation date

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetUser", new { id = user.UserID }, user);
            }
            catch (Exception ex)
            {
                // Log detailed error and return appropriate response
                Console.WriteLine("Error during registration: " + ex.Message);
                return StatusCode(500, new { message = "Error during registration: " + ex.Message });
            }
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            try
            {
                await DeleteAllUserTransactions(user.UserID);
            }
            finally
            {
                _context.Users.Remove(user);
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/User/5/transaction/all/delete
        [HttpDelete("{id}/transaction/all/delete")]
        public async Task<IActionResult> DeleteAllUserTransactions(int id)
        {
            var user = await _context.Users.Include(u => u.Transactions).FirstOrDefaultAsync(u => u.UserID == id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Transactions.RemoveRange(user.Transactions);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserID == id);
        }
    }
}
