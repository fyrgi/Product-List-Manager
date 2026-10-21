using System.Text.RegularExpressions;

Console.WriteLine("Product list manager");

string input = string.Empty;
// Show main menu and handle user input until the user chooses to exit
do
{
    ShowMainMenu();
    input = Console.ReadLine();
    chooseMainMenuOption(input);
} while (input != "9");

// Display the main menu options
void ShowMainMenu()
{
    Console.WriteLine("1. Add product");
    Console.WriteLine("2. View products");
    Console.WriteLine("3. Search products");
    Console.WriteLine("4. Remove product");
    Console.WriteLine("9. Exit");
}

// Handle the user's choice from the main menu
void chooseMainMenuOption(string input)
{
    
    switch (input)
    {
        case "1":
            Console.WriteLine("Adding product...");
            addProduct();
            break;
        case "2":
            Console.WriteLine("Viewing products...");
            // Code to view products
            break;
        case "3":
            Console.WriteLine("Searching products...");
            // Code to search product
            break;
        case "4":
            Console.WriteLine("Removing product...");
            //code to delete product
            break;
        case "9":
            Console.WriteLine("Exiting...");
            return;
        default:
            Console.WriteLine("Invalid option. Please try again.");
            break;
    }
}

// Add a new product. TODO: Implement product addition either to a list or a file.
void addProduct()
{
    Console.WriteLine("Enter product name:");
    string name = Console.ReadLine();

    try
    {
        productNameValidation(name);
        Console.WriteLine($"Product '{name}' is added successfully.");
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine(ex.Message);
    }
}

//TODO: Display a list of products. This could be implemented by reading from a file or a stored in a variable. For now, is just print a placeholder message.
void viewProducts()
{
    Console.WriteLine("Product list:");
}

//TODO: Implement product search functionality. This could involve searching through a list or reading from a file. For now, is just print a placeholder message.
void searchProducts()
{
    Console.WriteLine("Enter product name to search:");
    string searchName = Console.ReadLine();
    // Code to search for the product in the list
}

//TODO: Implement product removal functionality. This could involve removing from a list or updating a file. For now, is just print a placeholder message.
void deleteProduct()
{
    Console.WriteLine("Enter product name to remove:");
    string removeName = Console.ReadLine();
    // Code to remove the product from the list
}

//TODO: In case of implementing a file-based storage.
void saveProductsToFile()
{
}

//Validate the product name using a regular expression. The product name must start with alphabetical letters, followed by a dash and a number between 200 and 500.
void productNameValidation(string product)
{
    string pattern = @"^[A-Za-z]+-(?:2\d{2}|3\d{2}|4\d{2}|500)$";
    if (Regex.IsMatch(product, pattern))
    {
        Console.WriteLine("Valid product name.");
    }
    else
    {
        // Throw an exception if the product name is invalid for more elegant error handling.
        throw new ArgumentException("Invalid product name. A valid name starts with aphabetical letters, followed by a dash and a number between 200 and 500.");
    }
}