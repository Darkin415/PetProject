using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Contracts.Dtos;

public class SpeciesDto
{
    public Guid Id { get; init; }
    
    public string title { get; init; } = default!;
}
