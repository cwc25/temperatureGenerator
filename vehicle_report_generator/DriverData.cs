﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;

namespace vehicle_report_generator
{
    public class DriverData
    {
        [BsonId(IdGenerator = typeof(CombGuidGenerator))]
        public Guid Id { get; set; }

        [BsonElement("VehicleId")]
        public string VehicleId { get; set; }

        [BsonElement("CreatedDate")]
        public DateTime CreatedDate { get; set; }

        [BsonElement("Report")]
        public DailyInfo Report { get; set; }

        [BsonElement("ParkingSlots")]
        public List<ParkingSlot> ParkingSlots { get; set; }

    }

    public class DailyInfo
    {
        [BsonElement("TotalMiles")]
        public double TotalMiles { get; set; }

        [BsonElement("AverageMiles")]
        public double AverageMiles { get; set; }
    }

    public class ParkingSlot
    {
        [BsonElement("X")]
        public double X { get; set; }

        [BsonElement("Y")]
        public double Y { get; set; }
    }
}