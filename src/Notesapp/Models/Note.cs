using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Notesapp.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public string Image { get; set; }
        public string Owner { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}