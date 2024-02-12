using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace RepositoryLayer.Entities
{
    public class Notes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long NoteID { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public string Color { get; set; }

        public DateTime Reminder { get; set; }

        public bool IsArchive { get; set; }

        public bool IsPinned { get; set; }

        public bool IsTrash { get; set; }


        public DateTime CreatedAt { get; set; }


        public DateTime ModifiedAt { get; set; }

        [ForeignKey("User")]
        public long UserId {  get; set; }

        [JsonIgnore]
        public virtual User User { get; set; }

    }
}
