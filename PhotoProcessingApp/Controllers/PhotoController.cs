// Controllers/UserController.cs
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using PhotoProcessingApp;
using PhotoProcessingApp.DTOs;
using PhotoProcessingApp.Infrastructure;
using PhotoProcessingApp.Model;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IPublishEndpoint _publishEndpoint;
    

    public UserController(ApplicationDbContext context, IPublishEndpoint publishEndpoint)
    {
        _context = context;
        _publishEndpoint = publishEndpoint;
    }

    [HttpPost("register")]
    public async Task<IActionResult> UserRegistration([FromForm] UserDto userDto)
    {
        if (ModelState.IsValid)
        {
            if (userDto.UserPhoto == null || userDto.UserPhoto.Length == 0)
                return Content("File not selected");

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", userDto.UserPhoto.FileName);
            Directory.CreateDirectory(Path.GetDirectoryName(path));

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await userDto.UserPhoto.CopyToAsync(stream);
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = userDto.Name,
                Email = userDto.Email,
                Password = userDto.Password,
                UserPhoto = path,
                UserPhotoId = Guid.NewGuid()
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            await _publishEndpoint.Publish(new PhotoProcessingEvent
            {
                UserId = user.Id,
                PhotoPath = path
            });

            return Ok("User registered and photo is being processed.");
        }

        return BadRequest(ModelState);
    }
}
