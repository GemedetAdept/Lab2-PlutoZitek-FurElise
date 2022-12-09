public class MusicalNote {

	private double _duration;
	private string _noteLetter = "";
	private int _octave;
	private double _frequency;
	private bool _isRest;
	const int RootFrequency = 55;
	const int OctaveMin = 1;
	const int OctaveMax = 8;

	public static double wholeNoteDuration;
	public static void SetTimingProperties(double sigTop, double sigBottom, double tempo) {

		double secondsPerBeat = (1/tempo)*60;
		wholeNoteDuration = secondsPerBeat*8;
	}

	public MusicalNote(string duration, string note, int octave) {

		Duration = NoteDuration[duration]*wholeNoteDuration;
		Console.WriteLine(Duration);
		NoteLetter = note;
		Octave = octave;

		int halfSteps = NoteValues[NoteLetter]+4;
		double twelthRootofTwo = Math.Pow(2, (1.0/12.0));
		Frequency = (RootFrequency*(Math.Pow(twelthRootofTwo, halfSteps)))*(Octave*2);
	
		if (NoteLetter == "%%") {IsRest = true;}
		else {IsRest = false;}
	}

	public bool IsRest {

		get {return _isRest;}
		set {_isRest = value;}
	}
	public double Duration {

		get {return _duration;}
		set {_duration = value;}
	}
	public string NoteLetter {

		get {return _noteLetter;}
		set {
			string[] notes = NoteValues.Keys.ToArray();
			if (notes.Contains(value)) {_noteLetter = value;}
			else {Console.WriteLine($"{value} is an invalid note.");}
		}
	}
	public int Octave {

		get {return _octave;}
		set {
			if (value >= OctaveMin && value <= OctaveMax) {_octave = value;}
			else {Console.WriteLine($"Octave {value} is out of the range [{OctaveMin} - {OctaveMax}].");}
		}
	}
	public double Frequency {

		get {return _frequency;}
		set {_frequency = value;}
	}

	protected Dictionary<string, double> NoteDuration = 
	new Dictionary<string, double> {
		{"Whl", (1.0/1.0)},  // Whole
		{"Hlf", (1.0/2.0)},  // Half
		{"Qrt", (1.0/4.0)},  // Quarter
		{"Egt", (1.0/8.0)},  // Eighth
		{"Sxt", (1.0/16.0)}, // Sixteenth
		{"Ths", (1.0/32.0)}, // Thirty-Secondth
	};

	protected Dictionary<string, int> NoteValues = 
	new Dictionary<string, int> {
		{"%%",0}, // Rest
		{"B#",-9},
		{"C~",-9},
			{"C#",-8},
			{"Db",-8},
		{"D~",-7},
			{"D#",-6},
			{"Eb",-6},
		{"E~",-5},
		{"E#",-4},
		{"F~",-4},
			{"F#",-3},
			{"Gb",-3},
		{"G~",-2},
			{"G#",-1},
			{"Ab",-1},
		{"A~",0},
			{"A#",1},
			{"Bb",1},
		{"B~",2},
	};

}