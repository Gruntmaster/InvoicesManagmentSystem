namespace InvoicesManagmentSystem
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("System do zarządzania fakturami");
            while (true)
            {
                DisplayMenu();
                Console.WriteLine("Wybór:");
                int choice = int.Parse(Console.ReadLine());

                ClientManager clientManager = new ClientManager();
                ProductManager productManager = new ProductManager();
                InvoicesManager invoicesManager = new InvoicesManager();

                switch (choice)
                {
                    case 1:
                        clientManager.CreateClient();
                        break;
                    case 2:
                        clientManager.EditClient();
                        break;
                    case 3:
                        clientManager.DeleteClient();
                        break;
                    case 4:
                        clientManager.PrintAllClients();
                        break;
                    case 5:
                        clientManager.SearchClientByName();
                        break;
                    case 6:
                        productManager.CreateProduct();
                        break;
                    case 7:
                        productManager.EditProduct();
                        break;
                    case 8:
                        productManager.DeleteProduct();
                        break;
                    case 9:
                        productManager.PrintAllProducts();
                        break;
                    case 10:
                        productManager.SearchProductByName();
                        break;
                    case 11:
                        invoicesManager.CreateInvoice();
                        break;
                    case 12:
                        invoicesManager.PrintInvoiceDocuments();
                        break;
                    case 13:
                        invoicesManager.DeleteInvoiceDocument();
                        break;
                    case 14:
                        invoicesManager.GenerateSalesReportByProduct();
                         break;
                    case 15:
                        invoicesManager.GenerateSalesValueReportByProduct();
                        break;
                    case 16:
                        invoicesManager.GenerateSalesReportByClient();
                        break;
                    default:
                        return;
                }
            }
        }

        static void DisplayMenu()
        {
            Console.WriteLine("\nObsługa klientów");
            Console.WriteLine("1. Dodawanie nowego klienta");
            Console.WriteLine("2. Edytowanie klienta");
            Console.WriteLine("3. Usuwanie klienta");
            Console.WriteLine("4. Wyświetlanie wszystkich klientów");
            Console.WriteLine("5. Wyszkiwanie klientów na podsatwie nazwy");

            Console.WriteLine("\nZarządzanie produktami i usługami");
            Console.WriteLine("6. Dodawanie nowego produktu/usługi");
            Console.WriteLine("7. Edytowanie produktu/usługi");
            Console.WriteLine("8. Usuwanie produktu/usługi");
            Console.WriteLine("9. Wyświetlanie wszystkich produktów i usług");
            Console.WriteLine("10. Wyszkiwanie produktów i usług na podstawie nazwy");

            Console.WriteLine("\nZarządzanie fakturami");
            Console.WriteLine("11. Dodawanie nowej faktury");
            Console.WriteLine("12. Wyświetlanie faktur");
            Console.WriteLine("13. Usuwanie faktury");

            Console.WriteLine("\nGenerowanie zestawień i raportów");
            // Chcemy uzyskać informację, ile poszczgólnych towaró zostało sprzedane dla wszystkich faktur
            // Czyli sumę ilości z wszystkich pozycji faktur z podziałem na towary
            // np.
            // garnek duży | 20 szt
            // Laptop Dell | 30 szt

            Console.WriteLine("14. Generuj zestawienie ilości sprzedanych towarów");

            // Chcemy uzyskać informację, jaka jest wartość sprzedaży poszczgólnych towarów dla wszystkich faktur
            // Czyli sumę wartości z wszystkich pozycji faktur z podziałem na towary
            // np.
            // garnek duży | 500 zł
            // Laptop Dell | 3200 zł
            Console.WriteLine("15. Generuj zestawienie wartości sprzedanych towarów");

            // Chcemy uzyskać informację, jaka jest wartość sprzedaży dla poszczególnych klientów dla wszystkich faktur
            // Czyli sumę ilości i wartości z wszystkich pozycji faktur z podziałem na klientów
            // np.
            // Microsoft | 20 szt | 500 zł
            // Lidl | 100 szt | 10000 zł
            Console.WriteLine("16. Generuj zestawienie sprzedaży według kontrahentów");


        }
    }
}
