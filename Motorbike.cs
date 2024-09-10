using System;
namespace VehicleRentalSystemSoftware
{
	public class Motorbike : Vehicle
	{
        private double fuelCapacity;

        public Motorbike(string registrationNumber, string make, string model, double dailyRentalPrice, double fuelCapacity)
            : base(registrationNumber, make, model, dailyRentalPrice)
        {
            this.SetFuelCapacity(fuelCapacity);
        }


        public double GetFuelCapacity()
        {
            return fuelCapacity;
        }

        public void SetFuelCapacity(double value)
        {
            fuelCapacity = value;
        }

        //Overrides the method from the base class
        public override void DisplayVehicle()
        {

            Console.WriteLine($"{"Make: " + GetMake(),-20}{"Model: " + GetModel(),-20}{"Vehicle Type: " + this.GetType().Name,-40}" +
                $"{"Registration Number: " + GetRegistrationNumber(),-40}{"Daily Rental Price: " + GetDailyRentalPrice(),-40}" +
                $"{"Fuel Capacity: " + GetFuelCapacity() + " gallons",-20}");

        }

        //Overrides the method from the base class
        public override void DisplayVehicle(StreamWriter outputFile)
        {


            outputFile.WriteLine($"{"Make: " + GetMake(),-20}{"Model: " + GetModel(),-20}{"Vehicle Type: " + this.GetType().Name,-40}" +
                    $"{"Registration Number: " + GetRegistrationNumber(),-40}{"Daily Rental Price: " + GetDailyRentalPrice(),-40}" +
                    $"{"Fuel Capacity: " + GetFuelCapacity() + " gallons",-20}");



        }



    }
}

