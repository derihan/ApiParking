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
        int CheckData(MgParkingArea _ares);
        void UpdateArea(Dictionary<String, int> data);
        bool DeleteArea(int id);
        List<MgParkingArea> GetFilter(string filter);
    }
}
