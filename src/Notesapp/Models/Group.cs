using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Notesapp.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
        public string Owner { get; set; }
    }

    public static class GroupExtensions
    {
        public static IQueryable<Group> ForUser(this IQueryable<Group> groups, string userName)
        {
            return groups.Where(g => g.Owner == userName);
        }
    }

}