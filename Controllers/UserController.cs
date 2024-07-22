using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;
using TwUsers.Context;
using TwUsers.Models;
using System.Text.Json;

namespace TwUsers.Controllers
{
    
    public static class UserController
    {
        public static void AddUserController(this WebApplication app)
        {
            // Cria um grupo de rotas com o prefixo "user"
            var userGroup = app.MapGroup("/user");

            // Adiciona um endpoint POST ao grupo de rotas
            userGroup.MapPost("/", async (HttpContext context, TwUsersContext dbContext) =>
            {
                // Lê o corpo da solicitação JSON
                var novoUser = await context.Request.ReadFromJsonAsync<User>();

                if (novoUser == null)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync("Usuário inválido.");
                    return;
                }

                // Adiciona o novo usuário ao DbContext
                dbContext.Users.Add(novoUser);
                await dbContext.SaveChangesAsync();

                // Define o status e retorna o usuário criado
                context.Response.StatusCode = StatusCodes.Status201Created;
                await context.Response.WriteAsJsonAsync(novoUser);
            });
        }
    }
}
