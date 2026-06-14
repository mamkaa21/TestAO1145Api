using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TestAO1145Api.Controllers
{
    //[Authorize]
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

        [HttpPost("AddTest")] //создание test реально добавил ахуеть
        public async Task<ActionResult<Test>> AddTest(TModel test)
        {
            var t = new Test // сначала просто делаем "пустой" тест где будет айди (для дальнейшего создания вопросов) и просто название. в впф дальше используем метод AddQ
            {
                Id = test.Id,
                Name = test.Name,
                CountQuestionTest = test.CountQuestionTest,
                IdSubject = test.IdSubject, //выбираем из комбобокса
                IdTeacher = test.IdTeacher,//выбираем из комбобокса
                //IdMark = test.IdMark
            };
            context.Tests.Add(t);
            await context.SaveChangesAsync();
            return Ok("Новый класс добавлен");
        }

        [HttpPost("AddQ")] //создание voprosa сразу с ответами РАБОТАЕТ
        public async Task<ActionResult<QModel>> AddQ(QModel question)
        {
            context.Questions.Add((Question)question);
            await context.SaveChangesAsync();
            return Ok("Новый класс добавлен");
        }

        [HttpGet("GetAllTest")] //ok
        public async Task<List<TModel>> GetAllTest()
        {
            var tests = await context.Tests.Include(s => s.IdTeacherNavigation).Select(s=>(TModel)s).ToListAsync();
            return tests;

           
        }
 //подсчет оценки, вывод результатов, 

        [HttpGet("GetTestWithQ")] //РАБОТАЕТ это в тествин 
        public async Task<List<QModel>> GetTestWithQ(int id)
        {
            var Q = await context.Questions.Include(s => s.Answers).Where(s => s.IdTest == id).Select(s => (QModel)s).ToListAsync();
            var test = await context.Tests.FirstOrDefaultAsync(s => s.Id == id);
            if(Q.Count < test.CountQuestionTest) 
                return Q;
            else
            {
                Random random = new Random();
                return Q.OrderBy(s=>random.NextDouble() > 0.5).Take(test.CountQuestionTest.Value).ToList();
            } 
                
            //подсчет оценки, вывод результатов, 
        }
    }
}

