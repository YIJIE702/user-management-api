using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using UserManagementAPI.Services;
using UserManagementAPI.Models;
using UserManagementAPI.DTOs;

namespace UserManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _svc;

        public UsersController(IUserService svc)
        {
            _svc = svc;
        }

        // GET api/users
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _svc.GetAll();
            return Ok(users);
        }

        // GET api/users/{id}
        [HttpGet("{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            var user = _svc.GetById(id);
            if (user == null) return NotFound(new { message = "User not found." });
            return Ok(user);
        }

        // POST api/users
        [HttpPost]
        public IActionResult Create([FromBody] UserCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (_svc.EmailExists(dto.Email))
            {
                return Conflict(new { message = "Email already in use." });
            }

            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Role = dto.Role
            };

            var created = _svc.Create(user);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT api/users/{id}
        [HttpPut("{id:guid}")]
        public IActionResult Update(Guid id, [FromBody] UserUpdateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existing = _svc.GetById(id);
            if (existing == null) return NotFound(new { message = "User not found." });

            if (_svc.EmailExists(dto.Email, excludeId: id))
            {
                return Conflict(new { message = "Email already in use." });
            }

            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Role = dto.Role
            };

            var ok = _svc.Update(id, user);
            if (!ok) return StatusCode(500, new { message = "Failed to update user." });
            return NoContent();
        }

        // DELETE api/users/{id}
        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            var existing = _svc.GetById(id);
            if (existing == null) return NotFound(new { message = "User not found." });

            var ok = _svc.Delete(id);
            if (!ok) return StatusCode(500, new { message = "Failed to delete user." });
            return NoContent();
        }
    }
}
