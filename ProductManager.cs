using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicesManagmentSystem
{
    public class ProductManager
    {
        private const string FilePath = @"D:\C# projects folder\Nauka_C#\InvoicesManagmentSystem\data\products.csv";

        public void CreateProduct()
        {
            Console.WriteLine("Podaj nazwę produktu");
            string name = Console.ReadLine();

            Console.WriteLine("Podaj cenę produktu");
            decimal price = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Podaj jednostkę produktu");
            string unit = Console.ReadLine();

            Console.WriteLine("Podaj VAT produktu");
            decimal vat = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Podaj typ produktu");
            string type = Console.ReadLine();

            List<ProductBase> products = GetProducts();

            int newId = 1;
            if (products.Any())
            {
                newId = products.Max(x => x.Id) + 1;
            }

            ProductBase product;
            if(type == "Product")
            {
                product = new Product
                {
                    Id = newId,
                    Name = name,
                    Price = price,
                    Unit = unit,
                    VAT = vat,
                };
            }
            else
            {
                product = new Service
                {
                    Id = newId,
                    Name = name,
                    Price = price,
                    Unit = unit,
                    VAT = vat,
                };
            }

            products.Add(product);
            SaveData(products);
        }

        public void PrintAllProducts()
        {
            List<ProductBase> products = GetProducts();
            foreach (ProductBase product in products) 
            {
                product.PrintData();
            }
        }

        private List<ProductBase> GetProducts() 
        { 
            List<ProductBase> products = new List<ProductBase>();
            string[] lines = File
                .ReadAllLines(FilePath)
                .Skip(1)
                .ToArray();

            foreach (string line in lines) 
            { 
                ProductBase productBase = ProductBase.FromCSV(line);
                products.Add(productBase);
            }
            return products;
        }

        public string GetProductNameById(int productId)
        {
            List<ProductBase> products = GetProducts();
            var product = products.FirstOrDefault(p => p.Id == productId);
            return product != null ? product.Name : "Unknown Product";
        }


        private void SaveData(List<ProductBase> products)
        {
            List<string> lines = new List<string>()
            {
                "Id,Name,Price,Unit,VAT,Type"
            };

            foreach (ProductBase product in products)
            {
                string line = product.ToCSV();
                lines.Add(line);
            }

            File.WriteAllLines(FilePath, lines);
        }

        public void EditProduct() // EDIT PRODUCT
        {
            List<ProductBase> products = GetProducts();
            PrintAllProducts();

            Console.WriteLine("Podaj ID produktu/usługi do edycji:");
            int id = int.Parse(Console.ReadLine());

            ProductBase product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                Console.WriteLine("Produkt/usługa o podanym ID nie istnieje.");
                return;
            }

            Console.WriteLine("Edytuj dane produktu/usługi (pozostaw puste, aby nie zmieniać):");

            Console.Write("Nazwa: ");
            string name = Console.ReadLine();
            product.Name = !string.IsNullOrWhiteSpace(name) ? name : product.Name;

            Console.Write("Cena: ");
            string price = Console.ReadLine();
            product.Price = !string.IsNullOrWhiteSpace(price) ? decimal.Parse(price) : product.Price;

            Console.Write("Jednostka: ");
            string unit = Console.ReadLine();
            product.Unit = !string.IsNullOrWhiteSpace(unit) ? unit : product.Unit;

            Console.Write("VAT: ");
            string vat = Console.ReadLine();
            product.VAT = !string.IsNullOrWhiteSpace(vat) ? decimal.Parse(vat) : product.VAT;

            SaveData(products);
            Console.WriteLine("Dane produktu/usługi zaktualizowane.");
        }

        public void DeleteProduct() //DELETE PRODUCT
        {
            List<ProductBase> products = GetProducts();
            PrintAllProducts();

            Console.WriteLine("Podaj ID produktu/usługi do usunięcia:");
            int id = int.Parse(Console.ReadLine());

            ProductBase product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                Console.WriteLine("Produkt/usługa o podanym ID nie istnieje.");
                return;
            }

            products.Remove(product);
            SaveData(products);
            Console.WriteLine("Produkt/usługa usunięty.");
        }

        public void SearchProductByName() //SEARCH PRODUCT
        {
            Console.WriteLine("Podaj nazwę produktu/usługi do wyszukania:");
            string name = Console.ReadLine();

            List<ProductBase> products = GetProducts();
            var matchingProducts = products.Where(p => p.Name
            .Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();

            if (matchingProducts.Any())
            {
                Console.WriteLine("Znaleziono produkty/usługi:");
                foreach (var product in matchingProducts)
                {
                    product.PrintData();
                }
            }
            else
            {
                Console.WriteLine("Nie znaleziono produktów/usług o podanej nazwie.");
            }
        }
    }
}
