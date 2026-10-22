using System.Collections;
using System.Text.RegularExpressions;

Console.WriteLine("Product list manager");

string productFileName = "products.txt";
string input = string.Empty;
// On startup, check if the products.txt file exists in the program directory. If it doesn't, create it.
try
{
    checkAndCreateFile();
    do
    {
        // Show main menu and handle user input until the user chooses to exit
        showMainMenu();
        input = Console.ReadLine();
        chooseMainMenuOption(input.Trim());
    } while (input != "9");
}
catch (Exception ex)
{
    showErrorMessage($"The system terminates!\nError checking/creating file: {ex.Message}.");
}

// Display the main menu options
void showMainMenu()
{
    Console.WriteLine("1. Add product");
    Console.WriteLine("2. View products");
    Console.WriteLine("3. Search products");
    Console.WriteLine("4. Remove product");
    Console.WriteLine("5. Statistics");
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
            viewProducts();
            break;
        case "3":
            Console.WriteLine("Searching products...");
            searchProducts();
            break;
        case "4":
            Console.WriteLine("Removing product...");
            deleteProduct();
            break;
        case "5":
            Console.WriteLine("Showing statistics...");
            showStatistics();
            break;
        case "9":
            Console.WriteLine("Exiting...");
            return;
        default:
            showErrorMessage("Invalid option. Please try again.\n");
            break;
    }
}

// Add a new product.
void addProduct()
{
    Console.WriteLine("Enter product name:");
    string name = Console.ReadLine().Trim().ToUpper();

    try
    {
        productNameValidation(name);
        string[] foundItem = searchStringAndAddToArray(false, name, File.ReadAllLines(Path.Combine(AppContext.BaseDirectory, productFileName)));
        if(foundItem.Length > 0)
        {
            showWarningMessage($"Product {name} already exists in the list.\n");
            return;
        } else
        {
            savetoProductFile(name);
        }
    }
    catch (ArgumentException ex)
    {
        showErrorMessage(ex.Message);
    }
}

// Save a product to the product file
void savetoProductFile(string product)
{
    string programFolder = AppContext.BaseDirectory;
    string filePath = Path.Combine(programFolder, "products.txt");
    try
    {
        using (StreamWriter sw = new StreamWriter(filePath, true))
        {
            sw.WriteLine(product);
        }
        showSuccessMessage($"Product {product} saved to file successfully.\n");
    }
    catch (Exception ex)
    {
        showErrorMessage($"Error writing to file: {ex.Message}");
    }
}

// Display a list of products or throw an exception. If the file is empty, display a warning message.
void viewProducts()
{
    try
    {
        string programFolder = AppContext.BaseDirectory;
        string filePath = Path.Combine(programFolder, "products.txt");
        if (File.Exists(filePath))
        {
            string[] products = File.ReadAllLines(filePath);
            sortDisplayProducts(products);
            if (products.Length > 0) {
                Console.WriteLine("\n==== Product List: ====");
                foreach (string product in products)
                {
                    Console.WriteLine(product);
                }
                Console.WriteLine("==== End of List ====\n");
            }
            else
            {
                showWarningMessage("No products found.\n");
            }
        }
        else
        {
            showErrorMessage("The product file is not found!\nContat System Administrator!");
        }    
    }
    catch (Exception ex)
    {
        showErrorMessage($"Error reading from file: {ex.Message}");
    }
}

// Search for all records matching a given string in the products.txt file. If found, display the product(s).
void searchProducts()
{
    // Code to open the products.txt file and search for a product
    try
    {
        string programFolder = AppContext.BaseDirectory;
        string filePath = Path.Combine(programFolder, productFileName);
        if (File.Exists(filePath))
        {
            Console.WriteLine("Enter product name to search:");

            string searchName = Console.ReadLine().Trim().ToUpper();
            string[] products = File.ReadAllLines(filePath);
            string[] searchResult = new string[0];
            bool found = false;

            searchResult = searchStringAndAddToArray(false, searchName, products);

            if (searchResult.Length == 0)
            {
                showWarningMessage($"Product {searchName} not found.");
            } else
            {
                sortDisplayProducts(searchResult);
                Console.WriteLine("\n==== Products matching the Search ====");
                foreach (string product in searchResult)
                {
                    Console.WriteLine(product);
                }
                Console.WriteLine("==== End of Search Results ====\n");
            }
        }
        else
        {
            showErrorMessage("The product file is not found!\nContact System Administrator!");
        }

    }
    catch (Exception ex)
    {
        showErrorMessage($"Error searching for product: {ex.Message}");
    }
}

// Search for a string in an array of products and return an array of matching products.
static string[] searchStringAndAddToArray(bool exactMatch,string search, string[] products)
{
    string[] searchResult = new string[0];
    if (exactMatch == true)
    {
        foreach (string product in products)
        {
            if (product.Equals(search, StringComparison.OrdinalIgnoreCase))
            {
                Array.Resize(ref searchResult, searchResult.Length + 1);
                searchResult[searchResult.Length - 1] = product;
            }
        };
    }
    else
    { 
        foreach (string product in products)
        {
            if (product.Contains(search, StringComparison.OrdinalIgnoreCase))
            {
                Array.Resize(ref searchResult, searchResult.Length + 1);
                searchResult[searchResult.Length - 1] = product;
            }
        }
    }
    return searchResult;
}

// Remove a product from the file.
void deleteProduct()
{
    Console.WriteLine("Enter product name to remove:");
    string name = Console.ReadLine().Trim().ToUpper();
    string[] foundItem = searchStringAndAddToArray(true, name, File.ReadAllLines(Path.Combine(AppContext.BaseDirectory, productFileName)));

    // If the product is found, remove it from the file. If not, display a warning message.
    if (foundItem.Length > 0)
    {
        // Remove the product from the file
        string programFolder = AppContext.BaseDirectory;
        string filePath = Path.Combine(programFolder, productFileName);
        try
        {
            var products = File.ReadAllLines(filePath).ToList();
            products.RemoveAll(p => p.Equals(name, StringComparison.OrdinalIgnoreCase));
            File.WriteAllLines(filePath, products);
            showSuccessMessage($"Product {name} removed successfully.\n");
        }
        catch (Exception ex)
        {
            showErrorMessage($"Error removing product: {ex.Message}\n");
        }
    }
    else
    {
        showWarningMessage($"Product {name} not found.");
    }
}

void showStatistics()
{
    string programFolder = AppContext.BaseDirectory;
    string filePath = Path.Combine(programFolder, productFileName);
    if (File.Exists(filePath))
    {
        string[] products = File.ReadAllLines(filePath);
        if(products.Length == 0)
        {
            showWarningMessage("No products found.\n");
            return;
        } else {
            Console.WriteLine("\n==== Displaying statistics ==== ");
            int totalProducts = products.Length;
            showInfoMessage($"Total number of products: {totalProducts}");
            string[] productNumbers = getProductNumbers(products);
            int maxProductNumber = findMaxProductNumber(productNumbers);
            showInfoMessage($"Maximum product number: {maxProductNumber}");
            int minProductNumber = findMinProductNumber(productNumbers);
            showInfoMessage($"Minimum product number: {minProductNumber}");
            int averageProductNumber = findAverageProductNumber(productNumbers);
            showInfoMessage($"Average product number: {averageProductNumber}");
            Console.WriteLine("==== End of statistics ====\n");
        }
    }
    else
    {
        showErrorMessage("The product file is not found!\nContact System Administrator!");
    }
}

// Extract product numbers from an array of products. The product number is the part after the dash in the product name.
static string[] getProductNumbers(string[] products)
{
    List<string> productNumbers = new List<string>();
    foreach (string product in products)
    {
        string[] parts = product.Split('-');
        if (parts.Length == 2 && int.TryParse(parts[1], out int number))
        {
            productNumbers.Add(parts[1]);
        }
    }
    return productNumbers.ToArray();
}

// Find the maximum, minimum and average product number from an array of product numbers.
static int findMaxProductNumber(string[] productNumbers)
{
    int maxNumber = 0;
    foreach (string productNumber in productNumbers)
    {
        if (int.TryParse(productNumber, out int number))
        {
            if (number > maxNumber)
            {
                maxNumber = number;
            }
        }
    }
    return maxNumber;
}

static int findMinProductNumber(string[] productNumbers)
{
    int minNumber = int.MaxValue;
    foreach (string productNumber in productNumbers)
    {
        if (int.TryParse(productNumber, out int number))
        {
            if (number < minNumber)
            {
                minNumber = number;
            }
        }
    }
    return minNumber;
}

static int findAverageProductNumber(string[] productNumbers)
{
    int sum = 0;
    int count = 0;
    foreach (string productNumber in productNumbers)
    {
        if (int.TryParse(productNumber, out int number))
        {
            sum += number;
            count++;
        }
    }

    return count > 0 ? sum / count : 0;
}

//Validate the product name using a regular expression. The product name must start with alphabetical letters, followed by a dash and a number between 200 and 500.
void productNameValidation(string product)
{
    string pattern = @"^[A-Za-z]+-(?:2\d{2}|3\d{2}|4\d{2}|500)$";
    if (!Regex.IsMatch(product, pattern))
    {
        // Throw an exception if the product name is invalid for more elegant error handling.
        throw new ArgumentException("Invalid product name.\nA valid name starts with aphabetical letters, followed by a dash and a number between 200 and 500.\n");
    }    
}

// Check if a file exists and create it if it doesn't. 
void checkAndCreateFile()
{
    string programFolder = AppContext.BaseDirectory;
    string filePath = Path.Combine(programFolder, "products.txt");
    if (!File.Exists(filePath))
    {
        try
        {
            File.Create(filePath).Dispose();
            showSuccessMessage("File 'products.txt' created successfully.\n");
        }
        catch (Exception ex)
        {
            showErrorMessage($"Error creating file: {ex.Message}\n");
        }
    }
}

// Sort an array of products in alphabetical order and ascending numbers.
static string[] sortDisplayProducts(string[] products)
{
    Array.Sort(products, StringComparer.OrdinalIgnoreCase);
    return products;
}

// Display messages in different colors based on the type of message.
void showErrorMessage(string message)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(message);
    Console.ResetColor();
}

void showSuccessMessage(string message)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine(message);
    Console.ResetColor();
}

void showWarningMessage(string message)
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine(message);
    Console.ResetColor();
}

void showInfoMessage(string message)
{
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine(message);
    Console.ResetColor();
}