using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using autoOffice.Models;
using autoOffice.Common;

namespace autoOffice.Controllers
{
    public class LeaveController : Controller
    {
        private DbHelper db = new DbHelper();
        //
        // GET: /Leave/

        public ActionResult Index()
        {
            return View(db.Leaves.ToList());
        }

        //
        // GET: /Leave/Details/5

        public ActionResult Details(Guid id)
        {
            Leave leave = db.Leaves.Find(id);
            if (leave == null)
            {
                return HttpNotFound();
            }
            return View(leave);
        }

        //
        // GET: /Leave/Create

        public ActionResult Create()
        {
            db.Employees.Load();
            List<SelectListItem> items = new List<SelectListItem>();
            foreach(var item in db.Employees.Local){
                items.Add(new SelectListItem { Value = item.ID.ToString(), Text = item.MailAddress });
            }
            ViewData["approver"] = items;
            return View();
        }

        //
        // POST: /Leave/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Leave leave)
        {
            if (ModelState.IsValid)
            {
                leave.ID = Guid.NewGuid();
                leave.EmployName = CommonUser.pureName(User.Identity.Name);
                Employee emp=CommonUser.findEmployeeByName(leave.EmployName,db);
                Employee leader = CommonUser.findEmployeeById(emp.LeaveReportTo, db);
                leave.EmployId = emp.ID;
                leave.Approver = leader.Name;
                db.Leaves.Add(leave);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(leave);
        }

        //
        // GET: /Leave/Edit/5

        public ActionResult Edit(Guid id )
        {
            Leave leave = db.Leaves.Find(id);
            if (leave == null)
            {
                return HttpNotFound();
            }
            return View(leave);
        }

        //
        // POST: /Leave/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Leave leave)
        {
            if (ModelState.IsValid)
            {
                db.Entry(leave).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(leave);
        }

        //
        // GET: /Leave/Delete/5

        public ActionResult Delete(Guid id )
        {
            Leave leave = db.Leaves.Find(id);
            if (leave == null)
            {
                return HttpNotFound();
            }
            return View(leave);
        }

        //
        // POST: /Leave/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Leave leave = db.Leaves.Find(id);
            db.Leaves.Remove(leave);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}