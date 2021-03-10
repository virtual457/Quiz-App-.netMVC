using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using QuizApp.Models;

namespace QuizApp.Controllers
{
    [Authorize]
    public class SubmittionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Submittions
        [Authorize(Roles = "Administrators")]
        public ActionResult Index()
        {
            return View(db.Submittions.ToList());
        }

        // GET: Submittions/Details/5
        [Authorize(Roles = "Administrators")]
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Submittion submittion = db.Submittions.Find(id);
            if (submittion == null)
            {
                return HttpNotFound();
            }
            return View(submittion);
        }

        // GET: Submittions/Create
        public ActionResult Create()
        {
            var model = new SubmissionVeiwModel();
            model.Questions = db.Questions.ToList();
            return View(model);
        }

        // POST: Submittions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SubmissionVeiwModel model)
        {
            
            var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(model.Answer);
            var count = db.Questions.ToList().Count;
            var correctcount = 0;
            model.Questions = db.Questions.ToList();
            foreach (var kv in dict)
            {
                if (db.Questions.Find(Convert.ToInt64(kv.Key)).Answer.ToString() == kv.Value.ToString())
                {
                    correctcount = correctcount + 1;
                }
            }
            Submittion submittion = new Submittion();
            if (ModelState.IsValid)
            {
                var blast = Math.Round((((double)correctcount / count)*100),2);
                submittion.Result = blast.ToString()+"%";
                submittion.SubmittedOn = DateTime.Now;
                submittion.SubmittedAnswer = model.Answer;

                submittion.SubmittedBy = User.Identity.Name;
                db.Submittions.Add(submittion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [Authorize(Roles = "Administrators")]
        // GET: Submittions/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Submittion submittion = db.Submittions.Find(id);
            if (submittion == null)
            {
                return HttpNotFound();
            }
            return View(submittion);
        }

        // POST: Submittions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrators")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,SubmittedAnswer,Result,SubmittedBy,SubmittedOn")] Submittion submittion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(submittion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(submittion);
        }
        [Authorize(Roles = "Administrators")]
        // GET: Submittions/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Submittion submittion = db.Submittions.Find(id);
            if (submittion == null)
            {
                return HttpNotFound();
            }
            return View(submittion);
        }
        [Authorize(Roles = "Administrators")]
        // POST: Submittions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Submittion submittion = db.Submittions.Find(id);
            db.Submittions.Remove(submittion);
            db.SaveChanges();
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
    }
    
}
public class SubmissionVeiwModel
{
    public List<Question> Questions { get; set; }
    public string Answer { get; set; }
}