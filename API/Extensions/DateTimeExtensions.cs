using System;

namespace API.Extensions;

public static class DateTimeExtensions
{
    public static int CalculateAge(this DateOnly dob) //june 19, 1995
    {
        var today = DateOnly.FromDateTime(DateTime.Now); //November 15, 2024
        var age = today.Year - dob.Year; //2024-1995 = 29

        if(dob > today.AddYears(-age)) age--; //if june 19, 1995 is later than November 15, 1995 = 29--

        return age;
    }
}
