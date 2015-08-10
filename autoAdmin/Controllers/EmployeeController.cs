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
    public class EmployeeController : Controller
    {
        //private EmployeeDBContext db = new EmployeeDBContext();
        private DbHelper db = new DbHelper();

        //
        // GET: /Employee/

        public ActionResult Index()
        {
            //return View(db.Employees.ToList());
            db.Employees.Load();
            db.Accesses.Load();
            var eList = db.Employees.Local;
            foreach (Employee e in eList)
            {
                //var name = e.Name;
                e.LeaveReportName = eList.FirstOrDefault((Employee eone) => eone.ID == e.LeaveReportTo).Name;
            }
            ViewBag.hasAccess = db.Accesses.Local.Count
                ((Access acc) => acc.AccessType == AccessType.UserAdmin 
                    && acc.UserName==CommonUser.pureName(User.Identity.Name))>0;
            var sortList=db.Employees.ToList();
                sortList.Sort(new Comparison<Employee>((b, c) => string.Compare(b.Name , c.Name)));
            return View(sortList);
        }
        //
        // GET: /Employee/Details/5

        public ActionResult Details(Guid id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.LeaveReportTo = db.Employees.Where((Employee e) => e.ID == employee.LeaveReportTo).First();
            return View(employee);
        }

        //
        // GET: /Employee/Create

        public ActionResult Create()
        {

            db.Employees.Load();
            ViewBag.LeaveReportTo = new SelectList(db.Employees.Local, "ID", "Name");
            return View();
        }

        //
        // POST: /Employee/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.ID = Guid.NewGuid();
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        //
        // GET: /Employee/Edit/5

        public ActionResult Edit(Guid id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            db.Employees.Load();
            ViewBag.LeaveReportToShow = new SelectList(db.Employees.Local, "ID", "Name");
            //(ViewBag.LeaveReportToShow as SelectList).First().Selected = true;
            return View(employee);
        }

        //
        // POST: /Employee/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        //
        // GET: /Employee/Delete/5

        public ActionResult Delete(Guid id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        //
        // POST: /Employee/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        [HttpPost]
        public JsonResult doesUserExist(string userName)
        {
            var user = db.Employees.Where(e => e.Name == userName).ToList();
            return Json(user.Count > 0);
        }
    }
}