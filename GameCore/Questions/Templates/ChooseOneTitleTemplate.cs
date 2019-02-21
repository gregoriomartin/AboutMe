using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GameCore.Questions.Templates
{
    public class ChooseOneTitleTemplate : QuestionTemplate
    {
        private List<Title> _allTitles;
        private bool _correctAdded = false;
        private int _counter = 0;
        private int _numberOfTitles = 3;

        public static readonly IList<string> Texts = new ReadOnlyCollection<string>(new[]
        {
            $"If {QuestionTemplate.GameMasterName} would be alone on an island, with a laptop and he could enjoy a single title until the battery runs out, which one would he choose?",
            "If someone wants to pay me to enjoy a title all day, what do you think I would choose?",
            $"A friend comes running to me: '{QuestionTemplate.GameMasterName}, {QuestionTemplate.GameMasterName}! I need help! Some kind of intergalactic monster kidnapping my father! He will returns him if you choose one title in the world and describe it perfectly'. Which would be the most appropriate? (Thinking of saving his father obviously;) )"
        });

        protected override Question Handle(IQuestionGenerator questionGenerator)
        {
            return questionGenerator.GenerateQuiz(this);
        }

        public override object GetNextQuiz()
        {
            int pos = _random.Next(Texts.Count);

            return new TitlesQuestion(Texts[pos], GetTitles());
        }

        public ChooseOneTitleTemplate SetTitles(List<Title> allTitles)
        {
            _allTitles = allTitles.ToList();
            return this;
        }

        public ChooseOneTitleTemplate SetNumberOfTitles(int numberOfTitles)
        {
            _numberOfTitles = numberOfTitles;
            return this;
        }
        
        public List<Title> GetTitles()
        {
            if (_allTitles is null)
                throw new ArgumentException("You have to load the titles with 'SetTitles' before using 'GetSetOfThree'");

            var titles = new List<Title>();

            while (_counter != _numberOfTitles)
            {
                titles.Add(GetNext());
            }

            _counter = 0;
            _correctAdded = false;

            return titles;
        }

        private Title GetNext()
        {
            long id;

            if (_counter >= 2 && !_correctAdded)
                id = NextId(true);
            else if(_correctAdded)
                id = NextId(false);
            else
                id = NextId();

                _counter++;

            Title title = _allTitles.Single(t => t.Id == id);
            _allTitles.Remove(title);
            if (!title.Fake) _correctAdded = true;
            return title;
        }

        private long NextId(bool? correct = null)
        {
            List<Title> allTitles;
            if (correct is null)
                allTitles = _allTitles;
            else 
                allTitles = _allTitles.Where(t => t.Fake != correct).ToList();

            long id = allTitles[_random.Next(allTitles.Count)].Id;

            return id;
        }
    }
}
