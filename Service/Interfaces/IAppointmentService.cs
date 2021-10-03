using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IAppointmentService
    {
        Task<IEnumerable<Appointment>> FindByUserId(long? userId);
        void AddAppointment(Appointment appointment);
        void DeleteAppointment(long? appointmentId);
        void UpdateAppointment(Appointment appointment);
    }
}
