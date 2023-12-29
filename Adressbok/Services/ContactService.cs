using Adressbok.Models;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Adressbok.Services
{
    /// <summary>
    /// Interface för att hantera kontakter.
    /// </summary>
    public interface IContactService
    {
        /// <summary>
        /// Lägger till en ny kontakt.
        /// </summary>
        void AddContact();

        /// <summary>
        /// Hämtar alla kontakter.
        /// </summary>
        IEnumerable<Contact> GetAllContacts();

        /// <summary>
        /// Tar bort en kontakt.
        /// </summary>
        void RemoveContact();

        /// <summary>
        /// Sparar en kontakt till kontaktlistan.
        /// </summary>
        void SaveContactToContactlist(Contact contact);
    }

    /// <summary>
    /// Implementering av IContactService.
    /// </summary>
    public class ContactService : IContactService
    {
        private readonly FileService _fileService;
        private List<Contact> _contactList = new List<Contact>();

        /// <summary>
        /// Konstruktor som tar emot en FileService och initierar kontaktlistan.
        /// </summary>
        public ContactService(FileService fileService)
        {
            _fileService = fileService;
        }

        /// <summary>
        /// Sparar en kontakt till kontaktlistan om den inte redan finns.
        /// </summary>
        public void SaveContactToContactlist(Contact contact)
        {
            LoadContacts();
            try
            {
                if (!_contactList.Any(c => c.Email == contact.Email))
                {
                    _contactList.Add(contact);
                    _fileService.SaveContactToFile(JsonConvert.SerializeObject(_contactList));
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex); }
        }

        /// <summary>
        /// Laddar befintliga kontakter från fil.
        /// </summary>
        public void LoadContacts()
        {
            var existingContactsJson = _fileService.GetContactFromFile();
            if (!string.IsNullOrEmpty(existingContactsJson))
            {
                _contactList = JsonConvert.DeserializeObject<List<Contact>>(existingContactsJson);
            }
            else
            {
                _contactList = new List<Contact>();
            }
        }

        /// <summary>
        /// Hämtar och visar alla kontakter.
        /// </summary>
        public IEnumerable<Contact> GetAllContacts()
        {
            try
            {
                Console.Clear();
                var content = _fileService.GetContactFromFile();
                if (!string.IsNullOrEmpty(content))
                {
                    _contactList = JsonConvert.DeserializeObject<List<Contact>>(content)!;
                    Console.WriteLine("All Contacts:");

                    foreach (var contact in _contactList)
                    {
                        Console.WriteLine($"{contact.FirstName} {contact.LastName}");
                        Console.WriteLine($"Phone: {contact.PhoneNumber}");
                        Console.WriteLine($"Email: <{contact.Email}>");
                        Console.WriteLine($"Adress: {contact.Address}");
                        Console.WriteLine("--------------------------------");
                    }
                }
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }

            return _contactList;
        }

        /// <summary>
        /// Lägger till en ny kontakt och sparar den.
        /// </summary>
        public void AddContact()
        {
            Console.Clear();
            Console.WriteLine("Add new contact:");
            Console.Write("Fist name: ");
            string? firstName = Console.ReadLine();

            Console.Write("Last name: ");
            string? lastName = Console.ReadLine();

            Console.Write("Phone number: ");
            string? phoneNumber = Console.ReadLine();

            Console.Write("Email: ");
            string? email = Console.ReadLine();

            Console.Write("Adress: ");
            string? address = Console.ReadLine();

            Contact? newContact = new Contact(firstName, lastName, phoneNumber, email, address);

            SaveContactToContactlist(newContact);
            Console.Clear();
            Console.WriteLine($"{firstName} added to contacts!");
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// Tar bort en kontakt med hjälp av e-postadress.
        /// </summary>
        public void RemoveContact()
        {
            Console.Clear();
            LoadContacts();
            Console.Write("Enter the Email of the contact you would like to remove: ");
            string? emailToRemove = Console.ReadLine();

            Contact? contactToRemove = _contactList.FirstOrDefault(c => c.Email == emailToRemove);

            if (contactToRemove != null)
            {
                _contactList.Remove(contactToRemove);
                Console.Clear();
                Console.WriteLine($"Contact with the email: {emailToRemove} has been removed.");

                _fileService.SaveContactToFile(JsonConvert.SerializeObject(_contactList));
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"No contact with the email: {emailToRemove} was found, please try again.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
        public void ShowSpecificContact()
        {
            Console.Clear();
            LoadContacts();
            Console.Write("Enter the Email of the contact you would like to view: ");
            string? emailToView = Console.ReadLine();

            Contact? contactToView = _contactList.FirstOrDefault(c => c.Email == emailToView);

            if (contactToView != null)
            {
                Console.Clear();
                Console.WriteLine($"Contact with the email: {emailToView} has been found.");
                Console.WriteLine(contactToView.FirstName);
                Console.WriteLine(contactToView.LastName);
                Console.WriteLine(contactToView.PhoneNumber);
                Console.WriteLine(contactToView.Address);
                Console.WriteLine(contactToView.Email);

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"No contact with the email: {emailToView} was found, please try again.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }
}
