using dotenv.net;
using System;

class Program
{
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
        Console.WriteLine("Bonjour! Que souhaitez vous faire!");

        Console.WriteLine("1. Ajouter un produit");
        Console.WriteLine("2. Modifier un produit");
        Console.WriteLine("3. Suprimmer un produit");
        Console.WriteLine("4. Afficher un ou les produits");
        Console.WriteLine("5. Quitter");

        string? choix = Console.ReadLine();

        switch (choix)
        {
            case "1":
                Console.WriteLine("1");
                //AddProduct()
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
                Console.WriteLine("1. Afficher un produit");
                Console.WriteLine("2. Afficher tous les produit");
                string? choixProduits = Console.ReadLine();
                if (choixProduits == "1")
                {
                    //ShowOneProduct();
                    Console.WriteLine("1");
                } else if (choixProduits == "2"){
                    Console.WriteLine("2");
                }

                break;
            case "5":
                return;
        }
    }
}
