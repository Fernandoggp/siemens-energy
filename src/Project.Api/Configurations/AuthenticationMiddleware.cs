using System.IdentityModel.Tokens.Jwt;

namespace Project.Api.Configurations
{
    public class AuthenticationMiddleware
    {
        public void Configure(IApplicationBuilder app)
        {
            //app.Use(async (context, next) =>
            //{
            //    try
            //    {
            //        var authorization = context.Request.Headers["Authorization"].ToString();
            //        var token = authorization.Substring("bearer ".Length).Trim();

            //        var handler = new JwtSecurityTokenHandler();
            //        var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            //        if (jsonToken != null)
            //        {
            //            var dataExpiracao = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "dataExpiracaoToken")?.Value;
            //            if (dataExpiracao != null && DateTime.Parse(dataExpiracao) >= DateTime.Now)
            //            {
            //                var validarTokenService = context.RequestServices.GetRequiredService<IValidarToken>();
            //                var total = validarTokenService.PesquisarTokenInvalidoAsync(token);

            //                if (total.Result > 0)
            //                {
            //                    context.Response.StatusCode = 401;
            //                    await context.Response.WriteAsync("Token inválido");
            //                    return;
            //                }
            //            }
            //            else
            //            {
            //                context.Response.StatusCode = 401;
            //                await context.Response.WriteAsync("Token expirado");
            //                return;
            //            }
            //        }

            //        await next.Invoke();
            //    }
            //    catch (Exception ex)
            //    {
            //        context.Response.StatusCode = 500;
            //        await context.Response.WriteAsync("Houve um erro ao validar o token");
            //    }
            //});
        }
    }
}
