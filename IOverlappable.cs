using System;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace VehicleRentalSystemSoftware
{
	public interface IOverlappable
	{
        bool Overlaps(Schedule other);
    }
}

