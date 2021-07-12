using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineExam.DbContext;
using OnlineExam.Models;

namespace OnlineExam.Controllers
{
    public class ExamsController : Controller
    {
        private Exam_DBEntities db = new Exam_DBEntities();

        public int examid = 0;


     

      

        // GET: Exams
        public async Task<ActionResult> Index()
        {
            return View(await db.Exams.Where(d => d.IsDeleted == 0).ToListAsync());
        }

        // GET: Exams/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exam exam = await db.Exams.FindAsync(id);
            if (exam == null)
            {
                return HttpNotFound();
            }
            return View(exam);
        }

        // GET: Exams/Create
        //public ActionResult Create()
        //{

        //    ViewBag.ClassID = new SelectList(db.Classes.Where(d => d.IsDeleted == 0), "Id", "Name");
        //    ViewBag.ProgramID = new SelectList(db.Programmes.Where(d => d.IsDeleted == 0), "Id", "Name");
        //    ViewBag.SubProgramID = new SelectList(db.SubPrograms.Where(d => d.IsDeleted == 0), "Id", "Name");
        //    ViewBag.SubjectID = new SelectList(db.Subjects.Where(d => d.IsDeleted == 0), "Id", "Name");
        //    ViewBag.CourseID = new SelectList(db.Courses.Where(d => d.IsDeleted == 0), "Id", "Name");
        //    return View();
        //}
      
        // POST: Exams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create([Bind(Include = "Id,Name,PgmId,ClassId,CourseId,SubjectId,CreatedBy,CreatedDateTime,IsActive,IsDeleted,DeletedBy,ModifiedBy,ModifiedDateTime,FromDate,ToDate,DeletedDateTime,ExamTime,TotalMark")] Exam exam)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        exam.CreatedBy = 0;
        //        exam.IsDeleted = 0;
        //        exam.IsActive = 0;
        //        exam.DeletedBy = 0;
        //        exam.ModifiedBy = 0;
        //        exam.ModifiedDateTime = DateTime.Now;
        //        exam.DeletedDateTime = DateTime.Now;           
        //        exam.CreatedDateTime = DateTime.Now;



        //        db.Exams.Add(exam);
        //        await db.SaveChangesAsync();

        //        examid = exam.Id;

        //        //if (examid != 0)
        //        //{

        //        //    Exam_QnTable qn = new Exam_QnTable();

        //        //    foreach (Exam_QnTable qnId in questions)
        //        //    {
        //        //        //  qn.QnId.Add(qns);
        //        //        qn.QnId = Convert.ToInt32(qnId.QnId);
        //        //        qn.ExamId = examid;
        //        //    }


        //        //    db.Exam_QnTable.Add(qn);
        //        //}
                
        //        // db.Exam_QnTable.Add(data)
                
              


        //     //   return RedirectToAction("InsertExamQnTable", questions);
        //    }

        //    return RedirectToAction("Index");
        //}

        // GET: Exams/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exam exam = await db.Exams.FindAsync(id);

            ExamQnViewModel examdata = new ExamQnViewModel()
            {
                Id = exam.Id,
                Name = exam.Name,
                ExamTime = exam.ExamTime,
                FromDate = exam.FromDate,
                ToDate = exam.ToDate,
                TotalMark = exam.TotalMark,
                


            };
           
            if (examdata == null)
            {
                return HttpNotFound();
            }


            ViewBag.ClassID = new SelectList(db.Classes.Where(d => d.IsDeleted == 0), "Id", "Name", exam.ClassId.ToString());
            ViewBag.DefaultClassID = new SelectList(db.Classes.Where(d => d.IsDeleted == 0 && d.Id == exam.ClassId), "Id", "Name");
            // exam.ClassId.ToString();

            ViewBag.ProgramID = new SelectList(db.Programmes.Where(d => d.IsDeleted == 0), "Id", "Name", exam.PgmId.ToString());
            ViewBag.DefaultProgramID = new SelectList(db.Programmes.Where(d => d.IsDeleted == 0 && d.Id == exam.PgmId), "Id", "Name");



            ViewBag.SubjectID = new SelectList(db.Subjects.Where(d => d.IsDeleted == 0), "Id", "Name", exam.SubjectId.ToString());
            ViewBag.DefaultSubjectID = new SelectList(db.Subjects.Where(d => d.IsDeleted == 0 && d.Id == exam.SubjectId), "Id", "Name");

            ViewBag.CourseID = new SelectList(db.Courses.Where(d => d.IsDeleted == 0), "Id", "Name", exam.CourseId.ToString());
            ViewBag.DefaultCourseID = new SelectList(db.Courses.Where(d => d.IsDeleted == 0 && d.Id == exam.CourseId), "Id", "Name");


            var result = db.Exam_QnTable.Where(d => d.ExamId == id).ToList();

            return View(examdata);
        }

        // POST: Exams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,PgmId,ClassId,CourseId,SubjectId,CreatedBy,CreatedDateTime,IsActive,IsDeleted,DeletedBy,ModifiedBy,ModifiedDateTime,FromDate,ToDate,DeletedDateTime,ExamTime,TotalMark")] Exam exam, ExamQnViewModel model)
        {
            if (ModelState.IsValid)
            {

                exam.CreatedBy = 0;
                exam.IsDeleted = 0;
                exam.IsActive = 0;
                exam.DeletedBy = 0;
                exam.ModifiedBy = 0;
                exam.ModifiedDateTime = DateTime.Now;
                exam.DeletedDateTime = DateTime.Now;
                exam.CreatedDateTime = DateTime.Now;

                db.Entry(exam).State = EntityState.Modified;
                await db.SaveChangesAsync();




                var names = model.QnId;
                if (names != null)
                {
                    List<string> result = names.Split('|').ToList();



                    foreach (string qnId in result)
                    {
                        if (qnId != "")
                        {
                            var ExamQnTable = new Exam_QnTable()
                            {

                                QnId = qnId,
                                ExamId = model.Id,
                                IsDataEntryQn = model.IsDataEntryQn

                            };


                            db.Exam_QnTable.Add(ExamQnTable);
                            db.SaveChanges();
                        }
                    }
                }
            }
            //return View(exam);
            return RedirectToAction("Index");
        }

        // GET: Exams/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exam exam = await db.Exams.FindAsync(id);
            if (exam == null)
            {
                return HttpNotFound();
            }
            return View(exam);
        }

        // POST: Exams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Exam exam = await db.Exams.FindAsync(id);

            exam.IsDeleted = 1;
            exam.DeletedBy = 0;
            exam.DeletedDateTime = DateTime.Now;

            //   db.Exams.Remove(exam);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        //  GetCourseWiseClass
        public ActionResult GetCourseWiseClass(int id)
        {

            var result = new SelectList(db.Courses.Where(r => r.ClassId == id && r.IsDeleted == 0), "Id", "Name");
            return Json(result, JsonRequestBehavior.AllowGet);



        }

        //GetPgm
        public ActionResult GetPgm()
        {

            var result = new SelectList(db.Programmes.Where(d => d.IsDeleted == 0), "Id", "Name");
            return Json(result, JsonRequestBehavior.AllowGet);



        }

        //GetClass
        public ActionResult GetClass()
        {

            var result = new SelectList(db.Classes.Where(d => d.IsDeleted == 0), "Id", "Name");
            return Json(result, JsonRequestBehavior.AllowGet);



        }

        //GetSubjects
        public ActionResult GetSubjects()
        {

            var result = new SelectList(db.Subjects.Where(d => d.IsDeleted == 0), "Id", "Name");
            return Json(result, JsonRequestBehavior.AllowGet);


        }


        //GetQnFromQnBank
        public ActionResult GetQnFromQnBank(Exam exam)
        {

            var result = db.DataEntry_QuestionBank.Where(d => d.IsDeleted == 0 && d.PgmId == exam.PgmId && d.CourseId == exam.CourseId && d.SubjectId == exam.SubjectId).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);



        }
        //GetManualQn
        public ActionResult GetManualQn(Exam exam)
        {

            var result = db.Teachers_QuestionBank.Where(d => d.IsDeleted == 0 && d.PgmId == exam.PgmId && d.CourseId == exam.CourseId && d.SubjectId == exam.SubjectId && d.CreatedBy == exam.CreatedBy).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);



        }

        //GetExamIdWiseQns
        public ActionResult GetExamIdWiseQns(int id)
        {

            var result = db.GetExamIdWiseQuestions(id);

            //List<DataEntry_QuestionBank> dataqns = null;
            //List<Teachers_QuestionBank> manualqns = null;





            //foreach (var i in result)
            //{
            //    int qnid = Convert.ToInt32(i.QnId);

            //    if (i.IsDataEntryQn == 1)
            //    {

            //        dataqns = db.DataEntry_QuestionBank.Where(d => d.Id == qnid).ToList();

            //    }
            //    else
            //    {
            //        manualqns = db.Teachers_QuestionBank.Where(d => d.Id == qnid).ToList();

            //    }
            //}


            //if (dataqns == null)
            //{
            //    return Json(manualqns, JsonRequestBehavior.AllowGet);
            //}
            //else
            //{

            //    return Json(dataqns, JsonRequestBehavior.AllowGet);
            //}

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult CreateExam()
        {
            ViewBag.ClassID = new SelectList(db.Classes.Where(d => d.IsDeleted == 0), "Id", "Name");
            ViewBag.ProgramID = new SelectList(db.Programmes.Where(d => d.IsDeleted == 0), "Id", "Name");
            ViewBag.SubProgramID = new SelectList(db.SubPrograms.Where(d => d.IsDeleted == 0), "Id", "Name");
            ViewBag.SubjectID = new SelectList(db.Subjects.Where(d => d.IsDeleted == 0), "Id", "Name");
            ViewBag.CourseID = new SelectList(db.Courses.Where(d => d.IsDeleted == 0), "Id", "Name");
            return View();

        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CreateExam(ExamQnViewModel model)
        {
            var Exam = new Exam()
            {
                Name = model.Name,
                PgmId = model.PgmId,
                ClassId = model.ClassId,
                CourseId = model.CourseId,
                SubjectId = model.SubjectId,
                FromDate = model.FromDate,
                ToDate = model.ToDate,
                ExamTime = model.ExamTime,
                TotalMark = model.TotalMark,


                CreatedBy = 0,
                IsDeleted = 0,
                IsActive = 0,
                DeletedBy = 0,
                ModifiedBy = 0,
                ModifiedDateTime = DateTime.Now,
                DeletedDateTime = DateTime.Now,
                CreatedDateTime = DateTime.Now

            };


            if (ModelState.IsValid)
            {


                db.Exams.Add(Exam);
                
                db.SaveChanges();
                 examid = db.Exams.Max(item => item.Id);

            }

            var names = model.QnId;

            List<string> result = names.Split('|').ToList();

           

            foreach (string qnId in result)
            {
                if (qnId != "")
                {
                    var ExamQnTable = new Exam_QnTable()
                    {

                        QnId = qnId,
                        ExamId = examid,
                        IsDataEntryQn = model.IsDataEntryQn 

                    };


                    db.Exam_QnTable.Add(ExamQnTable);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
           // return View(model);
        }
      
    }
}

