using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adressbok.Services
{
    internal class MenuService
    {
        bool isRunning = true;

        /// <summary>
        /// Visar menyn och hanterar användarens val.
        /// </summary>
        public void ShowMenu()
        {
            while (isRunning)
            {
                var contactService = new ContactService(new FileService(@"C:\CSharp-projects-school\contacts.json"));
                Console.Clear();
                Console.WriteLine("Adress book");
                Console.WriteLine("1. Show all contacts");
                Console.WriteLine("2. Add contact");
                Console.WriteLine("3. Remove contact");
                Console.WriteLine("4. Show specific contact");

                int userChoice = int.Parse(Console.ReadLine());
                switch (userChoice)
                {
                    case 1:
                        contactService.GetAllContacts();
                        break;
                    case 2:
                        contactService.AddContact();
                        break;
                    case 3:
                        contactService.RemoveContact();
                        break;
                    case 4:
                        contactService.ShowSpecificContact();
                        break;
                    case 5:
                        File.Delete(@"C:\CSharp-projects-school\contacts.json");
                        isRunning = false;
                        break;
                }
            }
        }
    }
}
