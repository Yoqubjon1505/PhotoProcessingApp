using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoProcessingService.Model
{
    public class Photo
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string OriginalPath { get; set; }
        public string SmallPath { get; set; }
        public string MediumPath { get; set; }
        public string LargePath { get; set; }
  
    }
}
