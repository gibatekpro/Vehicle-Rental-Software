using System;
namespace VehicleRentalSystemSoftware
{
	public class ElectricCar : Vehicle
	{
        private double energyCapacity;


        public ElectricCar(string registrationNumber, string make, string model, double dailyRentalPrice, double energyCapacity)
            : base(registrationNumber, make, model, dailyRentalPrice)
        {
            this.SetEnergyCapacity(energyCapacity);
        }


        public double GetEnergyCapacity()
        {
            return energyCapacity;
        }

        public void SetEnergyCapacity(double value)
        {
            energyCapacity = value;
        }

        //Overrides the method from the base class
        public override void DisplayVehicle()
        {

            Console.WriteLine($"{"Make: " + GetMake(),-20}{"Model: " + GetModel(),-20}{"Vehicle Type: " + this.GetType().Name,-40}" +
                    $"{"Registration Number: " + GetRegistrationNumber(),-40}{"Daily Rental Price: " + GetDailyRentalPrice(),-40}" +
                    $"{"Energy Capacity: " + GetEnergyCapacity() + " kwH",-20}");

        }

        //Overrides the method from the base class
        public override void DisplayVehicle(StreamWriter outputFile)
        {


            outputFile.WriteLine($"{"Make: " + GetMake(),-20}{"Model: " + GetModel(),-20}{"Vehicle Type: " + this.GetType().Name,-40}" +
                    $"{"Registration Number: " + GetRegistrationNumber(),-40}{"Daily Rental Price: " + GetDailyRentalPrice(),-40}" +
                    $"{"Energy Capacity: " + GetEnergyCapacity() + " kwH",-20}");



        }


    }
}

