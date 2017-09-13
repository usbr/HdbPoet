using System;
using System.Windows.Forms;
namespace HdbPoet 
{
    interface IGraphControl
    {
        event EventHandler<EventArgs> DatesClick;
        bool DragPoints { get; set; }
        event EventHandler<EventArgs> EditSeriesClick;
        event EventHandler<global::HdbPoet.PointChangeEventArgs> PointChanged;
       void ChangeSeriesValue(TimeSeriesChangeEventArgs e);
       void DrawGraph(GraphData graphDef);
       void Cleanup();
        DockStyle Dock { get; set; }
        Control Parent { get; set; }
    }
}
