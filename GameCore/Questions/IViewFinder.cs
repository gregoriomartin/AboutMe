namespace GameCore.Questions.Visitor
{
    public interface IViewFinder
    {
        string GetView(TitlesQuestion titlesQuestion);
        string GetView(CommonQuestion commonQuestion);
        string GetView(TrueOrFalseQuestion trueOrFalseQuestion);
    }
}
