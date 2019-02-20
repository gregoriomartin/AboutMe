using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GameCore.Questions.Templates
{
    public class CodingTemplate : QuestionTemplate
    {
        public static readonly IList<QuizAndAnswer<string, string>> Questions = new ReadOnlyCollection<QuizAndAnswer<string, string>>(new[]
            {
                new QuizAndAnswer<string, string> { Quiz =  "What will be the output of the following code Snippet?\n" +
                    "using System;\n"+
                          "public class Program\n"+
                          "        {\n"+
                          "            public static void Main(string[] args)\n"+
                          "            {\n"+
                          "                Console.WriteLine(Math.Round(6.5));\n"+
                          "            }\n" +
                          "        }\n",
                    Answer = "6" },
                new QuizAndAnswer<string, string> { Quiz =  "What will be the output of the following code Snippet?\n" +
                          "using System;\n"+
                          "public class Program\n"+
                          "    {\n"+
                          "        static void arrayMethod(int[] a)\n"+
                          "        {\n"+
                          "            int[] b = new int[5];\n"+
                          "            a = b;\n"+
                          "        }\n"+
                          "        public static void Main(string[] args)\n"+
                          "        {\n"+
                          "            int[] arr = new int[10];\n"+
                          "            arrayMethod(arr);\n"+
                          "            Console.WriteLine(arr.Length);\n"+
                          "        }\n" +
                          "    }\n",
                    Answer = "10" },
                new QuizAndAnswer<string, string> { Quiz = "<strong>This one is pretty hard</strong>\n"+
                    "What will be the output of the following code? You can check the code here <a href=\"https://www.compilejava.net/\" target=\"_blank\">CompileJava</href>\n" +
                                                            "public class HelloWorld\n" +
                                                           "{\n" +
                                                           "  public static void main(String[] args)\n" +
                                                           "        {\n" +
                                                           "            A a = new C();\n" +
                                                           "            a.m3();\n" +
                                                           "        }\n" +
                                                           "    }\n" +
                                                           "\n"  +
                                                           "    public class A\n" +
                                                           "    {\n" +
                                                           "        public void m1()\n" +
                                                           "        {\n" +
                                                           "            System.out.println(\"1\");\n"+
                                                           "        }\n"+
                                                           "" +
                                                           "        public void m2()\n" +
                                                           "        {\n" +
                                                           "            System.out.println(\"2\");\n"+
                                                           "        }\n"+
                                                           "\n"+
                                                           "        public void m3()\n"+
                                                           "        {\n"+
                                                           "\n"+
                                                           "            System.out.println(\"3\");\n"+
                                                           "        }\n"+
                                                           "    }\n"+
                                                           "\n"+
                                                           "    public class B extends A\n"+
                                                           "    {\n"+
                                                           "    public void m1()\n"+
                                                           "    {\n"+
                                                           "        this.m2();\n"+
                                                           "    }\n"+
                                                           "\n"+
                                                           "    public void m2()\n"+
                                                           "    {\n"+
                                                           "        super.m3();\n"+
                                                           "    }\n"+
                                                           "\n"+
                                                           "    public void m3()\n"+
                                                           "    {\n"+
                                                           "        super.m1();\n"+
                                                           "    }\n"+
                                                           "}\n"+
                                                           "\n"+
                                                           "public class C extends B\n"+
                                                           "{\n"+
                                                           "    public void m1()\n"+
                                                           "{\n"+
                                                           "    super.m2();\n"+
                                                           "}\n"+
                                                           "\n"+
                                                           "public void m2()\n"+
                                                           "{\n"+
                                                           "    super.m3();\n"+
                                                           "}\n"+
                                                           "\n"+
                                                           "public void m3()\n"+
                                                           "{\n"+
                                                           "    super.m1();\n"+
                                                           "}\n"+
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
