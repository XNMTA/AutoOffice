﻿using System;
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
            String username=CommonUser.pureName(User.Identity.Name);
            ViewBag.Hours = GetLeftHoursByUser(username);
            ViewBag.UserName = username;
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
                Employee leader = CommonUser.findEmployeeByName(emp.LeaveReportTo, db);
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

        public ActionResult TeamWaitingList()
        {
            String userName=CommonUser.pureName(User.Identity.Name);
            return View(db.Leaves.Where(l => l.Approver==userName).ToList());
        }

        public ActionResult MyWaitingList()
        {
            String userName = CommonUser.pureName(User.Identity.Name);
            Employee user = CommonUser.findEmployeeByName(userName, db);
            if (user != null)
                return View(db.Leaves.Where(l => l.EmployId == user.ID).ToList());
            else
                return View(db.Leaves.Where(l => l.EmployId == Guid.Empty).ToList());
        }

        public ActionResult Approve(Leave leave)
        {
            if (ModelState.IsValid)
            {
                var leaveOne=db.Leaves.Find(leave.ID);
                leaveOne.status = LeaveApproveStatus.Approved;
                leaveOne.Comments = leaveOne.Comments + "------>[approve]";
                db.SaveChanges();
                updateLeavePool(leaveOne);
                return RedirectToAction("TeamWaitingList");
            }
            return View(leave);
        }

        private void updateLeavePool(Leave leaveOne)
        {
            var name=leaveOne.EmployName;
            var hours = leaveOne.Hours;
            var leavePool = GetLeavePoolByName(name);
            leavePool.Hours = leavePool.Hours - hours;
            db.SaveChanges();
        }

        public ActionResult Reject(Leave leave, string comment)
        {
            if (ModelState.IsValid)
            {
                var leaveOne = db.Leaves.Find(leave.ID);
                leaveOne.status = LeaveApproveStatus.Reject;
                leaveOne.Comments = leaveOne.Comments + "------>[reject]" + comment;
                db.SaveChanges();
                return RedirectToAction("TeamWaitingList");
            }
            return View(leave);
        }

        protected double GetLeftHoursByUser(string username) {
            var leavepool = GetLeavePoolByName(username);
            return leavepool.Hours;
        }

        private LeavePool GetLeavePoolByName(string username)
        {
            var query = from e in db.LeavePools
                        where e.EmployeeName == username
                        select e;
            var leavepool = query.FirstOrDefault();
            return leavepool;
        }
    }
}