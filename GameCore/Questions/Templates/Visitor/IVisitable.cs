namespace GameCore.Questions.Templates
{
    public interface IVisitable
    {
        Question Accept(IQuestionGenerator questionGenerator);
    }
}
