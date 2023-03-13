namespace SportsStore.Models.ViewModels
{
    public class CartViewModel
    {
        public Cart? Cart { get; set; } = new Cart();

        public string ReturnUrl { get; set; } = "/";
    }
}
