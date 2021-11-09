using System;
using System.Collections.Generic;

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

        //public virtual ICollection<User> Clients { get; set; }// = new List<User>();
        public virtual ICollection<User> Clients { get; set; }
        public DateTime Time { get; set; }
    }
}
