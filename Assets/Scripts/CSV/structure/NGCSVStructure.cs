using CSV;

public class NGCSVStructure : BaseCSVStructure
{
	[CsvColumn (CanBeNull = true)]
	public string name{ get; set; }
}
