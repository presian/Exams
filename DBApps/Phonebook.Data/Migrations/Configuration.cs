namespace Phonebook.Data.Migrations
{
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Models;

    public sealed class PhonebookEntitiesConfiguration : DbMigrationsConfiguration<PhonebookEntities>
    {
        public PhonebookEntitiesConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(PhonebookEntities context)
        {
            if (!context.Contacts.Any())
            {
                context.Contacts.Add(new Contact
                {
                    Name = "Peter Ivanov",
                    Position = "CTO",
                    Company = "Smart Ideas",
                    Emails = new List<Email>
                    {
                        new Email
                        {
                            EmailAdress = "peter@gmail.com"
                        },
                        new Email
                        {
                            EmailAdress = "peter_ivanov@yahoo.com"
                        }
                    },
                    Phones = new List<Phone>
                    {
                        new Phone
                        {
                            PhoneNumber = "+359 2 22 22 22"
                        },
                        new Phone
                        {
                            PhoneNumber = "+359 88 77 88 99"
                        }
                    },
                    Site = "http://blog.peter.com",
                    Note = "Friend from school"

                });

                context.Contacts.Add(new Contact
                {
                    Name = "Maria",
                    Phones = new List<Phone>
                    {
                        new Phone
                        {
                            PhoneNumber = "+359 22 33 44 55"
                        }
                    }
                });

                context.Contacts.Add(new Contact
                {
                    Name = "Angie Stanton",
                    Emails = new List<Email>
                    {
                        new Email
                        {
                            EmailAdress = "info@angiestanton.com"
                        }
                    },
                    Site = "http://angiestanton.com"
                });
            }




            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
