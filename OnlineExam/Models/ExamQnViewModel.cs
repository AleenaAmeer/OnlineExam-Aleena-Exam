using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineExam.Models
{
    public class ExamQnViewModel
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Exam Name")]
        public string Name { get; set; }

        [Display(Name = "Program")]
        public int PgmId { get; set; }

        [Display(Name = "Class")]
        public int ClassId { get; set; }

        [Display(Name = "Course")]
        public int CourseId { get; set; }

        [Display(Name = "Subject")]
        public int SubjectId { get; set; }

        [Display(Name = "From Date")]
        public System.DateTime FromDate { get; set; }
        [Display(Name = "To Date")]
        public System.DateTime ToDate { get; set; }



        [Display(Name = "Exam Time")]
        public string ExamTime { get; set; }

        [Display(Name = "Total Mark")]
        public int TotalMark { get; set; }

       

        [Display(Name = "Questions")]
        public string QnId { get; set; }


        [Display(Name = "IsDataEntry")]
        public int IsDataEntryQn { get; set; }

    }
}