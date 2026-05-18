using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TestAO1145Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly Testao1145Context context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthController(Testao1145Context context)
        {
            this.context = context;

        }

        public class AuthOptions
        {
            public const string ISSUER = "MyAuthServer"; // издатель токена
            public const string AUDIENCE = "MyAuthClient"; // потребитель токена
            const string KEY = "mysupersecret_secretsecretsecretkey!123";   // ключ для шифрации
            public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
        }
        [HttpPost("CheckAccountIsExist")] //все работает 
        public async Task<ActionResult> CheckAccountIsExist(Student Student) //добавить проверку еще на роль - если рольID = 1, это админ, если рольID = 2 -> это юзер
        {
            var newUser = new Student { Id = Student.Id, Age = Student.Age, Classes = Student.Classes, FirstName = Student.FirstName, LastName = Student.LastName, IdClass = Student.IdClass, 
            Login = Student.Login, Password = Student.Password};
            if (string.IsNullOrEmpty(newUser.Login) || string.IsNullOrEmpty(newUser.Password))
                return BadRequest("Логин или пароль не иожет быть пустым");

            var user = await context.Students.FirstOrDefaultAsync(s => s.Login == newUser.Login);
            if (user == null)
                return NotFound("Неверный логин");
            else
            {
                if (newUser.Password != user.Password)
                {
                    return NotFound("Неверный пароль");
                }
                else
                {
                    int id = user.Id;

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                    };
                    var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    //кладём полезную нагрузку
                    claims: claims,
                    //устанавливаем время жизни токена 2 минуты
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(20)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

                    string token = new JwtSecurityTokenHandler().WriteToken(jwt);
                    return Ok(token);
                    //return Ok(new ResponceTokenAndEmployee
                    //{
                    //    Token = token,
                    //    Role = role,
                    //    Employee = user
                    //});
                    // return Ok((UserModel) user);
                }
            }
        }
    }
}
