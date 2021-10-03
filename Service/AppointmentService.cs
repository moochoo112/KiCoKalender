using Domain;
using Repository;
using Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public class AppointmentService : IAppointmentService
    {
        private AppointmentRepository AppointmentRepository { get; }

        public AppointmentService(AppointmentRepository AppointmentRepository)
        {
            this.AppointmentRepository = AppointmentRepository;
        }

        public void AddAppointment(Appointment appointment)
        {
            AppointmentRepository.AddAppointment(appointment);
        }

        public void DeleteAppointment(long? appointmentId)
        {
            AppointmentRepository.DeleteAppointment(appointmentId);
        }

        public Task<IEnumerable<Appointment>> FindByUserId(long? userId)
        {
                
            return AppointmentRepository.FindByUserId(userId);
        }

        public void UpdateAppointment(Appointment appointment)
        {
            AppointmentRepository.UpdateAppointment(appointment);
        }
    }
}
