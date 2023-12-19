using Adressbok.Models;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Adressbok.Services
{
    public class ContactService
    {
        private readonly FileService _fileService = new FileService(@"C:\CSharp-projects-school\contacts.json");
        private List<Contact> _contactList = new List<Contact>();

        public void SaveContactToContactlist(Contact contact)
        {
            try
            {
                LoadContacts();
                if (!_contactList.Any(c => c.Email == contact.Email))
                {
                    _contactList.Add(contact);
                    _fileService.SaveContactToFile(JsonConvert.SerializeObject(_contactList));
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex); }
        }
        private void LoadContacts()
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

        public void RemoveContact()
        {
            Console.Clear();
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
    }
}
