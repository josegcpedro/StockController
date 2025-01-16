using dotenv.net;
using System;

class Program
{
    static List<Product> products = new List<Product>();
    static List<Category> categories = new List<Category>();

    static void Main()
    {
        VerifyIdentification();
    }

    static void VerifyIdentification()
    {
        Console.WriteLine("Veuillez vous identifier");

        DotEnv.Load();

        for (int attempts = 0; attempts < 3; attempts++)
        {
            string? password = Console.ReadLine();
            string? verificationPassword = Environment.GetEnvironmentVariable("password");
            if (password != verificationPassword)
            {
                Console.WriteLine("Mot de passe incorrect");
                if (attempts == 2)
                {
                    Console.WriteLine("Tentatives épuisées !");
                    return;
                }
            }
            else
            {
                Menu();
                return;
            }
        }
    }

    static void Menu()
    {
        Console.WriteLine("Que souhaitez-vous faire ?");
        Console.WriteLine("1. Ajouter un produit");
        Console.WriteLine("2. Modifier un produit");
        Console.WriteLine("3. Supprimer un produit");
        Console.WriteLine("4. Afficher les produits d'une catégorie");
        Console.WriteLine("5. Afficher les catégories");
        Console.WriteLine("6. Créer une catégorie");
        Console.WriteLine("7. Supprimer une catégorie");
        Console.WriteLine("8. Quitter");

        string? choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                AddProduct();
                break;
            case "2":
                ModifyProduct();
                break;
            case "3":
                DeleteProduct();
                break;
            case "4":
                ShowProductsByCategory();
                break;
            case "5":
                ShowCategories();
                break;
            case "6":
                AddCategory();
                break;
            case "7":
                DeleteCategory();
                break;
            case "8":
                return;
            default:
                Console.WriteLine("Choix invalide. Veuillez réessayer.");
                Menu();
                break;
        }
    }

    static void AddProduct()
    {
        Console.WriteLine("Quel est le nom du produit ?");
        string? productName = Console.ReadLine();
        Console.WriteLine("Prix du produit :");
        if (!decimal.TryParse(Console.ReadLine(), out decimal productPrice))
        {
            Console.WriteLine("Prix invalide.");
            return;
        }

        Console.WriteLine("Quelle est la catégorie du produit ?");
        string? categoryName = Console.ReadLine();

        var existingCategory = categories.FirstOrDefault(c => c.CategoryName.Equals(categoryName, StringComparison.OrdinalIgnoreCase));

        if (existingCategory == null)
        {
            Category newCategory = new Category(categoryName);
            categories.Add(newCategory);
        }

        Product product = new Product(categoryName, productName, productPrice);
        products.Add(product);

        Console.WriteLine("Produit ajouté avec succès !");
        Menu();
    }

    static void ModifyProduct()
    {
        Console.WriteLine("Quel est le nom du produit que vous voulez modifier ?");
        string? desiredProductName = Console.ReadLine();

        Product productToModify = products.Find(product => product.ProductName.Equals(desiredProductName, StringComparison.OrdinalIgnoreCase));

        if (productToModify != null)
        {
            Console.WriteLine("Que souhaitez-vous modifier ?");
            Console.WriteLine("1. Nom");
            Console.WriteLine("2. Prix");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("Quel est le nouveau nom ?");
                    string newName = Console.ReadLine();
                    productToModify.ProductName = newName;
                    Console.WriteLine("Nom modifié avec succès !");
                    break;
                case "2":
                    Console.WriteLine("Quel est le nouveau prix ?");
                    if (decimal.TryParse(Console.ReadLine(), out decimal newPrice))
                    {
                        productToModify.ProductPrice = newPrice;
                        Console.WriteLine("Prix modifié avec succès !");
                    }
                    else
                    {
                        Console.WriteLine("Prix invalide.");
                    }
                    break;
                default:
                    Console.WriteLine("Choix invalide.");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Produit non trouvé.");
        }
        Menu();
    }

    static void DeleteProduct()
    {
        Console.WriteLine("Quel est le produit que vous voulez supprimer ?");
        string productNameToDelete = Console.ReadLine();
        Product productToDelete = products.Find(product => product.ProductName.Equals(productNameToDelete, StringComparison.OrdinalIgnoreCase));

        if (productToDelete != null)
        {
            Console.WriteLine($"Voulez-vous vraiment supprimer {productToDelete.ProductName} ? (1. Oui / 2. Non)");
            string choice = Console.ReadLine();
            if (choice == "1")
            {
                products.Remove(productToDelete);

                bool isCategoryStillUsed = products.Any(p => p.Category.Equals(productToDelete.Category, StringComparison.OrdinalIgnoreCase));

                if (!isCategoryStillUsed)
                {
                    var categoryToDelete = categories.Find(c => c.CategoryName.Equals(productToDelete.Category, StringComparison.OrdinalIgnoreCase));

                    if (categoryToDelete != null)
                    {
                        categories.Remove(categoryToDelete);
                    }
                }
                Console.WriteLine("Produit supprimé avec succès !");
            }
        }
        else
        {
            Console.WriteLine("Produit non trouvé.");
        }
        Menu();
    }

    static void ShowProductsByCategory()
    {
        Console.WriteLine("Quel est le nom de la catégorie pour afficher les produits ?");
        string categoryName = Console.ReadLine();

        var filteredProducts = products.Where(p => p.Category.Equals(categoryName, StringComparison.OrdinalIgnoreCase)).ToList();

        if (filteredProducts.Any())
        {
            foreach (var product in filteredProducts)
            {
                Console.WriteLine(product);
            }
        }
        else
        {
            Console.WriteLine("Aucun produit trouvé dans cette catégorie.");
        }
        Menu();
    }

    static void ShowCategories()
    {
        Console.WriteLine("Voici toutes les catégories disponibles :");

        if (categories.Any())
        {
            foreach (var category in categories)
            {
                Console.WriteLine(category.CategoryName);
            }
        }
        else
        {
            Console.WriteLine("Aucune catégorie disponible.");
        }
        Menu();
    }

    static void AddCategory()
    {
        Console.WriteLine("Quel est le nom de la catégorie ?");
        string? categoryName = Console.ReadLine();

        var existingCategory = categories.FirstOrDefault(c => c.CategoryName.Equals(categoryName, StringComparison.OrdinalIgnoreCase));

        if (existingCategory == null)
        {
            Category newCategory = new Category(categoryName);
            categories.Add(newCategory);
            Console.WriteLine("Catégorie ajoutée avec succès !");
        }
        else
        {
            Console.WriteLine("La catégorie existe déjà.");
        }
        Menu();
    }

    static void DeleteCategory()
    {
        Console.WriteLine("Quel est le nom de la catégorie que vous souhaitez supprimer ?");
        string? categoryNameToDelete = Console.ReadLine();

        bool isCategoryStillUsed = products.Any(p => p.Category.Equals(categoryNameToDelete, StringComparison.OrdinalIgnoreCase));

        if (!isCategoryStillUsed)
        {
            Category? categoryToDelete = categories.Find(c => c.CategoryName.Equals(categoryNameToDelete, StringComparison.OrdinalIgnoreCase));

            if (categoryToDelete != null)
            {
                categories.Remove(categoryToDelete);
                Console.WriteLine("Catégorie supprimée avec succès !");
            }
            else
            {
                Console.WriteLine("Catégorie non trouvée.");
            }
        }
        else
        {
            Console.WriteLine("Impossible de supprimer, la catégorie est associée à un produit.");
        }
        Menu();
    }
}

class Product
{
    public string? ProductName { get; set; }
    public decimal? ProductPrice { get; set; }
    public string? Category { get; set; }

    public Product(string category, string productName, decimal productPrice)
    {
        Category = category;
        ProductName = productName;
        ProductPrice = productPrice;
    }

    public override string ToString()
    {
        return $"Nom: {ProductName}, Prix: {ProductPrice:C}, Catégorie: {Category}";
    }
}

class Category
{
    public string CategoryName { get; set; }

    public Category(string categoryName)
    {
        CategoryName = categoryName;
    }
}
