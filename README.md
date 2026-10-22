# Product List Manager

A .NET 10 console application for managing a simple product list stored in a
text file. The application provides a menu-driven interface for adding,
viewing, searching, removing, and analyzing products.

## Features

- Create `products.txt` automatically when the application starts.
- Add products after validating their names.
- View all products in alphabetical order.
- Search for products using a partial name match.
- Remove a product using an exact name match.
- Display product statistics:
  - Total number of products
  - Maximum product number
  - Minimum product number
  - Average product number
- Display success, warning, information, and error messages using different
  console colors.

## Product name format

A product name must contain alphabetical characters, followed by a dash and
a number from `200` through `500`.

Valid examples:

```
Product-200
Laptop-350
Chair-500
```
<img width="926" height="620" alt="image" src="https://github.com/user-attachments/assets/8ee67d35-2c6f-47b2-8d1d-dd9e6fb46fc9" />

The application converts product names to uppercase before storing them. A
product with the same name cannot be added more than once.

## Requirements

- .NET 10 SDK
- A terminal or command prompt

## Running the application

From the project directory, run:

```bash
dotnet run
```

or in [project directory]\bin\Debug\net10.0, run the compiled executable v43-Mini-Project.exe:


The application creates `products.txt` in the program's output directory if
the file does not already exist.

## Menu options

| Option | Action |
| --- | --- |
| 1 | Add a product |
| 2 | View all products |
| 3 | Search for products |
| 4 | Remove a product |
| 5 | Show product statistics |
| 9 | Exit the application |

## Data storage

Products are stored as one product name per line in `products.txt`. The file
is read and updated directly by the application; no database is required.

## Project structure

The current application is implemented in `Program.cs` using top-level
statements and local functions. File handling, validation, menu processing,
product operations, statistics, sorting, and console messages are kept in the
same console application.

<img width="321" height="429" alt="image" src="https://github.com/user-attachments/assets/4dfe0f76-cea0-408a-ad56-946e3ed67930" />
<img width="396" height="356" alt="image" src="https://github.com/user-attachments/assets/3f08af62-4b51-4619-8c23-998f8e374aa3" />
<img width="296" height="81" alt="image" src="https://github.com/user-attachments/assets/8cdb3c57-745b-4a58-965c-5a18eb95879a" />
<img width="312" height="82" alt="image" src="https://github.com/user-attachments/assets/12875218-9bde-4f9d-92a0-b344d7e58730" />
<img width="345" height="91" alt="image" src="https://github.com/user-attachments/assets/edd8245a-96fc-4933-9a9f-72c3f916876e" />
<img width="306" height="182" alt="image" src="https://github.com/user-attachments/assets/97f8bfff-4f47-4fdc-afee-3cfb58729781" />

## Problems that are not being solved in this program

- Data normalization - the file can be manipulated manually and there is no way to guarantee the format of the data.
  This problem will be solved with a database or if more checks are added to the code to make sure to sanitize the file first.
- Too many functions. Min, Max and Average can be in one function returning an Array or List with values.
- Inconsistencies with new lines on messages.
- The program is not tested on other Operative systems.

Although there problems are out of the scope of the task, they should be noted.





