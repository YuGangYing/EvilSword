using CSV;
using UnityEngine;

namespace RogerAssetBundle
{
	public class CSVParser : MonoBehaviour
	{
		protected CsvContext csvContext;

		private void Awake ()
		{
			csvContext = new CsvContext ();
		}

		virtual public void Parse ()
		{
		}
	}
}
