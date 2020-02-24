namespace Domain
{
    public class Guest
    {
        public int GuestId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
    }
}

// dotnet aspnet-codegenerator controller -name ReviewController  -actions -m Review  -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
// dotnet aspnet-codegenerator controller -name RoomFacilitiesController  -actions -m RoomFacilities  -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
// dotnet aspnet-codegenerator controller -name RoomPoliciesController  -actions -m RoomPolicies  -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
//
//
