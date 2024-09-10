using System;
namespace VehicleRentalSystemSoftware
{
	public class Driver
	{
        private string firstName;

        private string surname;

        private DateOnly? dateOfBirth;

        private string licenseNumber;

        public Driver(string firstName, string surname, DateOnly? dateOfBirth, string licenseNumber)
        {
            this.SetFirstName(firstName);
            this.SetSurname(surname);
            this.SetDateOfBirth(dateOfBirth);
            this.SetLicenseNumber(licenseNumber);
        }


        public string GetFirstName()
        {
            return firstName;
        }

        public void SetFirstName(string value)
        {
            firstName = value;
        }


        public string GetSurname()
        {
            return surname;
        }

        public void SetSurname(string value)
        {
            surname = value;
        }


        public DateOnly? GetDateOfBirth()
        {
            return dateOfBirth;
        }

        public void SetDateOfBirth(DateOnly? value)
        {
            dateOfBirth = value;
        }


        public string GetLicenseNumber()
        {
            return licenseNumber;
        }

        public void SetLicenseNumber(string value)
        {
            licenseNumber = value;
        }
    }
}

