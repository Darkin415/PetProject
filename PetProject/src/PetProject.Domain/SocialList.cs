namespace PetProject.Domain
{
   public partial class Volunteer
    {
        public record SocialList(IReadOnlyList<SocialMedia> SocialMedias);
    }
}
