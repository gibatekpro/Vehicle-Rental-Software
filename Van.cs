using System;
namespace VehicleRentalSystemSoftware
{
    public class Van : Vehicle
    {
        private double fuelCapacity;

        private double loadCapacity;

        public Van(string registrationNumber, string make, string model, double dailyRentalPrice, double fuelCapacity, double loadCapacity)
            : base(registrationNumber, make, model, dailyRentalPrice)
        {
            this.SetFuelCapacity(fuelCapacity);

            this.SetLoadCapacity(loadCapacity);
        }


        public double GetFuelCapacity()
        {
            return fuelCapacity;
        }

        public void SetFuelCapacity(double value)
        {
            fuelCapacity = value;
        }

        public double GetLoadCapacity()
        {
            return loadCapacity;
        }

        public void SetLoadCapacity(double value)
        {
            loadCapacity = value;
        }


        //Overrides the method from the base class
        public override void DisplayVehicle()
        {

            Console.WriteLine($"{"Make: " + GetMake(),-20}{"Model: " + GetModel(),-20}{"Vehicle Type: " + this.GetType().Name,-40}" +
                    $"{"Registration Number: " + GetRegistrationNumber(),-40}{"Daily Rental Price: " + GetDailyRentalPrice(),-40}" +
                    $"{"Fuel Capacity: " + GetFuelCapacity() + " gallons",-40}{"Load Capacity: " + GetLoadCapacity() + "kg",-20}");

        }

        //Overrides the method from the base class
        public override void DisplayVehicle(StreamWriter outputFile)
        {


            outputFile.WriteLine($"{"Make: " + GetMake(),-20}{"Model: " + GetModel(),-20}{"Vehicle Type: " + this.GetType().Name,-40}" +
                    $"{"Registration Number: " + GetRegistrationNumber(),-40}{"Daily Rental Price: " + GetDailyRentalPrice(),-40}" +
                    $"{"Fuel Capacity: " + GetFuelCapacity() + " gallons",-40}{"Load Capacity: " + GetLoadCapacity() + "kg",-20}");



        }

    }

}

