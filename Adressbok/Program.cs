using Adressbok.Models;
using Adressbok.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        var contactService = new ContactService();
        var menuService = new MenuService();
        Contact defaultContact1 = new Contact("Eddie", "Callenberg", "0735063585", "eddie@domain.com", "Adressgatan 11");
        Contact defaultContact2 = new Contact("Minnie", "Lind", "0726044647", "minnie@domanin.com", "Adressgatan 11");

        contactService.SaveContactToContactlist(defaultContact1);
        contactService.SaveContactToContactlist(defaultContact2);

        bool isRunning = true;
        while (isRunning)
        {
            menuService.ShowMenu();
        }
    }
}