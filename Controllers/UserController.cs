using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;
using TwUsers.Context;
using TwUsers.Models;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace TwUsers.Controllers
{
    
    public static class UserController
    {
        public static void AddUserController(this WebApplication app)
        {
            var userGroup = app.MapGroup("/user");

            userGroup.MapPost("/", async (HttpContext context, TwUsersContext dbContext) =>
            {
                var novoUser = await context.Request.ReadFromJsonAsync<User>();

                if (novoUser == null)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync("Usuário inválido.");
                    return;
                }
                var validationResults = new List<ValidationResult>();
                var validationContext = new ValidationContext(novoUser);
                bool isValid = Validator.TryValidateObject(novoUser, validationContext, validationResults, true);

                if(!IsValidEmail(novoUser.Email)){
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync("E-mail inválido.");
                    return;
                }
                dbContext.Users.Add(novoUser);
                await dbContext.SaveChangesAsync();

                context.Response.StatusCode = StatusCodes.Status201Created;
                await context.Response.WriteAsJsonAsync(novoUser);
            });
        }
        private static bool IsValidEmail(string email)
        {
            var emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailRegex);
        }
    }
}
