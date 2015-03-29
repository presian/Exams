namespace Phonebook.Data
{
    using System.Data.Entity;

    using Models;
    public class PhonebookEntities : DbContext
    {
        public PhonebookEntities()
            : base("PhonebookEntities")
        {
        }

        public virtual DbSet<Contact> Contacts { get; set; }

        public virtual DbSet<Phone> Phones { get; set; }

        public virtual DbSet<Email> Emails { get; set; }
    }
}
