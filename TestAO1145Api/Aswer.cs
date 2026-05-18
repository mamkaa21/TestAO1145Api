using System;
using System.Collections.Generic;

namespace TestAO1145Api;

public partial class Aswer
{
    public int Id { get; set; }

    public string? Text { get; set; }

    public int? IdQuestion { get; set; }

    public virtual Question? IdQuestionNavigation { get; set; }
}
public class SubmitTestDto
{
    public int StudentId { get; set; }
    public int TestId { get; set; }
    public List<AnswerSelectionDto> Answers { get; set; }
}

public class AnswerSelectionDto
{
    public int QuestionId { get; set; }
    public string SelectedText { get; set; }
}

