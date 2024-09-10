using System;
namespace VehicleRentalSystemSoftware
{
	public static class DateConverter
	{

		public static DateOnly? ToDateOnly(string date)
		{
            try
            {
                DateOnly parsedDate = DateOnly.ParseExact(date, MyDateFormat.format);

                return parsedDate;
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Could not convert this string to date. "+ ex.Message);
                return null;
            }
        }

        public static String? DateToString(DateOnly? date)
        {
            try
            {

                if (date.HasValue)
                {
                    string dateString = date.Value.ToString(MyDateFormat.format);
                    return dateString;
                }
                else
                {
                    Console.WriteLine("Date is null");
                    return null;
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Could not convert this date to string");
                return null;
            }
        }
    }
}

