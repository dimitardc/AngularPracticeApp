﻿namespace AngularApp1.Server.Domain.Entities
{
    public record TimePlace (string Place, DateTime Time);


    //  if we wanted a class instead
    //public class TimePlaceRm
    //{
    //    public string Place { get; }
    //    public DateTime Time { get; }

    //    public TimePlaceRm(string place, DateTime time)
    //    {
    //        Place = place;
    //        Time = time;
    //    }
    //}

}