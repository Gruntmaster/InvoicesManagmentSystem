using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicesManagmentSystem
{
    public class InvoicesManager
    {
        private const string FilePathInvoices = @"D:\C# projects folder\Nauka_C#\InvoicesManagmentSystem\data\invoices.csv";
        private const string FilePathInvoicePosition = @"D:\C# projects folder\Nauka_C#\InvoicesManagmentSystem\data\invoicePositions.csv"; //name means positions at invoice dokument
        public void CreateInvoice()
        {
            Console.WriteLine("Klienci:");

            ClientManager clientManager = new ClientManager();
            List<Client> clients = clientManager.GetClients();
            foreach (Client client in clients)
            {
                client.PrintData();
            }

            Invoice invoice = new Invoice();
            Console.WriteLine("\nPodaj id klienta:");
            invoice.ClientId = int.Parse(Console.ReadLine());

            Console.WriteLine("\nPodaj numer:");
            invoice.Number = Console.ReadLine();

            // Zapisywanie invoice i ustawianie id
            List<Invoice> invoices = GetInvoiceDocuments();
            int newId = 1;
            if (invoices.Any())
            {
                newId = invoices.Max(x => x.Id) + 1;
            }

            invoice.Id = newId;
            invoices.Add(invoice);
            SaveInvoice(invoices);

            Console.WriteLine("\nPodaj pozycje");

            // Pobrać wszystkie

            List<InvoicePosition> invoicesPositions = GetInvoicesPositions();

            int newInvoicesPositionId = 1;
            if (invoicesPositions.Any())
            {
                newInvoicesPositionId = invoicesPositions.Max(x => x.Id) + 1;
            }

            List<InvoicePosition> invoicePositions = new List<InvoicePosition>();
            while (true)
            {
                Console.WriteLine("\nPodaj pozycje");

                InvoicePosition invoicePosition = new InvoicePosition();
                invoicePosition.Id = newInvoicesPositionId;
                invoicePosition.InvoiceId = invoice.Id;

                Console.WriteLine("Podaj Id produktu");
                invoicePosition.ProductId = int.Parse(Console.ReadLine());

                Console.WriteLine("Podaj cenu");
                invoicePosition.Price = decimal.Parse(Console.ReadLine());

                Console.WriteLine("Podaj ilość");
                invoicePosition.Quantity = double.Parse(Console.ReadLine());

                invoicePositions.Add(invoicePosition);

                Console.WriteLine("Czy chcesz dodać jeszcze pozycję? (T/N) ?");
                string response = Console.ReadLine();
                if(response != "T")
                {
                    break;
                }

                newInvoicesPositionId++;
            }

            // Zapisywać do pliku
            SaveInvoicePositions(invoicePositions);
        }

        public void PrintInvoiceDocuments()
        {
            List<Invoice> invoiceDocuments = GetInvoiceDocuments();
            List<InvoicePosition> invoicesPositions = GetInvoicesPositions();
            foreach (Invoice invoice in invoiceDocuments)
            {
                invoice.PrintData();

                Console.WriteLine("---Pozycje---");
                // Wyszukać wszystkie pozycje dla tego invoice
                // a potem wyświetlić ich dane
                List<InvoicePosition> queryPosition = invoicesPositions
                    .Where(pos => pos.InvoiceId == invoice.Id)
                    .ToList();
                foreach (var positionItem in queryPosition)
                {
                    positionItem.PrintData();
                }
                Console.WriteLine("-------------");
            }
        }

        public void DeleteInvoiceDocument()
        {
            Console.WriteLine("\nFaktury:");
            List<Invoice> invoiceDocuments = GetInvoiceDocuments();
            foreach (Invoice invoice in invoiceDocuments)
            {
                invoice.PrintData();
            }

            List<InvoicePosition> invoicePositions = GetInvoicesPositions();

            Console.WriteLine("Podaj id faktury do usunięcia:");
            int id = int.Parse(Console.ReadLine());

            Invoice invoiceDokument = invoiceDocuments.FirstOrDefault(x => x.Id == id);
            if(invoiceDokument == null)
            {
                Console.WriteLine("Dokument FV o podanym ID nie istnieje.");
                return;
            }

            invoiceDocuments.Remove(invoiceDokument);
            SaveInvoice(invoiceDocuments);
            Console.WriteLine("Dokument FV usunięty");

            invoicePositions.RemoveAll(pos => pos.InvoiceId == id);
            SaveInvoicePositions(invoicePositions);

            // Usuwanie
            // 1. Pobrać wszystkie invoices
            // 2. Usunąć jedno o takim id jak podał użytkownika
            // 3. Zapisać do pliku zmienione dane


        }

        private List<Invoice> GetInvoiceDocuments()
        {
            List<Invoice> invoices = new List<Invoice>();
            string[] lines = File.
                ReadAllLines(FilePathInvoices)
                .Skip(1)
                .ToArray();
            foreach (string line in lines)
            {
                Invoice invoiceDokument = Invoice.FromCSV(line);
                invoices.Add(invoiceDokument);   
            }
            return invoices;
        }

        private void SaveInvoice(List<Invoice> invoices)
        {
            List<string> lines = new List<string>()
            {
                "Id,Number,Date,ClientId"
            };

            foreach (Invoice invoiceUnit in invoices)
            {
                string line = invoiceUnit.ToCSVLine();
                lines.Add(line);
            }

            File.WriteAllLines(FilePathInvoices, lines);
        }

        private List<InvoicePosition> GetInvoicesPositions()
        {
            List<InvoicePosition> invoiceUnits = new List<InvoicePosition>();
            string[] lines = File
                .ReadAllLines(FilePathInvoicePosition)
                .Skip(1)
                .ToArray();
            foreach (string line in lines)
            {
                InvoicePosition invoiceItems = InvoicePosition.FromCSV(line);
                invoiceUnits.Add(invoiceItems);
            }
            return invoiceUnits;
        }

        private void SaveInvoicePositions(List<InvoicePosition> invoiceUnits)
        {
            List<string> lines = new List<string>()
            {
                "Id,InvoiceId,ProductId,Price,Quantity"
            };

            foreach (InvoicePosition item in invoiceUnits)
            {
                string line = item.ToCSVLine();
                lines.Add(line);
            }

            File.WriteAllLines(FilePathInvoicePosition, lines);
        }

        public void GenerateSalesReportByProduct()
        {
            List<InvoicePosition> invoicePositions = GetInvoicesPositions();
            ProductManager productManager = new ProductManager();

            // Group by ProductId and calculate total quantity for each product
            var report = invoicePositions
                .GroupBy(p => p.ProductId)
                .Select(g => new
                {
                    ProductId = g.Key,
                    TotalQuantity = g.Sum(p => p.Quantity)
                })
                .ToList();

            Console.WriteLine("\n--- Sales Report ---");
            Console.WriteLine("Product Name | Total Quantity Sold");

            foreach (var item in report)
            {
                string productName = productManager.GetProductNameById(item.ProductId);
                Console.WriteLine($"{productName} | {item.TotalQuantity} szt");
            }
            Console.WriteLine("----------------------");
        }

        public void GenerateSalesValueReportByProduct()
        {
            List<InvoicePosition> invoicePositions = GetInvoicesPositions();
            ProductManager productManager = new ProductManager();

            // Group by ProductId and calculate total sales value for each product
            var report = invoicePositions
                .GroupBy(p => p.ProductId)
                .Select(g => new
                {
                    ProductId = g.Key,
                    TotalSalesValue = g.Sum(p => p.PriceTotal)
                })
                .ToList();

            Console.WriteLine("\n--- Sales Value Report ---");
            Console.WriteLine("Product Name | Total Sales Value");

            foreach (var item in report)
            {
                string productName = productManager.GetProductNameById(item.ProductId);
                Console.WriteLine($"{productName} | {item.TotalSalesValue} zł");
            }
            Console.WriteLine("---------------------------");
        }

        public void GenerateSalesReportByClient()
        {
            List<Invoice> invoices = GetInvoiceDocuments();
            List<InvoicePosition> invoicePositions = GetInvoicesPositions();
            ClientManager clientManager = new ClientManager();

            // Group positions by ClientId using the related Invoice.ClientId
            var report = invoicePositions
                .GroupBy(pos => invoices.FirstOrDefault(inv => inv.Id == pos.InvoiceId)?.ClientId)
                .Where(g => g.Key.HasValue) // Exclude any groups where ClientId is null
                .Select(g => new
                {
                    ClientId = g.Key.Value,
                    TotalQuantity = g.Sum(pos => pos.Quantity),
                    TotalSalesValue = g.Sum(pos => pos.PriceTotal)
                })
                .ToList();

            Console.WriteLine("\n--- Sales Report by Client ---");
            Console.WriteLine("Client Name | Total Quantity | Total Sales Value");

            foreach (var item in report)
            {
                string clientName = clientManager.GetClientNameById(item.ClientId);
                Console.WriteLine($"{clientName} | {item.TotalQuantity} szt | {item.TotalSalesValue} zł");
            }
            Console.WriteLine("-------------------------------");
        }

    }
}
