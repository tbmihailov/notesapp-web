using System;
using System.Collections.Generic;
using System.Linq;

namespace Notesapp.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public float Lat { get; set; }
        public float Lon { get; set; }
        public string Image { get; set; }
        public string Owner { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        
    }

}