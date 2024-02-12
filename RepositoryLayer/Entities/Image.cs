using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RepositoryLayer.Entities
{
    public class Image
    {
        public int ImageId { get; set; }

        public string ImageName {  get; set; }

        public string ImageUrl { get; set; }
        [ForeignKey("Notes")]
        public long Noteid {  get; set; }

        [JsonIgnore]
        public virtual Notes Notes { get; set; }
    }
}
