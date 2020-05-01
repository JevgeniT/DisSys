using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Domain.Identity
{
   
    public class AppRole : IdentityRole<Guid>
    {
    
        // public string Role { get; set; } = default!;

        
        
    }

    public class AppRole<TKey> : IdentityRole<TKey> 
        where TKey : IEquatable<TKey>
    {
    }
} 