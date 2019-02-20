using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GameCore.Questions.Templates
{
    public class CodingTemplate : QuestionTemplate
    {
        public static readonly IList<QuizAndAnswer<string, string>> Questions = new ReadOnlyCollection<QuizAndAnswer<string, string>>(new[]
            {
                new QuizAndAnswer<string, string> { Quiz =  "What will be the output of the following C# code Snippet?\n" +
                    "using System;\n"+
                          "public class Program\n"+
                          "&nbsp; {\n"+
                          "&nbsp; &nbsp; public static void Main(string[] args)\n"+
                          "&nbsp; &nbsp; {\n"+
                          "&nbsp; &nbsp; &nbsp; Console.WriteLine(Math.Round(6.5));\n"+
                          "&nbsp; &nbsp; }\n" +
                          "&nbsp; }\n",
                    Answer = "6" },
                new QuizAndAnswer<string, string> { Quiz =  "What will be the output of the following C# code Snippet?\n" +
                          "using System;\n"+
                          "public class Program\n"+
                          "&nbsp; {\n"+
                          "&nbsp; &nbsp; static void arrayMethod(int[] a)\n"+
                          "&nbsp; &nbsp; {\n"+
                          "&nbsp; &nbsp; &nbsp; int[] b = new int[5];\n"+
                          "&nbsp; &nbsp; &nbsp; a = b;\n"+
                          "&nbsp; &nbsp; }\n"+
                          "&nbsp; &nbsp; public static void Main(string[] args)\n"+
                          "&nbsp; &nbsp; {\n"+
                          "&nbsp; &nbsp; &nbsp; int[] arr = new int[10];\n"+
                          "&nbsp; &nbsp; &nbsp; arrayMethod(arr);\n"+
                          "&nbsp; &nbsp; &nbsp; Console.WriteLine(arr.Length);\n"+
                          "&nbsp; &nbsp; }\n" +
                          "&nbsp; }\n",
                    Answer = "10" },
                new QuizAndAnswer<string, string> { Quiz = "<strong>This one is pretty hard</strong>\n"+
                    "What will be the output of the following Java code? Check the code here <a href=\"https://www.compilejava.net/\" target=\"_blank\">CompileJava</a>\n" +
                                                            "public class HelloWorld\n" +
                                                           "{\n" +
                                                           "&nbsp; public static void main(String[] args)\n" +
                                                           "&nbsp; {\n" +
                                                           "&nbsp; &nbsp; A a = new C();\n" +
                                                           "&nbsp; &nbsp; a.m3();\n" +
                                                           "&nbsp; }\n" +
                                                           "}\n" +
                                                           "\n"  +
                                                           "public class A\n" +
                                                           "{\n" +
                                                           "&nbsp; &nbsp; public void m1()\n" +
                                                           "&nbsp; &nbsp; {\n" +
                                                           "&nbsp; &nbsp; &nbsp; System.out.println(\"1\");\n"+
                                                           "&nbsp; &nbsp; }\n"+
                                                           "\n" +
                                                           "&nbsp; &nbsp; public void m2()\n" +
                                                           "&nbsp; &nbsp; {\n" +
                                                           "&nbsp; &nbsp; &nbsp; System.out.println(\"2\");\n"+
                                                           "&nbsp; &nbsp; }\n"+
                                                           "\n"+
                                                           "&nbsp; &nbsp; public void m3()\n"+
                                                           "&nbsp; &nbsp; {\n"+
                                                           "&nbsp; &nbsp; &nbsp; System.out.println(\"3\");\n"+
                                                           "&nbsp; &nbsp; &nbsp; }\n"+
                                                           "&nbsp; &nbsp; }\n"+
                                                           "\n"+
                                                           "public class B extends A\n"+
                                                           "{\n"+
                                                           "&nbsp; &nbsp; public void m1()\n"+
                                                           "&nbsp; &nbsp; {\n"+
                                                           "&nbsp; &nbsp; &nbsp; this.m2();\n"+
                                                           "&nbsp; &nbsp; }\n"+
                                                           "\n"+
                                                           "&nbsp; &nbsp; public void m2()\n"+
                                                           "&nbsp; &nbsp; {\n"+
                                                           "&nbsp; &nbsp; &nbsp; super.m3();\n"+
                                                           "&nbsp; &nbsp; }\n"+
                                                           "\n"+
                                                           "&nbsp; &nbsp; public void m3()\n"+
                                                           "&nbsp; &nbsp; {\n"+
                                                           "&nbsp; &nbsp; &nbsp; super.m1();\n"+
                                                           "&nbsp; &nbsp; }\n"+
                                                           "}\n"+
                                                           "\n"+
                                                           "public class C extends B\n"+
                                                           "{\n"+
                                                           "&nbsp; &nbsp; public void m1()\n"+
                                                           "&nbsp; &nbsp; {\n"+
                                                           "&nbsp; &nbsp; &nbsp; super.m2();\n"+
                                                           "&nbsp; &nbsp; }\n"+
                                                           "\n"+
                                                           "&nbsp; &nbsp; public void m2()\n"+
                                                           "&nbsp; &nbsp; {\n"+
                                                           "&nbsp; &nbsp; &nbsp; super.m3();\n"+
                                                           "&nbsp; &nbsp; }\n"+
                                                           "\n"+
                                                           "&nbsp; &nbsp; public void m3()\n"+
                                                           "&nbsp; &nbsp; {\n"+
                                                           "&nbsp; &nbsp; &nbsp; super.m1();\n"+
                                                           "&nbsp; &nbsp; }\n"+
                                                           "}", Answer = "1" }
            });

        public static int TextQuant => Questions.Count;

        public override Question Accept(IQuestionGenerator questionGenerator)
        {
            return questionGenerator.GenerateQuiz(this);
        }

        public override object GetNextQuiz()
        {
            int pos;

            do
            {
                pos = _random.Next(Questions.Count);
            } while (_positionsUsed.Any(pu => pu == pos));

            _positionsUsed.Add(pos);

            return Questions[pos];
        }
    }
}
