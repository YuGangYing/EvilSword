using CSV;
[System.Serializable]
public class ConventionCSVStructure:BaseCSVStructure
{

	[CsvColumn (CanBeNull = true)]
	public string title{ get; set; }

	[CsvColumn (CanBeNull = true)]
	public string description{ get; set; }
}
