using System;
namespace FBMS.Core.Models;

public class DietaryRequirements
{
    public int Id { get; set; }

    public string Description { get; set; }

    IList<UserDietaryRequirements> UserDietaryRequirements { get; set; } = new List<UserDietaryRequirements>();

}