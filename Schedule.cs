using System;

namespace VehicleRentalSystemSoftware
{
    public class Schedule : IOverlappable, IComparable<Schedule>
    {
        private DateOnly pickUpDate;

        private DateOnly dropOffDate;

        private Driver driver;

        private int totalRentDays;

        public Schedule(DateOnly PickUpDate, DateOnly DropOffDate, Driver driver)
        {
            this.SetPickUpDate(PickUpDate);
            this.SetDropOffDate(DropOffDate);
            this.SetDriver(driver);

            SetTotalRentDays(1);

            //calculate the total number of days the vehicle will be rented

            DateOnly aPickupDate = new DateOnly(PickUpDate.Year, PickUpDate.Month, PickUpDate.Day);

            while (aPickupDate < DropOffDate)
            {
                aPickupDate = aPickupDate.AddDays(1);

                SetTotalRentDays(GetTotalRentDays() + 1);
            }
        }

        public Schedule(DateOnly PickUpDate, DateOnly DropOffDate)
        {
            this.SetPickUpDate(PickUpDate);
            this.SetDropOffDate(DropOffDate);
            this.SetDriver(driver);

            SetTotalRentDays(1);

            //calculate the total number of days the vehicle will be rented

            DateOnly aPickupDate = new DateOnly(PickUpDate.Year, PickUpDate.Month, PickUpDate.Day);

            while (aPickupDate < DropOffDate)
            {
                aPickupDate = aPickupDate.AddDays(1);

                SetTotalRentDays(GetTotalRentDays() + 1);
            }
        }

        public DateOnly GetPickUpDate()
        {
            return pickUpDate;
        }

        public void SetPickUpDate(DateOnly value)
        {
            pickUpDate = value;
        }

        public DateOnly GetDropOffDate()
        {
            return dropOffDate;
        }

        public void SetDropOffDate(DateOnly value)
        {
            dropOffDate = value;
        }

        public Driver GetDriver()
        {
            return driver;
        }

        public void SetDriver(Driver value)
        {
            driver = value;
        }

        public int GetTotalRentDays()
        {
            return totalRentDays;
        }

        public void SetTotalRentDays(int value)
        {
            totalRentDays = value;
        }


        public bool Overlaps(Schedule schedule)
        {
            //check if the intended pick up date has been booked
            if (schedule.GetPickUpDate() >= this.GetPickUpDate() && schedule.GetPickUpDate() <= this.GetDropOffDate())
            {
                return true;
            }

            return false;
        }


        public int CompareTo(Schedule? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return pickUpDate.CompareTo(other.pickUpDate);
        }
    }
}