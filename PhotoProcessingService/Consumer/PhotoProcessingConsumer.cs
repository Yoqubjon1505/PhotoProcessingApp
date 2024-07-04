using MassTransit;
using PhotoProcessingApp;
using PhotoProcessingService.Infrastructure;
using PhotoProcessingService.Model;
using System;
using System.Threading.Tasks;

public class PhotoProcessingConsumer : IConsumer<PhotoProcessingEvent>
{
    private readonly AppDbContext _context;

    public PhotoProcessingConsumer(AppDbContext context)
    {
        _context = context;
    }

    public async Task Consume(ConsumeContext<PhotoProcessingEvent> context)
    {
        var photoEvent = context.Message;

        // Process photo and create different sizes
        var smallPath = ProcessPhoto(photoEvent.PhotoPath, "small");
        var mediumPath = ProcessPhoto(photoEvent.PhotoPath, "medium");
        var largePath = ProcessPhoto(photoEvent.PhotoPath, "large");

        var photo = new Photo
        {
            Id = Guid.NewGuid(),
            UserId = photoEvent.UserId,
            OriginalPath = photoEvent.PhotoPath,
            SmallPath = smallPath,
            MediumPath = mediumPath,
            LargePath = largePath
        };

        _context.Photos.Add(photo);
        await _context.SaveChangesAsync();

        // Notify user (optional implementation)
    }

    private string ProcessPhoto(string photoPath, string size)
    {
        // Implement photo resizing logic here and return the new path
        return photoPath.Replace("original", size);
    }
}
