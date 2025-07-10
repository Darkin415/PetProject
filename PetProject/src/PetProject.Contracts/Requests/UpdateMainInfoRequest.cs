
using PetProject.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PetProject.Contracts.Requests;

public record UpdateMainInfoRequest(FullNameDto FullName, string TelephonNumber, string Description);

