namespace Domain
{
    public class Extra
    {
        public int ExtraId { get; set; }

        public string ExtraName { get; set; }

        public int FacilityId { get; set; }
        public Facility? Facility { get; set; }
    }
}