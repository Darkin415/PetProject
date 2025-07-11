﻿using PetProject.Contracts.Dtos;
using PetProject.Contracts.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Contracts.Commands;

public record UpdateSocialNetworksCommand(Guid VolunteerId, IEnumerable<SocialListDto> SocialMedias);