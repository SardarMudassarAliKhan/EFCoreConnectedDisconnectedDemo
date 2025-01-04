using EFCoreConnectedDisconnectedDemo.ApplicationContext;
using EFCoreConnectedDisconnectedDemo.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreConnectedDisconnectedDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiUserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApiUserController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ApiUser/5
        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        // PUT: api/ApiUser/5
        [HttpPut("{id}")]
        public IActionResult PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            // In disconnected scenario, we must attach the entity back to the context and mark it as modified
            _context.Users.Attach(user);
            _context.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            try
            {
                _context.SaveChanges(); // Save changes to the database manually
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Users.Any(e => e.Id == user.Id))
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
    }
}
