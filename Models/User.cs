using System.ComponentModel.DataAnnotations;

namespace TwUsers.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }

        // Construtor padrão necessário para desserialização
        public User() { }

        // Opcional: Construtor com parâmetros
        public User(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }
}

