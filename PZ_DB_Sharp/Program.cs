using Microsoft.Extensions.Configuration;
using DAL.Concrete;
using DTO;
using PZ_DB_Sharp;
using System.Reflection;
IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("config.json")
    .Build();

string connectionString = configuration.GetConnectionString("TradingCompany") ?? "";
var userInfoDal = new UserInfoDal(connectionString);
var bankCardDal = new BankCardDal(connectionString);

Console.WriteLine("You opened a Trading company\n");
while (true)
{

        Console.WriteLine("Please select an option:\n" +
        "1. Register a new user.\n" +
        "2. Add a new bank card.\n" +
        "3. Show users' list.\n" +
        "4. Show bank cards' list.\n" +
        "0. Exit.");
        string option = Console.ReadLine();

        switch (option)
        {
            case "1":
                AddNewUser();
                continue;

            case "2":
                AddNewBankCard();
                continue;

            case "3":
                UserList();
                continue;

            case "4":
                BankCardList();
                continue;

            case "5":
                ShowUserById();
                continue;

            case "6":
                ShowBankCardById();
                continue;

            case "0":
                Console.WriteLine("Good bye! Have a nice day!\n");
                break;

            default:
                Console.WriteLine("Invalid option, please try again.");
                continue;
        }
}

void AddNewUser()
{
    try
    {
        Console.WriteLine("User login: ");
        string userLogin = Console.ReadLine();
        Console.WriteLine("Password: ");
        string hashpassword = Hashing.HashPassword(Console.ReadLine());
        Console.WriteLine("Keyword for password: ");
        string keyword = Console.ReadLine();
        Console.WriteLine("Firs Name: ");
        string firstName = Console.ReadLine();
        Console.WriteLine("Last Name: ");
        string lastName = Console.ReadLine();
        Console.WriteLine("Gender (F/M): ");
        string gender = Console.ReadLine();
        Console.WriteLine("User Addres: ");
        string userAddres = Console.ReadLine();
        Console.WriteLine("Email: ");
        string email = Console.ReadLine();
        Console.WriteLine("Phone number: ");
        string phoneNumber = Console.ReadLine();

        var user = new UserInfo
        {
            UserLogin = userLogin,
            FirstName = firstName,
            LastName = lastName,
            HashPassword = hashpassword,
            PasswordKeyword = keyword,
            Gender = gender,
            UserAddress = userAddres,
            Email = email,
            PhoneNumber = phoneNumber
        };
        
        UserInfo newUser = userInfoDal.InsertUser(user);
        Console.WriteLine($"{newUser.UserId}| {newUser.UserLogin}\n" +
            $"\t{newUser.FirstName} {newUser.LastName}\n" +
            $"\t{newUser.UserAddress}, \t{newUser.Gender}, \t{newUser.Email}\n\t{newUser.PhoneNumber}");
    }
    catch(Exception ex)
    {
        Console.WriteLine("Wrong value");
    }
}
void AddNewBankCard()
{
    
}
void UserList()
{
    List<UserInfo> users = userInfoDal.GetAll();
    foreach (UserInfo user in users)
    {
        Console.WriteLine($"{user.UserId}| {user.UserLogin}\n" +
            $"\t{user.FirstName} {user.LastName}\n" +
            $"\t{user.UserAddress}, \t{user.Gender}, \t{user.Email}");
    }
}
void BankCardList()
{
    List<BankCard> bankCards = bankCardDal.GetAll();
    foreach(BankCard bankCard in bankCards)
    {
        Console.WriteLine($"{bankCard.BankCardId}|  {bankCard.Number}  -  Owner:{bankCard.OwnerId}");
    }
}
void ShowUserById()
{
    Console.WriteLine("Enter User ID: ");
    int userID = Convert.ToInt32( Console.ReadLine() );
    var user = userInfoDal.GetById(userID);
    Console.WriteLine($"{user.UserId}|  {user.UserLogin}\n\t{user.FirstName}{user.LastName}");
}
void ShowBankCardById()
{
    Console.WriteLine("Enter Bank card ID: ");
    int bankCardID = Convert.ToInt32( Console.ReadLine() );
    var bankCard = bankCardDal.GetById(bankCardID);
    Console.WriteLine($"{bankCard.BankCardId}|  {bankCard.Number}");
}