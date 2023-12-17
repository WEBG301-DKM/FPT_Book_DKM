namespace BookShop1Asm.ViewModels.UserRolesViewModel
{
    public class IndexVM
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string? Fullname { get; set; }
        public string? Address { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}
