public class Tone {

	/* Using A~ as the root, store half-step offset values within the same octave to use during frequency calculation
	Natural (~), Sharp (#), Flat (b)
	*/
	private string _noteLetter = "";
	private double _frequency = 0;
	const int RootFrequency = 55; // Using the frequency of A1 to allow easy multiplication by octave
	private int _octave = 0;
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

	public Tone(string note, int octave) {

		
	}

	public string NoteLetter {

		get {return _noteLetter;}
		set {
			string[] notes = NoteValues.Keys.ToArray();
			if (notes.Contains(value)) {_noteLetter = value;}
			else {Console.WriteLine($"{value} is an invalid note.");}
		}
	}

	public double Frequency {

		get {return _frequency;}
		set {_frequency = value;}

	}

	public int Octave {

		get {return _octave;}
		set {

			if (value >= OctaveMin && value <= OctaveMax) {_octave = value;}
			else {Console.WriteLine($"Octave {value} is out of the range [{OctaveMin} - {OctaveMax}].");}
		}
	}
}
