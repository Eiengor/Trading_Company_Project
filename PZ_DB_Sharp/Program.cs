using DAL.Concrete;
using DTO;
using Microsoft.Extensions.Configuration;
using PZ_DB_Sharp;
public class Program
{
    public static int Main(string[] args)
    {
        IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("config.json")
    .Build();

        string connectionString = configuration.GetConnectionString("TradingCompany") ?? "";
        var userInfoDal = new UserInfoDal(connectionString);
        var bankCardDal = new BankCardDal(connectionString);
        var productDal = new ProductDal(connectionString);

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
            "9. Update user by id.\n" +
            "10. Update bank card by id.\n" +
            "11. Add a new product.\n" +
            "12. Show products' list.\n" +
            "13. Show product by id.\n" +
            "14. Update product by id.\n" +
            "15. Delete product by id.\n" +
            "0. Exit.\n\n" +
            "Option: ");
            string option = Console.ReadLine();
            Console.WriteLine("\n");
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

                case "9":
                    UpdateUserById();
                    continue;

                case "10":
                    UpdateBankCardById();
                    continue;
                case "11":
                    AddNewProduct();
                    continue;
                case "12":
                    ProductList();
                    continue;
                case "13":
                    ShowProductById();
                    continue;
                case "14":
                    UpdateProductById();
                    continue;
                case "15":
                    DeleteProductById();
                    continue;
                case "0":
                    {
                        Console.WriteLine("Good bye! Have a nice day!\n");
                        break;
                    }
                    

                default:
                    Console.WriteLine("Invalid option, please try again.");
                    continue;
            }
        }

        //
        //Create methods
        //
        void AddNewUser()
        {
            try
            {
                Console.WriteLine("\nUser login: ");
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
                    $"\t{newUser.FirstName} {newUser.LastName}" +
                    $"\n\t{newUser.UserAddress}, {newUser.Gender}, {newUser.Email}\t{newUser.PhoneNumber}");
            }
            catch (FormatException ex)
            {
                Console.WriteLine("\nWrong value");
            }
        }
        void AddNewBankCard(int ownerID)
        {
            try
            {
                Console.WriteLine("\nNumber of card: ");
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
            catch (FormatException ex)
            {
                Console.WriteLine("\nWrong value");
            }

        }
        void AddNewProduct()
        {
            try
            {
                Console.WriteLine("\nProduct title: ");
                string title = Console.ReadLine();
                Console.WriteLine("Product price: ");
                int price = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Category id: ");
                int categoryId = Convert.ToInt32(Console.ReadLine());

                var product = new Product
                {
                    Title = title,
                    Price = price,
                    CategoryId = categoryId
                };
                Product newProduct = productDal.InsertProduct(product);
                Console.WriteLine($"Added\n{newProduct.ProductId}| {newProduct.Title} - {newProduct.Price}" +
                    $"\n\tCategory id: {newProduct.CategoryId}");
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Wrong value");
            }
        }
        //
        //Read and show methods
        //
        void UserList()
        {
            List<UserInfo> users = userInfoDal.GetAll();
            foreach (UserInfo user in users)
            {
                Console.WriteLine($"{user.UserId}| {user.UserLogin}\n" +
                    $"\t{user.FirstName} {user.LastName}");
            }
        }
        void BankCardList()
        {
            List<BankCard> bankCards = bankCardDal.GetAll();
            foreach (BankCard bankCard in bankCards)
            {
                Console.WriteLine($"{bankCard.BankCardId}|  {bankCard.Number}  -  Owner:{bankCard.OwnerId}");
            }
        }
        void ProductList()
        {
            List<Product> products = productDal.GetAll();
            foreach (Product product in products)
            {
                Console.WriteLine($"\n{product.ProductId}| {product.Title} - {product.Price}" +
                    $"\n\tCategory id: {product.CategoryId}");
            }
        }
        void ShowUserById()
        {
            Console.WriteLine("Enter User ID: ");
            int userID = Convert.ToInt32(Console.ReadLine());
            var user = userInfoDal.GetById(userID);
            Console.WriteLine($"{user.UserId}|  {user.UserLogin}\t{user.FirstName}{user.LastName}");
        }
        void ShowBankCardById()
        {
            Console.WriteLine("Enter Bank card ID: ");
            int bankCardID = Convert.ToInt32(Console.ReadLine());
            var bankCard = bankCardDal.GetById(bankCardID);
            Console.WriteLine($"{bankCard.BankCardId}|  {bankCard.Number}");
        }
        void ShowProductById()
        {
            Console.WriteLine("Enter prodyct ID: ");
            int productID = Convert.ToInt32(Console.ReadLine());
            var product = productDal.GetById(productID);
            Console.WriteLine($"\n{product.ProductId}| {product.Title} - {product.Price}" +
                    $"\n\tCategory id: {product.CategoryId}");
        }
        //
        //Delete methods
        //
        void DeleteUserById()
        {
            Console.WriteLine("Write user ID to delete: ");
            int userID = Convert.ToInt32(Console.ReadLine());
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
        void DeleteProductById()
        {
            Console.WriteLine("Write product ID to delete: ");
            int productID = Convert.ToInt32(Console.ReadLine());
            productDal.DeleteProduct(productID);
            Console.WriteLine("Product deleted");
        }
        //
        //Update methods
        //
        void UpdateUserById()
        {
            Console.WriteLine("Write user ID to edit: ");
            int userId = Convert.ToInt32(Console.ReadLine());
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
        void UpdateProductById()
        {
            Console.WriteLine("Write product ID to edit: ");
            int productId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Pick property to edit (only one): \n" +
            "1. Title\n" +
            "2. Price\n" +
            "3. Category ID\n" +
            "0. Exit\n" +
            "Option: ");

            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.WriteLine("Enter new Title:");
                    string newTitle = Console.ReadLine();
                    productDal.UpdateProduct(productId, "title", newTitle);
                    break;

                case 2:
                    Console.WriteLine("Enter new Price:");
                    string newPrice = Console.ReadLine();
                    productDal.UpdateProduct(productId, "price", newPrice);
                    break;

                case 3:
                    Console.WriteLine("Enter new Category ID:");
                    string newCategoryId = Console.ReadLine();
                    productDal.UpdateProduct(productId, "category_id", newCategoryId);
                    break;

                case 0:
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}