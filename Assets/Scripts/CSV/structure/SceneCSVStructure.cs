using CSV;

public class SceneCSVStructure : BaseCSVStructure {

	[CsvColumn (CanBeNull = true)]
	public string name{ get; set; }

	[CsvColumn (CanBeNull = true)]
	public string assetbundle_name{ get; set; }

}
