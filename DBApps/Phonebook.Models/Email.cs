namespace Phonebook.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Email
    {
        [Key]
        public int Id { get; set; }

        public string EmailAdress { get; set; }
    }
}
