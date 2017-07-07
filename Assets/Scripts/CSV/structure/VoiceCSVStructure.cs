using CSV;

public class VoiceCSVStructure : BaseCSVStructure
{
	[CsvColumn (CanBeNull = true)]
	public string name{ get; set; }

	[CsvColumn (CanBeNull = true)]
	public string line{ get; set; }

	[CsvColumn (CanBeNull = true)]
	public int category{ get; set; }

	[CsvColumn (CanBeNull = true)]
	public int play_spot{ get; set; }

	[CsvColumn (CanBeNull = true)]
	public int m_voice_play_condition_id{ get; set; }

	[CsvColumn (CanBeNull = true)]
	public int rarity{ get; set; }

	[CsvColumn (CanBeNull = true)]
	public int situation{ get; set; }

	[CsvColumn (CanBeNull = true)]
	public int m_character_id{ get; set; }

	[CsvColumn (CanBeNull = true)]
	public int m_character_motion_id{ get; set; }

	[CsvColumn (CanBeNull = true)]
	public int is_collection{ get; set; }

	[CsvColumn (CanBeNull = true)]
	public int is_vc{ get; set; }

	[CsvColumn (CanBeNull = true)]
	public int collection_number{ get; set; }

	[CsvColumn (CanBeNull = true)]
	public string file_name{ get; set; }

	[CsvColumn (CanBeNull = true)]
	public string assetbundle_name{ get; set; }

}
