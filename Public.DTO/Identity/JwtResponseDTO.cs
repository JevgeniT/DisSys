namespace Public.DTO.Identity
{
    public class JwtResponseDTO
    {
         public string Token { get; set; } = default!;
         public string Status { get; set; } = default!;

       
         public string FirstName { get; set; }  = default!;
      
         public string LastName { get; set; }  = default!;
    }

}