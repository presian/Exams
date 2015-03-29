namespace Phonebook.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Contact
    {
        public Contact()
        {
            this.Emails = new List<Email>();
            this.Phones = new List<Phone>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Position { get; set; }

        public string Company { get; set; }

        public virtual List<Email> Emails { get; set; }

        public virtual List<Phone> Phones { get; set; }

        public string Site { get; set; }

        public string Note { get; set; }
    }
}
