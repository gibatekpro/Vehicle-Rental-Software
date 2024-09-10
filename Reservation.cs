using System;
namespace VehicleRentalSystemSoftware
{
	public class Reservation : IComparable<Reservation>
	{
        private Schedule schedule;

        private DateOnly bookingDate;

        private double vehicleRentalPrice;

        private double totalPrice;

        public Reservation(Schedule schedule, DateOnly bookingDate, double vehicleRentalPrice)
		{
			this.SetSchedule(schedule);
			this.SetBookingDate(bookingDate);
			this.SetVehicleRentalPrice(vehicleRentalPrice);

            SetTotalPrice(vehicleRentalPrice * schedule.GetTotalRentDays());
		}

        public Schedule GetSchedule()
        {
            return schedule;
        }

        public void SetSchedule(Schedule value)
        {
            schedule = value;
        }


        public DateOnly? GetBookingDate()
        {
            return bookingDate;
        }

        public void SetBookingDate(DateOnly value)
        {
            bookingDate = value;
        }

        public double GetVehicleRentalPrice()
        {
            return vehicleRentalPrice;
        }

        public void SetVehicleRentalPrice(double value)
        {
            vehicleRentalPrice = value;
        }

        public double GetTotalPrice()
        {
            return totalPrice;
        }

        public void SetTotalPrice(double value)
        {
            totalPrice = value;
        }


        public void Display()
		{


            Console.WriteLine($"{DateConverter.DateToString(GetBookingDate()),-20}{GetSchedule().GetPickUpDate(),-20}{GetSchedule().GetDropOffDate(),-20}" +
				$"{GetTotalPrice(),-20:F2}{GetSchedule().GetDriver().GetFirstName(),-20}{GetSchedule().GetDriver()!.GetFirstName(),-20}{GetSchedule().GetDriver().GetSurname(),-20}{DateConverter.DateToString(GetSchedule().GetDriver().GetDateOfBirth()),-20}" +
				$"{GetSchedule().GetDriver().GetLicenseNumber(),-20}\n");
		}


		public void Display(StreamWriter outputFile)
		{


			outputFile.WriteLine($"{DateConverter.DateToString(GetBookingDate()),-20}{GetSchedule().GetPickUpDate(),-20}{GetSchedule().GetDropOffDate(),-20}" +
			                     $"{GetTotalPrice(),-20:F2}{GetSchedule().GetDriver().GetFirstName(),-20}{GetSchedule().GetDriver().GetSurname(),-20}{DateConverter.DateToString(GetSchedule().GetDriver().GetDateOfBirth()),-20}" +
			                     $"{GetSchedule().GetDriver().GetLicenseNumber(),-20}\n");
		}


		public int CompareTo(Reservation? other)
		{
			// If other is not a valid object reference, this instance is greater.
			if (other == null) return 1;

			//check if the pick up date of the current object
			//comes before the pick up date of the other object
			return GetSchedule().CompareTo(other.GetSchedule());
			
		}
	}
}

