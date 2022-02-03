using System.ComponentModel.DataAnnotations;

namespace UI.Models
{
    public class LoginViewModel
    {
        [Display(Name = "Kullanıcı Adı")]
        public string Username { get; set; }

        [Display(Name = "Şifre")]
        public string Password { get; set; }
    }
}
