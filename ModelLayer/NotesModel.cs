using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class NotesModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Color { get; set; }

        public ICollection<IFormFile> Imagepaths { get; set; }

        [DefaultValue("2022-05-20 12:12:55:389Z")]
        public DateTime Reminder { get; set; }

        public bool IsArchive { get; set; }

        public bool IsPinned { get; set; }

        public bool IsTrash { get; set; }
        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }

        public long UserId { get; set; }
    }
}
