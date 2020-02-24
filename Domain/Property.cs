namespace Domain
{
    public class Property
    {
        public int PropertyId { get; set; }
        public string PropertyName { get; set; }

        public int LocationId { get; set; }
        public Location Location { get; set; }
        
        
        
    }
}