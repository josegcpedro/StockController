using dotenv.net;
using System;

class Program
{
    static List<Product> products = new List<Product>();
    static List<Categorie> categories = new List<Categorie>();

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
        Console.WriteLine("Que souhaitez vous faire!");

        Console.WriteLine("1. Ajouter un produit");
        Console.WriteLine("2. Modifier un produit");
        Console.WriteLine("3. Suprimmer un produit");
        Console.WriteLine("4. Afficher les produits d'une categorie");
        Console.WriteLine("5. Afficher les categories");
        Console.WriteLine("6. Créer une catégorie");
        Console.WriteLine("7. Supprimer une catégorie");
        Console.WriteLine("8. Quitter");

        string? choix = Console.ReadLine();

        switch (choix)
        {
            case "1":
                Console.WriteLine("1");
                AddProduct();
                break;
            case "2":
                Console.WriteLine("2");
                ModifyProduct();
                break;
            case "3":
                Console.WriteLine("3");
                DeleteProduct();
                break;
            case "4":
                ShowProductByCategory();
                break;
            case "5":
                ShowCategory();
                break;
            case "6":
                AddCategory();
                break;
            case "7":
                DeleteCategory();
                break;
            case "8":
                return;
        }
    }


    static void AddProduct()
    {
        Console.WriteLine("Quel est le nom du produit?");
        string? productName = Console.ReadLine();
        Console.WriteLine("Prix du produit :");
        decimal productPrice;

        if (!decimal.TryParse(Console.ReadLine(), out productPrice))
        {
            Console.WriteLine("Prix invalide.");
            return;
        }
        Console.WriteLine("Quel est la categorie du produit?");
        string? CategorieName = Console.ReadLine();

        var existingCategory = categories.FirstOrDefault(c => c.CategorieName.Equals(CategorieName, StringComparison.OrdinalIgnoreCase));

        if (existingCategory == null)
        {
            Categorie newCategorie = new Categorie(CategorieName);
            categories.Add(newCategorie);
        }

        Product product = new Product(CategorieName, productName, productPrice);
        products.Add(product);
        Menu();
    }

    static void ModifyProduct()
    {
        Console.WriteLine("Quel est le nom du produit que vous vouleuz modifier?");
        string? desiredModificationProduct = Console.ReadLine();

        Product productToModify = products.Find(product => product.ProductName.Equals(desiredModificationProduct, StringComparison.OrdinalIgnoreCase));

        if (productToModify != null)
        {
            Console.WriteLine("Que souhaitez vous modifier?");

            Console.WriteLine("1. Nom");
            Console.WriteLine("2. Prix");

            string choix = Console.ReadLine();

            switch (choix)
            {
                case "1":
                    Console.WriteLine("Quel est le nouveau Nom?");
                    string newName = Console.ReadLine();
                    productToModify.ProductName = newName;
                    Menu();
                    break;
                case "2":
                    Console.WriteLine("Quel est le nouveau prix?");
                    decimal newPrice;
                    if (decimal.TryParse(Console.ReadLine(), out newPrice))
                    {
                        productToModify.ProductPrice = newPrice;
                    }
                    Menu();
                    break;
            }
        }
    }
    static void DeleteProduct()
    {
        Console.WriteLine("Quel est le produit que voulez-vous supprimer?");
        string desiredDeletion = Console.ReadLine();
        Product productToDelete = products.Find(product => product.ProductName.Equals(desiredDeletion, StringComparison.OrdinalIgnoreCase));

        if (productToDelete != null)
        {
            Console.WriteLine($"Voulez-vous supprimer {productToDelete.ProductName}?");
            Console.WriteLine("1. Oui");
            Console.WriteLine("2. Non");
            string choix = Console.ReadLine();
            if (choix == "1")
            {
                products.Remove(productToDelete);
                bool isCategoryStillUsed = products.Any(p => p.Categorie.Equals(productToDelete.Categorie, StringComparison.OrdinalIgnoreCase));

                if (!isCategoryStillUsed)
                {
                    var categoryToDelete = categories.Find(c => c.CategorieName.Equals(productToDelete.Categorie, StringComparison.OrdinalIgnoreCase));

                    categories.Remove(categoryToDelete);
                }
                Menu();
            }
        }
        else
        {
            Console.WriteLine("Produit pas trouvé!");
            Menu();
        }
    }

    static void ShowProductByCategory()
    {
        Console.WriteLine("Quel est le nom de la categorie que vous souhaitez afficher les produits?");
        string choix = Console.ReadLine();

        var filteredProducts = products.Where(p => p.Categorie.Equals(choix, StringComparison.OrdinalIgnoreCase)).ToList();

        if (filteredProducts.Any())
        {
            foreach (var product in products)
            {
                Console.WriteLine(product);
            }
            Menu();
        }
        else
        {
            Console.WriteLine("Pas de produit de cette categorie trouvé");
            Menu();
        }
    }

    static void ShowCategory()
    {
        Console.WriteLine("Voici toutes les catégories!");

        if (categories.Any())
        {
            foreach (var categorie in categories)
            {
                Console.WriteLine(categorie.CategorieName);
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
        Console.WriteLine("Quel est le nom de la categorie?");
        string? CategorieName = Console.ReadLine();

        var existingCategory = categories.FirstOrDefault(c => c.CategorieName.Equals(CategorieName, StringComparison.OrdinalIgnoreCase));

        if (existingCategory == null)
        {
            Categorie newCategorie = new Categorie(CategorieName);
            categories.Add(newCategorie);
            Menu();
        }

    }

    static void DeleteCategory()
    {
        Console.WriteLine("Quel est le nom de la categorie que vous souhaitez supprimer?");
        string? categorieNameToDelete = Console.ReadLine();

        bool isCategoryStillUsed = products.Any(p => p.Categorie.Equals(categorieNameToDelete, StringComparison.OrdinalIgnoreCase));

        if (!isCategoryStillUsed)
        {
            Categorie? categorieToDelete = categories.Find(c => c.CategorieName.Equals(categorieNameToDelete, StringComparison.OrdinalIgnoreCase));

            if (categorieToDelete != null)
            {
                categories.Remove(categorieToDelete);
                Menu();
            } else {
                Console.WriteLine("Aucune categorie trouvée");
                Menu();
            }

        }
        else
        {
            Console.WriteLine("Pas possible car la categorie est associé à un produit");
            Menu();
        }

    }

}

class Product
{
    public string? ProductName { get; set; }
    public decimal? ProductPrice { get; set; }
    public string? Categorie { get; set; }



    public Product(string categorie, string productName, decimal productPrice)
    {
        Categorie = categorie;
        ProductName = productName;
        ProductPrice = productPrice;
    }

    public override string ToString()
    {
        return $"Nom: {ProductName}, Prix: {ProductPrice:C}, Catégorie: {Categorie}";
    }
}

class Categorie
{
    public string CategorieName { get; set; }

    public Categorie(string categorieName)
    {
        CategorieName = categorieName;
    }
}