using System;
using System.Collections.Generic;
using System.Text;

namespace HdbPoet
{
    /// <summary>
    /// Provides series index, and date data was changed
    /// </summary>
    public class PointChangeEventArgs:EventArgs
    {
        int m_seriesIndex;

        public int SeriesIndex
        {
            get { return m_seriesIndex; }
        }

        DateTime m_dateTime;

        public DateTime DateTime
        {
            get { return m_dateTime; }
        }

        
        public PointChangeEventArgs(int seriesIndex,
             DateTime t)
        {
            m_seriesIndex = seriesIndex;
            m_dateTime = t;
        }
    }
}
