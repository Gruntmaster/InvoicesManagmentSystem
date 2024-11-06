namespace InvoicesManagmentSystem
{
    public class ProductBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Unit { get; set; }
        public decimal VAT { get; set; }
        public string Type { get; set; }


        public void PrintData()
        {
            string line = ToCSV();
            Console.WriteLine(line);
        }

        public string ToCSV()
        {
            return $"{Id},{Name},{Price},{Unit},{VAT},{Type}";
        }

        public static ProductBase FromCSV(string line)
        {
            string[] splited = line.Split(',');
            string type = splited.LastOrDefault();
            if(type == "Product")
            {
                return new Product
                {
                    Id = int.Parse(splited[0]),
                    Name = splited[1],
                    Price = decimal.Parse(splited[2]),
                    Unit = splited[3],
                    VAT = decimal.Parse(splited[4])
                };
            }

            return new Service
            {
                Id = int.Parse(splited[0]),
                Name = splited[1],
                Price = decimal.Parse(splited[2]),
                Unit = splited[3],
                VAT = decimal.Parse(splited[4])
            };
        }
    }
}
