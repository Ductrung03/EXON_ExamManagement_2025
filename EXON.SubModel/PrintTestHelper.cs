using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EXON.SubModel.Models;
using System.Web.Script.Serialization;

namespace EXON.SubModel
{
    public class PrintTestHelper
    {
        MTAQuizDbContext db;
        public PrintTestHelper()
        {
            db = new MTAQuizDbContext();
        }
        public List<Question2Print> GetListQuestion2Print(int testID)
        {
            List<TEST_DETAILS> lst_testdetails = db.TEST_DETAILS.Where(p => p.TestID == testID).OrderBy(p => p.TestDetailID).ToList();
            List<Question2Print> lst_ques2print = new List<Question2Print>();
            int no = 0;
            foreach (TEST_DETAILS td in lst_testdetails)
            {
                QUESTION question = db.QUESTIONS.Where(p => p.QuestionID == td.QuestionID).SingleOrDefault();
                // each test_detail contain a list of random answer (each random answer is 1 subquestion and list of its answer)
                List<RandomAnswer> lst_randomanswer = new JavaScriptSerializer().Deserialize<List<RandomAnswer>>(td.RandomAnswer);
                if (question != null)
                {
                    Question2Print printquestion = new Question2Print();
                    printquestion.QuestionID = question.QuestionID;
                    printquestion.QuestionContent = question.QuestionContent;
                    printquestion.Type = question.Type != null ? (int)question.Type : 0;
                    printquestion.NumberOfSubquestion = lst_randomanswer.Count();
                    // Get all Subquestion and list of its answer
                    List<SubQuestion2Print> lst_subques2print = new List<SubQuestion2Print>();
                    foreach (RandomAnswer ra in lst_randomanswer)
                    {
                        SUBQUESTION subquestion = db.SUBQUESTIONS.Where(p => p.SubQuestionID == ra.SubQuestionID).SingleOrDefault();
                        SubQuestion2Print printsub = new SubQuestion2Print();
                        printsub.SubQuestionID = subquestion.SubQuestionID;
                        printsub.SubQuestionContent = subquestion.SubQuestionContent;
                        printsub.Score = (float) subquestion.Score;
                        printsub.No = ++no;
                        List<Answer2Print> lst_ans2print = new List<Answer2Print>();
                        foreach(int id in ra.ListAnswerID)
                        {
                            ANSWER answer = db.ANSWERS.Where(p => p.AnswerID == id).SingleOrDefault();
                            Answer2Print printans = new Answer2Print();
                            printans.AnswerID = answer.AnswerID;
                            printans.AnswerContent = answer.AnswerContent;
                            printans.IsCorrect = answer.IsCorrect;
                            lst_ans2print.Add(printans);
                        }
                        printsub.lst_answers = lst_ans2print;
                        lst_subques2print.Add(printsub);
                    }
                    printquestion.lst_subquestions = lst_subques2print;
                    lst_ques2print.Add(printquestion);
                }
            }
            return lst_ques2print;
        }
          public List<Question2Print> GetListQuestion3Print(int testID)
          {
               List<TEST_DETAILS> lst_testdetails = db.TEST_DETAILS.Where(p => p.TestID == testID).OrderBy(p => p.TestDetailID).ToList();
               List<Question2Print> lst_ques2print = new List<Question2Print>();
               int no = 0;
               foreach (TEST_DETAILS td in lst_testdetails)
               {
                    QUESTION question = db.QUESTIONS.Where(p => p.QuestionID == td.QuestionID).SingleOrDefault();
                    // each test_detail contain a list of random answer (each random answer is 1 subquestion and list of its answer)
                    List<RandomAnswer> lst_randomanswer = new JavaScriptSerializer().Deserialize<List<RandomAnswer>>(td.RandomAnswer);
                    if (question != null)
                    {
                         Question2Print printquestion = new Question2Print();
                         printquestion.QuestionID = question.QuestionID;
                         printquestion.QuestionContent = question.QuestionContent;
                         printquestion.Type = question.Type != null ? (int)question.Type : 0;
                         printquestion.NumberOfSubquestion = lst_randomanswer.Count();
                         // Get all Subquestion and list of its answer
                         List<SubQuestion2Print> lst_subques2print = new List<SubQuestion2Print>();
                         foreach (RandomAnswer ra in lst_randomanswer)
                         {
                              SUBQUESTION subquestion = db.SUBQUESTIONS.Where(p => p.SubQuestionID == ra.SubQuestionID).SingleOrDefault();
                              SubQuestion2Print printsub = new SubQuestion2Print();
                              printsub.SubQuestionID = subquestion.SubQuestionID;
                              printsub.SubQuestionContent = subquestion.SubQuestionContent;
                              printsub.Score = (float)subquestion.Score;
                              printsub.No = ++no;
                              List<Answer2Print> lst_ans2print = new List<Answer2Print>();
                              List<Answer2Print> temp = new List<Answer2Print>();
                              foreach (int id in ra.ListAnswerID)
                              {
                                   ANSWER answer = db.ANSWERS.Where(p => p.AnswerID == id).SingleOrDefault();
                                   Answer2Print printans = new Answer2Print();
                                   printans.AnswerID = answer.AnswerID;
                                   printans.AnswerContent = answer.AnswerContent;
                                   printans.IsCorrect = answer.IsCorrect;
                                   if (answer.IsCorrect == true)
                                   {
                                        temp.Add(printans);
                                        lst_ans2print.InsertRange(0, temp);
                                   }
                                   else
                                   {
                                        lst_ans2print.Add(printans);
                                   }
                                   
                              }
                              printsub.lst_answers = lst_ans2print;
                              lst_subques2print.Add(printsub);
                         }
                         printquestion.lst_subquestions = lst_subques2print;
                         lst_ques2print.Add(printquestion);
                    }
               }
               return lst_ques2print;
          }
     }

    public class Question2Print
    {
        public int QuestionID { get; set; }
        public string QuestionContent { get; set; }
        public List<SubQuestion2Print> lst_subquestions { get; set; }
        public int Type { get; set; }
        public int NumberOfSubquestion { get; set; }
        public Question2Print()
        {
            QuestionID = -1;
            QuestionContent = "";
            lst_subquestions = null;
            Type = -1;
            NumberOfSubquestion = 0;
        }
    }

    public class SubQuestion2Print
    {
        public int SubQuestionID { get; set; }
        public string SubQuestionContent { get; set; }
        public float Score { get; set; }
        public int No { get; set; }
        public List<Answer2Print> lst_answers { get; set; }
        public SubQuestion2Print()
        {
            SubQuestionID = -1;
            SubQuestionContent = "";
            lst_answers = null;
        }
    }

    public class Answer2Print
    {
        public int AnswerID { get; set; }
        public string AnswerContent { get; set; }
         public bool IsCorrect { get; set; }
        public Answer2Print()
        {
            AnswerID = -1;
            AnswerContent = "";
            IsCorrect = false;
        }
    }

}
