using System;
namespace VehicleRentalSystemSoftware
{
    public class Vehicle : IComparable<Vehicle>

    {
        private string registrationNumber;

        private string make;

        private string model;

        private double dailyRentalPrice;

        private List<Reservation> reservations;

        public Vehicle(string registrationNumber, string make, string model, double dailyRentalPrice)
        {
            this.SetRegistrationNumber(registrationNumber);
            this.SetMake(make);
            this.SetModel(model);
            this.SetDailyRentalPrice(dailyRentalPrice);
            SetReservations(new List<Reservation>());
        }

        public List<Reservation> GetReservations()
        {
            return reservations;
        }

        public void SetReservations(List<Reservation> value)
        {
            reservations = value;
        }

        public double GetDailyRentalPrice()
        {
            return dailyRentalPrice;
        }

        public void SetDailyRentalPrice(double value)
        {
            dailyRentalPrice = value;
        }



        public string GetModel()
        {
            return model;
        }

        public void SetModel(string value)
        {
            model = value;
        }


        public string GetMake()
        {
            return make;
        }

        public void SetMake(string value)
        {
            make = value;
        }

        public string GetRegistrationNumber()
        {
            return registrationNumber;
        }

        public void SetRegistrationNumber(string value)
        {
            registrationNumber = value;
        }

        //This will be used to display details of a vehicle on the console
        public virtual void DisplayVehicle()
        {

                Console.WriteLine($"{"Make: " + GetMake(),-20}{"Model: " + GetModel(),-20}{"Vehicle Type: " + this.GetType().Name,-40}" +
                    $"{"Registration Number: " + GetRegistrationNumber(),-40}{"Daily Rental Price: " + GetDailyRentalPrice().ToString("F2"),-20}");
  

        }

        //This will be used to display details of a vehicle on an output file
        public virtual void DisplayVehicle(StreamWriter outputFile)
        {


                outputFile.WriteLine($"{"Make: " + GetMake(),-20}{"Model: " + GetModel(),-20}{"Vehicle Type: " + this.GetType().Name,-40}" +
                    $"{"Registration Number: " + GetRegistrationNumber(),-40}{"Daily Rental Price: " + GetDailyRentalPrice().ToString("F2"),-20}");
            

        }
        
        // Add reservation by accepting a Reservation object as parameter
        public bool AddReservation(Reservation theReservation)
        {
            foreach (Reservation reservation in GetReservations())
            {
                if (reservation.GetSchedule().Overlaps(theReservation.GetSchedule()))
                {

                    //This reservation already exists
                    Console.WriteLine("Cannot create this reservation");
                    return false;
                }
            }

            GetReservations().Add(theReservation);
            return true;
        }

        // Add reservation by accepting a Schedule object as parameter
        public bool AddReservation(Schedule theSchedule)
        {
            foreach (Reservation reservation in GetReservations())
            {
                if (reservation.GetSchedule().Overlaps(theSchedule))
                {

                    //This reservation already exists
                    Console.WriteLine("Cannot create this reservation");
                    return false;
                }
            }

            GetReservations().Add(new Reservation(theSchedule, DateOnly.FromDateTime(DateTime.Now), GetDailyRentalPrice()));
            return true;
        }

        //To display reservations
        public void DisplayReservations()
        {
            Console.WriteLine($"\n{"", -10}{"Reservations for", -5} {GetMake(), -7}{GetModel(), -10}{GetRegistrationNumber()}: ");
            Console.WriteLine($"{"", -10}-------------------------------------------------------------------------------" +
                              $"");

            int rCount = 0;

            if(GetReservations().Count > 0) {

                //Sort the reservations/bookings by date
                GetReservations().Sort();
                
                Console.WriteLine($"{"", -10}{"S/N ", -5}{"Booking Date", -20}{"Pickup Date", -20}{"Dropoff Date", -20}{"Booking Price",-20}{"Driver First Name", -20}{"Driver Surname", -20}" +
    $"{"Driver DOB", -20}{"Driver License Number", -20}");
                foreach (Reservation reservation in GetReservations())
                {
                    rCount++;
                    Console.Write($"{"", -10}{rCount, -5}");
                    reservation.Display();
                }
            }
            else
            {
                //If there are no reservations
                Console.WriteLine($"{"", -10}No Reservations to display for this vehicle");
            }
            
        }

        //To display reservations
        public void DisplayReservations(StreamWriter outputFile)
        {
            outputFile.WriteLine($"\n{"", -10}{"Reservations for", -5} {GetMake(), -7}{GetModel(), -10}{GetRegistrationNumber()}: ");
            outputFile.WriteLine($"{"", -10}-------------------------------------------------------------------------------" +
                                 $"");

            int rCount = 0;

            if(GetReservations().Count > 0) {

                //Sort the reservations/bookings by date
                GetReservations().Sort();
                
                outputFile.WriteLine($"{"", -10}{"S/N ", -5}{"Booking Date", -20}{"Pickup Date", -20}{"Dropoff Date", -20}{"Total Booking Price",-20}{"Driver First Name", -20}{"Driver Surname", -20}" +
                                     $"{"Driver DOB", -20}{"Driver License Number", -20}");
                foreach (Reservation reservation in GetReservations())
                {
                    rCount++;
                    outputFile.Write($"{"", -10}{rCount, -5}");
                    reservation.Display(outputFile);
                }
            }
            else
            {
                //If there are no reservations
                outputFile.WriteLine($"{"", -10}No Reservations to display for this vehicle");
            }


          
        }

        //Implementaion of IComparable interface used to sort a List
        public int CompareTo(Vehicle? other)
        {

            // If other is not a valid object reference, this instance is greater.
            if (other == null) return 1;

            int result = GetMake().CompareTo(other.GetMake());

            if (result == 0)
            {
                //Order by model if makes are the same
                return GetModel().CompareTo(other.GetModel());
            }
            

            //check if the make of the current object
            //comes before the make of the other object
            return result;

        }

     
    }

}

