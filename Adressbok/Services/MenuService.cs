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
        public void ShowMenu()
        {
            while (isRunning)
            {
                var contactService = new ContactService();
                Console.Clear();
                Console.WriteLine("Adress book");
                Console.WriteLine("1. Show all contacts");
                Console.WriteLine("2. Add contact");
                Console.WriteLine("3. Remove contact");

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
                        File.Delete(@"C:\CSharp-projects-school\contacts.json");
                        isRunning = false;
                        break;
                }
            }
        }
    }
}
