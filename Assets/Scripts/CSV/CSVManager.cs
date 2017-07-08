using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BattleFramework.Data;
using CSV;
using System.IO;

public class CSVManager : SingleMonoBehaviour<CSVManager>
{
	//	public const string CSV_PATH = @"Assets/CSV";
//	private const string CSV_CONVENTION = "m_convention";
	//	public const string CSV_HELP = "m_help";
//	private const string CSV_NG = "m_ng";
//	private const string CSV_QA = "m_qa";
	const string CSV_SCENE = "m_scene";
//	private const string CSV_VOICE = "m_voice";
//	private const string CSV_CHARACTER = "m_character";

	private CsvContext mCsvContext;

	public List<SceneCSVStructure> sceneList;
	public Dictionary<int, SceneCSVStructure> sceneDic ;

//	public List<GeneralCSVStructure> ConventionList { get; private set;}
//	public Dictionary<int, GeneralCSVStructure> ConventionDic { get; private set;}

//	public List<GeneralCSVStructure> NgList { get; private set;}
//	public Dictionary<int, GeneralCSVStructure> NgDic { get; private set;}
//
//	public List<GeneralCSVStructure> QaList { get; private set;}
//	public Dictionary<int, GeneralCSVStructure> QaDic { get; private set;}
//
//	public List<VoiceCSVStructure> voiceList { get; private set;}
//	public Dictionary<int, VoiceCSVStructure> VoiceDic { get; private set;}
//	public  Dictionary<int, List<VoiceCSVStructure>> VoiceDicByCharacterId { get; private set;}
//
//	public List<MCharacterCSVStructure> CharacterList { get; private set;}
//	public Dictionary<int, MCharacterCSVStructure> CharacterDic { get; private set;}


	void Awake ()
	{
		LoadCSV ();
	}

	byte[] GetCSV (string fileName)
	{
		#if UNITY_EDITOR
		return Resources.Load<TextAsset> ("CSV/" + fileName).bytes;
		#else
//		return ResourcesManager.Ins.GetCSV (fileName);
		return null;
		#endif
	}

	void LoadCSV ()
	{
		mCsvContext = new CsvContext ();
//		LoadNG ();
//		LoadQA ();
//		LoadConvention ();
//		LoadVoice ();
//		LoadCharacter ();
	}

	void LoadScene(){
		sceneList = CreateCSVList<SceneCSVStructure> (CSV_SCENE);
		sceneDic = GetDictionary (sceneList);
	}

//	void LoadNG ()
//	{
//		NgList = CreateCSVList<GeneralCSVStructure> (CSV_NG);
//		NgDic = GetDictionary (NgList);
//	}
//
//	void LoadConvention ()
//	{
//		ConventionList = CreateCSVList<GeneralCSVStructure> (CSV_CONVENTION);
//		ConventionDic = GetDictionary (ConventionList);
//	}
//
//	void LoadQA ()
//	{
//		QaList = CreateCSVList<GeneralCSVStructure> (CSV_QA);
//		QaDic = GetDictionary (QaList);
//	}

//	void LoadVoice ()
//	{
//		voiceList = CreateCSVList<VoiceCSVStructure> (CSV_VOICE);
//		VoiceDic = GetDictionary (voiceList);
//		VoiceDicByCharacterId = new Dictionary<int, List<VoiceCSVStructure>> ();
//		for(int i=0;i<voiceList.Count;i++){
//			if (!VoiceDicByCharacterId.ContainsKey (voiceList [i].m_character_id))
//				VoiceDicByCharacterId.Add (voiceList [i].m_character_id,new List<VoiceCSVStructure>());
//			VoiceDicByCharacterId [voiceList [i].m_character_id].Add (voiceList [i]);
//		}
//		Debug.Log (VoiceDicByCharacterId.Count);
//	}
//
//	void LoadCharacter()
//	{
//		CharacterList = CreateCSVList<MCharacterCSVStructure> (CSV_CHARACTER);
//		CharacterDic = GetDictionary (CharacterList);
//	}
		
	public List<T> CreateCSVList<T> (string csvname)
	where T:BaseCSVStructure, new()
	{
		var stream = new MemoryStream (GetCSV (csvname));
		var reader = new StreamReader (stream);
		IEnumerable<T> list = mCsvContext.Read<T> (reader);
		return new List<T> (list);
	}

	Dictionary<int,T> GetDictionary<T> (List<T> list) where T : BaseCSVStructure
	{
		Dictionary<int,T> dic = new Dictionary<int, T> ();
		foreach (T t in list) {
			if (!dic.ContainsKey (t.id))
				dic.Add (t.id, t);
			else
				Debug.Log (string.Format("Multi key:{0}{1}",typeof(T).ToString (),t.id).YellowColor());
		}
		return dic;
	}


}
