using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HdbPoet
{
    public class HDBSeries:Reclamation.TimeSeries.Series
    {
        OracleHdb.TimeSeriesDataSet m_ds;
        //Hdb m_hdb;
        int m_seriesNumber;
        public HDBSeries(OracleHdb.TimeSeriesDataSet ds, int seriesNumber )
        {
            m_ds = ds;
            m_seriesNumber = seriesNumber;
        }

        protected override void ReadCore(DateTime t1, DateTime t2)
        {
            var r = m_ds.Series.FindBySeriesNumber(m_seriesNumber);
            var timeZone = m_ds.Graph[0].TimeZone;

            var tbl = Hdb.Instance.Table(r.hdb_site_datatype_id, r.hdb_r_table, r.Interval, 60, t1, t2, timeZone, false, 0);
            tbl.Columns.Remove("SourceColor");
            tbl.Columns.Remove("ValidationColor");
            tbl.Columns.Add("flag");   
            this.Clear();
            for (int i = 0; i < tbl.Rows.Count; i++)
            {
                DateTime t = Convert.ToDateTime(tbl.Rows[i][0]);
                if (tbl.Rows[i][1] != DBNull.Value)
                {
                    double val = Convert.ToDouble(tbl.Rows[i][1]);
                    Add(t, val);
                }
            }

            this.TimeInterval = StringToInterval(r.Interval);
            this.Name = r.DisplayName;
            this.Units = r.Units;

        }

        private Reclamation.TimeSeries.TimeInterval StringToInterval(string interval)
        {
            var rval = Reclamation.TimeSeries.TimeInterval.Daily;
            
            switch (interval)
            {
                case "hour":
                    rval = Reclamation.TimeSeries.TimeInterval.Hourly;
                    break;
                case "day":
                    rval = Reclamation.TimeSeries.TimeInterval.Daily;
                    break;
                case "month":
                    rval = Reclamation.TimeSeries.TimeInterval.Monthly;
                    break;
                case "year":
                    rval = Reclamation.TimeSeries.TimeInterval.Yearly;
                    break;
                case "wy":
                    rval = Reclamation.TimeSeries.TimeInterval.Yearly;
                    break;
                default:
                    rval = Reclamation.TimeSeries.TimeInterval.Irregular;
                    break;
            }

            return rval;
        }

    }
}
