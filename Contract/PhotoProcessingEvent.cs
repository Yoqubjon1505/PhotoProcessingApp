namespace PhotoProcessingApp
{
    public class PhotoProcessingEvent
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string OriginalPath { get; set; }
        public string SmallPath { get; set; }
        public string MediumPath { get; set; }
        public string LargePath { get; set; }
        public string PhotoPath { get; set; }
    }
}

