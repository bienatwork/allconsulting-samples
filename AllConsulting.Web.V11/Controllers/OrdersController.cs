using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Net; 
using System.Web.Mvc;
using AllConsulting.Entities;
using Newtonsoft.Json;
using PagedList;

namespace AllConsulting.Web.Controllers
{
    public class OrdersController : Controller
    {
        private ACSampleEntities db = new ACSampleEntities();

        // GET: Orders
        public ActionResult Index(string sortColumn, string sortBy, int? page, string searchString)
        { 
            //DateTime dt = DateTime.Now.AddDays(-1);
            //DateTime dt1 = DateTime.Now;
            //var days = dt.Subtract(dt1).TotalDays;
            //Response.Write(days); 
            ViewBag.sortColumn = sortColumn;
            ViewBag.sortBy = sortBy;
            IEnumerable<Order> result = null;

            string columnSort = sortColumn + sortBy;
            switch (columnSort)
            {
                case "customerorderasc":
                    result = db.Orders.OrderBy(o => o.CustomerNumber);
                    break;
                case "customerorderdesc":
                    result = db.Orders.OrderByDescending(o => o.ID);
                    break;
                case "deliverydateasc":
                    result = db.Orders.OrderBy(o => o.DeliveryDate);
                    break;
                case "deliverydatedesc":
                    result = db.Orders.OrderByDescending(o => o.DeliveryDate);
                    break;
                case "totalpriceasc":
                    result = db.Orders.OrderBy(o => o.TotalPrice);
                    break;
                case "totalpricedesc":
                    result = db.Orders.OrderByDescending(o => o.TotalPrice);
                    break;
                case "orderdateeasc":
                    result = db.Orders.OrderBy(o => o.OrderDate);
                    break;
                case "orderdatedesc":
                    result = db.Orders.OrderByDescending(o => o.OrderDate);
                    break;
                default:
                    result = db.Orders.OrderByDescending(o => o.ID);
                    break;
            }
            if (!string.IsNullOrWhiteSpace(searchString))
                result = result.Where(o => o.CustomerNumber.Contains(searchString) ||
                                           (o.DeliveryDate.HasValue && o.DeliveryDate.Value.ToShortDateString().Contains(searchString)) ||
                                           (o.OrderDate.HasValue && o.OrderDate.Value.ToShortDateString().Contains(searchString)) ||
                                           o.TotalPrice.ToString().Contains(searchString));
                //result = result.Where(x => x.CustomerNumber.Contains(searchString)
                //            ||
                //            (SqlFunctions.DateName("dd", x.OrderDate) + "/" +
                //             SqlFunctions.DatePart("mm", x.OrderDate) + "/" +
                //             SqlFunctions.DateName("yyyy", x.OrderDate)).Contains(searchString)
                //            ||
                //            (SqlFunctions.DateName("dd", x.DeliveryDate) + "/" +
                //             SqlFunctions.DatePart("mm", x.DeliveryDate) + "/" +
                //             SqlFunctions.DateName("yyyy", x.DeliveryDate)).Contains(searchString) ||
                //             SqlFunctions.StringConvert(x.TotalPrice).Contains(searchString));
            int pageNumber = page ?? 1;
            return View(result.ToPagedList(pageNumber, 10));
        }
        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CustomerNumber,DeliveryDate,TotalPrice,OrderDate,OnUpdate,Locked")] Order order)
        {
            if (ModelState.IsValid)
            {
                if (order.DeliveryDate.HasValue)
                {
                    if (order.DeliveryDate.Value.Subtract(DateTime.Now).TotalDays < 1)
                    {
                        ModelState.AddModelError(string.Empty, "Delivery date of order must be in the future.");
                        return View(order);
                    }
                }

                string strJsonPosition = string.Empty;
                if (!string.IsNullOrWhiteSpace(Request.Form["hdPosition"]))
                    strJsonPosition = Request.Form["hdPosition"];
                var listPosition = JsonConvert.DeserializeObject<List<OrderPosition>>(strJsonPosition);
                if (listPosition.Count < 1)
                {
                    ModelState.AddModelError(string.Empty, "Order must have at least one position");
                    order.OrderPositions = listPosition;
                    return View(order);
                }
                 
                order.OnUpdate = DateTime.Now;
                order.Locked = false;
                order.OrderDate = DateTime.Now;

                db.Orders.Add(order);
                db.SaveChanges();

                double totalPrice = 0;
                foreach (var position in listPosition)
                {
                    position.OrderID = order.ID;
                    position.Total = position.PositionNumber * position.Price;
                    totalPrice += position.Total;
                    db.OrderPositions.Add(position); 
                }
                order.TotalPrice = totalPrice;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        } 

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( int? id, byte[] rowVersion)
        {
            //[Bind(Include = "ID,CustomerNumber,DeliveryDate,TotalPrice,OrderDate,OnUpdate,Locked")]
            Order order = null;
            var fieldsToBind = new string[] { "ID", "CustomerNumber", "DeliveryDate", "TotalPrice", "OrderDate", "RowVersion" }; // "OnUpdate",

            if (ModelState.IsValid)
            {
                if (!id.HasValue) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                order = db.Orders.Find(id);
                if (order == null)
                {
                    order = new Order();
                    TryUpdateModel(order, fieldsToBind);
                    ModelState.AddModelError(string.Empty,
                 "Unable to save changes. The department was deleted by another user.");
                    return View(order);
                }
                var deliveryDate = order.DeliveryDate;
                if (TryUpdateModel(order, fieldsToBind))
                {
                    try
                    {
                        db.Entry(order).OriginalValues["RowVersion"] = rowVersion;

                        string strJsonPosition = string.Empty, strJsonPositionDelete = string.Empty;
                        if (!string.IsNullOrWhiteSpace(Request.Form["hdPosition"]))
                            strJsonPosition = Request.Form["hdPosition"];
                        if (!string.IsNullOrWhiteSpace(Request.Form["hdPositionDelete"]))
                            strJsonPositionDelete = Request.Form["hdPositionDelete"];

                        var listPosition = JsonConvert.DeserializeObject<List<OrderPosition>>(strJsonPosition);
                        var listPositionDelete = JsonConvert.DeserializeObject<List<int>>(strJsonPositionDelete);

                        if (deliveryDate.HasValue && order.DeliveryDate.HasValue)
                        {
                            var value = order.DeliveryDate.Value.Subtract(deliveryDate.Value).TotalDays;
                            if ((int)value != 0)
                            {
                                value = order.DeliveryDate.Value.Subtract(DateTime.Now).TotalDays;
                                if (value <= 0)
                                {
                                    ModelState.AddModelError(string.Empty, "DeliveryDate of order must be in the future");
                                    //order.OrderPositions = listPosition;
                                    return View(order);
                                }
                            }
                        }
                        else
                        {
                            if (!deliveryDate.HasValue && order.DeliveryDate.HasValue)
                            {
                                var value = order.DeliveryDate.Value.Subtract(DateTime.Now).TotalDays;
                                if (value <= 0)
                                {
                                    ModelState.AddModelError(string.Empty, "DeliveryDate of order must be in the future");
                                    //order.OrderPositions = listPosition;
                                    return View(order);
                                }
                            }
                        }

                        foreach (var positinId in listPositionDelete)
                        {
                            var o = db.OrderPositions.Find(positinId);
                            db.OrderPositions.Remove(o);
                        }

                        if (listPosition.Count < 1)
                        {
                            ModelState.AddModelError(string.Empty, "Order must have at least one position");
                            return View(order);
                        }

                        order.Locked = false;
                        order.OnUpdate = DateTime.Now; 
                         

                        double totalPrice = 0;
                        foreach (var position in listPosition)
                        {
                            if (position.ID > 0)
                            {
                                position.Total = position.PositionNumber * position.Price;
                                if (db.Entry(position).State != EntityState.Modified)
                                    db.Entry(position).State = EntityState.Modified;
                                //db.SaveChanges();
                            }
                            else
                            {
                                position.OrderID = order.ID;
                                position.Total = position.PositionNumber * position.Price;
                                db.OrderPositions.Add(position);
                            }
                            totalPrice += position.Total;
                        }
                        order.TotalPrice = totalPrice;

                        db.Entry(order).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        var entry = ex.Entries.Single();
                        var clientValues =  (Order)entry.Entity;
                        var databaseEntry = entry.GetDatabaseValues();
                        if (databaseEntry == null)
                        {
                            ModelState.AddModelError(string.Empty,
                                "Unable to save changes. The department was deleted by another user.");
                        }
                        else
                        {
                            var databaseValues = (Order) databaseEntry.ToObject();
                            if (databaseValues.CustomerNumber != clientValues.CustomerNumber)
                                ModelState.AddModelError("CustomerNumber", "Current value: "
                                                                           + databaseValues.CustomerNumber);
                            if (databaseValues.DeliveryDate != clientValues.DeliveryDate)
                                ModelState.AddModelError("DeliveryDate", "Current value: "
                                                                   + String.Format("{0:yyyy/MM/dd}", databaseValues.DeliveryDate));
                            if (databaseValues.TotalPrice != clientValues.TotalPrice)
                                ModelState.AddModelError("TotalPrice", "Current value: "
                                                                      + String.Format("{0:C2}", databaseValues.TotalPrice)); 
                            ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                                                                   +
                                                                   "was modified by another user after you got the original value. The "
                                                                   +
                                                                   "edit operation was canceled and the current values in the database "
                                                                   +
                                                                   "have been displayed. If you still want to edit this record, click "
                                                                   +
                                                                   "the Save button again. Otherwise click the Back to List hyperlink.");
                            order.RowVersion = databaseValues.RowVersion;
                        }
                    }
                    catch (RetryLimitExceededException ex)
                    {
                        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                    }
                }

            }
            else
            {
                order = new Order();
                TryUpdateModel(order, fieldsToBind);
            }
            return View(order); 
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id, bool? concurrencyError)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                if (concurrencyError.GetValueOrDefault())
                {
                    return RedirectToAction("Index");
                }
                return HttpNotFound();
            }
            if (concurrencyError.GetValueOrDefault())
            {
                ViewBag.ConcurrencyErrorMessage = "The record you attempted to delete "
                    + "was modified by another user after you got the original values. "
                    + "The delete operation was canceled and the current values in the "
                    + "database have been displayed. If you still want to delete this "
                    + "record, click the Delete button again. Otherwise "
                    + "click the Back to List hyperlink.";
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Order order)
        {
            try
            {
                db.Entry(order).State = EntityState.Deleted;
                  db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                return RedirectToAction("Delete", new { concurrencyError = true, id = order.ID });
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError(string.Empty, "Unable to delete. Try again, and if the problem persists contact your system administrator.");
                return View(order);
            }
        }

        [HttpPost, ActionName("ReOrder")]
        [ValidateAntiForgeryToken]
        public ActionResult ReOrder(int? id)
        {
            if (!id.HasValue) RedirectToAction("Index");

            var order = db.Orders.Find(id);
            if (order == null) RedirectToAction("Index");
           
            order.OnUpdate = DateTime.Now;
            order.OrderDate = DateTime.Now;
            order.DeliveryDate = null;
            double totPrice = 0;

            foreach (var p in order.OrderPositions)
            {
                p.Total = p.PositionNumber*p.Price;
                totPrice += p.Total;
                db.OrderPositions.Add(p);
            }
            order.TotalPrice = totPrice;
            db.Orders.Add(order);
            db.SaveChanges();

            return RedirectToAction("Edit", new { id = order.ID});
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
