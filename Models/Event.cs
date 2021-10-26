using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASUSport.Models
{
    public class Event
    {
        public int Id { get; set; }
        public virtual Section Section { get; set; }
        private User trainer;
        public virtual User Trainer
        {
            get { return trainer; }
            set
            {
                if (value.Role.Name.Trim().ToLower() == "trainer")
                    trainer = value;
            }
        }
        //public virtual List<string> Clients { get; set; } = new List<string>();

        [Column(TypeName = "jsonb")]
        public List<User> Clients { get; set; } = new List<User>();

        public DateTime Time { get; set; }
    }
}
