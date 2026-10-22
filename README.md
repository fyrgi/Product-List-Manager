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
