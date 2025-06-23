using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetProject.Application.Volunteers.Create.Pet.AddPet;
using PetProject.Application.Volunteers.Create.Pet.AddPetPhoto;
using PetProject.Application.Volunteers.Create.SocialList;
using PetProject.Application.Volunteers.CreateVolunteer;
using PetProject.Application.Volunteers.Delete;
using PetProject.Application.Volunteers.Delete.DeleteFiles;
using PetProject.Application.Volunteers.DeletePhotos;
using PetProject.Application.Volunteers.UpdateMainInfo;

namespace PetProject.Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateVolunteerHandler>();
        services.AddScoped<UpdateMainInfoHandler>();
        services.AddScoped<DeleteVolunteerHandler>();
        services.AddScoped<UpdateSocialListHandler>();
        services.AddScoped<AddPetHandler>();
        services.AddScoped<RemovePhotoHandler>();       
        services.AddScoped<UploadPetPhotosHandler>();
        services.AddValidatorsFromAssembly(typeof(Inject).Assembly);
        return services;
    }
}
