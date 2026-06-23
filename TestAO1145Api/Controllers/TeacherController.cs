using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace TestAO1145Api.Controllers
{
    [Authorize]
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

        [HttpGet] //okо
        public async Task<ActionResult<TeacherModel>> GetUserData()
        {
            var id = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return Ok((TeacherModel)context.Teachers.Include(s=>s.IdSubjects).FirstOrDefault(s => s.Id == id));
        }

        [HttpPost("AddTest")] //создание test реально добавил ахуеть
        public async Task<ActionResult> AddTest([FromBody]TModel test)
        {
            var t = new Test // сначала просто делаем "пустой" тест где будет айди (для дальнейшего создания вопросов) и просто название. в впф дальше используем метод AddQ
            {
                Name = test.Name,
                CountQuestionTest = test.CountQuestionTest,
                IdSubject = test.IdSubject, //выбираем из комбобокса
                IdTeacher = test.IdTeacher,//выбираем из комбобокса
                //IdMark = test.IdMark
            };
            context.Tests.Add(t);
            await context.SaveChangesAsync();
            return Ok(t.Id);
        }

        [HttpPost("AddQ")] //создание voprosa сразу с ответами РАБОТАЕТ
        public async Task<ActionResult<QModel>> AddQ([FromBody] QModel question)
        {
            context.Questions.Add((Question)question);
            await context.SaveChangesAsync();
            return Ok((QModel)question);
        }

        [HttpPost("EditQ")] //создание voprosa сразу с ответами РАБОТАЕТ
        public async Task<ActionResult<QModel>> EditQ([FromBody]QModel question)
        {
            var find = await context.Questions.FirstOrDefaultAsync(s => s.Id == question.Id);
            if (find == null)
                return BadRequest("Нет такого вопроса");
            var edit = (Question)question;
            context.Entry(find).CurrentValues.SetValues(edit);

            foreach (var answer in edit.Answers)
            {
                var findAnswer = await context.Answers.FirstOrDefaultAsync(s => s.Id == answer.Id);
                if (findAnswer == null)
                    context.Answers.Add(answer);
                else
                    context.Entry(findAnswer).CurrentValues.SetValues(answer);
            }
            await context.SaveChangesAsync();

            var result = await context.Questions.Include(s=>s.Answers).FirstOrDefaultAsync(s => s.Id == question.Id);
            return Ok((QModel)result);
        }

        [HttpGet("RemoveAnswer")]
        public async Task<ActionResult> RemoveAnswer(int answer)
        {
            if (context.Testcrossquestions.FirstOrDefault(s=>s.IdAnswer == answer) != null)
                return BadRequest("Нельзя удалить вариант ответа, он используется в результатах теста");

            var findAnswer = await context.Answers.FirstOrDefaultAsync(s => s.Id == answer);
            if (findAnswer == null)
                return BadRequest("Не найден вариант ответа");
            context.Answers.Remove(findAnswer);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("GetTest")] //тесты на гл меню препода уловно последние 5 штук
        public async Task<List<TModel>> GetTest()
        {
            var prepod = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var tests = (await context.Tests.Include(s => s.IdTeacherNavigation).Where(s => s.IdTeacher == prepod).OrderByDescending(s=>s.Id).Take(5).ToListAsync()).Select(s => (TModel)s).ToList();
            return tests;
        }

        [HttpGet("GetResults")] //тесты на гл меню препода уловно последние 5 штук
        public async Task<List<StAModel>> GetResults()
        {
            var prepod = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var results = await context.Studentanswers.Include(s=>s.IdStudentNavigation).Include(s => s.IdTestNavigation).Include(s => s.IdMarkNavigation).Where(s => s.IdTestNavigation.IdTeacher == prepod).ToListAsync();

            return results.Select(s=>(StAModel)s).ToList();
        }

        [HttpGet("GetAllTest")] //ok переделать под теты только для опреджеленного препода
        public async Task<List<TModel>> GetAllTest()
        {
            var tests = await context.Tests.Include(s => s.IdTeacherNavigation).Select(s=>(TModel)s).ToListAsync();
            return tests;

           
        }

        [HttpGet("GetSubjects")] //
        public async Task<IEnumerable<SubModel>> GetSubjects()
        {
            var prepod = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var tests = await context.Subjects.Include(s=>s.IdTeachers).Where(s => s.IdTeachers.FirstOrDefault(s => s.Id == prepod) != null).ToListAsync();
            return tests.Select(s=>(SubModel)s).ToList();
        }

        [HttpGet("GetTestWithQ")] //РАБОТАЕТ
        public async Task<List<QModel>> GetTestWithQ(int id)
        {
            var Q = await context.Questions.Include(s => s.Answers).Where(s => s.IdTest == id).Select(s => (QModel)s).ToListAsync();
            return Q;
        }

        [HttpPut("SaveChangedByTeacherWin")]
        public async Task<ActionResult> SaveChangedByTeacherWin(TeacherModel teacher)
        {
            try
            {
                var origin = context.Teachers.FirstOrDefault(s => s.Id == teacher.Id);
                context.Entry(origin).CurrentValues.SetValues((Teacher)teacher);
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

