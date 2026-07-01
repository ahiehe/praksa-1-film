using praktika1.Models;

namespace Filmoteka.API.DTOs
{
    public class UserInfoDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
    }
}
