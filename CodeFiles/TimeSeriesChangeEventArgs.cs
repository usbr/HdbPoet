using System;
using System.Collections.Generic;
using System.Text;

namespace HdbPoet
{
    /// <summary>
    /// Provides series index, and row where data was changed
    /// withing a TimeSeriesDataSet
    /// </summary>
    public class TimeSeriesChangeEventArgs:EventArgs
    {
        int m_seriesIndex;

        public int SeriesIndex
        {
            get { return m_seriesIndex; }
        }
        int m_rowIndex;

        //DateTime m_dateTime;

        //public DateTime DateTime
        //{
        //    get { return m_dateTime; }
        //}

        public int RowIndex
        {
            get { return m_rowIndex; }
        }

        double? m_value;

        public double? Value
        {
            get { return m_value; }
        }

        public TimeSeriesChangeEventArgs(int seriesIndex,
            int rowIndex , double? newValue)
        {
            m_seriesIndex = seriesIndex;
            m_rowIndex = rowIndex;
            m_value = newValue;
            //m_dateTime = t;
        }
    }
}
