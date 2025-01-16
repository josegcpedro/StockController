# Stock Control System

This is a simple stock control system developed in C# that allows users to manage products and categories. The system enables operations such as adding, modifying, deleting products, displaying products by category, and creating new categories.

## Features

- **Authentication**: The system requires a password authentication loaded from a `.env` file.
- **Add Product**: Allows adding new products with name, price, and category.
- **Modify Product**: Allows modifying the name or price of an existing product.
- **Delete Product**: Allows deleting a product and, if no other products remain in the category, the category is also removed.
- **Show Products by Category**: Displays all products in a specific category.
- **Show Categories**: Displays all registered categories.
- **Create Category**: Allows adding new categories to the system.
- **Delete Category**: Removes a category if there are no products associated with it.

## Technologies Used

- **C#**: Programming language used to build the application.
- **.NET**: Framework used for development.
- **dotenv.net**: Used to load environment variables (such as password) from a `.env` file.
