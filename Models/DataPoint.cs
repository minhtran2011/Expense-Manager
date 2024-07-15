using System.Runtime.Serialization;

namespace DoAn1.Models
{
	[DataContract]
	public class DataPoint
	{
		public DataPoint(string label, int y)
		{
			this.Label = label;
			this.Y = y;
		}
		[DataMember(Name = "label")]
		public string Label = "";
		[DataMember(Name = "y")]
		public int Y;
	}
}
