//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlineExam.DbContext
{
    using System;
    using System.Collections.Generic;
    
    public partial class DataEntry_QuestionBank
    {
        public int Id { get; set; }
        public string Questions { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public string CorrectAns { get; set; }
        public string Mark { get; set; }
        public string PrevQnYear { get; set; }
        public int SubjectId { get; set; }
        public int ChapterId { get; set; }
        public int CreatedBy { get; set; }
        public int IsDeleted { get; set; }
        public int DeletedBy { get; set; }
        public int ModifiedBy { get; set; }
        public int IsActive { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public System.DateTime ModifiedDateTime { get; set; }
        public System.DateTime DeletedDateTime { get; set; }
        public int PgmId { get; set; }
        public int CourseId { get; set; }
        public int SubPgmId { get; set; }
        public string Photo { get; set; }
    }
}