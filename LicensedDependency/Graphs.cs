using System;
using Steema.TeeChart;
using System.Data;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
namespace HdbPoet
{
	/// <summary>
	/// Graphs Class contains static methods
	/// that return ready to use Graph components, using TimeSeriesDataSet as input.
	/// </summary>
	public class Graphs
	{

    /// <summary>
    /// This overloaded StandardTChart is used with WindowsForms.
    /// </summary>
    /// <returns></returns>
		public static void StandardTChart(GraphData ds, TChart tChart1, bool preserveFormat)
		{
			if( tChart1 == null)
			{
				tChart1 = new TChart();
			}
			if (ds == null || ds.SeriesRows.Count() == 0)
			{
				tChart1.Series.Clear();
				tChart1.Text = "";
				//return tChart1;
			}

			try
			{
                int sz = ds.SeriesRows.Count();

                if (!preserveFormat)
                {
                    tChart1.Aspect.View3D = false;
                    tChart1.Aspect.Orthogonal = false;
                    tChart1.Series.Clear();
                    if (sz == 1)  // single graph series.
                    {
                        tChart1.Legend.Visible = false;
                    }
                    else
                    {
                        tChart1.Legend.Visible = true;
                        tChart1.Legend.Alignment = LegendAlignments.Top;
                    }
                    
                }				
				tChart1.Text = ds.GraphRow.Title;

				tChart1.Axes.Right.Title.Text="";
				tChart1.Axes.Left.Title.Text="";

				string unitsOfFirstSeries = ds.SeriesRows.First().Units.Trim();
                //for(int i=0; i<sz; i++)
                int leftCount = 0;
                int rightCount = 0;
                int i = 0;
                foreach (var s in ds.SeriesRows)
                {
					string tblName = s.TableName;
					DataTable dataTable = ds.GetTable(tblName);
					string columnName = dataTable.Columns[1].ColumnName;
					double avg = AverageOfColumn(dataTable,columnName);

                    Steema.TeeChart.Styles.Line series= null;
                    if (preserveFormat && i < tChart1.Series.Count && tChart1[i] is Steema.TeeChart.Styles.Line)
                    {
                        series = tChart1[i] as Steema.TeeChart.Styles.Line;
                    }
                    else
                    {
                       series = CreateSeries(tChart1, dataTable, columnName, avg);
                    }

                    LoadSeriesData(dataTable, columnName, avg, series);

					series.Title = s.Title;

                    if ( i > 0) // check units to determine what yaxis to use(left,or right)
					{
						string units = s.Units.Trim();
						if( units != unitsOfFirstSeries && rightCount < leftCount)
						{ // right axis
							series.VertAxis =Steema.TeeChart.Styles.VerticalAxis.Right;
							if( tChart1.Axes.Right.Title.Text.IndexOf(s.Units) < 0)
							{
                                tChart1.Axes.Right.Title.Text += s.Units + ", ";
							}
                            rightCount++;
						}
						else
						{ // left axis
							if( tChart1.Axes.Left.Title.Text.IndexOf(s.Units) < 0)
							{
								tChart1.Axes.Left.Title.Text += s.Units + ", ";
                            }
                            leftCount++;
						}
					}
					else
					{
						tChart1.Axes.Left.Title.Text = s.Units +", ";
					}
                    series.Dark3D = true;
					tChart1.Series.Add(series);
                    i++;
				}
                tChart1.Axes.Left.Title.Text = tChart1.Axes.Left.Title.Text.Remove(tChart1.Axes.Left.Title.Text.Length - 2);
                if (tChart1.Axes.Right.Title.Text.Length - 2 > 0)
                {
                    tChart1.Axes.Right.Title.Text = tChart1.Axes.Right.Title.Text.Remove(tChart1.Axes.Right.Title.Text.Length - 2);
                }
                //tChart1.Zoom.Undo();
                //tChart1.Zoom.ZoomPercent(97);
            }
			catch(Exception exc)
			{
				System.Windows.Forms.MessageBox.Show(exc.Message+"\n "+exc.StackTrace);
			}
		}

        /// <summary>
        /// Used with Windows Forms
        /// </summary>
        /// <param name="tChart1"></param>
        /// <param name="table"></param>
        /// <param name="columnName"></param>
        /// <param name="avg"></param>
        /// <returns></returns>
        static Steema.TeeChart.Styles.Line CreateSeries(TChart tChart1, DataTable table, string columnName, double avg)
        {
            Steema.TeeChart.Styles.Line series1 = new Steema.TeeChart.Styles.Line();
            series1.XValues.DateTime = true;
            series1.Legend.Visible = true;
            series1.Pointer.Visible = true;
            series1.Pointer.HorizSize = 2;
            series1.Pointer.VertSize = 2;
            series1.Dark3D = true;
            Color[] colors =
            {
                Color.Black, Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Violet, Color.Goldenrod, Color.Salmon,
                Color.DarkRed, Color.DarkBlue, Color.DarkGreen, Color.DarkOrange, Color.DarkViolet, Color.DarkGoldenrod, Color.DarkSalmon,
                Color.LightPink, Color.LightBlue, Color.LightGreen, Color.Plum, Color.PaleVioletRed, Color.LightGoldenrodYellow, Color.LightSalmon
            };

            if (tChart1.Series.Count < colors.Length)
            {
                series1.Color = colors[tChart1.Series.Count];
            }


            return series1;

        }

        private static void LoadSeriesData(DataTable table, string columnName, double avg, Steema.TeeChart.Styles.Line series1)
        {
            series1.Title = columnName;
            int sz = table.Rows.Count;
            for (int i = 0; i < sz; i++)
            {
                DateTime date = (DateTime)table.Rows[i][0];
                if (table.Rows[i][columnName] != System.DBNull.Value)
                {
                    double val = Convert.ToDouble(table.Rows[i][columnName]);
                    series1.Add(date, val);
                }
                else
                {
                    series1.Add(date, avg, Color.Transparent);
                }
            }
        }
    /// <summary>
    /// Used with WebChart
    /// </summary>
    /// <param name="tChart1"></param>
    /// <param name="table"></param>
    /// <param name="columnName"></param>
    /// <param name="avg"></param>
    /// <returns></returns>
    static Steema.TeeChart.Styles.Line TChartSeries(Chart tChart1, DataTable table , string columnName, double avg)
    {
      Steema.TeeChart.Styles.Line series1 = new Steema.TeeChart.Styles.Line();
      series1.XValues.DateTime = true;
      series1.Legend.Visible = true;
      series1.Pointer.Visible = true;
      series1.Pointer.HorizSize = 2;
      series1.Pointer.VertSize = 2;
      Color[] colors = 
            {
                Color.Black, Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Violet, Color.Goldenrod, Color.Salmon,
                Color.DarkRed, Color.DarkBlue, Color.DarkGreen, Color.DarkOrange, Color.DarkViolet, Color.DarkGoldenrod, Color.DarkSalmon,
                Color.LightPink, Color.LightBlue, Color.LightGreen, Color.Plum, Color.PaleVioletRed, Color.LightGoldenrodYellow, Color.LightSalmon
            };

      if( tChart1.Series.Count <colors.Length)
      {
//        series1.Color = colors[tChart1.Series.Count];
      }
      series1.Title = columnName;

      int sz = table.Rows.Count;
      for(int i=0; i<sz; i++)
      {
        DateTime date = (DateTime)table.Rows[i][0];
        if( table.Rows[i][columnName] != System.DBNull.Value)
        {
          double val = (double)table.Rows[i][columnName];
          series1.Add((double)date.ToOADate(),val);
        }
        else
        {
          series1.Add((double)date.ToOADate(),avg,Color.Transparent);
        }
      }
      return series1;

    }

    /// <summary>
    /// This overloaded StandardTChart is used with WebChart.
    /// </summary>
    /// <param name="ds"></param>
    /// <param name="tChart1"></param>
    /// <returns></returns>
     static Chart StandardTChart(OracleHdb.TimeSeriesDataSet ds, Chart tChart1)
    {
      if( tChart1 == null)
      {
        tChart1 = new Chart();
      }
      if (ds == null || ds.Series.Rows.Count == 0)
        return tChart1;

      try
      {
        tChart1.Aspect.View3D = false;
        tChart1.Aspect.Orthogonal = false;
        tChart1.Series.Clear();

        int sz = ds.Series.Count;
        if( sz == 1)  // single graph series.
        {
          tChart1.Legend.Visible = false;
          //tChart1.Text = ds.Series[0].Title;
        }
        else
        {
          tChart1.Legend.Visible = true;
          tChart1.Legend.Alignment = LegendAlignments.Top;
          //tChart1.Text = "";
        }
				
        //tChart1.Text = ds.Graph[0].Title;

        string unitsOfFirstSeries = ds.Series[0].Units.Trim();
        for(int i=0; i<sz; i++)
        {
          string tblName = ds.Series[i].TableName;
          DataTable dataTable = ds.Tables[tblName];
          string columnName = dataTable.Columns[1].ColumnName;
          double avg = AverageOfColumn(dataTable,columnName);
          Steema.TeeChart.Styles.Line series = TChartSeries(tChart1,dataTable,columnName,avg);
          series.Title = ds.Series[i].Title;
          if( i>0) // check units to determine what yaxis to use(left,or right)
          {
            string units = ds.Series[i].Units.Trim();
            if( units != unitsOfFirstSeries)
            {
              series.VertAxis =Steema.TeeChart.Styles.VerticalAxis.Right;
              tChart1.Axes.Right.Labels.Color = Color.Red;//series.Color;
            }
          }
          tChart1.Series.Add(series);
        }

        //tChart1.Zoom.Undo();
        //tChart1.Zoom.ZoomPercent(97);
      }
      catch(Exception exc)
      {
        //System.Windows.Forms.MessageBox.Show("Error\n","\n"+exc.ToString());
        throw exc;
      }
      return tChart1;
    }
     static double AverageOfColumn(DataTable table, string columnName)
    {
        int sz = table.Rows.Count;
        int counter = 0;
        double rval = 0;
        for (int i = 0; i < sz; i++)
        {
            if (table.Rows[i][columnName] != System.DBNull.Value)
            {
                double x = Convert.ToDouble(table.Rows[i][columnName]);
                rval += x;
                counter++;
            }
        }
        if (counter > 0)
            return rval / counter;
        else return 0;
    }

	}
}
