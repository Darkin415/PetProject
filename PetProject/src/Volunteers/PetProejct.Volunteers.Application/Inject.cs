using Microsoft.Extensions.DependencyInjection;
using PetProejct.Volunteers.Application.Commands.AddPetPhotos;
using PetProejct.Volunteers.Application.Commands.CreatePet;
using PetProejct.Volunteers.Application.Commands.CreateVolunteer;
using PetProejct.Volunteers.Application.Commands.DeleteVolunteer;
using PetProejct.Volunteers.Application.Commands.GetVolunteerById;
using PetProejct.Volunteers.Application.Commands.MovePet;
using PetProejct.Volunteers.Application.Commands.Queries.GetPets.GetPetById;
using PetProejct.Volunteers.Application.Commands.Queries.GetPets.GetPetsWIthPagination;
using PetProejct.Volunteers.Application.Commands.Queries.GetVolunteers;
using PetProejct.Volunteers.Application.Commands.RemovePetPhotos;
using PetProejct.Volunteers.Application.Commands.UpdateSocialNetwork;
using PetProejct.Volunteers.Application.Commands.UpdateVolunteerMainInfo;
using PetProject.Files.Application;
using PetProject.Files.Infrastructure.Providers;

namespace PetProejct.Volunteers.Application
{
    public static class Inject
    {
        public static IServiceCollection AddVolunteersUseCases(this IServiceCollection services)
        {
            services.AddScoped<CreateVolunteerHandler>();
            
            services.AddScoped<AddPetHandler>();

            services.AddScoped<UploadPetPhotosHandler>();
            
            services.AddScoped<DeleteVolunteerHandler>();

            services.AddScoped<GetVolunteerByIdHandler>();
            
            services.AddScoped<MovePetHandler>();
            
            services.AddScoped<GetPetByIdHandler>();

            services.AddScoped<GetPetsWithPaginationHandler>();

            services.AddScoped<GetVolunteerByIdHandler>();
            
            services.AddScoped<GetVolunteersWithPaginationHandler>();
            
            services.AddScoped<RemovePhotoHandler>();

            services.AddScoped<UpdateSocialListHandler>();
            
            services.AddScoped<UpdateMainInfoHandler>();
            
            return services;
        }
    }
}