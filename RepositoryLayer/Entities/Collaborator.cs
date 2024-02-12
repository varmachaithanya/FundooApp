using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RepositoryLayer.Entities
{
    public class Collaborator
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CollaboratorId {  get; set; }

        public string CollaboratorEmail { get; set; }

        [ForeignKey("User")]
        public long UserId {  get; set; }

        [JsonIgnore]
        public virtual User User { get; set; }

        [ForeignKey("Notes")]

        public long NoteId {  get; set; }

        [JsonIgnore]
        public virtual Notes Notes { get; set; }
    }
}
