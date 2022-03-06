namespace Aniverse.Core.Entites
{
    public class UserSM
    {
        public int Id { get; set; }
        public string IconClassName { get; set; }
        public string SmName {get; set; }
        public string SmLink {get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
    }
}
