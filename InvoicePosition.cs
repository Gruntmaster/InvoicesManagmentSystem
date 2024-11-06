namespace InvoicesManagmentSystem
{
    public class InvoicePosition
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public double Quantity { get; set; }
        public decimal PriceTotal
        {
            get
            {
                return Price * (decimal)Quantity;
            }
        }

        public void PrintData()
        {
            string line = ToCSVLine();
            Console.WriteLine(line);
        }

        public string ToCSVLine()
        {
            return $"{Id},{InvoiceId},{ProductId},{Price},{Quantity}";
        }

        public static InvoicePosition FromCSV(string line)
        {
            string[] splited = line.Split(',');
            return new InvoicePosition
            {
                Id = int.Parse(splited[0]),
                InvoiceId = int.Parse(splited[1]),
                ProductId = int.Parse(splited[2]),
                Price = decimal.Parse(splited[3]),
                Quantity = double.Parse(splited[4])
            };
        }
    }
}
