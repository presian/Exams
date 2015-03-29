namespace ListAllContacts
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    using Phonebook.Data;
    using Phonebook.Data.Migrations;

    class ListAllContacts
    {
        static void Main()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<PhonebookEntities,
                PhonebookEntitiesConfiguration>());

            using (var db = new PhonebookEntities())
            {
                foreach (var contact in db.Contacts
                    .Select(c=> new
                    {
                        c.Name,
                        c.Position,
                        c.Company,
                        Emails = c.Emails.Select(e => e.EmailAdress),
                        Phones = c.Phones.Select(p => p.PhoneNumber),
                        c.Site,
                        c.Note
                    }))
                {
                    Console.WriteLine("++++++++++++");
                    Console.WriteLine(contact.Name);
                    Console.WriteLine(contact.Position);
                    Console.WriteLine(contact.Company);
                    foreach (var email in contact.Emails)
                    {
                        Console.WriteLine(email);
                    }

                    foreach (var phone in contact.Phones)
                    {
                        Console.WriteLine(phone);
                    }

                    Console.WriteLine(contact.Site);
                    Console.WriteLine(contact.Note);
                }
            }
        }
    }
}
