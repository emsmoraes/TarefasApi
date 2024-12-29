using Microsoft.AspNetCore.Mvc;
using TarefasApi.Dtos;
using TarefasApi.Dtos.Users;
using TarefasApi.Models;
using TarefasApi.Services;

namespace TarefasApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAllAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _userService.GetByIdAsync(id);
        if (user is null) return NotFound();
        return Ok(user);
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Add([FromForm] CreateUserDto createUserDto)
    {
        if (createUserDto.ProfilePicture == null)
            return BadRequest("Profile picture is required.");

        var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

        if (!Directory.Exists(uploadPath))
            Directory.CreateDirectory(uploadPath);

        var fileName = $"{Guid.NewGuid()}_{createUserDto.ProfilePicture.FileName}";
        var filePath = Path.Combine(uploadPath, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await createUserDto.ProfilePicture.CopyToAsync(stream);
        }

        var user = new User
        {
            Name = createUserDto.Name,
            Email = createUserDto.Email,
            Idade = createUserDto.Idade,
            ProfilePictureUrl = $"/uploads/{fileName}"
        };

        await _userService.AddAsync(user);

        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDto updateUserDto)
    {
        var user = await _userService.GetByIdAsync(id);
        if (user is null) return NotFound();

        user.Name = updateUserDto.Name;
        user.Email = updateUserDto.Email;
        user.Idade = updateUserDto.Idade;

        await _userService.UpdateAsync(user);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await _userService.GetByIdAsync(id);
        if (user is null) return NotFound();

        await _userService.DeleteAsync(user.Id);
        return NoContent();
    }
}