using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

public class User : IdentityUser<int> {
  public ICollection<FavouriteTrails> FavouriteTrails { get; set;}
}