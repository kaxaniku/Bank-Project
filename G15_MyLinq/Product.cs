namespace G15_MyLinq
{
    public record Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        override public string ToString()
        {
            return $"{Id}: {Name} - {Price:C} ({Quantity})";
        }
    }
}
