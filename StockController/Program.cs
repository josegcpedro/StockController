using dotenv.net;
using System;

class Program
{
    static List<Product> products = new List<Product>();

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
        Console.WriteLine("5. Quitter");

        string? choix = Console.ReadLine();

        switch (choix)
        {
            case "1":
                Console.WriteLine("1");
                AddProduct();
                break;
            case "2":
                Console.WriteLine("2");
                //AddProduct()
                break;
            case "3":
                Console.WriteLine("3");
                //AddProduct()
                break;
            case "4":
                ShowProductByCategory();
                break;
            case "5":
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
        string? categorie = Console.ReadLine();

        Product product = new Product(categorie, productName, productPrice);
        products.Add(product);
        Menu();
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
    public string NomCategorie { get; set; }

    public Categorie(string nomCategorie)
    {
        NomCategorie = nomCategorie;
    }
}