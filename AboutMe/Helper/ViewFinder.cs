using GameCore.Questions;
using GameCore.Questions.Visitor;
using System;

namespace AboutMe.Helper
{
    public class ViewFinder : IViewFinder
    {
        public string GetView(TitlesQuestion titlesQuestion)
        {
            return "ChooseOneQuestion";
        }

        public string GetView(CommonQuestion commonQuestion)
        {
            return "CommonQuestion";
        }

        public string GetView(TrueOrFalseQuestion trueOrFalseQuestion)
        {
            return "TrueOrFalseQuestion";
        }
    }
}
