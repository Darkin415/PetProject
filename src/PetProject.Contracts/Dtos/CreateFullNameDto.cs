using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Contracts.Dtos;

public record CreateFullNameDto(string FirstName, string LastName, string? Surname);
