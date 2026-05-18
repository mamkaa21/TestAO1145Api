using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TestAO1145Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        readonly Testao1145Context context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TeacherController(Testao1145Context context)
        {
            this.context = context;
        }

        [HttpPost("CreateTests")]
        public async Task<ActionResult> CreateTests([FromBody] CreateTestDto dto)
        {
            if (dto == null) return BadRequest();

            // 1. Создаем объект теста
            var newTest = new Test
            {
                Name = dto.Name,
                IdSubject = dto.SubjectId,
                IdTeacher = dto.TeacherId
            };

            // Начинаем транзакцию, чтобы если один вопрос не сохранился, то и тест не создался
            using var transaction = await context.Database.BeginTransactionAsync();

            try
            {
                context.Tests.Add(newTest);
                await context.SaveChangesAsync(); // Сохраняем, чтобы получить ID теста

                // 2. Проходим по всем вопросам из DTO
                foreach (var qDto in dto.Questions)
                {
                    var question = new Question
                    {
                        Name = qDto.Text
                    };

                    // Связываем вопрос с этим тестом                 
                    question.Tests.Add(newTest);

                    context.Questions.Add(question);
                    await context.SaveChangesAsync(); // Получаем ID вопроса

                    // 3. Добавляем варианты ответов к этому вопросу
                    foreach (var ansText in qDto.Answers)
                    {
                        var answer = new Aswer
                        {
                            Text = ansText,
                            IdQuestion = question.Id
                        };
                        context.Aswers.Add(answer);
                    }
                }

                await context.SaveChangesAsync();
                await transaction.CommitAsync(); // Подтверждаем изменения в БД

                return Ok(new { message = "Тест успешно создан!", testId = newTest.Id });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(); // Если ошибка — откатываем всё назад
                return BadRequest($"Ошибка при создании теста: {ex.Message}");
            }

            //    try
            //    {
            //        var goodNew = new Good
            //        {
            //            Title = good.Title,
            //            CategoryId = good.CategoryId,
            //            Price = good.Price,
            //            Amount = good.Amount,
            //            Description = good.Description,
            //            Image = good.Image,
            //            Rating = good.Rating
            //        };
            //        context.Goods.Add(goodNew);
            //        await context.SaveChangesAsync();
            //        return Ok("Успешно");
            //    }
            //    catch (Exception ex)
            //    {
            //        return BadRequest(ex.Message);
            //    }
        }




       

     
    }
}
public class CreateTestDto
{
    public string Name { get; set; } = string.Empty;
    public int SubjectId { get; set; }
    public int TeacherId { get; set; }
    public List<QuestionDto> Questions { get; set; } = new();
}

public class QuestionDto
{
    public string Text { get; set; } = string.Empty;
    public List<string> Answers { get; set; } = new(); // Список вариантов ответов
}

