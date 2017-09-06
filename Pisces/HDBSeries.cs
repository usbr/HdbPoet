using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HdbPoet
{
    public class HDBSeries:Reclamation.TimeSeries.Series
    {
        TimeSeriesDataSet m_ds;
        Hdb m_hdb;
        int m_seriesNumber;
        public HDBSeries(TimeSeriesDataSet ds, int seriesNumber )
        {
            m_ds = ds;
            m_seriesNumber = seriesNumber;
        }

        protected override void ReadCore(DateTime t1, DateTime t2)
        {

            var r = m_ds.Series.FindBySeriesNumber(m_seriesNumber);
            var timeZone = m_ds.Graph[0].TimeZone;

            /// <param name="r_table">one of: r_instant, r_hour, r_day, r_month, r_year, r_wy, r_base</param>
            // KT. TO DO.  figure out table name...
            string tableName = "r_day";
            
            var tbl = Hdb.Instance.Table(r.hdb_site_datatype_id, tableName,
                r.Interval,60,t1, t2, timeZone, false, 0);
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

        }

        private Reclamation.TimeSeries.TimeInterval StringToInterval(string interval)
        {
            var rval = Reclamation.TimeSeries.TimeInterval.Daily;

            if (interval == "daily")
                rval = Reclamation.TimeSeries.TimeInterval.Daily;


            return rval;
        }

    }
}
