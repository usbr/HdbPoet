using System;
using ZedGraph;
using System.Data;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
namespace HdbPoet
{
	/// <summary>
	/// Graphs Class contains static methods
	/// that return ready to use Graph components, using TimeSeriesDataSet as input.
    /// This version uses ZedGraph
	/// </summary>
	public class Graphs
	{

    /// <summary>
    /// This overloaded StandardTChart is used with WindowsForms.
    /// </summary>
    /// <returns></returns>
		public static void StandardTChart(GraphData ds, ZedGraphControl tChart1)
		{
			if( tChart1 == null)
			{
				return;
			}
			 
            ZedGraphDataLoader loader = new ZedGraphDataLoader(tChart1);
            loader.DrawTimeSeries(ds, ds.GraphRow.Title, "", true, false);
		}


	}
}
