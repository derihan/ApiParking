using ApiParking;
using ApiParking.Models;
using System;
using System.Collections.Generic;

namespace ApiParking.Data.Area
{
    public interface IAreaRepo
    {
        bool SaveChanges();
        List<MgParkingArea> GetAllArea();
        MgParkingArea GetAreaById(int id);
        void CreateArea(MgParkingArea mg_areas);
        MgParkingArea CheckData(int number, int katid);
        void UpdateArea(Dictionary<String, int> data);
        void DeleteArea(MgParkingArea mgParking);
    }
}
