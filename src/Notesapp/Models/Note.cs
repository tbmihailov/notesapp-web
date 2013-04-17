using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Spatial;

namespace Notesapp.Models
{
    public class Note
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Content { get; set; }
        [Display(AutoGenerateField = false)]
        public DateTime Created { get; set; }
        //public System.Data.Spatial.DbGeography Location { get; set; }
        public float Lat { get; set; }
        public float Lon { get; set; }
        public string Image { get; set; }
        public string Owner { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        
    }

    public static class NoteExtensions
    {
        public static IQueryable<Note> ForUser(this IQueryable<Note> notes, string userName)
        {
            return notes.Where(n => n.Owner == userName);
        }
    }
}