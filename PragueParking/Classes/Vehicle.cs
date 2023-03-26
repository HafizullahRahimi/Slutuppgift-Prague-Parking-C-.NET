using System;
using System.Buffers;
using System.Data;
using System.Data.Common;

namespace Classes
{

    public enum VehicleType { car, motorC }


    // Fordonet Class
    public class Vehicle
    {

        public string Owner { get; private set; }
        public VehicleType Type { get; private set; }
        public Byte ParkPlace { get;  set; }
        public  Byte ParkPlacePart { get;  set; }

        public DateTime ArrivalTime { get; private set; }
        public string RegNumber { get; private set; }


        //Constructor Car
        public Vehicle(string owner, Byte parkPlace, DateTime arrivalTime, string regNumber)
        {
            Owner = owner;
            Type = VehicleType.car;
            ParkPlace = parkPlace;
            ParkPlacePart = 2;
            ArrivalTime = arrivalTime;
            RegNumber = regNumber;

        }

        //Constructor MotorC
        public Vehicle(string owner, Byte parkPlace, byte parkPlacePart, DateTime arrivalTime, string regNumber)
        {
            Owner = owner;
            Type = VehicleType.motorC;
            ParkPlace = parkPlace;
            ParkPlacePart = parkPlacePart;
            ArrivalTime = arrivalTime;
            RegNumber = regNumber;
        }

    }
}
