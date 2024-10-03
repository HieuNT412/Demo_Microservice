using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CustomerController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        // GET: api/products/5
        [HttpGet("{username}")]
        public async Task<ActionResult<Customer>> GetCustomer(string username)
        {
            var customer = await _context.Customers.FindAsync(username);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // POST: api/products
        [HttpPost]
        public async Task<ActionResult<Customer>> CreateCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCustomer), new { username = customer.Username }, customer);
        }

        // PUT: api/customers/{username}
        [HttpPut("{username}")]
        public async Task<IActionResult> UpdateCustomer(string username, Customer customer)
        {
            if (username != customer.Username)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(username))
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

        private bool CustomerExists(string username)
        {
            return _context.Customers.Any(e => e.Username == username);
        }


        // DELETE: api/products/5
        [HttpDelete("{username}")]
        public async Task<IActionResult> DeleteCustomer(string username)
        {
            var customer = await _context.Customers.FindAsync(username);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}