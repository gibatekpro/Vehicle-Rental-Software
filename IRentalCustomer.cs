using System;
using System.Security.Claims;
namespace VehicleRentalSystemSoftware
{
	public interface IRentalCustomer
	{
        void ListAvailableVehicles(Schedule wantedSchedule, Type type);
        bool AddReservation(String number, Schedule wantedSchedule);

        bool ChangeReservation(String number, Schedule oldSchedule, Schedule newSchedule);

        bool DeleteReservation(String number, Schedule schedule);

    }
}

