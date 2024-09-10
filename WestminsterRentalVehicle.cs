using System;
using System.IO;
using System.Reflection;
using System.Security.AccessControl;
using System.Security.Claims;
using VehicleRentalSystemSoftware;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VehicleSystemRentalSoftware
{
    public class WestminsterRentalVehicle : IRentalManager, IRentalCustomer
    {
        private int parkingLotsMaximumSize = 50;

        private List<Vehicle> parkingLots = new List<Vehicle>();
        
        private static WestminsterRentalVehicle rentalSystem = new WestminsterRentalVehicle();
        
        private static bool isAdmin = false;


        public void ListAvailableVehicles(Schedule wantedSchedule, Type type)
        {

            int vCount = 0;

            // Check if type is a subclass of Vehicle
            if (typeof(Vehicle).IsAssignableFrom(type))
            {

                // Display heading
                Console.WriteLine("\nVehicles available on provided schedule include: \n");

                //List the vehicles
                foreach (Vehicle vehicle in parkingLots)
                {

                    //Check if it is the type of Vehicle required
                    if (vehicle.GetType() == type)
                    {
                        if (vehicle.GetReservations().Count > 0)
                        {
                            foreach (Reservation reservation in vehicle.GetReservations())
                            {
                                //If the car is not booked for a schedule
                                if (!reservation.GetSchedule().Overlaps(wantedSchedule))
                                {
                                    //No reservations for this vehicle, it is available
                                    vCount++;
                                    Console.Write($"{vCount,-5}");

                                    vehicle.DisplayVehicle();
                                    
                                    Console.WriteLine("\n\n");
                                    
                                }
                            }

                        }
                        else
                        {
                            //No reservations for this vehicle, it is available
                            vCount++;
                            Console.Write($"{vCount,-5}");

                            vehicle.DisplayVehicle();
                        }
                    }

                }
                if (vCount == 0) Console.WriteLine("No vehicle available.");

            }
            else
            {
                Console.WriteLine($"{type.Name} is not a Vehicle in this company");
            }
        }


        public bool AddReservation(string number, Schedule wantedSchedule)
        {

            //check all vehicles in the parking lot
            foreach (Vehicle vehicle in parkingLots)
            {
                //check if the vehicle exists using its number
                if (vehicle.GetRegistrationNumber().Equals(number))
                {
                    //The vehicle exists
                    //check if the wanted schedule overlaps with any of its reservations
                    if (vehicle.GetReservations().Count > 0)
                    {
                        foreach (Reservation reservation in vehicle.GetReservations())
                        {
                            //If the car is not booked for a schedule
                            if (reservation.GetSchedule().Overlaps(wantedSchedule))
                            {
                                //A resevation exists for this schedule
                                Console.WriteLine("\nThe schedule is unavailable!!!");
                                return false;
                            }
                        }

                        //No resevation exists for this schedule
                        //We can add the reservation
                        vehicle.AddReservation(wantedSchedule);
                        Console.WriteLine("\nReservation added successfully!!!");
                        
                        vehicle.DisplayReservations();
                        return true;
                    }
                    else
                    {

                        //No reservations for this vehicle, it is available
                        vehicle.AddReservation(wantedSchedule);
                        Console.WriteLine("\nReservation added successfully!!!");
                        
                        vehicle.DisplayReservations();
                        return true;

                    }
                }
            }

            //The license number does not exist
            Console.WriteLine("\nVehicle with this license does not exist!!!.");

            return false;
        }

        public bool ChangeReservation(string number, Schedule oldSchedule, Schedule newSchedule)
        {

            //check all vehicles in the parking lot
            foreach (Vehicle vehicle in parkingLots)
            {
                //check if the vehicle exists using its number
                if (vehicle.GetRegistrationNumber().Equals(number))
                {
                    //The vehicle exists
                    //check if the schedule you want to change exists
                    if (vehicle.GetReservations().Count > 0)
                    {
                        foreach (Reservation reservation in vehicle.GetReservations())
                        {

                            if (reservation.GetSchedule().GetPickUpDate() == oldSchedule.GetPickUpDate()
                                && reservation.GetSchedule().GetDropOffDate() == oldSchedule.GetDropOffDate())
                            {
                                //The schedule exists
                                //Change it to the new schedule pick ups and drop offs
                                reservation.GetSchedule().SetPickUpDate(newSchedule.GetPickUpDate());
                                reservation.GetSchedule().SetDropOffDate(newSchedule.GetDropOffDate());
                                Console.WriteLine("\n" +
                                    "Reservation changed successfully!!!\n");
                                
                                vehicle.DisplayReservations();
                                return true;
                            }
                        }

                        //No resevation exists for this schedule
                        //We can not change any schedule
                        Console.WriteLine("The schedule is unavailable or does not exist!!!");
                        return false;

                    }
                    else
                    {

                        //No reservations, the schedule does not exist
                        Console.WriteLine("\nThe schedule is unavailable or does not exist!!!");
                        return false;
                    }
                }
            }


            Console.WriteLine("\nThis vehicle is does not exist.");

            return false;
        }


        public bool DeleteReservation(string number, Schedule schedule)
        { 
            //check all vehicles in the parking lot
            foreach (Vehicle vehicle in parkingLots)
            {
                //check if the vehicle exists using its number
                if (vehicle.GetRegistrationNumber().Equals(number))
                {
                    //The vehicle exists
                    //check if the schedule of the reservation you want to delete exists
                    if (vehicle.GetReservations().Count > 0)
                    {
                        foreach (Reservation reservation in vehicle.GetReservations())
                        {
                            if (reservation.GetSchedule().GetPickUpDate() == schedule.GetPickUpDate()
                                && reservation.GetSchedule().GetDropOffDate() == schedule.GetDropOffDate())
                            {
                                //The schedule exists
                                //delete the reservation of the schedule
                                vehicle.GetReservations().Remove(reservation);
                                Console.WriteLine("\n" +
                                    "Reservation deleted successfully!!!\n");
                                
                                vehicle.DisplayReservations();
                                return true;
                            }

                        }

                        //No resevation exists for this schedule
                        Console.WriteLine("\nThe reservation does not exist");
                        
                        vehicle.DisplayReservations();
                        return false;

                    }
                    else
                    {

                        //No reservations, the reservation does not exist
                        Console.WriteLine("\nThe reservation does not exist");
                        
                        vehicle.DisplayReservations();
                        return false;
                    }
                }
            }


            Console.WriteLine("\nThis vehicle is does not exist, cannot delete the reservation.");

            return false;
        }


        //
        
        public bool AddVehicle(Vehicle v)
        {
            //Check number of available parking lots available
            if (parkingLots.Count == 50)
            {
                Console.WriteLine("The parking lot is full. Cannot add vehicle");

                return false;
            }

            //Check if vehicle is already available in parking lot using license number
            if (InParkingLot(v))
            {
                
                Console.WriteLine("This vehicle is already in the parking lot");

                return false;
            }



            //Display number of available parking lots left after successful
            parkingLots.Add(v);

            //Successful
            Console.WriteLine($"\nSuccessfully added the vehicle with license number {v.GetRegistrationNumber()}\n");

            Console.WriteLine($"\nThe number of available parking lots left is: {parkingLotsMaximumSize - parkingLots.Count}\n");

            return true;
        }


        public bool DeleteVehicle(string number)
        {
            //Check if vehicle is available in parking lot using license number
            if (!InParkingLot(number))
            {
                Console.WriteLine("\nThis vehicle is not in the parking lot");

                return false;
            }

            //delete the vehicle from the parking lot
            Vehicle vehicle = GetVehicleFromLicense(number);
            
            parkingLots.Remove(vehicle);


            //Display number of available parking lots left after successful
            Console.WriteLine($"\n Vehicle has been deleted successfully\n");
            Console.WriteLine($"\n The deleted vehicle is: \n");
            vehicle.DisplayVehicle();
            Console.WriteLine($"\n\nThe number of available parking lots left is: {parkingLotsMaximumSize - parkingLots.Count}");


            return true;
        }


        public void ListVehicles()
        {
            int vCount = 0;
            if (parkingLots.Count > 0)
            {
                foreach (Vehicle vehicle in parkingLots)
                {
                    vCount++;
                    Console.Write($"\n{vCount,-5}");

                    vehicle.DisplayVehicle();
                    vehicle.DisplayReservations();
                }
            }
            else
            {
                Console.WriteLine("\n\nNo vehicles available in the parking lot");
            }
        }


        public void ListOrderedVehicles()
        {
            int vCount = 0;
            if (parkingLots.Count > 0)
            {
                //This makes use of IComparable in the Vehicle class
                parkingLots.Sort();

                foreach (Vehicle vehicle in parkingLots)
                {
                    vCount++;
                    Console.Write($"\n{vCount,-5}");

                    vehicle.DisplayVehicle();
                    vehicle.DisplayReservations();
                }
            }
            else
            {
                Console.WriteLine("No vehicles available in the parking lot");
            }
        }


        //TODO: Correct method (accept file name as input)
        public void GenerateReport(string filename)
        {
            // Set a variable to the Documents path.
            string docPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..");

            string nameOfFile = filename + ".txt";

            // Specify the file path
            string filePath = Path.Combine(docPath, nameOfFile);

            try
            {
                // Append text to an existing file or create a new one if it doesn't exist.
                using (StreamWriter outputFile = new StreamWriter(filePath, true))
                {
                    int vCount = 0;
                    if (parkingLots.Count > 0)
                    {
                        // Clear all data in the existing file
                        File.WriteAllText(filePath, "");

                        // Display heading
                        outputFile.WriteLine($"\n\n{"",-20}{"WESTMINSTER RENTAL VEHICLE REPORT", -20}\n\n");

                        //This makes use of IComparable in the Vehicle class
                        parkingLots.Sort();

                        foreach (Vehicle vehicle in parkingLots)
                        {
                            vCount++;
                            outputFile.Write($"\n{vCount,-5}");

                            vehicle.DisplayVehicle(outputFile);

                            vehicle.DisplayReservations(outputFile);

                        }
                    }
                    else
                    {
                        outputFile.WriteLine("No vehicles available in the parking lot");
                    }
                }
                Console.WriteLine($"Report generated successfully. File path: {filePath}");
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately (e.g., log or display an error message)
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }


        private bool InParkingLot(Vehicle v)
        {
            foreach (Vehicle vehicle in parkingLots)
            {
                if (vehicle.GetRegistrationNumber().Equals(v.GetRegistrationNumber()))
                {
                    return true;
                }
            }

            return false;
        }


        private bool InParkingLot(string registrationNumber)
        {
            foreach (Vehicle vehicle in parkingLots)
            {
                if (vehicle.GetRegistrationNumber().Equals(registrationNumber))
                {
                    return true;
                }
            }

            return false;
        }

        private Vehicle GetVehicleFromLicense(string number)
        {
            foreach (Vehicle vehicle in parkingLots)
            {
                if (vehicle.GetRegistrationNumber().Equals(number))
                {
                    return vehicle;
                }
            }

            return null;
        }

        private void AddVehicleToParkingLot(Vehicle v)
        {
            parkingLots.Add(v);
        }


    static void Main(string[] args)
    {



        Console.WriteLine("\n\n\n\n\n======== Welcome to Westminster Rental ========\n");

        DisplayUserFunctionalities();

        RunSoftware(rentalSystem);

    }

    private static void DisplayUserFunctionalities()
    {
        Console.WriteLine("\nYou are a User. Input the number for the option you want to select." + "\n");

        Console.WriteLine("User menu: ");

        Console.WriteLine("(1) List available vehicles.");
        Console.WriteLine("(2) Add a reservation.");
        Console.WriteLine("(3) Change a reservation.");
        Console.WriteLine("(4) Delete a reservation.");
        Console.WriteLine("(5) Switch to Admin menu");


        Console.WriteLine("\nTo exit, input \"exit\"\n");

    }


    private static void DisplayAdminFunctionalities()
    {
        Console.WriteLine("\nYou have switched to Admin. Input the number for the option you want to select." + "\n");

        Console.WriteLine("Admin menu: ");

        Console.WriteLine("(1) Add vehicle.");
        Console.WriteLine("(2) Delete vehicle.");
        Console.WriteLine("(3) List vehicles.");
        Console.WriteLine("(4) List ordered vehicles.");
        Console.WriteLine("(5) Generate report.");
        Console.WriteLine("(6) Switch to User menu");


        Console.WriteLine("\nTo exit, input \"exit\"\n");

    }
    
    private static Driver GetDriverInfo()
    {
        Console.WriteLine("You need to input driver details.\n");

        string? fName = null;

        while ( string.IsNullOrEmpty(fName))
        {
            Console.Write("Enter driver's first name: ");
            fName = Console.ReadLine();
        }

        string? surname = null;

        while ( string.IsNullOrEmpty(surname))
        {
            Console.Write("Enter driver's surname: ");
            surname = Console.ReadLine();
        }

        DateOnly? dobAsDate = null;
        while (dobAsDate == null)
        {
            Console.Write($"\n\nEnter driver's DOB (Format: {MyDateFormat.format}): ");
            string? dob = Console.ReadLine();
            dobAsDate = DateConverter.ToDateOnly(dob);
            
        }

        string? lNumber = null;

        while ( string.IsNullOrEmpty(lNumber))
        {
            Console.Write("Enter driver's license number: ");
            lNumber = Console.ReadLine();
        }

        return new Driver(fName, surname, dobAsDate, lNumber);

    }
    
    private static Schedule GetScheduleInfo(bool requiresDriverInfo)
    {
        
        Console.Write($"\nInput Schedule details:\n");
        DateOnly? pickupDate = null;
        while (pickupDate == null)
        {
            Console.Write($"\n\nEnter Pickup Date (Format: {MyDateFormat.format}): ");
            string? pickupDateString = Console.ReadLine();
            pickupDate = DateConverter.ToDateOnly(pickupDateString);
            
        }

        
        DateOnly? dropoffDate = null;
        while (dropoffDate == null || dropoffDate < pickupDate)
        {
            
            Console.Write($"Enter Dropoff Date (Format: {MyDateFormat.format}): ");
            string dropoffDateString = Console.ReadLine();
            dropoffDate = DateConverter.ToDateOnly(dropoffDateString);
            
            if (dropoffDate != null && dropoffDate < pickupDate)
            {
                Console.WriteLine("\nERROR: Drop off date cannot be before pick up date\n");
            
            }

        }


        Schedule schedule;

        if (!requiresDriverInfo)
        {
            schedule = new Schedule(pickupDate.Value, dropoffDate.Value);
        }else
        {
            Driver driverInfo = GetDriverInfo();

            schedule = new Schedule(pickupDate.Value, dropoffDate.Value, driverInfo);
        }
        

        return schedule;

    }

    private static string GetRegNumberInfo()
    {
        string regNumberString = null;

        while (string.IsNullOrEmpty(regNumberString))
        {
            Console.Write($"Input License Number: \n\nEnter Vehicle license number: ");
            regNumberString = Console.ReadLine();
        }
        

        return regNumberString;

    }


    private static Type GetTypeFromString(string typeString)
    {
        switch (typeString.ToLower())
        {
            case "van":
                return typeof(Van);
            case "car":
                return typeof(Car);
            case "electriccar":
                return typeof(ElectricCar);
            case "motorbike":
                return typeof(Motorbike);
            default:
                Console.WriteLine("Invalid vehicle type");
                return null;
        }
    }

    private static void CreateVan()
    {
        Console.WriteLine("\nInput details of the Van\n");

        string rNum = null;
        while (string.IsNullOrEmpty(rNum))
        {
            Console.WriteLine("\nEnter registration number: \n");
            rNum = Console.ReadLine();
            
            if (rNum != null && rNum == "")
            {
                Console.WriteLine("\nYou must input registration number!!!\n");
            }
        }

        string make = null;
        while (string.IsNullOrEmpty(make))
        {
            Console.WriteLine("\nEnter make: \n");
            make = Console.ReadLine();
            
            if (make != null && make == "")
            {
                Console.WriteLine("\nYou must input make!!!\n");
            }
        }

        string model = null;
        while (string.IsNullOrEmpty(model))
        {
            Console.WriteLine("\nEnter model: \n");
            model = Console.ReadLine();
            
            if (model != null && model == "")
            {
                Console.WriteLine("\nYou must input model!!!\n");
            }
        }

        string price = null;
        while (string.IsNullOrEmpty(price))
        {
            Console.WriteLine("\nEnter daily rental price (input digits only): \n");
            price = Console.ReadLine();
            
            if (price != null && price == "")
            {
                Console.WriteLine("\nYou must input daily rental price!!!\n");
            }
        }
        string fCapacity = null;
        while (string.IsNullOrEmpty(fCapacity))
        {
            Console.WriteLine("\nEnter fuel capacity (input digits only): \n");
            fCapacity = Console.ReadLine();
            
            if (fCapacity != null && fCapacity == "")
            {
                Console.WriteLine("\nYou must input fuel capacity!!!\n");
            }
        }
        
        string lCapacity = null;
        while (string.IsNullOrEmpty(lCapacity))
        {
            Console.WriteLine("\nEnter load capacity (input digits only): \n");
            lCapacity = Console.ReadLine();
            
            if (lCapacity != null && lCapacity == "")
            {
                Console.WriteLine("\nYou must input load capacity!!!\n");
            }
        }

        double priceD;
        double fCapacityD;
        double lCapacityD;

        // Try to convert string to double
        try
        {
            priceD = double.Parse(price);

            fCapacityD = double.Parse(fCapacity);

            lCapacityD = double.Parse(lCapacity);


            Vehicle van = new Van(rNum, make, model, priceD, fCapacityD, lCapacityD);

            rentalSystem.AddVehicle(van);


        }
        catch (FormatException ex)
        {
            Console.WriteLine("\n\nInvalid input for price!!!!");

            return;

        }


    }

    private static void CreateCar()
    {
        Console.WriteLine("\nInput details of the Car\n");

        string rNum = null;
        while (string.IsNullOrEmpty(rNum))
        {
            Console.WriteLine("\nEnter registration number: \n");
            rNum = Console.ReadLine();
            
            if (rNum != null && rNum == "")
            {
                Console.WriteLine("\nYou must input registration number!!!\n");
            }
        }

        string make = null;
        while (string.IsNullOrEmpty(make))
        {
            Console.WriteLine("\nEnter make: \n");
            make = Console.ReadLine();
            
            if (make != null && make == "")
            {
                Console.WriteLine("\nYou must input make!!!\n");
            }
        }

        string model = null;
        while (string.IsNullOrEmpty(model))
        {
            Console.WriteLine("\nEnter model: \n");
            model = Console.ReadLine();
            
            if (model != null && model == "")
            {
                Console.WriteLine("\nYou must input model!!!\n");
            }
        }

        string price = null;
        while (string.IsNullOrEmpty(price))
        {
            Console.WriteLine("\nEnter daily rental price (input digits only): \n");
            price = Console.ReadLine();
            
            if (price != null && price == "")
            {
                Console.WriteLine("\nYou must input daily rental price!!!\n");
            }
        }

        string fCapacity = null;
        while (string.IsNullOrEmpty(fCapacity))
        {
            Console.WriteLine("\nEnter fuel capacity (input digits only): \n");
            fCapacity = Console.ReadLine();
            
            if (fCapacity != null && fCapacity == "")
            {
                Console.WriteLine("\nYou must input fuel capacity!!!\n");
            }
        }

        double priceD;
        double fCapacityD;

        // Try to convert string to double
        try
        {
            priceD = double.Parse(price);

            fCapacityD = double.Parse(fCapacity);


            Vehicle car = new Car(rNum, make, model, priceD, fCapacityD);

            rentalSystem.AddVehicle(car);

        }
        catch (FormatException ex)
        {
            Console.WriteLine("\n\nIn valid input");

            return;

        }

    }

    private static void CreateElectricCar()
    {
        Console.WriteLine("\nInput details of the Electric Car\n");

        string rNum = null;
        while (string.IsNullOrEmpty(rNum))
        {
            Console.WriteLine("\nEnter registration number: \n");
            rNum = Console.ReadLine();
            
            if (rNum != null && rNum == "")
            {
                Console.WriteLine("\nYou must input registration number!!!\n");
            }
        }

        string make = null;
        while (string.IsNullOrEmpty(make))
        {
            Console.WriteLine("\nEnter make: \n");
            make = Console.ReadLine();
            
            if (make != null && make == "")
            {
                Console.WriteLine("\nYou must input make!!!\n");
            }
        }

        string model = null;
        while (string.IsNullOrEmpty(model))
        {
            Console.WriteLine("\nEnter model: \n");
            model = Console.ReadLine();
            
            if (model != null && model == "")
            {
                Console.WriteLine("\nYou must input model!!!\n");
            }
        }

        string price = null;
        while (string.IsNullOrEmpty(price))
        {
            Console.WriteLine("\nEnter daily rental price (input digits only): \n");
            price = Console.ReadLine();
            
            if (price != null && price == "")
            {
                Console.WriteLine("\nYou must input daily rental price!!!\n");
            }
        }

        string eCapacity = null;
        while (string.IsNullOrEmpty(eCapacity))
        {
            Console.WriteLine("\nEnter energy capacity (input digits only): \n");
            eCapacity = Console.ReadLine();
            
            if (eCapacity != null && eCapacity == "")
            {
                Console.WriteLine("\nYou must input energy capacity!!!\n");
            }
        }
        
        double priceD;
        double eCapacityD;

        // Try to convert string to double
        try
        {
            priceD = double.Parse(price);

            eCapacityD = double.Parse(eCapacity);


            Vehicle electricCar = new ElectricCar(rNum, make, model, priceD, eCapacityD);

            rentalSystem.AddVehicle(electricCar);

        }
        catch (FormatException ex)
        {
            Console.WriteLine("\n\nThe value you entered is invalid!!!");

            return;

        }

    }

    private static void CreateMotorbike()
    {
        Console.WriteLine("\nInput details of the Motorbike\n");

        string rNum = null;
        while (string.IsNullOrEmpty(rNum))
        {
            Console.WriteLine("\nEnter registration number: \n");
            rNum = Console.ReadLine();
            
            if (rNum != null && rNum == "")
            {
                Console.WriteLine("\nYou must input registration number!!!\n");
            }
        }

        string make = null;
        while (string.IsNullOrEmpty(make))
        {
            Console.WriteLine("\nEnter make: \n");
            make = Console.ReadLine();
            
            if (make != null && make == "")
            {
                Console.WriteLine("\nYou must input make!!!\n");
            }
        }
        
        string model = null;
        while (string.IsNullOrEmpty(model))
        {
            Console.WriteLine("\nEnter model: \n");
            model = Console.ReadLine();
            
            if (model != null && model == "")
            {
                Console.WriteLine("\nYou must input model!!!\n");
            }
        }

        string price = null;
        while (string.IsNullOrEmpty(price))
        {
            Console.WriteLine("\nEnter daily rental price (input digits only): \n");
            price = Console.ReadLine();
            
            if (price != null && price == "")
            {
                Console.WriteLine("\nYou must input daily rental price!!!\n");
            }
        }

        string fCapacity = null;
        while (string.IsNullOrEmpty(fCapacity))
        {
            Console.WriteLine("\nEnter fuel capacity (input digits only): \n");
            fCapacity = Console.ReadLine();
            
            if (fCapacity != null && fCapacity == "")
            {
                Console.WriteLine("\nYou must input fuel capacity!!!\n");
            }
        }

        double priceD;
        double fCapacityD;

        // Try to convert string to double
        try
        {
            priceD = double.Parse(price);

            fCapacityD = double.Parse(fCapacity);


            Vehicle motorbike = new Motorbike(rNum, make, model, priceD, fCapacityD);

            rentalSystem.AddVehicle(motorbike);

        }
        catch (FormatException ex)
        {
            Console.WriteLine("Invalid input. ");

            return;

        }

    }

    private static string GetVehicleType()
    {
        Console.WriteLine("\n Select the vehicle type: \n");

        Console.WriteLine("(1) Van");
        Console.WriteLine("(2) Car");
        Console.WriteLine("(3) ElectricCar");
        Console.WriteLine("(4) Motorbike");

        Console.WriteLine("");

        string option = Console.ReadLine();

        switch (option)
        {
            case "1":
                return "Van";
            case "2":
                return "Car";
            case "3":
                return "ElectricCar";
            case "4":
                return "Motorbike";
            default:
                Console.WriteLine("\nInvalid or null input. Input a correct option.");
                return GetVehicleType();

        }
    }

    private static string RunSoftware(WestminsterRentalVehicle rentalSystem)
    {
        //This is used to get the option
        string option = Console.ReadLine();

        // isAdmin is used to check if it is User or Admin
        
        switch (option)
        {
            case "1" when !isAdmin:
                Schedule mSchedule = GetScheduleInfo(false);
        
                Type vType = GetTypeFromString(GetVehicleType());
        
                rentalSystem.ListAvailableVehicles(mSchedule, vType);

                break;

            case "1" when isAdmin:
                string type = GetVehicleType();

                if (type == "Van")
                    CreateVan();
                else if (type == "Car")
                    CreateCar();
                else if (type == "ElectricCar")
                    CreateElectricCar();
                else if (type == "Motorbike")
                    CreateMotorbike();
                else
                    CreateVan();
                break;

            case "2" when !isAdmin:
                
                string theRegNumber = GetRegNumberInfo();

                Schedule schedule = GetScheduleInfo(true);

                rentalSystem.AddReservation(theRegNumber, schedule);

                break;

            case "2" when isAdmin:
                
                string? number = null;

                while (string.IsNullOrEmpty(number))
                {
            
                    Console.WriteLine("\nTo delete a vehicle, input the license number of the vehicle");

                    number = Console.ReadLine();

                    if (string.IsNullOrEmpty(number))
                    {
                        Console.WriteLine("\n\nInvalid input!!!");
                    }

                }

                rentalSystem.DeleteVehicle(number);
                break;

            case "3" when !isAdmin:
                string rNumber = GetRegNumberInfo();

                Console.WriteLine("\nOld Schedule Info:");
                Schedule oldSchedule = GetScheduleInfo(false);


                Console.WriteLine("\nNew Schedule Info:\n");
                Schedule newSchedule = GetScheduleInfo(false);

                rentalSystem.ChangeReservation(rNumber, oldSchedule, newSchedule);

                break;

            case "3" when isAdmin:
                rentalSystem.ListVehicles();
                break;

            case "4" when !isAdmin:
                string regNumber = GetRegNumberInfo();

                Console.WriteLine("\nTo delete a reservation, input the schedule Info:\n");
                Schedule theSchedule = GetScheduleInfo(false);
        

                rentalSystem.DeleteReservation(regNumber, theSchedule);

                break;

            case "4" when isAdmin:
                rentalSystem.ListOrderedVehicles();
                break;

            case "5" when !isAdmin:
                isAdmin = true;
                DisplayAdminFunctionalities();
                break;

            case "5" when isAdmin:
                        
                string? fileName = null;

                while (string.IsNullOrEmpty(fileName))
                {
                    Console.Write($"\nInput the name of the file: ");
                    fileName = Console.ReadLine();

                }
        
                rentalSystem.GenerateReport(fileName);

                break;

            case "6" when isAdmin:
                isAdmin = false;
                DisplayUserFunctionalities();
                break;

            case "exit":
                Console.WriteLine("\n Exiting Software...");
                return "exit";

            default:
                Console.WriteLine("\nInvalid input. Try again\n");
                break;
        }

        Console.WriteLine("\nInput another option or input \"exit\" to quit");

        //Recursive method to keep the code running
        return RunSoftware(rentalSystem);
    }



    }


}

