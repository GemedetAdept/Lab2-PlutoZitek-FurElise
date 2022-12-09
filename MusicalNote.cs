public class MusicalNote {

	private int _duration;
	private string _noteLetter;
	private int _octave;
	private double _frequency;
	const int RootFrequency = 55;
	const int OctaveMin = 1;
	const int OctaveMax = 8;
	protected Dictionary<string, int> NoteValues = new Dictionary<string, int> {
		{"C~",-9},
		{"C#",-8},
		{"Db",-8},
		{"D~",-7},
		{"D#",-6},
		{"Eb",-6},
		{"E~",-5},
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

	public MusicalNote(int duration, string note, int octave) {

		Duration = duration;
		NoteLetter = note;
		Octave = octave;

		int halfSteps = NoteValues[NoteLetter];
		double twelthRootofTwo = Math.Pow(2, (1.0/12.0));
		Frequency = (RootFrequency*(Math.Pow(twelthRootofTwo, halfSteps)))*(Octave*2);
	}

	public int Duration {

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


// 	protected enum Durations {

// 		Whole
// 		Half
// 		Quarter
// 		Eigth
// 		Sixteenth
// 		ThirtySecondth
// 	}

}