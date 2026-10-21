using System;
using System.Collections.Generic;
using System.Text;
using FixFlow.Data;
using FixFlow.Models;

namespace FixFlow.Services
{
    public static class BackendSelfTest
    {
        public static string Run()
        {
            StringBuilder log = new StringBuilder();
            int failed = 0;

            Action<string, bool> check = (name, ok) =>
            {
                log.AppendLine((ok ? "PASS  " : "FAIL  ") + name);
                if (!ok)
                    failed++;
            };

            Action<string, Action> expectError = (name, action) =>
            {
                bool raised = false;
                try
                {
                    action();
                }
                catch (Exception)
                {
                    raised = true;
                }
                check(name, raised);
            };

            User previousUser = Session.CurrentUser;
            int customerId = 0;
            int deviceId = 0;
            int orderId = 0;
            int partId = 0;
            int stockBefore = 0;

            try
            {
                log.AppendLine("Connected to: " + Database.ConnectedServer());

                UserRepository users = new UserRepository();
                List<User> admins = users.GetByRole(UserRole.Administrator);
                List<User> technicians = users.GetByRole(UserRole.Technician);
                if (admins.Count == 0)
                    throw new InvalidOperationException("The self test needs an administrator account.");
                if (technicians.Count == 0)
                    throw new InvalidOperationException("The self test needs a technician account.");

                User admin = admins[0];
                User technician = technicians[0];
                Session.Start(admin);

                CustomerService customers = new CustomerService();
                DeviceService devices = new DeviceService();
                PartService parts = new PartService();
                ServiceRateService rates = new ServiceRateService();
                RepairService repairs = new RepairService();
                InventoryService inventory = new InventoryService();
                BillingService billing = new BillingService();
                ReportService reports = new ReportService();

                Customer customer = customers.Add("Self Test Customer", "09000000000", "Self test address");
                customerId = customer.CustomerId;
                check("customer saved and found again",
                    customerId > 0 && customers.GetById(customerId) != null);

                Device device = devices.Add(customerId, "Laptop", "TestBrand", "TestModel", "SELFTEST-001");
                deviceId = device.DeviceId;
                check("device saved and found again",
                    deviceId > 0 && devices.GetById(deviceId) != null);

                RepairOrder order = repairs.CreateOrder(deviceId, "Hardware Repair", "Self test problem");
                orderId = order.OrderId;
                check("repair order saved with status Received",
                    orderId > 0 && repairs.GetOrder(orderId).Status == RepairStatus.Received);

                ServiceType hardware = rates.GetAll().Find(s => s.Name == "Hardware Repair");
                if (hardware == null)
                    throw new InvalidOperationException("The Hardware Repair rate is missing.");

                Part part = parts.GetAll().Find(p => p.Stock >= 2);
                if (part == null)
                    throw new InvalidOperationException("The self test needs a part with at least 2 in stock.");
                partId = part.PartId;
                stockBefore = part.Stock;

                repairs.AssignTechnician(orderId, technician.UserId);
                repairs.UpdateDetails(orderId, "Self test diagnosis", 2m);
                check("technician and hours saved",
                    repairs.GetOrder(orderId).TechnicianId == technician.UserId
                    && repairs.GetOrder(orderId).HoursWorked == 2m);

                inventory.UsePart(orderId, partId, 2);
                check("stock went down by 2", parts.GetById(partId).Stock == stockBefore - 2);
                check("part added to the repair order", repairs.GetOrder(orderId).Parts.Count == 1);

                decimal expectedTotal = 2m * hardware.Rate + 2m * part.Price;
                BillingSummary summary = billing.GetSummary(orderId);
                check("total cost = labor + parts", summary.TotalCost == expectedTotal);

                billing.RecordPayment(orderId, 100m);
                summary = billing.GetSummary(orderId);
                check("balance = total - 100",
                    summary.AmountPaid == 100m && summary.Balance == expectedTotal - 100m);

                expectError("a payment above the balance is blocked",
                    () => billing.RecordPayment(orderId, expectedTotal));
                expectError("a repair cannot jump from Received to Repairing",
                    () => repairs.MoveOrder(orderId, RepairStatus.Repairing));

                repairs.MoveOrder(orderId, RepairStatus.Diagnosing);
                repairs.MoveOrder(orderId, RepairStatus.Repairing);
                check("repair reached Repairing", repairs.GetOrder(orderId).Status == RepairStatus.Repairing);

                User otherTechnician = technicians.Find(u => u.UserId != technician.UserId);
                if (otherTechnician != null)
                {
                    Session.Start(otherTechnician);
                    expectError("a technician cannot move another technician's repair",
                        () => repairs.MoveOrder(orderId, RepairStatus.Testing));
                }
                else
                {
                    log.AppendLine("SKIP  needs two technicians to test repair ownership");
                }

                Session.Start(technician);
                repairs.MoveOrder(orderId, RepairStatus.Testing);
                check("the assigned technician can move the repair", repairs.GetOrder(orderId).Status == RepairStatus.Testing);

                Session.Start(admin);
                repairs.MoveOrder(orderId, RepairStatus.Completed);
                expectError("release is blocked while a balance is unpaid",
                    () => repairs.MoveOrder(orderId, RepairStatus.Released));

                billing.RecordPayment(orderId, billing.GetSummary(orderId).Balance);
                check("balance is 0 after full payment", billing.GetSummary(orderId).Balance == 0m);

                repairs.MoveOrder(orderId, RepairStatus.Released);
                RepairOrder released = repairs.GetOrder(orderId);
                check("repair released with a DateOut",
                    released.Status == RepairStatus.Released && released.DateOut.HasValue);

                expectError("a released repair cannot get more parts",
                    () => inventory.UsePart(orderId, partId, 1));

                check("customer repair history finds the repair",
                    repairs.GetHistoryForCustomer(customerId).Count == 1);
                check("device repair history finds the repair",
                    repairs.GetHistoryForDevice(deviceId).Count == 1);

                check("report: repairs per status", reports.RepairsPerStatus().Rows.Count > 0);
                check("report: income per month", reports.IncomePerMonth().Rows.Count > 0);
                check("report: parts usage", reports.PartsUsage().Rows.Count > 0);
                check("report: technician workload", reports.TechnicianWorkload().Rows.Count > 0);
                check("report: unpaid balances runs", reports.UnpaidBalances() != null);
                check("report: repairs filtered by today's date",
                    reports.RepairsPerStatus(DateTime.Today, DateTime.Today).Rows.Count > 0);

                Session.Start(technician);
                expectError("a technician cannot create repairs",
                    () => repairs.CreateOrder(deviceId, "Software Fix", "Not allowed"));
                expectError("a technician cannot record payments",
                    () => billing.RecordPayment(orderId, 1m));
                expectError("a technician cannot open reports",
                    () => reports.RepairsPerStatus());
                expectError("a technician cannot change parts",
                    () => parts.Add("Not allowed", 1m, 1));
            }
            catch (Exception ex)
            {
                log.AppendLine("ERROR  " + ex.Message);
                failed++;
            }
            finally
            {
                try
                {
                    RepairPartRepository lines = new RepairPartRepository();
                    PaymentRepository payments = new PaymentRepository();

                    if (orderId > 0)
                    {
                        foreach (Payment payment in payments.GetByOrder(orderId))
                            payments.Delete(payment.PaymentId);
                        foreach (RepairPart line in lines.GetByOrder(orderId))
                            lines.ReturnPart(orderId, line.PartId);
                        new RepairOrderRepository().Delete(orderId);
                    }
                    if (deviceId > 0)
                        new DeviceRepository().Delete(deviceId);
                    if (customerId > 0)
                        new CustomerRepository().Delete(customerId);

                    log.AppendLine("Test data removed.");

                    if (partId > 0)
                        check("stock restored after cleanup",
                            new PartRepository().GetById(partId).Stock == stockBefore);
                }
                catch (Exception ex)
                {
                    log.AppendLine("CLEANUP FAILED  " + ex.Message);
                    failed++;
                }

                if (previousUser != null)
                    Session.Start(previousUser);
                else
                    Session.End();
            }

            log.AppendLine();
            log.AppendLine(failed == 0 ? "RESULT: ALL PASSED" : "RESULT: " + failed + " PROBLEM(S) FOUND");
            return log.ToString();
        }
    }
}
