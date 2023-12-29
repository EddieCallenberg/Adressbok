using Adressbok.Models;
using Adressbok.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        ///Summary
        ///Anger sökvägen till filen som appen ska använda.
        ///Summary
        var contactService = new ContactService(new FileService(@"C:\CSharp-projects-school\contacts.json"));
        var menuService = new MenuService();
        //Lägger till kontakter i filen, därför at jag tar bort filen när programmet stängs i switchen i MenuService.
        Contact defaultContact1 = new Contact("Eddie", "Callenberg", "0735063585", "eddie@domain.com", "Adressgatan 11");
        Contact defaultContact2 = new Contact("Minnie", "Lind", "0726044647", "minnie@domanin.com", "Adressgatan 11");

        contactService.SaveContactToContactlist(defaultContact1);
        contactService.SaveContactToContactlist(defaultContact2);
        //Visar menyn och laddar in kontakter.
        contactService.LoadContacts();
        menuService.ShowMenu();
    }
}