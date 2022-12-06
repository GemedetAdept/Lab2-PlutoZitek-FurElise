public class Tone {

	private string _noteLetter;
	private int _frequency;
	private string[] _validNotes = new string[] {

		"Rest",
		"C",
			"CSharp",
			"DFlat",
		"D",
			"DSharp",
			"EFlat",
		"E",
		"F",
			"FSharp",
			"GFlat",
		"G",
			"GSharp",
			"AFlat",
		"A",
			"ASharp",
			"BFlat",
		"B",
	};

	public Tone() {


	}

	public string NoteLetter {

		get {return _noteLetter;}

	}

	public int Frequency {


	}
}
