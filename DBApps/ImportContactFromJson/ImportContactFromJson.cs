namespace ImportContactFromJson
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Newtonsoft.Json;

    using Helper;
    using Phonebook.Data;
    using Phonebook.Models;

    class ImportContactFromJson
    {
        static void Main()
        {
//            Database.SetInitializer(new MigrateDatabaseToLatestVersion<PhonebookEntities,
//                PhonebookEntitiesConfiguration>());

            var doc = File.ReadAllText(Utility.ImportFolder + "contacts.json");
            var objects = JsonConvert.DeserializeObject<List<ImportJson>>(doc);
            var db = new PhonebookEntities();
            foreach (var json in objects)
            {
                if (!db.Contacts.Any(c => c.Name == json.Name))
                {
                    try
                    {
                        CheckMandatoryField(json);
                        var currentEmails = GetEmails(json);
                        var currentPhones = GetPhones(json);

                        db.Contacts.Add(new Contact
                        {
                            Company = json.Company,
                            Emails = currentEmails,
                            Phones = currentPhones,
                            Name = json.Name,
                            Note = json.Note,
                            Position = json.Position,
                            Site = json.Site
                        });

                        db.SaveChanges();
                        Console.WriteLine("Contact {0} imported", json.Name);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine("This contact allready in database!");
                }
            }
        }

        private static List<Phone> GetPhones(ImportJson json)
        {
            var phones = new List<Phone>();
            if (json.Phones != null)
            {
                foreach (var phone in json.Phones)
                {
                    phones.Add(new Phone
                    {
                        PhoneNumber = phone
                    });
                }
            }
           

            return phones;
        }

        private static List<Email> GetEmails(ImportJson json)
        {
            var emails = new List<Email>();
            if (json.Emails != null)
            {
                foreach (var email in json.Emails)
                {
                    emails.Add(new Email
                    {
                        EmailAdress = email
                    });
                } 
            }

            return emails;
        }

        private static void CheckMandatoryField(ImportJson json)
        {
            if (json.Name == null)
            {
                throw new Exception("Name is requared");
            }
        }
    }
}
