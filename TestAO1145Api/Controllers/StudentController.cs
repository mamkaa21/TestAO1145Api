using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using static TestAO1145Api.Controllers.AuthController;
namespace TestAO1145Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        readonly Testao1145Context context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public StudentController(Testao1145Context context)
        {
            this.context = context;
        }

        [HttpGet] //ОЧЕНЬ сомнително
        public async Task<ActionResult<Student>> GetUserData()
        {
            var id = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return Ok(context.Students);
        }

        [HttpGet("GetTests")] //сомнтельно
        public async Task<List<Test>> GetTests()
        {
            await Task.Delay(10);
            var goods = context.Tests.Include(s => s.IdTeacherNavigation).ToList();
            return goods;
        }


        [HttpPost("submit")] //ебать как сомнтельно . оттпрака теста хз 
        public async Task<IActionResult> SubmitTest([FromBody] SubmitTestDto dto)
        {
            if (dto == null) return BadRequest("Данные не получены");

            // 1. Проверяем существование студента и теста
            var studentExists = await context.Students.AnyAsync(s => s.Id == dto.StudentId);
            var test = await context.Tests.FindAsync(dto.TestId);

            if (!studentExists || test == null)
            {
                return NotFound("Студент или Тест не найдены в базе");
            }

            // 2. Создаем объект результата
            var result = new Studentanswer
            {
                IdStudent = dto.StudentId,
                IdTest = dto.TestId,
                DateTime = DateTime.Now,
                Answers = dto.Answers.Select(a => new Aswer
                {
                    IdQuestion = a.QuestionId,
                    Text = a.SelectedText
                }).ToList()
            };

            // 3. Сохраняем в базу данных
            try
            {
                context.Add(result);
                await context.SaveChangesAsync();

                return Ok(new
                {
                    Message = "Тест успешно отправлен!",
                    SubmissionId = result.Id
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка при сохранении: {ex.Message}");
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllResults()
        {
            var results = await context.Tests.Include(navigationPropertyPath: s => s.IdQuestionNavigation.Aswers).ToListAsync();
            return Ok(results);
        }

        [HttpPut("SaveChangedByStudentWin")]
        public async Task<ActionResult> SaveChangedByUserWin(Student student)
        {
            try
            {
                context.Students.Update(student);
                await context.SaveChangesAsync();
                return Ok("Успешно");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }

}

