public class Tone {

	/* Using A~ as the root, store half-step offset values within the same octave to use during frequency calculation
	Natural (~), Sharp (#), Flat (b)
	*/
	private string _noteLetter = "";
	private int _frequency = 0;
	private static int _rootFrequency = 440;
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

	public Tone() {


	}

	public int Frequency {

		get {return _frequency;}

	}

	public string NoteLetter {

		get {return _noteLetter;}
		set {
			string[] notes = NoteValues.Keys.ToArray();
			if (notes.Contains(value)) {_noteLetter = value;}
			else {Console.WriteLine($"{value} is an invalid note.");}
		}
	}
}
