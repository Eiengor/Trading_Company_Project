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
        "5. Show user by id.\n" +
        "6. Show bank card by id.\n" +
        "7. Delete user by id.\n" +
        "8. Delete bank card by id.\n" +
        "0. Exit.");
        string option = Console.ReadLine();

        switch (option)
        {
            case "1":
                AddNewUser();
                continue;

            case "2":
                Console.WriteLine("Write owner ID: ");
                int ownerID = Convert.ToInt32(Console.ReadLine());
                AddNewBankCard(ownerID);
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

            case "7":
                DeleteUserById();
                continue;

            case "8":
                DeleteBankCardById();
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
        AddNewBankCard(newUser.UserId);
        Console.WriteLine($"Added\n{newUser.UserId}| {newUser.UserLogin}\n" +
            $"\t{newUser.FirstName} {newUser.LastName}\n" +
            $"\t{newUser.UserAddress}, \t{newUser.Gender}, \t{newUser.Email}\n\t{newUser.PhoneNumber}");
    }
    catch(Exception ex)
    {
        Console.WriteLine("Wrong value");
    }
}
void AddNewBankCard(int ownerID)
{
    try
    {
        Console.WriteLine("Number of card: ");
        string number = Console.ReadLine();
        Console.WriteLine("PIN: ");
        int pin = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("CVV: ");
        int cvv = Convert.ToInt32(Console.ReadLine());

        var bankCard = new BankCard
        {
            Number = number,
            PIN = pin,
            CVV = cvv,
            OwnerId = ownerID
        };

        BankCard newBankCard = bankCardDal.InsertBankCard(bankCard);
        Console.WriteLine($"Added\n{newBankCard.BankCardId}| {newBankCard.Number}" +
            $"\t{newBankCard.PIN},  {newBankCard.CVV}");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Wrong value");
    }

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
void DeleteUserById()
{
    Console.WriteLine("Write user ID to delete: ");
    int userID = Convert.ToInt32( Console.ReadLine() );
    userInfoDal.DeleteUser(userID);
    Console.WriteLine("User deleted");
}
void DeleteBankCardById()
{
    Console.WriteLine("Write bank card ID to delete: ");
    int bankCardID = Convert.ToInt32(Console.ReadLine());
    bankCardDal.DeleteBankCard(bankCardID);
    Console.WriteLine("Bank card deleted");
}
void UpdateUserById()
{
    Console.WriteLine("Write user ID to edit: ");
    int userId = Convert.ToInt32( Console.ReadLine() ) ;
    Console.WriteLine("Pick property to edit (only one): \n" +
     "1. Login\n" +
     "2. Password\n" +
     "3. Password Keyword\n" +
     "4. First Name\n" +
     "5. Last Name\n" +
     "6. Gender\n" +
     "7. Address\n" +
     "8. Email\n" +
     "9. Phone Number\n" +
     "0. Exit\n" +
     "Option: ");

    int choice = Convert.ToInt32(Console.ReadLine());

    switch (choice)
    {
        case 1:
            Console.WriteLine("Enter new Login:");
            string newLogin = Console.ReadLine();
            userInfoDal.UpdateUser(userId, "user_login", newLogin);
            break;
        
        case 2:
            Console.WriteLine("Enter new Password:");
            string newPassword = Hashing.HashPassword(Console.ReadLine());
            userInfoDal.UpdateUser(userId, "hashpassword", newPassword);
            break;
       
        case 3:
            Console.WriteLine("Enter new Password Keyword:");
            string newPasswordKeyword = Console.ReadLine();
            userInfoDal.UpdateUser(userId, "password_keyword", newPasswordKeyword);
            break;
        case 4:
            Console.WriteLine("Enter new First Name:");
            string newFirstName = Console.ReadLine();
            userInfoDal.UpdateUser(userId, "first_name", newFirstName);
            break;
        case 5:
            Console.WriteLine("Enter new Last Name:");
            string newLastName = Console.ReadLine();
            userInfoDal.UpdateUser(userId, "last_name", newLastName);
            break;
        case 6:
            Console.WriteLine("Enter new Gender(F/M):");
            string newGender = Console.ReadLine();
            userInfoDal.UpdateUser(userId, "gender", newGender);
            break;
        case 7:
            Console.WriteLine("Enter new Address:");
            string newAddress = Console.ReadLine();
            userInfoDal.UpdateUser(userId, "user_address", newAddress);
            break;
        case 8:
            Console.WriteLine("Enter new Email:");
            string newEmail = Console.ReadLine();
            userInfoDal.UpdateUser(userId, "email", newEmail);
            break;
        case 9:
            Console.WriteLine("Enter new Phone Number:");
            string newPhoneNumber = Console.ReadLine();
            userInfoDal.UpdateUser(userId, "phone_number", newPhoneNumber);
            break;
        case 0:
            break;
        default:
            Console.WriteLine("Invalid choice. Please try again.");
            break;
    }


}
void UpdateBankCardById()
{
    Console.WriteLine("Write bank card ID to edit: ");
    int bankCardId = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("Pick property to edit (only one): \n" +
    "1. Number\n" +
    "2. CVV\n" +
    "3. PIN\n" +
    "4. Owner ID\n" +
    "0. Exit\n" +
    "Option: ");

    int choice = Convert.ToInt32(Console.ReadLine());

    switch (choice)
    {
        case 1:
            Console.WriteLine("Enter new Card Number:");
            string newNumber = Console.ReadLine();
            bankCardDal.UpdateBankCard(bankCardId, "number", newNumber);
            break;

        case 2:
            Console.WriteLine("Enter new CVV:");
            string newCVV = Console.ReadLine();
            bankCardDal.UpdateBankCard(bankCardId, "cvv", newCVV);
            break;

        case 3:
            Console.WriteLine("Enter new PIN:");
            string newPIN = Console.ReadLine();
            bankCardDal.UpdateBankCard(bankCardId, "pin", newPIN);
            break;

        case 4:
            Console.WriteLine("Enter new Owner ID:");
            string newOwnerId = Console.ReadLine();
            bankCardDal.UpdateBankCard(bankCardId, "owner_id", newOwnerId);
            break;
        
        case 0:
            break;
        
        default:
            Console.WriteLine("Invalid choice. Please try again.");
            break;
    }

}