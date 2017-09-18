﻿using System;
using HdbPoet;
namespace Reclamation.TimeSeries.OracleHdb
{
    public class ImportFromHdb
    {

        public static bool Import(string filename, TimeSeriesDatabase db, PiscesFolder folder)
        {
            var ds = new HdbPoet.OracleHdb.TimeSeriesDataSet();
            ds.Clear();
            ds.ReadXmlFile(filename);
            for (int i = 0; i < ds.Graph.Count; i++)
			{
                var gd = new GraphData(ds, ds.Graph[i].GraphNumber);
			 Import(gd, db, folder);
			}
            return true;
        }
        public static bool Import(GraphData ds, TimeSeriesDatabase db, PiscesFolder folder)
        {

            var root = folder; // db.AddFolder(folder, Path.GetFileNameWithoutExtension(filename));

            var seriesCatalog = db.GetSeriesCatalog();
            int sdi = db.NextSDI();
            foreach (var s in ds.SeriesRows)
            {
                var instantInterval = ds.GraphRow.InstantInterval;

                string hostname = "hdbhost";
                if (Hdb.Instance != null)
                    hostname = Hdb.Instance.Server.Host;

                string cs = "hdb_r_table=" + s.hdb_r_table
                          + ";hdb_site_datatype_id=" + s.hdb_site_datatype_id
                          + ";hdb_interval=" + s.Interval
                          + ";hdb_instant_interval=" + instantInterval
                          + ";hdb_time_zone=" + ds.GraphRow.TimeZone
                          + ";hdb_host_name=" +hostname;

                var name = s.SiteName+ " "+ s.ParameterType + " " + s.Units;
                seriesCatalog.AddSeriesCatalogRow(sdi++, root.ID, 0, 1, "Hdb", name,
                    s.SiteName, s.Units, IntervalString(s.hdb_r_table), s.ParameterType,
                    "",  "HdbOracleSeries", cs, "","",1);
            }

            db.Server.SaveTable(seriesCatalog);
            
            return true;
        }

        private static string IntervalString(string modelTable)
        {
            if (String.Compare("r_month", modelTable, true) == 0)
                return "Monthly";
            if (String.Compare("r_hour", modelTable, true) == 0)
                return "Hourly";
            if (String.Compare("r_day", modelTable, true) == 0)
                return "Daily";
            if (String.Compare("r_year", modelTable, true) == 0)
                return "Yearly";

            return "Irregular";

        }

    }
}
