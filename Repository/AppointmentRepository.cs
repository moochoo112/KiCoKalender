using DAL;
using Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AppointmentRepository
    {
        ILogger Logger { get; }
        private readonly KiCoKalenderContext _kiCoContext;
        public AppointmentRepository(ILogger<UserRepository> Logger, KiCoKalenderContext kiCoContext)
        {
            this.Logger = Logger;
            _kiCoContext = kiCoContext;
        }

        public async Task<IEnumerable<Appointment>> FindByUserId(long? userId)
        {

            IEnumerable<Appointment> appointments = _kiCoContext.Appointment.Where(a => a.userId == userId).ToList();
            Logger.LogInformation("Found assets by id: ", userId);
            return appointments;
        }

        public void AddAppointment(Appointment appointment)
        {
            _kiCoContext.Appointment.Add(appointment);
            _kiCoContext.SaveChanges();
            Logger.LogInformation("Added appointment");
        }

        public void DeleteAppointment(long? appointmentId)
        {
            Appointment foundAppointment = _kiCoContext.Appointment.Find(appointmentId);
            _kiCoContext.Appointment.Remove(foundAppointment);
            _kiCoContext.SaveChanges();
            Logger.LogInformation("Deleted appointment" + appointmentId);
        }

        public void UpdateAppointment(Appointment appointment)
        {
            Logger.LogInformation("Updated appointment");
        }
    }
}
