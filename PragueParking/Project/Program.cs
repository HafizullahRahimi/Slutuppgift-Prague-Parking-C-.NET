using Classes;
using System;

namespace Project
{
    internal class Program
    {
        static void Main()
        {
            //Parkring class-----------------------------------------------------
            Parking PragueParking = new Parking("Prague", 20, 2);
            
            DateTime time = new DateTime(2022, 10, 22, 9, 30, 0);
            DateTime time1 = new DateTime(2022, 10, 24, 8, 30, 0);
            DateTime time2 = new DateTime(2022, 10, 25, 7, 30, 0);



            //Submission Of Car 
            PragueParking.SubmissionOfVehicle("Wille", 3);
            PragueParking.SubmissionOfVehicle("Ali", 16, time1);

            //Submission Of MotorC
            PragueParking.SubmissionOfVehicle("Neo", 17, 1);
            PragueParking.SubmissionOfVehicle("William", 13, 2, time);
            PragueParking.SubmissionOfVehicle("Sara", 6, 2, time2);



            bool showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu.Show(PragueParking);
            }
        }
    }
}
